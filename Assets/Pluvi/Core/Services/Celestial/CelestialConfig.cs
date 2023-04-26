// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;

namespace Mosuva.Pluvi.Services.Celestial
{
    [CreateAssetMenu(fileName = "CelestialConfig", menuName = "Pluvi/CelestialConfig", order = 1)]
    public class CelestialConfig : ScriptableObject
    {
        public Vector3 SunOffset;
        public Vector3 MoonOffset;
        public Vector3 lightOffset;
        public GameObject SunObject;
        public GameObject MoonObject;
        public GameObject DirectionalLight;
    }
}