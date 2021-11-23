using UGF.Application.Runtime;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.EnabledProperty;
using UnityEditor;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationConfigAsset), true)]
    internal class ApplicationConfigAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private EnabledPropertyListDrawer m_list;
        private ReorderableListSelectionDrawerByPath m_listSelection;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");

            SerializedProperty propertyModules = serializedObject.FindProperty("m_modules");

            m_list = new EnabledPropertyListDrawer(propertyModules);

            m_listSelection = new ReorderableListSelectionDrawerByPath(m_list, "m_value")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_list.Enable();
            m_listSelection.Enable();
        }

        private void OnDisable()
        {
            m_list.Disable();
            m_listSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(m_propertyScript);
            }

            m_list.DrawGUILayout();
            m_listSelection.DrawGUILayout();

            serializedObject.ApplyModifiedProperties();

            if (!m_listSelection.Drawer.HasEditor)
            {
                EditorGUILayout.HelpBox("Select any module to display.", MessageType.Info);
            }
        }
    }
}
