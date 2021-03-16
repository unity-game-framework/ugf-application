using System;
using System.Threading.Tasks;
using UGF.Initialize.Runtime;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application Launcher", 2000)]
    public class ApplicationLauncherComponent : MonoBehaviour, IInitialize
    {
        [SerializeField] private ApplicationBuilderComponent m_builder;
        [SerializeField] private ApplicationLauncherResourceLoaderComponent m_resourceLoader;
        [SerializeField] private bool m_launchOnStart = true;
        [SerializeField] private bool m_stopOnQuit = true;
        [SerializeField] private bool m_sceneAccess = true;

        public ApplicationBuilderComponent Builder { get { return m_builder; } set { m_builder = value; } }
        public ApplicationLauncherResourceLoaderComponent ResourceLoader { get { return m_resourceLoader; } set { m_resourceLoader = value; } }
        public bool LaunchOnStart { get { return m_launchOnStart; } set { m_launchOnStart = value; } }
        public bool StopOnQuit { get { return m_stopOnQuit; } set { m_stopOnQuit = value; } }
        public bool SceneAccess { get { return m_sceneAccess; } set { m_sceneAccess = value; } }
        public IApplicationLauncher Launcher { get { return m_launcher ?? throw new InvalidOperationException("Application Launcher not exists."); } }
        public bool HasLauncher { get { return m_launcher != null; } }
        public bool IsInitialized { get { return m_state; } }

        public event InitializeHandler Initialized;
        public event InitializeHandler Uninitialized;
        public event ApplicationHandler Launched;
        public event ApplicationHandler Stopped;

        private InitializeState m_state;
        private IApplicationLauncher m_launcher;

        public void Initialize()
        {
            Log.Debug("Launcher initialization", new
            {
                launcher = this,
                scene = gameObject.scene.name,
                LaunchOnStart,
                StopOnQuit,
                SceneAccess
            });

            m_state = m_state.Initialize();

            m_launcher = OnCreateLauncher(m_builder, m_resourceLoader);
            m_launcher.Initialize();

            Initialized?.Invoke(this);
        }

        public void Uninitialize()
        {
            m_state = m_state.Uninitialize();

            if (Launcher.IsLaunched)
            {
                Stop();
            }

            m_launcher.Uninitialize();

            Uninitialized?.Invoke(this);

            Log.Debug("Launcher uninitialized", new
            {
                launcher = this,
                scene = gameObject.scene.name,
            });
        }

        public async void Launch()
        {
            await LaunchAsync();
        }

        public async Task LaunchAsync()
        {
            await Launcher.Launch();

            if (m_sceneAccess)
            {
                OnRegisterAtScene(Launcher.Application);
            }
        }

        public void Stop()
        {
            if (m_sceneAccess)
            {
                OnUnregisterAtScene(Launcher.Application);
            }

            Launcher.Stop();
        }

        protected virtual IApplicationLauncher OnCreateLauncher(IApplicationBuilder builder, IApplicationLauncherResourceLoader resourceLoader)
        {
            var launcher = new ApplicationLauncher(builder, resourceLoader);

            launcher.Launched += Launched;
            launcher.Stopped += Stopped;

            return launcher;
        }

        protected virtual void OnRegisterAtScene(IApplication application)
        {
            if (ProviderInstance.TryGet(out IProvider<Scene, IApplication> provider))
            {
                provider.Add(gameObject.scene, application);

                Log.Debug("Launcher register application at scene", new
                {
                    application,
                    launcher = this,
                    scene = gameObject.scene.name,
                });
            }
        }

        protected virtual void OnUnregisterAtScene(IApplication application)
        {
            if (ProviderInstance.TryGet(out IProvider<Scene, IApplication> provider))
            {
                provider.Remove(gameObject.scene);

                Log.Debug("Launcher unregister application at scene", new
                {
                    application,
                    launcher = this,
                    scene = gameObject.scene.name,
                });
            }
        }

        private void Start()
        {
            if (m_launchOnStart)
            {
                Initialize();
                Launch();
            }
        }

        private void OnDestroy()
        {
            if (IsInitialized)
            {
                Uninitialize();
            }
        }

        private void OnApplicationQuit()
        {
            if (m_stopOnQuit)
            {
                Log.Debug("Launcher stop on application quit", new
                {
                    launcher = this,
                    scene = gameObject.scene.name,
                });

                if (IsInitialized)
                {
                    Uninitialize();
                }
            }
        }
    }
}
