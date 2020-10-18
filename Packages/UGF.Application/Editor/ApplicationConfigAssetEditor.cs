using System.Collections.Generic;
using UGF.Application.Runtime;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationConfigAsset), true)]
    internal class ApplicationConfigAssetEditor : UnityEditor.Editor
    {
        private readonly Dictionary<SerializedProperty, UnityEditor.Editor> m_editors = new Dictionary<SerializedProperty, UnityEditor.Editor>();
        private SerializedProperty m_propertyScript;
        private ReorderableList m_list;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");

            SerializedProperty propertyModules = serializedObject.FindProperty("m_modules");

            m_list = new ReorderableList(serializedObject, propertyModules);
            m_list.drawHeaderCallback = OnDrawHeader;
            m_list.drawElementCallback = OnDrawElement;
            m_list.elementHeightCallback = OnElementHeight;
            m_list.onAddCallback = OnAdd;
            m_list.onSelectCallback = OnSelect;
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

            if (m_editors.Count > 0)
            {
                DrawEditors();
            }
            else
            {
                EditorGUILayout.HelpBox("Select any module to display settings.", MessageType.Info);
            }
        }

        private void OnDrawHeader(Rect rect)
        {
            string label = $"Modules (Size: {m_list.count})";

            GUI.Label(rect, label, EditorStyles.boldLabel);
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyElement = m_list.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty propertyActive = propertyElement.FindPropertyRelative("m_active");
            SerializedProperty propertyInfo = propertyElement.FindPropertyRelative("m_info");

            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float heightActive = EditorGUI.GetPropertyHeight(propertyActive);
            float heightInfo = EditorGUI.GetPropertyHeight(propertyInfo);

            rect.y += spacing;

            var rectActive = new Rect(rect.x, rect.y, 15F, heightActive);
            var rectBuilder = new Rect(rectActive.xMax + spacing, rect.y, rect.width - rectActive.width - spacing, heightInfo);

            propertyActive.boolValue = GUI.Toggle(rectActive, propertyActive.boolValue, GUIContent.none);

            EditorGUI.PropertyField(rectBuilder, propertyInfo, GUIContent.none);
        }

        private float OnElementHeight(int index)
        {
            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float height = EditorGUIUtility.singleLineHeight;

            return height + spacing * 2F;
        }

        private void OnAdd(ReorderableList list)
        {
            SerializedProperty propertyModules = list.serializedProperty;

            propertyModules.InsertArrayElementAtIndex(propertyModules.arraySize);

            SerializedProperty propertyElement = propertyModules.GetArrayElementAtIndex(propertyModules.arraySize - 1);
            SerializedProperty propertyActive = propertyElement.FindPropertyRelative("m_active");
            SerializedProperty propertyInfo = propertyElement.FindPropertyRelative("m_info");

            propertyActive.boolValue = true;
            propertyInfo.objectReferenceValue = null;

            serializedObject.ApplyModifiedProperties();
        }

        private void OnSelect(ReorderableList list)
        {
            SerializedProperty propertyElement = list.serializedProperty.GetArrayElementAtIndex(list.index);

            ClearEditors();
            CreateEditors(propertyElement);
        }

        private void DrawEditors()
        {
            foreach (KeyValuePair<SerializedProperty, UnityEditor.Editor> pair in m_editors)
            {
                SerializedProperty serializedProperty = pair.Key;
                UnityEditor.Editor editor = pair.Value;

                serializedProperty.isExpanded = EditorGUILayout.InspectorTitlebar(serializedProperty.isExpanded, editor);

                if (serializedProperty.isExpanded)
                {
                    editor.OnInspectorGUI();
                }
            }
        }

        private void CreateEditors(SerializedProperty propertyElement)
        {
            SerializedProperty propertyInfo = propertyElement.FindPropertyRelative("m_info");

            if (propertyInfo.objectReferenceValue != null)
            {
                UnityEditor.Editor editor = CreateEditor(propertyInfo.objectReferenceValue);

                m_editors.Add(propertyElement, editor);
            }
        }

        private void ClearEditors()
        {
            foreach (KeyValuePair<SerializedProperty, UnityEditor.Editor> pair in m_editors)
            {
                DestroyImmediate(pair.Value);
            }

            m_editors.Clear();
        }
    }
}
