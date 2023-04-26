// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;

namespace Mosuva.Pluvi.Services.Weather
{
    public struct OnWeatherChange
    {
        public WeatherScriptableObject CurrentWeatherAsset;
        public WeatherScriptableObject PreviousWeatherAsset;
        public float WeatherLerpSpeed;
    }
}