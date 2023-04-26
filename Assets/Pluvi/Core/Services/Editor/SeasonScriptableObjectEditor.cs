// Created by: William Dye - 2023
// License Type: Proprietary

using System;
using UnityEditor;
using UnityEngine;

namespace Mosuva.Pluvi.Services.Season
{
    [CustomEditor(typeof(SeasonScriptableObject))]
    public class SeasonScriptableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var seasonScriptableObject = target as SeasonScriptableObject;
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.fontSize = 13;

            var boldStyle = GUI.skin.GetStyle("Foldout");
            boldStyle.fontSize = 13;
            boldStyle.fontStyle = FontStyle.Bold;

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("| ☀ ☁ ☂ ☃     Season Asset     ϟ ☼ ☾ ❄ |" + "   ", centeredStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Name: ", EditorStyles.miniBoldLabel);
            seasonScriptableObject.SeasonName = EditorGUILayout.TextField("Season Name: ", seasonScriptableObject.SeasonName);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Cycle Amount: ", EditorStyles.miniBoldLabel);
            seasonScriptableObject.CycleCount = EditorGUILayout.IntField("Cycles: ", seasonScriptableObject.CycleCount);
            seasonScriptableObject.LerpToValue = EditorGUILayout.FloatField("Lerp Value: ", seasonScriptableObject.LerpToValue);
            seasonScriptableObject.AmbientIntensity = EditorGUILayout.FloatField("Ambient Intensity: ", seasonScriptableObject.AmbientIntensity);
            seasonScriptableObject.ReflectionIntensity = EditorGUILayout.FloatField("Reflection Intensity: ",seasonScriptableObject.ReflectionIntensity);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Time: ", EditorStyles.miniBoldLabel);
            SerializedProperty sTimeCurve = serializedObject.FindProperty("TimeCurve");
            EditorGUILayout.PropertyField(sTimeCurve, true);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Lighting: ", EditorStyles.miniBoldLabel);
            EditorGUILayout.GradientField("Lighting Gradient: ", seasonScriptableObject.LightingGradient);
            if (RenderSettings.fog)
            {
                EditorGUILayout.Space(20);
                EditorGUILayout.LabelField("Lighting Fog:", EditorStyles.miniBoldLabel);
                seasonScriptableObject.FogGradient = EditorGUILayout.GradientField("Fog Gradient: ", seasonScriptableObject.FogGradient);
                SerializedProperty sFogCurve = serializedObject.FindProperty("FogDensityCurve");
                EditorGUILayout.PropertyField(sFogCurve, true);
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20);
            SerializedProperty sDayPool = serializedObject.FindProperty("DayAssets");
            EditorGUILayout.PropertyField(sDayPool, true);
            EditorGUILayout.Space();

            EditorGUILayout.Space(20);
            SerializedProperty sNightPool = serializedObject.FindProperty("NightAssets");
            EditorGUILayout.PropertyField(sNightPool, true);
            EditorGUILayout.Space();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(seasonScriptableObject);
                serializedObject.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
            }
        }
    }
}