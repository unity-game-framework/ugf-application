using UGF.Application.Runtime;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.EnabledProperty;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationConfigAsset), true)]
    internal class ApplicationConfigAssetEditor : UnityEditor.Editor
    {
        private EnabledPropertyListDrawer m_list;
        private ReorderableListSelectionDrawerByPath m_listSelection;

        private void OnEnable()
        {
            SerializedProperty propertyModules = serializedObject.FindProperty("m_modules");

            m_list = new EnabledPropertyListDrawer(propertyModules);

            m_listSelection = new ReorderableListSelectionDrawerByPath(m_list, "m_value")
            {
                Drawer = { DisplayTitlebar = true }
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
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_list.DrawGUILayout();
                m_listSelection.DrawGUILayout();
            }

            if (!m_listSelection.Drawer.HasEditor)
            {
                EditorGUILayout.HelpBox("Select any module to display.", MessageType.Info);
            }
        }
    }
}
