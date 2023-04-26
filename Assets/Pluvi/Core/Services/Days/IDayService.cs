// Created by: William Dye - 2023
// License Type: Proprietary

using Mosuva.Pluvi.Services.Core;

namespace Mosuva.Pluvi.Services.Day
{
    public interface IDayService : IService
    {
        void GetNewDayAsset();
        void GetNewNightAsset();
        void AddDayAssets();
    }
}