// Created by: William Dye - 2023
// License Type: Proprietary

using System.Collections.Generic;
using UnityEngine;
using Mosuva.Messaging.Core;
using Mosuva.Pluvi.Services.Timing;
using Mosuva.Pluvi.Services.Day;
using Mosuva.Pluvi.Services.Core;

namespace Mosuva.Pluvi.Services.Weather
{
    public class WeatherService : MonoBehaviour, IWeatherService, 
                                    ISubscribe<OnMinute>, ISubscribe<OnDayAsset>, ISubscribe<OnNightAsset>, 
                                    IDispatch<OnWeatherChange>
    {
        private DayScriptableObject currentDayAsset;
        public WeatherScriptableObject currentWeatherAsset;
        public WeatherScriptableObject previousWeatherAsset;
        public List<GameObject> activeWeatherObjectItems = new List<GameObject>();

        private bool newWeatherTriggered = false;
        public double weatherEnd = 0;

        private float secondsInDay = TimeDefines.Time.SECONDS_IN_DAY;

        private bool isInitialised = false;

        private MessagingService messagingService;
        private OnWeatherChange onWeatherChange = new OnWeatherChange();
        private TimeData time;

        public void HandleTransmitMessage(OnWeatherChange message)
        {
            messagingService.Dispatch(onWeatherChange);
        }

        public void Initialise()
        {
            messagingService = ServiceLocator.Instance.Get<MessagingService>();
            messagingService.Subscribe<OnMinute>((ISubscribe<OnMinute>)this);
            messagingService.Subscribe<OnDayAsset>((ISubscribe<OnDayAsset>)this);
            messagingService.Subscribe<OnNightAsset>((ISubscribe<OnNightAsset>)this);

            activeWeatherObjectItems.Clear();
        }
        
        public void Execute(OnMinute message)
        {
            time = message.Time;
            
            if (!isInitialised) return;

            if (time.TimeSpan.TotalSeconds >= weatherEnd)
            {
                if (currentWeatherAsset != null)
                    OnWeatherEnd();
            }

            UpdateWeather();
        }

        public void Execute(OnDayAsset message)
        {
            currentDayAsset = message.CurrentDayAsset;

            if (!isInitialised) EnableWeather();
        }

        public void Execute(OnNightAsset message)
        {
            currentDayAsset = message.CurrentNightAsset;
                        
            if (!isInitialised) EnableWeather();
        }

        public void EnableWeather()
        {
            if (ValidateWeatherService())
            {
                isInitialised = true;

                SetCurrentWeather();
            }
            else
            {
                Debug.LogWarning("WARNING: No weather assets have been set. Disabling Weather!");
            }
        }

        private void UpdateWeather()
        {

            if (!newWeatherTriggered) return;


            if (!ValidateWeatherService())
            {
                Debug.LogWarning("WARNING: No weather assets have been set.");

                return;
            }

            SpawnWeather(currentDayAsset);

            newWeatherTriggered = false;
        }

        public WeatherScriptableObject GetCurrentWeather()
        {
            return currentWeatherAsset;
        }

        public WeatherScriptableObject GetPreviousWeather()
        {
            return previousWeatherAsset;
        }

        public void SpawnWeather(DayScriptableObject currentDayAsset)
        {
            CreateWeatherObjectItems(currentDayAsset, GetWeatherDuration());
        }

        public void SetWeatherItems(List<DayScriptableObject> currentDayAssets)
        {
            foreach (var dayAsset in currentDayAssets)
            {
                CreateWeatherObjectItems(dayAsset, GetWeatherDuration());
            }
        }

        private void CreateWeatherObjectItems(DayScriptableObject currentDayAsset, float weatherDuration)
        {
            DestroyWeatherObjects();

            if (currentWeatherAsset.WeatherPool.Count > 0)
            {
                foreach (var weatherObject in currentWeatherAsset.WeatherPool)
                {
                    if (weatherObject.ObjectToPool != null) GeneratePooledObject(weatherObject.NextToCamera, weatherObject);
                }
            }
        }

        private void DestroyWeatherObjects()
        {
            foreach (var weatherObject in activeWeatherObjectItems)
            {
                Destroy(weatherObject);
            }

            activeWeatherObjectItems.Clear();
        }

        private void GeneratePooledObject(bool isNextToCamera, WeatherObjectItem weatherObject)
        {
            var transform = isNextToCamera ? Camera.main.transform : this.transform;
            var rotation = isNextToCamera ? weatherObject.ObjectToPool.transform.rotation : transform.rotation;
            var position = isNextToCamera ? weatherObject.OffsetAmount : transform.localPosition + weatherObject.OffsetAmount;

            for (int i = 0; i < weatherObject.AmountToPool; i++)
            {
                var tempPooledObject = Instantiate(weatherObject.ObjectToPool, position, rotation);
                tempPooledObject.transform.parent = transform;
                tempPooledObject.transform.localPosition = position;

                activeWeatherObjectItems.Add(tempPooledObject);

                var particleSystem = tempPooledObject.GetComponent<ParticleSystem>();
                particleSystem.Stop();
                ParticleSystem.MainModule mainModule = particleSystem.main;
                mainModule.duration = GetWeatherDuration() * (secondsInDay / 86400);
                particleSystem.Play();
            }
        }

        private WeatherScriptableObject SetCurrentWeather()
        {
            previousWeatherAsset = currentWeatherAsset;

            var randomValueAsset = UnityEngine.Random.Range(1, (currentDayAsset.WeatherAsset.Count));
            
            currentWeatherAsset = currentDayAsset.WeatherAsset[randomValueAsset - 1];

            onWeatherChange.PreviousWeatherAsset = previousWeatherAsset;
            onWeatherChange.CurrentWeatherAsset = currentWeatherAsset;

            return currentWeatherAsset;
        }

        private float GetWeatherDuration()
        {
            if (currentWeatherAsset != null)
            {
                float weatherDuration = UnityEngine.Random.Range(currentWeatherAsset.WeatherDurationMin, currentWeatherAsset.WeatherDurationMax);
                return weatherDuration;
            }
            else
            {
                return UnityEngine.Random.Range(WeatherDefines.Time.WEATHER_DURATION_MIN, WeatherDefines.Time.WEATHER_DURATION_MAX);
            }
        }

        private bool ValidateWeatherService()
        {
            // Validate
            if (currentDayAsset == null) return false;

            return true;
        }

        private void OnWeatherEnd()
        {
            newWeatherTriggered = true;

            SetCurrentWeather();

            float weatherDuration = (float)GetWeatherDuration();
            weatherEnd = weatherDuration + time.TimeSpan.TotalSeconds;

            HandleTransmitMessage(onWeatherChange);
        }

        private void OnDisable()
        {
            messagingService.Unsubscribe<OnMinute>((ISubscribe<OnMinute>)this);
            messagingService.Unsubscribe<OnDayAsset>((ISubscribe<OnDayAsset>)this);
            messagingService.Unsubscribe<OnNightAsset>((ISubscribe<OnNightAsset>)this);
        }
    }
}