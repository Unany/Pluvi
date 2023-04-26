// Created by: William Dye - 2023
// License Type: Proprietary

using Mosuva.Pluvi.Services.Core;

namespace Mosuva.Pluvi.Services.Skybox
{
    public interface ISkyboxService : IService
    {
        void ReadMaterialProperties();
        void WriteMaterialProperties();
    }
}