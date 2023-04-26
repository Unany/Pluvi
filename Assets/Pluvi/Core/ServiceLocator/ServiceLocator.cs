// Created by: William Dye - 2023
// License Type: Proprietary

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mosuva.Pluvi.Services.Core
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private readonly Dictionary<string, IService> services = new Dictionary<string, IService>();
        
        public override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Gets the service instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>The service instance.</returns>
        public T Get<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)services[key];
        }

        /// <summary>
        /// Registers the service with the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance.</param>
        public void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                throw new InvalidOperationException();
                return;
            }

            services.Add(key, service);

            service.Initialise();
        }

        /// <summary>
        /// Registers the service with the current service locator as a component to attach to a gameobject.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance.</param>
        public void RegisterComponent<T>(T service, int order = 0) where T : Component, IService
        {
            string key = typeof(T).Name;
            if (services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                throw new InvalidOperationException();
                return;
            }

            services.Add(key, service);

            var component = Instance.gameObject.AddComponent<T>();

            for (int i = 0; i < order; i++)
            {
                UnityEditorInternal.ComponentUtility.MoveComponentUp(component);
            }

            component.Initialise();
        }

        /// <summary>
        /// Unregisters the service from the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        public void Unregister<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                throw new InvalidOperationException();
                return;
            }

            services.Remove(key);
        }
    }
}