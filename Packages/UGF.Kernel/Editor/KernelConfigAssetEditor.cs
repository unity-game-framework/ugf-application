using System.Collections.Generic;
using UGF.CustomSettings.Editor;
using UGF.Kernel.Runtime;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UGF.Kernel.Editor
{
    [CustomEditor(typeof(KernelConfigAsset), true)]
    internal class KernelConfigAssetEditor : UnityEditor.Editor
    {
        private readonly Dictionary<SerializedProperty, UnityEditor.Editor> m_editors = new Dictionary<SerializedProperty, UnityEditor.Editor>();
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyName;
        private ReorderableList m_list;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyName = serializedObject.FindProperty("m_description.m_name");

            SerializedProperty propertyModules = serializedObject.FindProperty("m_description.m_modules");

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

            using (new CustomSettingsGUIScope())
            {
                EditorGUILayout.PropertyField(m_propertyName);

                m_list.DoLayoutList();
            }

            serializedObject.ApplyModifiedProperties();

            if (m_editors.Count > 0)
            {
                using (new CustomSettingsGUIScope())
                {
                    DrawEditors();
                }
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
            SerializedProperty propertyBuilder = propertyElement.FindPropertyRelative("m_builder");

            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float heightActive = EditorGUI.GetPropertyHeight(propertyActive);
            float heightBuilder = EditorGUI.GetPropertyHeight(propertyBuilder);

            rect.y += spacing;

            var rectActive = new Rect(rect.x, rect.y, 15F, heightActive);
            var rectBuilder = new Rect(rectActive.xMax + spacing, rect.y, rect.width - rectActive.width - spacing, heightBuilder);

            propertyActive.boolValue = EditorGUI.ToggleLeft(rectActive, GUIContent.none, propertyActive.boolValue);

            EditorGUI.PropertyField(rectBuilder, propertyBuilder, GUIContent.none);
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
            SerializedProperty propertyBuilder = propertyElement.FindPropertyRelative("m_builder");

            propertyActive.boolValue = true;
            propertyBuilder.objectReferenceValue = null;

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
            SerializedProperty propertyBuilder = propertyElement.FindPropertyRelative("m_builder");

            if (propertyBuilder.objectReferenceValue != null)
            {
                UnityEditor.Editor editor = CreateEditor(propertyBuilder.objectReferenceValue);

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
