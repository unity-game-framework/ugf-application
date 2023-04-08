using UGF.Application.Runtime;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationModuleCollectionListAsset), true)]
    internal class ApplicationModuleCollectionListAssetEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_listModules;
        private ReorderableListSelectionDrawerByElement m_listModulesSelection;

        private void OnEnable()
        {
            m_listModules = new ReorderableListDrawer(serializedObject.FindProperty("m_modules"));

            m_listModulesSelection = new ReorderableListSelectionDrawerByElement(m_listModules)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listModules.Enable();
            m_listModulesSelection.Enable();
        }

        private void OnDisable()
        {
            m_listModules.Disable();
            m_listModulesSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listModules.DrawGUILayout();
                m_listModulesSelection.DrawGUILayout();
            }
        }
    }
}
