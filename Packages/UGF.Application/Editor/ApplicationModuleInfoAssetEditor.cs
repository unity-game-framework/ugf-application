using System;
using UGF.Application.Runtime;
using UnityEditor;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationModuleInfoAsset), true)]
    public class ApplicationModuleInfoAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawModuleInfo();
        }

        protected void DrawModuleInfo()
        {
            Type type = target.GetType();

            if (typeof(ApplicationModuleInfoAsset<>).IsAssignableFrom(type))
            {
                Type registerType = type.GetGenericArguments()[0];

                EditorGUILayout.Space();
                EditorGUILayout.HelpBox($"Register Type: '{registerType}'.", MessageType.Info);
            }
        }
    }
}
