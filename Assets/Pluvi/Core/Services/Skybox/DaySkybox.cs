// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;

namespace Mosuva.Pluvi.Services.Skybox
{
    public struct DaySkybox
    {
        public Cubemap DayTexture;
        public Color DayColour;
        public Color WeatherColour;
        public float DayLOD;

        public Texture CloudTextureDay;
        public Color CloudColourDay;
        public float CloudSpeedDay;
        public float CloudScaleDay;
        public float CloudHeightDay;
        public float WeatherCloudMultiplier;
        
        public Vector2 FogScaleDay;
    }
}
