// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;
using UnityEditor;

namespace Mosuva.Pluvi.Services.Timing
{
    [CustomEditor(typeof(TimeService))]
    public class TimeServiceEditor : Editor
    {   
        public override void OnInspectorGUI()
        {
            var timeService = target as TimeService;

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
            EditorGUILayout.LabelField("| ☀ ☁  ☃      Time Service      ϟ ☂ ❄ |" + "   ", centeredStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Time: ", EditorStyles.miniBoldLabel);
            EditorGUILayout.FloatField("Seconds in full day: ", timeService.SecondsInFullDay);
            timeService.Time.TimeOffset = EditorGUILayout.FloatField("Time Offset: ", timeService.Time.TimeOffset);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Debug: ", EditorStyles.miniBoldLabel);
            timeService.IsDebug = GUILayout.Toggle(timeService.IsDebug, " Show Debug Time?: ");
            if (timeService.IsDebug)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.CurveField("Time Cycle: ", timeService.TimeCurve);
                timeService.Time.EvaluatedValue = EditorGUILayout.FloatField("Evaluated Value: ", timeService.Time.EvaluatedValue);
                EditorGUILayout.TextField("DD:HH:MM:SS", timeService.TimeText);
                EditorGUILayout.Space();
                EditorGUILayout.FloatField("FogDensity: ", RenderSettings.fogDensity);
                EditorGUILayout.FloatField("Ambient Intensity: ", RenderSettings.ambientIntensity);
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20);

            EditorUtility.SetDirty(timeService);
            serializedObject.ApplyModifiedProperties();
        }
    }
}