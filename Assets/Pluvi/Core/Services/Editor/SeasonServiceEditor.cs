// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;
using UnityEditor;

namespace Mosuva.Pluvi.Services.Season
{
    [CustomEditor(typeof(SeasonService))]
    public class SeasonServiceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var seasonService = target as SeasonService;

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
            EditorGUILayout.LabelField("| ☀ ☁  ☃     Season Service     ϟ ☂ ❄ |" + "   ", centeredStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Seasons: ", EditorStyles.miniBoldLabel);
            SerializedProperty scurrentSeasonAsset = serializedObject.FindProperty("currentSeasonAsset");
            EditorGUILayout.PropertyField(scurrentSeasonAsset, false);
            SerializedProperty spreviousSeasonAsset = serializedObject.FindProperty("previousSeasonAsset");
            EditorGUILayout.PropertyField(spreviousSeasonAsset, false);
            EditorGUILayout.Space();
            EditorGUILayout.FloatField("Season Lerping Progress: ", seasonService.SeasonLerpingProgress);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            SerializedProperty sSeasons = serializedObject.FindProperty("seasons");
            EditorGUILayout.PropertyField(sSeasons, true);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Random Seasons: ", EditorStyles.miniBoldLabel);
            seasonService.RandomSeason = GUILayout.Toggle(seasonService.RandomSeason, "Random Season");
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            EditorGUILayout.Space();

            EditorUtility.SetDirty(seasonService);
            serializedObject.ApplyModifiedProperties();
        }
    }
}