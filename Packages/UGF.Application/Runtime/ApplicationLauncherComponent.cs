using System;
using System.Threading.Tasks;
using UGF.Application.Runtime.Scenes;
using UnityEngine;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application Launcher", 2000)]
    public class ApplicationLauncherComponent : MonoBehaviour
    {
        [SerializeField] private ApplicationBuilderComponent m_builder;
        [SerializeField] private ApplicationLauncherResourceLoader m_resourceLoader;
        [SerializeField] private bool m_launchOnStart = true;
        [SerializeField] private bool m_stopOnQuit = true;
        [SerializeField] private bool m_sceneAccess = true;

        public ApplicationBuilderComponent Builder { get { return m_builder; } set { m_builder = value; } }
        public ApplicationLauncherResourceLoader ResourceLoader { get { return m_resourceLoader; } set { m_resourceLoader = value; } }
        public bool LaunchOnStart { get { return m_launchOnStart; } set { m_launchOnStart = value; } }
        public bool StopOnQuit { get { return m_stopOnQuit; } set { m_stopOnQuit = value; } }
        public bool SceneAccess { get { return m_sceneAccess; } set { m_sceneAccess = value; } }
        public IApplicationLauncher Launcher { get { return m_launcher ?? throw new InvalidOperationException("Application Launcher not exists."); } }
        public bool HasLauncher { get { return m_launcher != null; } }

        public event ApplicationHandler Launched;
        public event ApplicationHandler Stopped;

        private IApplicationLauncher m_launcher;

        public async void Launch()
        {
            await LaunchAsync();
        }

        public async Task LaunchAsync()
        {
            await Launcher.Launch();

            if (SceneAccess)
            {
                OnRegisterAtScene(Launcher.Application);
            }
        }

        public void Stop()
        {
            if (Launcher.IsLaunched)
            {
                if (SceneAccess)
                {
                    OnUnregisterAtScene(Launcher.Application);
                }

                Launcher.Stop();
            }
        }

        protected virtual IApplicationLauncher OnCreateLauncher(IApplicationBuilder builder, IApplicationLauncherResourceLoader resourceLoader)
        {
            return new ApplicationLauncherDefault(builder, resourceLoader);
        }

        protected virtual void OnRegisterAtScene(IApplication application)
        {
            ApplicationSceneProviderInstance.Provider.Add(gameObject.scene, application);
        }

        protected virtual void OnUnregisterAtScene(IApplication application)
        {
            ApplicationSceneProviderInstance.Provider.Remove(gameObject.scene);
        }

        private void Start()
        {
            if (m_builder == null) throw new ArgumentNullException(nameof(m_builder), "Value can not be null.");
            if (m_resourceLoader == null) throw new ArgumentNullException(nameof(m_builder), "Value can not be null.");

            m_launcher = OnCreateLauncher(m_builder, m_resourceLoader);
            m_launcher.Launched += Launched;
            m_launcher.Stopped += Stopped;

            if (m_launchOnStart)
            {
                Launch();
            }
        }

        private void OnDestroy()
        {
            Stop();
        }

        private void OnApplicationQuit()
        {
            if (m_stopOnQuit)
            {
                Stop();
            }
        }
    }
}
