// Created by: William Dye - 2023
// License Type: Proprietary

using System;
using UnityEngine;
using Mosuva.Messaging.Core;
using Mosuva.Pluvi.Services.Core;
using Mosuva.Pluvi.Services.Season;

namespace Mosuva.Pluvi.Services.Timing
{
    public class TimeService : MonoBehaviour, IService, 
                                ISubscribe<OnSeasonChange>, 
                                IDispatch<OnMinute>, IDispatch<OnHour>, IDispatch<OnDay>, IDispatch<OnSunrise>, IDispatch<OnSunset>
    {
        private TimeData time;
        public TimeData Time { get { return time; } }
        
        private AnimationCurve timeCurve;
        public AnimationCurve TimeCurve { get { return timeCurve; } }

        private float secondsInFullDay = TimeDefines.Time.SECONDS_IN_DAY;
        public float SecondsInFullDay { get { return secondsInFullDay; } }

        private bool isDebug = true;
        public bool IsDebug { get { return isDebug; } set { isDebug = value; } }

        private string timeText = "00 | 00:00:00";
        public string TimeText { get { return timeText; } }

        private bool dayTimeTriggered = false;
        private bool nightTimeTriggered = false;

        private MessagingService messagingService;
        private OnMinute onMinute = new OnMinute();
        private OnHour onHour = new OnHour();
        private OnDay onDay = new OnDay();
        private OnSunrise onSunrise = new OnSunrise();
        private OnSunset onSunset = new OnSunset();

        public void HandleTransmitMessage(OnMinute message)
        {
            messagingService.Dispatch(message);
        }

        public void HandleTransmitMessage(OnHour message)
        {
            messagingService.Dispatch(message);
        }

        public void HandleTransmitMessage(OnDay message)
        {
            messagingService.Dispatch(message);
        }

        public void HandleTransmitMessage(OnSunrise message)
        {
            messagingService.Dispatch(message);
        }

        public void HandleTransmitMessage(OnSunset message)
        {
            messagingService.Dispatch(message);
        }

        public void Initialise()
        {
            time = new TimeData();
            onMinute.Time = onHour.Time = onDay.Time = time;
            messagingService = ServiceLocator.Instance.Get<MessagingService>();
            messagingService.Subscribe<OnSeasonChange>(this);
        }

        public void Execute(OnSeasonChange message)
        {
            var currentSeasonAsset = message.CurrentSeasonAsset;
            UpdateTimeCurve(currentSeasonAsset.TimeCurve);

            timeCurve = currentSeasonAsset.TimeCurve;
            time.DayTimeValue = TimeCurve[1].time;
            time.NightTimeValue = TimeCurve[3].time;
        }
        
        private void UpdateTimeCurve(AnimationCurve newTimeCurve)
        {
            timeCurve = newTimeCurve;
        }

        public void Update()
        {
            time.TimeSpan = TimeSpan.FromSeconds(CurrentTime());

#if UNITY_EDITOR
            if (isDebug)
            {
                timeText = string.Format(" {0:D2} | {1:D2}:{2:D2}:{3:D2}", 
                time.TimeSpan.Days, time.TimeSpan.Hours, time.TimeSpan.Minutes, time.TimeSpan.Seconds);
            }
#endif

            // Sets the timer to 0 - 1 e.g. total days - days = 8.9 - 8
            time.CurrentTimeOfDay = (float)(time.TimeSpan.TotalDays - time.TimeSpan.Days);

            CheckMinute();

            CheckHour();

            CheckDay();

            if (!dayTimeTriggered && time.CurrentTimeOfDay > time.DayTimeValue)
            {
                HandleTransmitMessage(onSunrise);
                dayTimeTriggered = true;
            }

            if (!nightTimeTriggered && time.CurrentTimeOfDay > time.NightTimeValue)
            {
                HandleTransmitMessage(onSunset);
                nightTimeTriggered = true;
            }
        }

        private void CheckMinute()
        {
            if (time.TimeSpan.Minutes != time.CurrentMinute)
            {
                if (timeCurve != null)
                {
                    onMinute.Time.EvaluatedTime = time.EvaluatedValue = timeCurve.Evaluate(time.CurrentTimeOfDay);
                }

                onMinute.Time = time;
                HandleTransmitMessage(onMinute);

                time.CurrentMinute = time.TimeSpan.Minutes;
            }
        }

        private void CheckHour()
        {
            if (time.TimeSpan.Hours != time.CurrentHour)
            {
                onHour.Time = time;
                HandleTransmitMessage(onHour);

                time.CurrentHour = time.TimeSpan.Hours;
            }
        }

        private void CheckDay()
        {
            if (time.TimeSpan.Days != time.CurrentDay)
            {
                onDay.Time = time;
                HandleTransmitMessage(onDay);

                time.CurrentDay = time.TimeSpan.Days;

                dayTimeTriggered = false;
                nightTimeTriggered = false;
            }
        }

        private float CurrentTime()
        {
            // 1440 minutes in a day & 86400 seconds in a day
            float minutesPerSecond = 1440f / secondsInFullDay;
            time.CurrentTime = ((UnityEngine.Time.time * (minutesPerSecond / 1440f)) * 86400f / 2) + time.TimeOffset;

            return time.CurrentTime;
        }

        private void OnDisable()
        {
            messagingService.Unsubscribe<OnSeasonChange>(this);
        }
    }
}