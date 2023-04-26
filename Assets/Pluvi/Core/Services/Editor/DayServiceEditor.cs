// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;
using UnityEditor;

namespace Mosuva.Pluvi.Services.Day
{
    [CustomEditor(typeof(DayService))]
    public class DayServiceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var dayService = target as DayService;

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
            EditorGUILayout.LabelField("| ☀ ☁  ☃      Day Service      ϟ ☂ ❄ |" + "   ", centeredStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Days: ", EditorStyles.miniBoldLabel);
            SerializedProperty scurrentDayAsset = serializedObject.FindProperty("currentDayAsset");
            EditorGUILayout.PropertyField(scurrentDayAsset, false);
            SerializedProperty scurrentNightAsset = serializedObject.FindProperty("currentNightAsset");
            EditorGUILayout.PropertyField(scurrentNightAsset, false);
            EditorGUILayout.Space();
            SerializedProperty scurrentSeasonAsset = serializedObject.FindProperty("currentSeasonAsset");
            EditorGUILayout.PropertyField(scurrentSeasonAsset, false);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorUtility.SetDirty(dayService);
            serializedObject.ApplyModifiedProperties();
        }
    }
}