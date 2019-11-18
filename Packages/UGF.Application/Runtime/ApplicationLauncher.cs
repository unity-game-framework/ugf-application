using System;
using System.Collections;
using System.Collections.Generic;
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

        /// <summary>
        /// Gets or sets value that determines whether to launch application on start.
        /// </summary>
        public bool LaunchOnStart { get { return m_launchOnStart; } set { m_launchOnStart = value; } }

        /// <summary>
        /// Gets or sets value that determines whether to stop application on Unity Application quit.
        /// </summary>
        public bool StopOnQuit { get { return m_stopOnQuit; } set { m_stopOnQuit = value; } }

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

        private InitializeState m_state;
        private IApplication m_application;

        private IEnumerator Start()
        {
            if (m_launchOnStart)
            {
                yield return Launch();
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

            if (m_stopOnQuit && m_state)
            {
                Stop();
            }
        }

        /// <summary>
        /// Launch application creation and initialization.
        /// </summary>
        public IEnumerator Launch()
        {
            m_state = m_state.Initialize();

            OnLaunch();

            yield return PreloadResourcesAsync();

            IApplication application = CreateApplication();

            if (application == null)
            {
                throw new ArgumentNullException(nameof(application), "Result of application creation is null.");
            }

            InitializeApplication(application);

            yield return InitializeModulesAsync(application);

            m_application = application;

            OnLaunched(application);
        }

        /// <summary>
        /// Stops and uninitialize created application.
        /// </summary>
        public void Stop()
        {
            m_state = m_state.Uninitialize();

            IApplication application = Application;

            OnStop(application);

            if (application.IsInitialized)
            {
                application.Uninitialize();
            }

            m_application = null;

            OnStopped();
        }

        /// <summary>
        /// Invoked right at the start of the launch.
        /// </summary>
        protected virtual void OnLaunch()
        {
        }

        /// <summary>
        /// Invoked before application creation.
        /// </summary>
        protected virtual IEnumerator PreloadResourcesAsync()
        {
            yield break;
        }

        /// <summary>
        /// Invoked after resources preload and ready to create application.
        /// </summary>
        protected abstract IApplication CreateApplication();

        /// <summary>
        /// Invoked after application creation.
        /// </summary>
        /// <param name="application">The application.</param>
        protected virtual void InitializeApplication(IApplication application)
        {
            application.Initialize();
        }

        /// <summary>
        /// Invoked after application and modules initialized.
        /// </summary>
        /// <param name="application">The application.</param>
        protected virtual IEnumerator InitializeModulesAsync(IApplication application)
        {
            foreach (KeyValuePair<Type, IApplicationModule> pair in application.Modules)
            {
                if (pair.Value is IInitializeAsync module)
                {
                    yield return module.InitializeAsync();
                }
            }
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
