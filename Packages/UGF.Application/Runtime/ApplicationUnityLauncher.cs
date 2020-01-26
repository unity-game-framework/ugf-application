using UnityEngine;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents <see cref="ApplicationUnity"/> launcher.
    /// </summary>
    public class ApplicationUnityLauncher : ApplicationLauncher
    {
        [SerializeField] private bool m_provideStaticInstance = true;
        [SerializeField] private bool m_requestScriptReloadOnQuit;

        /// <summary>
        /// Gets or sets the value that determines whether application provide static instance via <see cref="ApplicationInstance"/>.
        /// </summary>
        public bool ProvideStaticInstance { get { return m_provideStaticInstance; } set { m_provideStaticInstance = value; } }

        public bool RequestScriptReloadOnQuit { get { return m_requestScriptReloadOnQuit; } set { m_requestScriptReloadOnQuit = value; } }

        protected override IApplication CreateApplication(IApplicationResources resources)
        {
            return new ApplicationUnity(resources, m_provideStaticInstance);
        }

#if UNITY_EDITOR
        protected override void OnQuitting(IApplication application)
        {
            base.OnQuitting(application);

            if (m_requestScriptReloadOnQuit)
            {
                UnityEditor.EditorUtility.RequestScriptReload();
            }
        }
#endif
    }
}
