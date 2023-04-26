// Created by: William Dye - 2023
// License Type: Proprietary

using System;
using System.Collections.Generic;
using UnityEngine;
using Mosuva.Pluvi.Services.Day;

namespace Mosuva.Pluvi.Services.Season
{
    [Serializable]
    [CreateAssetMenu(fileName = "Season", menuName = "Pluvi/Season Asset", order = 1)]
    public class SeasonScriptableObject : ScriptableObject
    {
        public string SeasonName = "Season";
        public int CycleCount = 1;
        public float LerpToValue = 0.5f;
        
        [SerializeField] public List<DayScriptableObject> DayAssets = new List<DayScriptableObject>();
        [SerializeField] public List<DayScriptableObject> NightAssets = new List<DayScriptableObject>();
        
        [SerializeField] public AnimationCurve TimeCurve;
        [SerializeField] public Gradient LightingGradient;

        public float AmbientIntensity = 1f;
        public float ReflectionIntensity = 1f;

        [SerializeField] public AnimationCurve FogDensityCurve;
        [SerializeField] public Gradient FogGradient;
    }
}