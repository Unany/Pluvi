// Created by: William Dye - 2023
// License Type: Proprietary

using System.Collections.Generic;
using UnityEngine;
using Mosuva.Messaging.Core;
using Mosuva.Pluvi.Services.Timing;
using Mosuva.Pluvi.Services.Core;
using Mosuva.Pluvi.Utils;

namespace Mosuva.Pluvi.Services.Season
{
    public class SeasonService : MonoBehaviour, ISeasonService, 
                                    ISubscribe<OnMinute>, ISubscribe<OnHour>, ISubscribe<OnDay>, 
                                    IDispatch<OnRequestDayAsset>, IDispatch<OnRequestNightAsset>, IDispatch<OnSeasonChange>
    {
        public SeasonScriptableObject currentSeasonAsset;
        public SeasonScriptableObject previousSeasonAsset;
        public List<SeasonScriptableObject> seasons = new List<SeasonScriptableObject>();

        private bool randomSeason = false;
        public bool RandomSeason { get { return randomSeason; } set { randomSeason = value; } }

        private float seasonLerpingProgress = 0;
        public float SeasonLerpingProgress => seasonLerpingProgress;

        private float lerpToValue = 0.3f;
        private int seasonCount;
        private int currentCycleCount;
        private int nextSeasonCycleCount;
        private bool newSeasonTriggered = false;
        private bool isInitialised = false;

        private MessagingService messagingService;
        private OnRequestDayAsset onRequestDayAsset = new OnRequestDayAsset();
        private OnRequestNightAsset onRequestNightAsset = new OnRequestNightAsset();
        private OnSeasonChange onSeasonChange = new OnSeasonChange();
        private TimeData time;

        public void HandleTransmitMessage(OnRequestDayAsset message)
        {
            messagingService.Dispatch(onRequestDayAsset);
        }

        public void HandleTransmitMessage(OnRequestNightAsset message)
        {
            messagingService.Dispatch(onRequestNightAsset);
        }

        public void HandleTransmitMessage(OnSeasonChange message)
        {
            messagingService.Dispatch(onSeasonChange);
        }

        public void Initialise()
        {
            messagingService = ServiceLocator.Instance.Get<MessagingService>();
            messagingService.Subscribe<OnMinute>(this);
            messagingService.Subscribe<OnHour>(this);
            messagingService.Subscribe<OnDay>(this);

            var seasonsConfig = Resources.Load<SeasonsScriptableObject>("SeasonsConfig");
            seasons = seasonsConfig.seasons;

            InitialiseSeasonAsset();
        }

        public void Execute(OnMinute message)
        {
            if (!isInitialised) return;

            time = message.Time;

            if (newSeasonTriggered)
            {
                NewSeasonTriggered();
            }
            else
            {
                RenderSettings.fogColor = currentSeasonAsset.FogGradient.Evaluate(time.EvaluatedValue);
                RenderSettings.fogDensity = currentSeasonAsset.FogDensityCurve.Evaluate(time.EvaluatedValue);
                RenderSettings.ambientIntensity = currentSeasonAsset.AmbientIntensity;
            }
        }

        public void Execute(OnHour message)
        {
            time = message.Time;

            if (time.CurrentHour == 11)
            {
                HandleTransmitMessage(onRequestNightAsset);
            }
        }

        public void Execute(OnDay message)
        {
            time = message.Time;
            IncrementDay();
        }

        public void InitialiseSeasonAsset()
        {
            seasonCount = 0;

            var randomNumber = UnityEngine.Random.Range(0, (seasons.Count + 1));

            currentSeasonAsset = randomSeason ? seasons[randomNumber] : seasons[seasonCount];

            //Set the count to move onto the next season
            currentCycleCount = 1;
            nextSeasonCycleCount = currentSeasonAsset.CycleCount;

            onSeasonChange.CurrentSeasonAsset = currentSeasonAsset;
            onSeasonChange.PreviousSeasonAsset = previousSeasonAsset;

            //Set the Time Curve to be the new seasons timecurve
            HandleTransmitMessage(onSeasonChange);

            isInitialised = true;
        }

        public void GetNewSeasonAsset()
        {
            previousSeasonAsset = currentSeasonAsset;

            var randomNumber = UnityEngine.Random.Range(0, (seasons.Count + 1));
            currentSeasonAsset = randomSeason ? seasons[randomNumber] : seasons[seasonCount];

            onSeasonChange.CurrentSeasonAsset = currentSeasonAsset;
            onSeasonChange.PreviousSeasonAsset = previousSeasonAsset;

            HandleTransmitMessage(onSeasonChange);
        }

        private void NewSeasonTriggered()
        {
            var inverseEvaluatedValue = 1 - time.EvaluatedValue;
            seasonLerpingProgress = TimeUtils.Scale(0, lerpToValue, 0, 1, inverseEvaluatedValue);

            var lerpedColour = Color.Lerp(previousSeasonAsset.FogGradient.Evaluate(1), currentSeasonAsset.FogGradient.Evaluate(lerpToValue), seasonLerpingProgress);

            RenderSettings.fogColor = lerpedColour;
            RenderSettings.fogDensity = Mathf.Lerp(previousSeasonAsset.FogDensityCurve.Evaluate(1), currentSeasonAsset.FogDensityCurve.Evaluate(1), seasonLerpingProgress);
            RenderSettings.ambientIntensity = Mathf.Lerp(previousSeasonAsset.AmbientIntensity, currentSeasonAsset.AmbientIntensity, seasonLerpingProgress);
            RenderSettings.reflectionIntensity = Mathf.Lerp(previousSeasonAsset.ReflectionIntensity, currentSeasonAsset.ReflectionIntensity, seasonLerpingProgress);

            if (seasonLerpingProgress >= 1)
            {
                newSeasonTriggered = false;
                seasonLerpingProgress = 0;
            }
        }
        
        public void IncrementDay()
        {
            currentCycleCount += 1;

            if (currentCycleCount >= nextSeasonCycleCount)
            {
                seasonCount += 1;
                newSeasonTriggered = true;

                //Reset seasons if we run out of seasons
                if (seasonCount >= seasons.Count)
                {
                    seasonCount = 0;
                }

                GetNewSeasonAsset();
            }
        }
        
        private void OnDisable()
        {
            messagingService.Unsubscribe<OnMinute>(this);
            messagingService.Unsubscribe<OnHour>(this);
            messagingService.Unsubscribe<OnDay>(this);
        }
    }
}
