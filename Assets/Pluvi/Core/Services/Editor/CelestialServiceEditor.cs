// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;
using UnityEditor;

namespace Mosuva.Pluvi.Services.Celestial
{
    [CustomEditor(typeof(CelestialService))]
    public class CelestialServiceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var celestialService = target as CelestialService;

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
            EditorGUILayout.LabelField("| ☀ ☁  ☃    Celestial Service   ϟ ☂ ❄ |" + "   ", centeredStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Sun / Moon: ", EditorStyles.miniBoldLabel);
            EditorGUILayout.ObjectField("Directional Light: ", celestialService.InstantiatedLight, typeof(GameObject), true);
            EditorGUILayout.Space();
            EditorGUILayout.ObjectField("Sun Prefab: ", celestialService.CelestialConfig.SunObject, typeof(GameObject), true);
            EditorGUILayout.Vector3Field("Sun Offset: ", celestialService.CelestialConfig.SunOffset);
            EditorGUILayout.Space();
            EditorGUILayout.ObjectField("Moon Prefab: ", celestialService.CelestialConfig.MoonObject, typeof(GameObject), true);
            EditorGUILayout.Vector3Field("Moon Offset: ", celestialService.CelestialConfig.MoonOffset);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorUtility.SetDirty(celestialService);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
