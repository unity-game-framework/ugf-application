using UGF.Application.Runtime;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationSettingsAsset), true)]
    internal class ApplicationSettingsAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyConfig;
        private EditorObjectReferenceDrawer m_drawer;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent ConfigLabel { get; } = new GUIContent("Project Config", "This is global project config used from Application Config Project asset " +
                                                                                      "which can be specified in Application Launcher Resources component to load config from project settings during initialization.");
        }

        private void OnEnable()
        {
            m_propertyConfig = serializedObject.FindProperty("m_config");

            m_drawer = new EditorObjectReferenceDrawer(m_propertyConfig);
            m_drawer.Drawer.DisplayTitlebar = true;
            m_drawer.Enable();
        }

        private void OnDisable()
        {
            m_drawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            m_styles ??= new Styles();

            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(m_propertyConfig, m_styles.ConfigLabel);

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();

            if (m_propertyConfig.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Select Application Config Asset to display.", MessageType.Info);
            }
            else
            {
                m_drawer.DrawGUILayout();
            }
        }
    }
}
