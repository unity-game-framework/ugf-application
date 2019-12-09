using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Kernel.Runtime.Loaders;
using UGF.Logs.Runtime;
using UnityEngine;

namespace UGF.Kernel.Runtime
{
    public class KernelLauncher : ApplicationUnityLauncher
    {
        [SerializeField] private KernelConfigLoader m_configLoader;
        [SerializeField] private bool m_requestScriptReloadOnQuit;

        public IKernelConfigLoader ConfigLoader
        {
            get
            {
                if (m_configLoader == null)
                {
                    throw new ArgumentNullException(nameof(m_configLoader), "The config loader not specified.");
                }

                return m_configLoader;
            }
            set { m_configLoader = (KernelConfigLoader)value; }
        }

        public bool RequestScriptReloadOnQuit { get { return m_requestScriptReloadOnQuit; } set { m_requestScriptReloadOnQuit = value; } }

        public IKernelConfig Config
        {
            get
            {
                if (m_config == null)
                {
                    throw new InvalidOperationException("The Config not loaded.");
                }

                return m_config;
            }
        }

        public bool HasConfig { get { return m_config != null; } }

        private IKernelConfig m_config;

        protected override async Task PreloadResourcesAsync()
        {
            m_config = await ConfigLoader.Load();

            Log.Debug($"Config:'{m_config}', name:'{m_config.Name}', modules:'{m_config.Modules.Count}'.");
        }

        protected override IApplication CreateApplication()
        {
            IApplication application = new KernelApplication(Config, ProvideStaticInstance);

            Log.Debug($"Application: '{application}'.");

            return application;
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
