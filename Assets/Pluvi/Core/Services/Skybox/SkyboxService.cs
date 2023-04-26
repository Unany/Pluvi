// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;
using Mosuva.Pluvi.Services.Core;
using Mosuva.Pluvi.Services.Timing;
using Mosuva.Pluvi.Services.Day;
using Mosuva.Pluvi.Services.Weather;
using Mosuva.Messaging.Core;

namespace Mosuva.Pluvi.Services.Skybox
{
    public class SkyboxService : MonoBehaviour, IWeatherService, 
                                    ISubscribe<OnMinute>, ISubscribe<OnDayAsset>, ISubscribe<OnNightAsset>, ISubscribe<OnWeatherChange>
    {
        DaySkybox daySkybox = new DaySkybox();
        NightSkybox nightSkybox = new NightSkybox();
        public DayScriptableObject currentDayAsset;
        public DayScriptableObject currentNightAsset;

        public float dayNightCycle;
        public float weatherLerpProgress = 0;
        public float weatherLerpSpeed = 2;
        public bool hideComponents = false;
        public bool isWeatherUpdating = false;

        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;
        private Material material;

        private MessagingService messagingService;
        public OnWeatherChange currentWeatherAsset;
        private TimeData time;

        public void Initialise()
        {
            meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            meshFilter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
            material = RenderSettings.skybox;
            meshRenderer.material = material;

            meshRenderer.hideFlags = HideFlags.None;
            meshFilter.hideFlags = HideFlags.None;

            this.gameObject.layer = 4;
            ReadMaterialProperties();
            WriteMaterialProperties();

            messagingService = ServiceLocator.Instance.Get<MessagingService>();
            messagingService.Subscribe<OnMinute>((ISubscribe<OnMinute>)this);
            messagingService.Subscribe<OnDayAsset>((ISubscribe<OnDayAsset>)this);
            messagingService.Subscribe<OnNightAsset>((ISubscribe<OnNightAsset>)this);
            messagingService.Subscribe<OnWeatherChange>((ISubscribe<OnWeatherChange>)this);
        }

        public void Execute(OnMinute message)
        {
            time = message.Time;
            material.SetFloat(SkyboxDefines.Time.EVALUATED_TIME, time.EvaluatedValue);
            
            if (isWeatherUpdating) WeatherUpdate();
        }

        public void Execute(OnDayAsset message)
        {  
            currentDayAsset = message.CurrentDayAsset;
            
            if (currentDayAsset.HaveUniqueSkybox == true)
            {
                material.SetTexture(SkyboxDefines.Day.TEXTURE, currentDayAsset.UniqueSkybox);
            }
            
            material.SetColor(SkyboxDefines.Day.COLOUR, currentDayAsset.SkyColour);

            material.SetTexture(SkyboxDefines.Day.CLOUD_TEXTURE, currentDayAsset.CloudTexture);
            material.SetColor(SkyboxDefines.Day.CLOUD_COLOUR, currentDayAsset.CloudColour);
            material.SetFloat(SkyboxDefines.Day.CLOUD_SPEED, currentDayAsset.CloudSpeed);
            material.SetFloat(SkyboxDefines.Day.CLOUD_SCALE, currentDayAsset.CloudScale);
            material.SetFloat(SkyboxDefines.Day.CLOUD_HEIGHT, currentDayAsset.CloudHeight);
        }

        public void Execute(OnNightAsset message)
        {
            currentNightAsset = message.CurrentNightAsset;

            if (currentNightAsset.HaveUniqueSkybox == true)
            {
                material.SetTexture(SkyboxDefines.Night.TEXTURE, currentNightAsset.UniqueSkybox);
            }

            material.SetColor(SkyboxDefines.Night.COLOUR, currentNightAsset.SkyColour);

            material.SetTexture(SkyboxDefines.Night.CLOUD_TEXTURE, currentNightAsset.CloudTexture);
            material.SetColor(SkyboxDefines.Night.CLOUD_COLOUR, currentNightAsset.CloudColour);
            material.SetFloat(SkyboxDefines.Night.CLOUD_SPEED, currentNightAsset.CloudSpeed);
            material.SetFloat(SkyboxDefines.Night.CLOUD_SCALE, currentNightAsset.CloudScale);
            material.SetFloat(SkyboxDefines.Night.CLOUD_HEIGHT, currentNightAsset.CloudHeight);

            material.SetColor(SkyboxDefines.Night.STAR_COLOUR, currentNightAsset.StarColour);
            material.SetFloat(SkyboxDefines.Night.STAR_SPEED, currentNightAsset.CloudSpeed);
            material.SetFloat(SkyboxDefines.Night.STAR_DENSITY, currentNightAsset.StarDensity);
            material.SetFloat(SkyboxDefines.Night.STAR_SIZE, currentNightAsset.StarSize);
        }

        public void Execute(OnWeatherChange message)
        {
            currentWeatherAsset = message;

            if (currentWeatherAsset.PreviousWeatherAsset == null) return;

            isWeatherUpdating = true;
        }

        private void WeatherUpdate()
        {
            var tempWeatherColour = Color.Lerp(currentWeatherAsset.PreviousWeatherAsset.WeatherColourMultiplier,
                currentWeatherAsset.CurrentWeatherAsset.WeatherColourMultiplier, weatherLerpProgress);
            var tempWeatherCloudMultiplier = Mathf.Lerp(currentWeatherAsset.PreviousWeatherAsset.CloudMultiplier,
                currentWeatherAsset.CurrentWeatherAsset.CloudMultiplier, weatherLerpProgress);

            material.SetColor(SkyboxDefines.Weather.COLOUR, tempWeatherColour);
            material.SetFloat(SkyboxDefines.Weather.CLOUD_MULTIPLIER, tempWeatherCloudMultiplier);

            weatherLerpProgress += UnityEngine.Time.deltaTime * currentWeatherAsset.CurrentWeatherAsset.WeatherLerpSpeed;

            if (weatherLerpProgress >= 1)
            {
                weatherLerpProgress = 0;
                isWeatherUpdating = false;
            }
        }

        private void ReadMaterialProperties()
        {
            if (meshRenderer) material = meshRenderer.sharedMaterial;
            if (!material) return;

            daySkybox.DayTexture = (Cubemap)material.GetTexture(SkyboxDefines.Day.TEXTURE);
            daySkybox.DayColour = material.GetColor(SkyboxDefines.Day.COLOUR);
            daySkybox.WeatherColour = material.GetColor(SkyboxDefines.Weather.COLOUR);
            daySkybox.CloudTextureDay = material.GetTexture(SkyboxDefines.Day.CLOUD_TEXTURE);
            daySkybox.CloudColourDay = material.GetColor(SkyboxDefines.Day.CLOUD_COLOUR);
            daySkybox.CloudSpeedDay = material.GetFloat(SkyboxDefines.Day.CLOUD_SPEED);
            daySkybox.CloudScaleDay = material.GetFloat(SkyboxDefines.Day.CLOUD_SCALE);
            daySkybox.CloudHeightDay = material.GetFloat(SkyboxDefines.Day.CLOUD_HEIGHT);
            daySkybox.WeatherCloudMultiplier = material.GetFloat(SkyboxDefines.Weather.CLOUD_MULTIPLIER);
            daySkybox.FogScaleDay = material.GetVector(SkyboxDefines.Day.FOG_SCALE);

            nightSkybox.NightTexture = (Cubemap)material.GetTexture(SkyboxDefines.Night.TEXTURE);
            nightSkybox.NightColour = material.GetColor(SkyboxDefines.Night.COLOUR);
            nightSkybox.CloudTextureNight = material.GetTexture(SkyboxDefines.Night.CLOUD_TEXTURE);
            nightSkybox.CloudColourNight = material.GetColor(SkyboxDefines.Night.CLOUD_COLOUR);
            nightSkybox.CloudSpeedNight = material.GetFloat(SkyboxDefines.Night.CLOUD_SPEED);
            nightSkybox.CloudScaleNight = material.GetFloat(SkyboxDefines.Night.CLOUD_SCALE);
            nightSkybox.CloudHeightNight = material.GetFloat(SkyboxDefines.Night.CLOUD_HEIGHT);
            nightSkybox.FogScaleNight = material.GetVector(SkyboxDefines.Night.FOG_SCALE);
            nightSkybox.StarColourNight = material.GetColor(SkyboxDefines.Night.STAR_COLOUR);
            nightSkybox.StarTimeNight = material.GetFloat(SkyboxDefines.Night.STAR_TIME);
            nightSkybox.StarDensityNight = material.GetFloat(SkyboxDefines.Night.STAR_DENSITY);
            nightSkybox.StarSizeNight = material.GetFloat(SkyboxDefines.Night.STAR_SIZE);

            dayNightCycle = material.GetFloat(SkyboxDefines.Time.EVALUATED_TIME);
        }

        private void WriteMaterialProperties()
        {
            if (!material) return;
            string shaderName = material.shader.name;

            if (material && meshRenderer && meshFilter && meshRenderer.sharedMaterial && !Application.isPlaying)
            {
                meshRenderer.sharedMaterial.hideFlags = (hideComponents) ? HideFlags.HideInInspector : HideFlags.None;
                meshRenderer.hideFlags = (hideComponents) ? HideFlags.HideInInspector : HideFlags.None;
                meshFilter.hideFlags = (hideComponents) ? HideFlags.HideInInspector : HideFlags.None;
            }

            material.SetTexture(SkyboxDefines.Day.TEXTURE, daySkybox.DayTexture);
            material.SetColor(SkyboxDefines.Day.COLOUR, daySkybox.DayColour);
            material.SetColor(SkyboxDefines.Weather.COLOUR, daySkybox.WeatherColour);
            material.SetTexture(SkyboxDefines.Day.CLOUD_TEXTURE, daySkybox.CloudTextureDay);
            material.SetColor(SkyboxDefines.Day.CLOUD_COLOUR, daySkybox.CloudColourDay);
            material.SetFloat(SkyboxDefines.Day.CLOUD_SPEED, daySkybox.CloudSpeedDay);
            material.SetFloat(SkyboxDefines.Day.CLOUD_SCALE, daySkybox.CloudScaleDay);
            material.SetFloat(SkyboxDefines.Day.CLOUD_HEIGHT, daySkybox.CloudHeightDay);
            material.SetFloat(SkyboxDefines.Weather.CLOUD_MULTIPLIER, daySkybox.WeatherCloudMultiplier);
            material.SetVector(SkyboxDefines.Day.FOG_SCALE, daySkybox.FogScaleDay);

            material.SetTexture(SkyboxDefines.Night.TEXTURE, nightSkybox.NightTexture);
            material.SetColor(SkyboxDefines.Night.COLOUR, nightSkybox.NightColour);
            material.SetTexture(SkyboxDefines.Night.CLOUD_TEXTURE, nightSkybox.CloudTextureNight);
            material.SetColor(SkyboxDefines.Night.CLOUD_COLOUR, nightSkybox.CloudColourNight);
            material.SetFloat(SkyboxDefines.Night.CLOUD_SPEED, nightSkybox.CloudSpeedNight);
            material.SetFloat(SkyboxDefines.Night.CLOUD_SCALE, nightSkybox.CloudScaleNight);
            material.SetFloat(SkyboxDefines.Night.CLOUD_HEIGHT, nightSkybox.CloudHeightNight);
            material.SetVector(SkyboxDefines.Night.FOG_SCALE, nightSkybox.FogScaleNight);
            material.SetColor(SkyboxDefines.Night.STAR_COLOUR, nightSkybox.StarColourNight);
            material.SetFloat(SkyboxDefines.Night.STAR_TIME, nightSkybox.StarTimeNight);
            material.SetFloat(SkyboxDefines.Night.STAR_DENSITY, nightSkybox.StarDensityNight);
            material.SetFloat(SkyboxDefines.Night.STAR_SIZE, nightSkybox.StarSizeNight);
            
            material.SetFloat(SkyboxDefines.Time.EVALUATED_TIME, dayNightCycle);
        }

        private void OnDisable()
        {
            ResetHideFlags();
            messagingService.Unsubscribe((ISubscribe<OnMinute>)this);
            messagingService.Unsubscribe((ISubscribe<OnDayAsset>)this);
            messagingService.Unsubscribe((ISubscribe<OnNightAsset>)this);
            messagingService.Unsubscribe((ISubscribe<OnWeatherChange>)this);
        }

        private void OnDestroy()
        {
            ResetHideFlags();
        }

        private void ResetHideFlags()
        {
            if (material && meshRenderer && meshFilter && !Application.isPlaying)
            {
                meshRenderer.sharedMaterial.hideFlags = HideFlags.None;
                meshRenderer.hideFlags = HideFlags.None;
                meshFilter.hideFlags = HideFlags.None;
            }
        }
    }
}
