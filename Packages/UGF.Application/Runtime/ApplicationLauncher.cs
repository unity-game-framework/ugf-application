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
        private InitializeState m_launchState = new InitializeState();

        private IEnumerator Start()
        {
            yield return Launch();
        }

        /// <summary>
        /// Launch application creation and initialization.
        /// <para>This method called from start, if component is active and enabled.</para>
        /// <para>This method can be called only once.</para>
        /// </summary>
        public IEnumerator Launch()
        {
            m_launchState.Initialize();

            OnLaunch();

            yield return PreloadResourcesAsync();

            IApplication application = CreateApplication();

            if (application == null)
            {
                throw new ArgumentNullException(nameof(application), "Result of application creation is null.");
            }

            InitializeApplication(application);

            yield return InitializeModulesAsync(application);

            OnLaunched(application);
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
    }
}
