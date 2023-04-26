// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;
using UnityEditor;

namespace Mosuva.Pluvi.Services.Skybox
{
    [CustomEditor(typeof(SkyboxService))]
    public class SkyboxServiceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var skyboxService = target as SkyboxService;

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
            EditorGUILayout.LabelField("| ☀ ☁  ☃      Skybox Service      ϟ ☂ ❄ |" + "   ", centeredStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Skybox: ", EditorStyles.miniBoldLabel);
            EditorGUILayout.FloatField("Weather Lerping Progress: ", skyboxService.weatherLerpProgress);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Current Day Assets: ", EditorStyles.miniBoldLabel);
            SerializedProperty scurrentDayAsset = serializedObject.FindProperty("currentDayAsset");
            EditorGUILayout.PropertyField(scurrentDayAsset, false);
            SerializedProperty scurrentNightAsset = serializedObject.FindProperty("currentNightAsset");
            EditorGUILayout.PropertyField(scurrentNightAsset, false);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorUtility.SetDirty(skyboxService);
            serializedObject.ApplyModifiedProperties();
        }
    }
}