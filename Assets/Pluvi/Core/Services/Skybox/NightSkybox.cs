// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;

namespace Mosuva.Pluvi.Services.Skybox
{
    public struct NightSkybox
    {
        public Cubemap NightTexture;
        public Color NightColour;

        public Texture CloudTextureNight;
        public Color CloudColourNight;
        public float CloudSpeedNight;
        public float CloudScaleNight;
        public float CloudHeightNight;

        public Vector2 FogScaleNight; 
        
        public Color StarColourNight;
        public float StarTimeNight;
        public float StarDensityNight;
        public float StarSizeNight;
    }
}
