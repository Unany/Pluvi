// Created by: William Dye - 2023
// License Type: Proprietary

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mosuva.Messaging.Core;
using Mosuva.Messaging.Core;
using Mosuva.Pluvi.Services.Timing;
using Mosuva.Pluvi.Services.Core;

namespace Mosuva.Pluvi.Demo
{
    public class Lamp : MonoBehaviour, ISubscribe<OnSunrise>, ISubscribe<OnSunset>
    {
        [SerializeField]
        private GameObject pointLight;
        private MessagingService messagingService;

        public void OnEnable()
        {   
            messagingService = ServiceLocator.Instance.Get<MessagingService>();
            messagingService.Subscribe<OnSunrise>(this);
            messagingService.Subscribe<OnSunset>(this);
        }

        public void Execute(OnSunrise message)
        {
            pointLight.SetActive(false);
        }

        public void Execute(OnSunset message)
        {
            pointLight.SetActive(true);
        }

        private void OnDisable()
        {
            messagingService.Unsubscribe<OnSunrise>(this);
            messagingService.Unsubscribe<OnSunset>(this);
        }
    }
}
