using UGF.Application.Runtime;
using UnityEditor;

namespace UGF.Application.Editor
{
    [CustomEditor(typeof(ApplicationModuleAsset), true)]
    public class ApplicationModuleAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            OnDrawModuleInfo();
        }

        protected virtual void OnDrawModuleInfo()
        {
            DrawModuleRegisterTypeInfo();
        }

        protected void DrawModuleRegisterTypeInfo()
        {
            var asset = (ApplicationModuleAsset)target;

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox($"Register Type: '{asset.RegisterType}'.", MessageType.Info);
        }
    }
}
