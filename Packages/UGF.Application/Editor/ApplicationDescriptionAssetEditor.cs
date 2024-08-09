using UGF.Application.Runtime;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationAsset), true)]
    internal class ApplicationDescriptionAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyProvideStaticInstance;
        private AssetIdReferenceListDrawer m_listModules;
        private ReorderableListSelectionDrawerByPathGlobalId m_listModulesSelection;

        private void OnEnable()
        {
            m_propertyProvideStaticInstance = serializedObject.FindProperty("m_provideStaticInstance");
            m_listModules = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_modules"));

            m_listModulesSelection = new ReorderableListSelectionDrawerByPathGlobalId(m_listModules, "m_guid")
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

                EditorGUILayout.PropertyField(m_propertyProvideStaticInstance);

                m_listModules.DrawGUILayout();
                m_listModulesSelection.DrawGUILayout();
            }
        }
    }
}
