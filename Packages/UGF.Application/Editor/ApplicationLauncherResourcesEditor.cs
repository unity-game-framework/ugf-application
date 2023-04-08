using UGF.Application.Runtime;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationLauncherResourcesComponent), true)]
    internal class ApplicationLauncherResourcesEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_list;

        private void OnEnable()
        {
            m_list = new ReorderableListDrawer(serializedObject.FindProperty("m_resources"));
            m_list.Enable();
        }

        private void OnDisable()
        {
            m_list.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_list.DrawGUILayout();
            }
        }
    }
}
