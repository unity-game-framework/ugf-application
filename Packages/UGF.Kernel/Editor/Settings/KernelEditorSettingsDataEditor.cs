using UnityEditor;

namespace UGF.Kernel.Editor.Settings
{
    [CustomEditor(typeof(KernelEditorSettingsData), true)]
    internal class KernelEditorSettingsDataEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyConfig;
        private UnityEditor.Editor m_editor;

        private void OnEnable()
        {
            m_propertyConfig = serializedObject.FindProperty("m_config");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(m_propertyConfig);

            DrawEditor(m_propertyConfig);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawEditor(SerializedProperty serializedProperty)
        {
            if (serializedProperty.objectReferenceValue != null)
            {
                if (m_editor == null || m_editor.target != serializedProperty.objectReferenceValue)
                {
                    m_editor = CreateEditor(serializedProperty.objectReferenceValue);
                }
            }
            else if (m_editor != null)
            {
                DestroyImmediate(m_editor);
            }

            if (m_editor != null)
            {
                serializedProperty.isExpanded = EditorGUILayout.InspectorTitlebar(serializedProperty.isExpanded, m_editor);

                if (serializedProperty.isExpanded)
                {
                    m_editor.OnInspectorGUI();
                }
            }
            else
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("Select config asset to display.", MessageType.Info);
            }
        }
    }
}
