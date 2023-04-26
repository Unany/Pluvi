// Created by: William Dye - 2023
// License Type: Proprietary

using System;
using System.Collections.Generic;
using UnityEngine;
using Mosuva.Pluvi.Services.Weather;

namespace Mosuva.Pluvi.Services.Day
{
    [Serializable]
    [CreateAssetMenu(fileName = "Day", menuName = "Pluvi/Day Asset", order = 2)]
    public class DayScriptableObject : ScriptableObject
    {
        public string DayName = "Day";
        public int Weighting;

        [SerializeField]
        public List<WeatherScriptableObject> WeatherAsset = new List<WeatherScriptableObject>();

        public bool HaveUniqueSkybox;
        public Texture UniqueSkybox;
        public Color SkyColour;
        public float SkyLOD;

        public Texture CloudTexture;
        public Color CloudColour;
        public float CloudSpeed;
        public float CloudScale;
        public float CloudHeight;
        public Vector2 FogScale;

        public Color StarColour;
        public float StarSpeed;
        public float StarDensity;
        public float StarSize;
    }
}