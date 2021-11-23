using UGF.Application.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationConfigProjectAsset), true)]
    internal class ApplicationConfigProjectAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("This is Application Project Config Resource asset which can be specified in Application Launcher Resources component to load config from project settings during initialization.", MessageType.Info);

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Open Application Project Settings", GUILayout.Width(250F)))
                {
                    SettingsService.OpenProjectSettings("Project/Unity Game Framework/Application");
                }

                GUILayout.FlexibleSpace();
            }
        }
    }
}
