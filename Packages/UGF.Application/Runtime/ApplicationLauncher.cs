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

        public bool LaunchOnStart { get { return m_launchOnStart; } set { m_launchOnStart = value; } }
        public bool IsLaunched { get { return m_state.IsInitialized; } }
        public IApplication Application { get { return m_application ?? throw new InvalidOperationException("The application is not created."); } }
        public bool HasApplication { get { return m_application != null; } }

        private InitializeState m_state = new InitializeState();
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
            if (m_application != null && m_state.IsInitialized)
            {
                Stop();
            }
        }

        /// <summary>
        /// Launch application creation and initialization.
        /// <para>This method called from start, if component is active and enabled.</para>
        /// <para>This method can be called only once.</para>
        /// </summary>
        public IEnumerator Launch()
        {
            m_state.Initialize();

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

        public void Stop()
        {
            m_state.Uninitialize();

            OnStop(m_application);

            m_application.Uninitialize();
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
        /// Invoked after all launch completed.
        /// </summary>
        /// <param name="application">The application.</param>
        protected virtual void OnLaunched(IApplication application)
        {
        }

        protected virtual void OnStop(IApplication application)
        {
        }

        protected virtual void OnStopped()
        {
        }
    }
}
