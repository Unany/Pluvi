// Created by: William Dye - 2023
// License Type: Proprietary

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mosuva.Pluvi.Services.Season
{
    [Serializable]
    [CreateAssetMenu(fileName = "SeasonConfig", menuName = "Pluvi/Seasons Config", order = 1)]
    public class SeasonsScriptableObject : ScriptableObject
    {
        [SerializeField] public List<SeasonScriptableObject> seasons;
    }
}