// Created by: William Dye - 2023
// License Type: Proprietary

using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Mosuva.Pluvi.Services.Weather
{
    [CustomEditor(typeof(WeatherScriptableObject))]
    public class WeatherScriptableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var weatherScriptableObject = target as WeatherScriptableObject;
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.fontSize = 13;

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("| ☀ ☁ ☂ ☃     Weather Asset     ϟ ☼ ☾ ❄ |" + "   ", centeredStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Name: ", EditorStyles.miniBoldLabel);
            weatherScriptableObject.WeatherName = EditorGUILayout.TextField("Weather Name: ", weatherScriptableObject.WeatherName);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("General: ", EditorStyles.miniBoldLabel);
            weatherScriptableObject.WeatherDurationMin = EditorGUILayout.FloatField("Duration Min: ", weatherScriptableObject.WeatherDurationMin);
            weatherScriptableObject.WeatherDurationMax = EditorGUILayout.FloatField("Duration Max: ", weatherScriptableObject.WeatherDurationMax);
            EditorGUILayout.Space();
            weatherScriptableObject.WeatherLerpSpeed = EditorGUILayout.IntField("Weather Lerp Speed: ", weatherScriptableObject.WeatherLerpSpeed);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Multipliers: ", EditorStyles.miniBoldLabel);
            weatherScriptableObject.WeatherColourMultiplier = EditorGUILayout.ColorField("Weather Tint: ", weatherScriptableObject.WeatherColourMultiplier);
            EditorGUILayout.Space();
            weatherScriptableObject.CloudMultiplier = EditorGUILayout.FloatField("Cloud Multiplier: ", weatherScriptableObject.CloudMultiplier);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Object Pool: ", EditorStyles.miniBoldLabel);
            SerializedProperty sWeatherPool = serializedObject.FindProperty("WeatherPool");
            EditorGUILayout.PropertyField(sWeatherPool, true);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(weatherScriptableObject);
                serializedObject.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
            }
        }
    }
}