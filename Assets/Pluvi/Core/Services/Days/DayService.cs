// Created by: William Dye - 2023
// License Type: Proprietary

using System.Collections.Generic;
using UnityEngine;
using Mosuva.Messaging.Core;
using Mosuva.Pluvi.Services.Core;
using Mosuva.Pluvi.Services.Season;
using Mosuva.Pluvi.Services.Timing;

namespace Mosuva.Pluvi.Services.Day
{
    public class DayService : MonoBehaviour, IDayService,
                                ISubscribe<OnDay>, ISubscribe<OnRequestDayAsset>, ISubscribe<OnRequestNightAsset>, ISubscribe<OnSeasonChange>,
                                IDispatch<OnDayAsset>, IDispatch<OnNightAsset>
    {
        public SeasonScriptableObject currentSeasonAsset;
        public DayScriptableObject currentDayAsset;
        public DayScriptableObject currentNightAsset;
        private List<DayScriptableObject> weightedDay = new List<DayScriptableObject>();
        private List<DayScriptableObject> weightedNight = new List<DayScriptableObject>();

        private bool isInitialised = false;

        private MessagingService messagingService;
        private OnDayAsset onDayAsset = new OnDayAsset();
        private OnNightAsset onNightAsset = new OnNightAsset();

        public void HandleTransmitMessage(OnDayAsset message)
        {
            messagingService.Dispatch(onDayAsset);
        }

        public void HandleTransmitMessage(OnNightAsset message)
        {
            messagingService.Dispatch(onNightAsset);
        }

        public void Initialise()
        {
            messagingService = ServiceLocator.Instance.Get<MessagingService>();
            messagingService.Subscribe<OnDay>(this);
            messagingService.Subscribe<OnSeasonChange>(this);
            messagingService.Subscribe<OnRequestDayAsset>(this);
            messagingService.Subscribe<OnRequestNightAsset>(this);
        }

        public void Execute(OnDay message)
        {
            if (!isInitialised) return;

            GetNewDayAsset();
            HandleTransmitMessage(onDayAsset);
        }

        public void Execute(OnRequestDayAsset message)
        {
            if (!isInitialised) return;

            GetNewDayAsset();
            HandleTransmitMessage(onDayAsset);
        }

        public void Execute(OnRequestNightAsset message)
        {
            if (!isInitialised) return;

            GetNewNightAsset();
            HandleTransmitMessage(onNightAsset);
        }

        public void Execute(OnSeasonChange message)
        {
            currentSeasonAsset = message.CurrentSeasonAsset;

            ClearWeightedAssets();
            AddDayAssets();

            isInitialised = true;
        }

        public void GetNewDayAsset()
        {
            if (!isInitialised) return;

            var randomWeightedDay = UnityEngine.Random.Range(0, (weightedDay.Count));
            currentDayAsset = weightedDay[randomWeightedDay];

            onDayAsset.CurrentDayAsset = currentDayAsset;
        }

        public void GetNewNightAsset()
        {
            if (!isInitialised) return;

            //Always add another 1 as the Max is exclusive
            var randomNumber = UnityEngine.Random.Range(0, (weightedNight.Count));
            currentNightAsset = weightedNight[randomNumber];
            onNightAsset.CurrentNightAsset = currentNightAsset;
        }

        /// <summary>
        /// Allows the day assets to be weighted towards their selection
        /// </summary>
        public void AddDayAssets()
        {
            foreach (var dayAsset in currentSeasonAsset.DayAssets)
            {
                for (int i = 0; i < dayAsset.Weighting; i++)
                {
                    weightedDay.Add(dayAsset);
                }
            }

            currentDayAsset = weightedDay[0];

            foreach (var nightAsset in currentSeasonAsset.NightAssets)
            {
                for (int i = 0; i < nightAsset.Weighting; i++)
                {
                    weightedNight.Add(nightAsset);
                }
            }

            currentNightAsset = weightedNight[0];
        }

        private void ClearWeightedAssets()
        {
            weightedDay = new List<DayScriptableObject>();
            weightedNight = new List<DayScriptableObject>();
        }


        private void OnDisable()
        {
            messagingService.Unsubscribe<OnDay>(this);
            messagingService.Unsubscribe<OnSeasonChange>(this);
            messagingService.Unsubscribe<OnRequestDayAsset>(this);
            messagingService.Unsubscribe<OnRequestNightAsset>(this);
        }
    }
}