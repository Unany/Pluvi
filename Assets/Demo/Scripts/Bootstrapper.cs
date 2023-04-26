// Created by: William Dye - 2023
// License Type: Proprietary

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mosuva.Messaging.Core;
using Mosuva.Pluvi.Services.Timing;
using Mosuva.Pluvi.Services.Season;
using Mosuva.Pluvi.Services.Day;
using Mosuva.Pluvi.Services.Weather;
using Mosuva.Pluvi.Services.Celestial;
using Mosuva.Pluvi.Services.Skybox;

namespace Mosuva.Pluvi.Services.Core
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialise()
        {
            ServiceLocator serviceLocator = ServiceLocator.Instance;

            serviceLocator.RegisterComponent<MessagingService>(new MessagingService());
            serviceLocator.RegisterComponent<TimeService>(new TimeService());
            serviceLocator.RegisterComponent<SeasonService>(new SeasonService());
            serviceLocator.RegisterComponent<DayService>(new DayService());
            serviceLocator.RegisterComponent<WeatherService>(new WeatherService());
            serviceLocator.RegisterComponent<CelestialService>(new CelestialService());
            serviceLocator.RegisterComponent<SkyboxService>(new SkyboxService());
        }
    }
}
