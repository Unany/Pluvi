// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;

namespace Mosuva.Pluvi.Utils
{
    public static class TimeUtils
    {
        /// <summary>
        /// Rescales a range between two floats into another range
        /// </summary>
        public static float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
        {
            float OldRange = (OldMax - OldMin);
            float NewRange = (NewMax - NewMin);
            float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

            return (NewValue);
        }
    }
}