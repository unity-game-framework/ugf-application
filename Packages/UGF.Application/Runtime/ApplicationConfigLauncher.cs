using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public class ApplicationConfigLauncher : ApplicationUnityLauncher
    {
        [SerializeField] private ApplicationConfigLoader m_configLoader;
        [SerializeField] private bool m_requestScriptReloadOnQuit;

        public ApplicationConfigLoader ConfigLoader { get { return m_configLoader; } set { m_configLoader = value; } }
        public bool RequestScriptReloadOnQuit { get { return m_requestScriptReloadOnQuit; } set { m_requestScriptReloadOnQuit = value; } }
        public IApplicationConfig Config { get { return m_config ?? throw new ArgumentNullException(nameof(m_config), "Config not loaded."); } }
        public bool HasConfig { get { return m_config != null; } }

        private IApplicationConfig m_config;

        protected override async Task PreloadResourcesAsync()
        {
            if (m_configLoader == null) throw new ArgumentNullException(nameof(m_configLoader), "A config loader not specified.");

            m_config = await m_configLoader.Load();
        }

        protected override IApplication CreateApplication()
        {
            return new ApplicationConfigured(m_config, ProvideStaticInstance);
        }

        protected override void OnStopped()
        {
            base.OnStopped();

            m_config = null;
        }

#if UNITY_EDITOR
        protected override void OnQuitting()
        {
            base.OnQuitting();

            if (m_requestScriptReloadOnQuit)
            {
                UnityEditor.EditorUtility.RequestScriptReload();
            }
        }
#endif
    }
}
