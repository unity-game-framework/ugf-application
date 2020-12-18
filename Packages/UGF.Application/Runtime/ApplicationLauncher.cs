using System;
using System.Threading.Tasks;
using UGF.Application.Runtime.Scenes;
using UGF.Initialize.Runtime;
using UnityEngine;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents component which load, create and initialize application.
    /// </summary>
    public abstract class ApplicationLauncher : MonoBehaviour
    {
        [SerializeField] private ApplicationLauncherResourceLoader m_resourceLoader;
        [SerializeField] private bool m_launchOnStart = true;
        [SerializeField] private bool m_stopOnQuit = true;
        [SerializeField] private bool m_sceneAccess = true;

        public ApplicationLauncherResourceLoader ResourceLoader { get { return m_resourceLoader; } set { m_resourceLoader = value; } }

        /// <summary>
        /// Gets or sets value that determines whether to launch application on start.
        /// </summary>
        public bool LaunchOnStart { get { return m_launchOnStart; } set { m_launchOnStart = value; } }

        /// <summary>
        /// Gets or sets value that determines whether to stop application on Unity Application quit.
        /// </summary>
        public bool StopOnQuit { get { return m_stopOnQuit; } set { m_stopOnQuit = value; } }

        /// <summary>
        /// Gets or sets value that determines whether to provide access to created application using Launcher scene.
        /// </summary>
        public bool SceneAccess { get { return m_sceneAccess; } set { m_sceneAccess = value; } }

        /// <summary>
        /// Gets value that determines whether launcher started already.
        /// </summary>
        /// <remarks>
        /// This property becomes 'true' after launch immediately and stays 'true' until launcher will be called to stop.
        /// </remarks>
        public bool IsLaunched { get { return m_state; } }

        /// <summary>
        /// Gets an instance of the application.
        /// </summary>
        /// <remarks>
        /// Use 'HasApplication' property to determine whether application is available.
        /// </remarks>
        public IApplication Application { get { return m_application ?? throw new InvalidOperationException("The application is not created."); } }

        /// <summary>
        /// Gets the value that determines whether instance of the application is created and launch completed.
        /// </summary>
        /// <remarks>
        /// You can use this property to determine whether launch is complete.
        /// By itself application created during launch, but becomes available only after launch complete.
        /// </remarks>
        public bool HasApplication { get { return m_application != null; } }

        /// <summary>
        /// Triggered after all launch completed and application becomes available.
        /// </summary>
        public event ApplicationHandler Launched;

        /// <summary>
        /// Triggered when launcher performs stop.
        /// </summary>
        public event ApplicationHandler Stopped;

        /// <summary>
        /// Triggered when Unity application performs quitting.
        /// </summary>
        public event ApplicationHandler Quitting;

        private InitializeState m_state;
        private IApplication m_application;

        private async void Start()
        {
            if (m_launchOnStart)
            {
                await Launch();
            }
        }

        private void OnDestroy()
        {
            if (m_state)
            {
                Stop();
            }
        }

        private void OnApplicationQuit()
        {
            if (HasApplication)
            {
                IApplication application = Application;

                OnQuitting(application);

                Quitting?.Invoke(application);
            }

            if (m_stopOnQuit && m_state)
            {
                Stop();
            }
        }

        /// <summary>
        /// Launch application creation and initialization.
        /// </summary>
        public async Task Launch()
        {
            m_state = m_state.Initialize();

            OnLaunch();

            IApplicationResources resources;

            if (m_resourceLoader != null)
            {
                resources = await m_resourceLoader.LoadAsync() ?? throw new ArgumentNullException(nameof(resources), "Resources not loaded.");
            }
            else
            {
                resources = new ApplicationResources();
            }

            IApplication application = OnCreateApplication(resources) ?? throw new ArgumentNullException(nameof(application), "Application not created.");

            OnInitializeApplication(application);

            await application.InitializeAsync();

            m_application = application;

            if (m_sceneAccess)
            {
                OnRegisterAtScene(application);
            }

            OnLaunched(application);

            Launched?.Invoke(application);
        }

        /// <summary>
        /// Stops and uninitialize created application.
        /// </summary>
        public void Stop()
        {
            m_state = m_state.Uninitialize();

            IApplication application = Application;

            if (m_sceneAccess)
            {
                OnUnregisterAtScene(application);
            }

            OnStopped(application);

            Stopped?.Invoke(application);

            UninitializeApplication(application);

            m_application = null;
        }

        /// <summary>
        /// Invoked right at the start of the launch.
        /// </summary>
        protected virtual void OnLaunch()
        {
        }

        /// <summary>
        /// Invoked after resources preload and ready to create application.
        /// </summary>
        /// <param name="resources">The application resources.</param>
        protected abstract IApplication OnCreateApplication(IApplicationResources resources);

        /// <summary>
        /// Invoked after application creation.
        /// </summary>
        /// <param name="application">The application.</param>
        protected virtual void OnInitializeApplication(IApplication application)
        {
            application.Initialize();
        }

        /// <summary>
        /// Invoked after launcher is stopped.
        /// </summary>
        /// <param name="application">The application.</param>
        protected virtual void UninitializeApplication(IApplication application)
        {
            application.Uninitialize();
        }

        protected virtual void OnRegisterAtScene(IApplication application)
        {
            ApplicationSceneProviderInstance.Provider.Add(gameObject.scene, application);
        }

        protected virtual void OnUnregisterAtScene(IApplication application)
        {
            ApplicationSceneProviderInstance.Provider.Remove(gameObject.scene);
        }

        /// <summary>
        /// Invoked after application creation and initialization.
        /// </summary>
        /// <param name="application">The active and initialized application.</param>
        protected virtual void OnLaunched(IApplication application)
        {
            if (application is IApplicationLauncherEventHandler handler)
            {
                handler.OnLaunched(application);
            }
        }

        /// <summary>
        /// Invoked before uninitialize and destroy application.
        /// </summary>
        /// <param name="application">The active and initialized application.</param>
        protected virtual void OnStopped(IApplication application)
        {
            if (application is IApplicationLauncherEventHandler handler)
            {
                handler.OnStopped(application);
            }
        }

        /// <summary>
        /// Invoked when Unity performs quitting and before active application stops.
        /// </summary>
        /// <param name="application">The active and initialized application.</param>
        protected virtual void OnQuitting(IApplication application)
        {
            if (application is IApplicationLauncherEventHandler handler)
            {
                handler.OnQuitting(application);
            }
        }
    }
}
