using System;
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

        public event ApplicationHandler Quitting;

        private IApplicationLauncher m_launcher;

        private void Awake()
        {
            if (m_builder == null) throw new ArgumentNullException(nameof(m_builder), "Value can not be null.");
            if (m_resourceLoader == null) throw new ArgumentNullException(nameof(m_builder), "Value can not be null.");

            m_launcher = OnCreateLauncher(m_builder, m_resourceLoader);
        }

        private async void Start()
        {
            if (m_launchOnStart)
            {
                await Launcher.Launch();
            }
        }

        private void OnDestroy()
        {
            if (Launcher.IsLaunched)
            {
                Launcher.Stop();
            }
        }

        private void OnApplicationQuit()
        {
            if (Launcher.HasApplication)
            {
                IApplication application = Launcher.Application;

                OnQuitting(application);

                Quitting?.Invoke(application);
            }

            if (m_stopOnQuit && Launcher.IsLaunched)
            {
                Launcher.Stop();
            }
        }

        protected virtual IApplicationLauncher OnCreateLauncher(IApplicationBuilder builder, IApplicationLauncherResourceLoader resourceLoader)
        {
            return new ApplicationLauncherDefault(builder, resourceLoader);
        }

        protected virtual void OnQuitting(IApplication application)
        {
            if (application is IApplicationLauncherEventHandler handler)
            {
                handler.OnQuitting(application);
            }
        }
    }
}
