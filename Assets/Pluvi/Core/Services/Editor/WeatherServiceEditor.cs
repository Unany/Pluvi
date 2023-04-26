// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;
using UnityEditor;

namespace Mosuva.Pluvi.Services.Weather
{
    [CustomEditor(typeof(WeatherService))]
    public class WeatherServiceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var weatherService = target as WeatherService;

            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.fontSize = 15;

            var boldStyle = GUI.skin.GetStyle("Foldout");
            boldStyle.fontSize = 13;
            boldStyle.fontStyle = FontStyle.Bold;

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("| ☀ ☁  ☃      Weather Service      ϟ ☂ ❄ |" + "   ", centeredStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            /*
        private double weatherEnd = 0;
        private float weatherLerpProgress = 0;
        private float _weatherLerpSpeed;*/
            
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Weather: ", EditorStyles.miniBoldLabel);
            SerializedProperty scurrentWeatherAsset = serializedObject.FindProperty("currentWeatherAsset");
            EditorGUILayout.PropertyField(scurrentWeatherAsset, false);
            SerializedProperty spreviousWeatherAsset = serializedObject.FindProperty("previousWeatherAsset");
            EditorGUILayout.PropertyField(spreviousWeatherAsset, false);
            EditorGUILayout.Space();
            SerializedProperty sactiveWeatherObjectItems = serializedObject.FindProperty("activeWeatherObjectItems");
            EditorGUILayout.PropertyField(sactiveWeatherObjectItems, true);
            EditorGUILayout.Space();
            EditorGUILayout.DoubleField("Weather End: ", weatherService.weatherEnd);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorUtility.SetDirty(weatherService);
            serializedObject.ApplyModifiedProperties();
        }
    }
}