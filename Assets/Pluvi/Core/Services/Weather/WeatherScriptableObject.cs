// Created by: William Dye - 2023
// License Type: Proprietary

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mosuva.Pluvi.Services.Weather
{
    [Serializable]
    [CreateAssetMenu(fileName = "Weather", menuName = "Pluvi/Weather Asset", order = 3)]
    public class WeatherScriptableObject : ScriptableObject
    {
        public string WeatherName = "Weather";
        public float WeatherDurationMin;
        public float WeatherDurationMax;
        public int WeatherLerpSpeed;
        public Color WeatherColourMultiplier;
        
        [SerializeField]
        public List<WeatherObjectItem> WeatherPool = new List<WeatherObjectItem>();

        public float CloudMultiplier;
    }
}