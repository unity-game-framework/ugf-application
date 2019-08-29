using System;
using System.Collections;
using System.Collections.Generic;
using UGF.Initialize.Runtime;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationLauncher : MonoBehaviour
    {
        private InitializeState m_launchState = new InitializeState();

        private IEnumerator Start()
        {
            yield return Launch();
        }

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

        protected virtual void OnLaunch()
        {
        }

        protected virtual IEnumerator PreloadResourcesAsync()
        {
            yield break;
        }

        protected abstract IApplication CreateApplication();

        protected virtual void InitializeApplication(IApplication application)
        {
            application.Initialize();
        }

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

        protected virtual void OnLaunched(IApplication application)
        {
        }
    }
}
