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

            if (TryGetRegisterType(type, out Type registerType))
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox($"Register Type: '{registerType}'.", MessageType.Info);
            }
        }

        private static bool TryGetRegisterType(Type target, out Type type)
        {
            while (target != null)
            {
                if (target.IsGenericType)
                {
                    Type definition = target.GetGenericTypeDefinition();

                    if (definition == typeof(ApplicationModuleInfoAsset<>))
                    {
                        type = target.GetGenericArguments()[0];
                        return true;
                    }
                }

                target = target.BaseType;
            }

            type = null;
            return false;
        }
    }
}
