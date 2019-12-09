using UGF.Description.Editor;
using UGF.Module.Runtime;
using UnityEditor;

namespace UGF.Module.Editor
{
    [CustomEditor(typeof(ModuleBuilderAsset), true)]
    public class ModuleBuilderAssetEditor : DescriptionAssetEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawModuleInfo();
        }

        protected override void DrawDescription()
        {
            SerializedProperty propertyDescription = serializedObject.FindProperty("m_description");

            if (propertyDescription != null)
            {
                base.DrawDescription();
            }
        }

        protected virtual void DrawModuleInfo()
        {
            var builder = target as ModuleBuilderAsset;

            if (builder != null)
            {
                DrawModuleInfo(builder);
            }
        }

        protected virtual void DrawModuleInfo(IModuleBuilder builder)
        {
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox($"Register Type: '{builder.RegisterType}'.", MessageType.Info);
        }
    }
}
