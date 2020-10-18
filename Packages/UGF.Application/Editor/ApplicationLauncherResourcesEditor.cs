using UGF.Application.Runtime;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationLauncherResources), true)]
    internal class ApplicationLauncherResourcesEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private ReorderableListDrawer m_list;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");

            SerializedProperty propertyResources = serializedObject.FindProperty("m_resources");

            m_list = new ReorderableListDrawer(propertyResources);
            m_list.Enable();
        }

        private void OnDisable()
        {
            m_list.Disable();
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
        }
    }
}
