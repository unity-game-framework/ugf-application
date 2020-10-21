using UGF.Application.Runtime;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.EnabledProperty;
using UnityEditor;
using UnityEditorInternal;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationConfigAsset), true)]
    internal class ApplicationConfigAssetEditor : UnityEditor.Editor
    {
        private readonly EditorDrawer m_drawer = new EditorDrawer
        {
            DisplayTitlebar = true
        };

        private SerializedProperty m_propertyScript;
        private EnabledPropertyListDrawer m_list;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");

            SerializedProperty propertyModules = serializedObject.FindProperty("m_modules");

            m_list = new EnabledPropertyListDrawer(propertyModules);
            m_list.List.onSelectCallback = OnSelect;

            m_list.Enable();
            m_drawer.Disable();
        }

        private void OnDisable()
        {
            m_list.Disable();
            m_drawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(m_propertyScript);
            }

            m_list.DrawGUILayout();

            serializedObject.ApplyModifiedProperties();

            if (m_drawer.HasEditor)
            {
                m_drawer.DrawGUILayout();
            }
            else
            {
                EditorGUILayout.HelpBox("Select any module to display.", MessageType.Info);
            }
        }

        private void OnSelect(ReorderableList list)
        {
            if (list.index >= 0 && list.index < list.count)
            {
                SerializedProperty propertyElement = m_list.SerializedProperty.GetArrayElementAtIndex(list.index);
                SerializedProperty propertyModule = propertyElement.FindPropertyRelative("m_value");

                if (propertyModule.objectReferenceValue != null)
                {
                    m_drawer.Set(propertyModule.objectReferenceValue);
                }
                else
                {
                    m_drawer.Clear();
                }
            }
            else
            {
                m_drawer.Clear();
            }
        }
    }
}
