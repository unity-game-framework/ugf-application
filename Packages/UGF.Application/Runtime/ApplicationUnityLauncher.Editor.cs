#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public partial class ApplicationUnityLauncher
    {
        [SerializeField] private bool m_requestScriptReloadOnQuit;

        /// <summary>
        /// Gets or sets the value that determines whether to force script reload after Unity Editor quitting play mode.
        /// </summary>
        /// <remarks>
        /// This property available only in Editor.
        /// </remarks>
        public bool RequestScriptReloadOnQuit { get { return m_requestScriptReloadOnQuit; } set { m_requestScriptReloadOnQuit = value; } }

        protected override void OnQuitting(IApplication application)
        {
            base.OnQuitting(application);

            if (m_requestScriptReloadOnQuit)
            {
                EditorUtility.RequestScriptReload();
            }
        }
    }
}
#endif
