using System;
using System.Threading.Tasks;
using UGF.Initialize.Runtime;
using UnityEngine;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents a <see cref="MonoBehaviour"/> that create and initialize application.
    /// </summary>
    public abstract class ApplicationLauncher : MonoBehaviour
    {
        [SerializeField] private bool m_launchOnStart = true;
        [SerializeField] private bool m_stopOnQuit = true;
        [SerializeField] private ApplicationLauncherResourceLoader m_resourceLoader;

        /// <summary>
        /// Gets or sets value that determines whether to launch application on start.
        /// </summary>
        public bool LaunchOnStart { get { return m_launchOnStart; } set { m_launchOnStart = value; } }

        /// <summary>
        /// Gets or sets value that determines whether to stop application on Unity Application quit.
        /// </summary>
        public bool StopOnQuit { get { return m_stopOnQuit; } set { m_stopOnQuit = value; } }

        public ApplicationLauncherResourceLoader ResourceLoader { get { return m_resourceLoader; } set { m_resourceLoader = value; } }

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
        /// Triggered after launcher is completely stopped and application no longer available.
        /// </summary>
        public event Action Stopped;

        /// <summary>
        /// Triggered when Unity application performs quitting.
        /// </summary>
        public event Action Quitting;

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
            OnQuitting();

            Quitting?.Invoke();

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
            if (m_resourceLoader == null) throw new ArgumentNullException(nameof(m_resourceLoader), "Resource loader must be specified.");

            m_state = m_state.Initialize();

            OnLaunch();

            IApplicationResources resources = await m_resourceLoader.LoadAsync() ?? throw new ArgumentNullException(nameof(resources), "Resources not loaded.");
            IApplication application = CreateApplication(resources) ?? throw new ArgumentNullException(nameof(application), "Application not created.");

            InitializeApplication(application);

            await application.InitializeAsync();

            m_application = application;

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

            OnStop(application);

            UninitializeApplication(application);

            m_application = null;

            OnStopped();

            Stopped?.Invoke();
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
        /// <param name="resources"></param>
        protected abstract IApplication CreateApplication(IApplicationResources resources);

        /// <summary>
        /// Invoked after application creation.
        /// </summary>
        /// <param name="application">The application.</param>
        protected virtual void InitializeApplication(IApplication application)
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

        /// <summary>
        /// Invoked after all launch completed and application becomes available.
        /// </summary>
        /// <param name="application">The application.</param>
        protected virtual void OnLaunched(IApplication application)
        {
        }

        /// <summary>
        /// Invoked right at the start of stopping launcher.
        /// </summary>
        /// <param name="application">The application.</param>
        protected virtual void OnStop(IApplication application)
        {
        }

        /// <summary>
        /// Invoked after launcher is completely stopped and application no longer available.
        /// </summary>
        protected virtual void OnStopped()
        {
        }

        /// <summary>
        /// Invoked when Unity application performs quitting.
        /// </summary>
        protected virtual void OnQuitting()
        {
        }
    }
}
