// Created by: William Dye - 2023
// License Type: Proprietary

using System;
using UnityEngine;
using UnityEditor;

namespace Mosuva.Pluvi.Services.Day
{
    [CustomEditor(typeof(DayScriptableObject))]
    public class DayScriptableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var dayScriptableObject = target as DayScriptableObject;
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.fontSize = 13;

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("| ☀ ☁ ☂ ☃     Day/Night Asset     ϟ ☼ ☾ ❄ |" + "   ", centeredStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Name: ", EditorStyles.miniBoldLabel);
            dayScriptableObject.DayName = EditorGUILayout.TextField("Weather Name: ", dayScriptableObject.DayName);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Weather Weighting: ", EditorStyles.miniBoldLabel);
            dayScriptableObject.Weighting = EditorGUILayout.IntField(dayScriptableObject.Weighting);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Skybox: ", EditorStyles.miniBoldLabel);
            dayScriptableObject.SkyLOD = EditorGUILayout.FloatField("Sky LOD: ", dayScriptableObject.SkyLOD);
            dayScriptableObject.SkyColour = EditorGUILayout.ColorField("Sky Colour: ", dayScriptableObject.SkyColour);
            EditorGUILayout.Space();
            dayScriptableObject.HaveUniqueSkybox = GUILayout.Toggle(dayScriptableObject.HaveUniqueSkybox, " Unique Skybox?: ");
            if (dayScriptableObject.HaveUniqueSkybox)
            {
                EditorGUILayout.Space();
                dayScriptableObject.UniqueSkybox = (Texture)EditorGUILayout.ObjectField("Image: ", dayScriptableObject.UniqueSkybox, typeof(Texture), false);
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Cloud: ", EditorStyles.miniBoldLabel);
            dayScriptableObject.CloudColour = EditorGUILayout.ColorField("Cloud Colour: ", dayScriptableObject.CloudColour);
            dayScriptableObject.CloudSpeed = EditorGUILayout.FloatField("Cloud Speed: ", dayScriptableObject.CloudSpeed);
            dayScriptableObject.CloudScale = EditorGUILayout.FloatField("Cloud Scale: ", dayScriptableObject.CloudScale);
            dayScriptableObject.CloudHeight = EditorGUILayout.FloatField("Cloud Height: ", dayScriptableObject.CloudHeight);
            EditorGUILayout.Space();
            dayScriptableObject.CloudTexture = (Texture)EditorGUILayout.ObjectField("Cloud Texture", dayScriptableObject.CloudTexture, typeof(Texture), false);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Fog: ", EditorStyles.miniBoldLabel);
            EditorGUILayout.Space();
            dayScriptableObject.FogScale = EditorGUILayout.Vector2Field("Fog Scale: ", dayScriptableObject.FogScale);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Celestial: ", EditorStyles.miniBoldLabel);
            EditorGUILayout.Space();
            dayScriptableObject.StarColour = EditorGUILayout.ColorField("Star Colour: ", dayScriptableObject.StarColour);
            dayScriptableObject.StarSpeed = EditorGUILayout.FloatField("Star Speed: ", dayScriptableObject.StarSpeed);
            dayScriptableObject.StarDensity = EditorGUILayout.FloatField("Star Density: ", dayScriptableObject.StarDensity);
            dayScriptableObject.StarSize = EditorGUILayout.FloatField("Star Size:", dayScriptableObject.StarSize);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Weather Assets: ", EditorStyles.miniBoldLabel);
            EditorGUILayout.Space();
            SerializedProperty sWeatherAssets = serializedObject.FindProperty("WeatherAsset");
            EditorGUILayout.PropertyField(sWeatherAssets, true);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(dayScriptableObject);
                serializedObject.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
            }
        }
    }
}