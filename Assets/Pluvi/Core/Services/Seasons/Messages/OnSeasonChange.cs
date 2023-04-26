// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;

namespace Mosuva.Pluvi.Services.Season
{
    public struct OnSeasonChange
    {
        public SeasonScriptableObject CurrentSeasonAsset;
        public SeasonScriptableObject PreviousSeasonAsset;
    }
}