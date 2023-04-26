// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;

namespace Mosuva.Pluvi.Services.Skybox
{
    public static class SkyboxDefines
    {
        public static class Day
        {
            public static string TEXTURE = "_dayTexture";
            public static string COLOUR = "_dayColour";
            public static string CLOUD_TEXTURE = "_cloudTextureDay";
            public static string CLOUD_COLOUR = "_cloudColourDay";
            public static string CLOUD_SPEED = "_cloudSpeedDay";
            public static string CLOUD_SCALE = "_cloudScaleDay";
            public static string CLOUD_HEIGHT = "_cloudHeightDay";
            public static string FOG_SCALE = "_fogScaleDay";
        }

        public static class Night
        {
            public static string TEXTURE = "_nightTexture";
            public static string COLOUR = "_nightColour";
            public static string CLOUD_TEXTURE = "_cloudTextureNight";
            public static string CLOUD_COLOUR = "_cloudColourNight";
            public static string CLOUD_SPEED = "_cloudSpeedNight";
            public static string CLOUD_SCALE = "_cloudScaleNight";
            public static string CLOUD_HEIGHT = "_cloudHeightNight";
            public static string FOG_SCALE = "_fogScaleNight";
            public static string STAR_COLOUR = "_starColourNight";
            public static string STAR_TIME = "_starTimeNight";
            public static string STAR_SPEED = "_starSpeedNight";
            public static string STAR_DENSITY = "_starDensityNight";
            public static string STAR_SIZE = "_starSizeNight";
        }

        public static class Time
        {
            public static string EVALUATED_TIME = "_evaluatedTime";
        }

        public static class Weather
        {
            public static string COLOUR = "_weatherColour";
            public static string CLOUD_MULTIPLIER = "_weatherCloudMultiplier";
        }
    }
}