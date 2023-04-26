// Created by: William Dye - 2023
// License Type: Proprietary

using System;

namespace Mosuva.Pluvi.Services.Timing
{
    public class TimeData
    {
        public float EvaluatedValue { get { return evaluatedValue; } set { if (value >= 0 && value <= 1) { evaluatedValue = value; } } }
        private float evaluatedValue = 0;

        public float DayTimeValue { get { return dayTimeValue; } set { if (value >= 0 && value <= 1) { dayTimeValue = value; } } }
        private float dayTimeValue = 0;

        public float NightTimeValue { get { return nightTimeValue; } set { if (value >= 0 && value <= 1) { nightTimeValue = value; } } }
        private float nightTimeValue = 0;

        public TimeSpan TimeSpan = new TimeSpan();

        public float CurrentTimeOfDay = 0;
        public float CurrentTime;
        public int CurrentMinute;
        public int CurrentHour;
        public int CurrentDay = 0;
        public float EvaluatedTime = 0;

        public float TimeOffset = 0;
    }
}