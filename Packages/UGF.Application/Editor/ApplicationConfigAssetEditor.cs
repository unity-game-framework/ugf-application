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
        private ReorderableListDrawer m_listCollections;
        private ReorderableListSelectionDrawerByElement m_listCollectionsSelection;

        private void OnEnable()
        {
            m_list = new EnabledPropertyListDrawer(serializedObject.FindProperty("m_modules"));

            m_listSelection = new ReorderableListSelectionDrawerByPath(m_list, "m_value")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listCollections = new ReorderableListDrawer(serializedObject.FindProperty("m_collections"));

            m_listCollectionsSelection = new ReorderableListSelectionDrawerByElement(m_listCollections)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_list.Enable();
            m_listSelection.Enable();
            m_listCollections.Enable();
            m_listCollectionsSelection.Enable();
        }

        private void OnDisable()
        {
            m_list.Disable();
            m_listSelection.Disable();
            m_listCollections.Disable();
            m_listCollectionsSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_list.DrawGUILayout();
                m_listCollections.DrawGUILayout();

                m_listSelection.DrawGUILayout();
                m_listCollectionsSelection.DrawGUILayout();
            }

            if (!m_listSelection.Drawer.HasEditor)
            {
                EditorGUILayout.HelpBox("Select any module or collection to display.", MessageType.Info);
            }
        }
    }
}
