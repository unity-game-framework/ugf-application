using UGF.Application.Runtime;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationLauncherResources))]
    internal class ApplicationLauncherResourcesEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private ReorderableList m_list;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");

            SerializedProperty propertyResources = serializedObject.FindProperty("m_resources");

            m_list = new ReorderableList(serializedObject, propertyResources);
            m_list.drawHeaderCallback = OnDrawHeader;
            m_list.elementHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2F;
            m_list.drawElementCallback = OnDrawElement;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(m_propertyScript);
            }

            m_list.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnDrawHeader(Rect rect)
        {
            GUI.Label(rect, $"{m_list.serializedProperty.displayName} (Size: {m_list.serializedProperty.arraySize})", EditorStyles.boldLabel);
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyElement = m_list.serializedProperty.GetArrayElementAtIndex(index);

            rect.y += EditorGUIUtility.standardVerticalSpacing;
            rect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(rect, propertyElement, GUIContent.none);
        }
    }
}
