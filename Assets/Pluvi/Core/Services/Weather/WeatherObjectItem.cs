// Created by: William Dye - 2023
// License Type: Proprietary

using System;
using UnityEngine;

namespace Mosuva.Pluvi.Services.Weather
{
    [Serializable]
    public class WeatherObjectItem
    {        
        public GameObject ObjectToPool;
        public int AmountToPool;
        public bool NextToCamera = true;
        public Vector3 OffsetAmount = new Vector3(0, 0, 0);
    }
}