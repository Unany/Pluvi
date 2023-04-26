// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;
using Mosuva.Messaging.Core;
using Mosuva.Pluvi.Services.Core;
using Mosuva.Pluvi.Services.Timing;

namespace Mosuva.Pluvi.Services.Celestial
{
    public class CelestialService : MonoBehaviour, ICelestialService, ISubscribe<OnMinute>
    {
        public GameObject InstantiatedSun { get { return instantiatedSun; } set { instantiatedSun = value; } }
        public GameObject InstantiatedMoon { get { return instantiatedMoon; } set { instantiatedMoon = value; } }
        public GameObject InstantiatedLight { get { return instantiatedLight; } set { instantiatedLight = value; } }
        public CelestialConfig CelestialConfig { get { return celestialConfig; } set { celestialConfig = value; } }

        private GameObject instantiatedSun;
        private GameObject instantiatedMoon;
        private GameObject instantiatedLight;
        private CelestialConfig celestialConfig;

        private MessagingService messagingService;
        private TimeData time;

        public void Initialise()
        {
            messagingService = ServiceLocator.Instance.Get<MessagingService>();
            messagingService.Subscribe<OnMinute>((ISubscribe<OnMinute>)this);

            celestialConfig = Resources.Load<CelestialConfig>("CelestialConfig");

            InstantiateCelestial();
        }

        public void Execute(OnMinute message)
        {
            time = message.Time;
            RotationCalculation(time.CurrentTimeOfDay);
        }

        /// <summary>
        /// Instantiates the sun, moon and main light, and set's the sun and moons parent to the light
        /// </summary>
        private void InstantiateCelestial()
        {
            instantiatedLight = Instantiate(celestialConfig.DirectionalLight,
            celestialConfig.lightOffset, Quaternion.identity);

            InstantiatedSun = Instantiate(celestialConfig.SunObject,
                (instantiatedLight.transform.position + celestialConfig.SunOffset), transform.rotation);

            InstantiatedMoon = Instantiate(celestialConfig.MoonObject,
                (instantiatedLight.transform.position + celestialConfig.MoonOffset), transform.rotation);

            InstantiatedSun.transform.SetParent(instantiatedLight.transform);
            InstantiatedMoon.transform.SetParent(instantiatedLight.transform);
        }

        /// <summary>
        /// Rotates the sun & moon by rotating the main light
        /// </summary>
        /// <param name="currentTimeOfDay"> A float, representing the time of day between 0 - 1</param>
        public void RotationCalculation(float currentTimeOfDay)
        {
            float currentRotation = Mathf.Lerp(0.0f, 360.0f, currentTimeOfDay);

            instantiatedLight.transform.eulerAngles = new Vector3(currentRotation, transform.rotation.y, transform.rotation.z);
        }

        private void OnDisable()
        {
            messagingService.Unsubscribe<OnMinute>((ISubscribe<OnMinute>)this);
        }
    }
}