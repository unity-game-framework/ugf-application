using System;
using UnityApplication = UnityEngine.Application;

namespace UGF.Application.Runtime
{
    public class ApplicationUnity : ApplicationBase
    {
        public bool UninitializeOnUnityQuitting { get; set; } = true;
        public bool ProvideStaticInstance { get; set; } = true;

        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();

            if (UninitializeOnUnityQuitting)
            {
                UnityApplication.quitting += OnUnityApplicationQuitting;
            }

            if (ProvideStaticInstance)
            {
                if (ApplicationInstance.HasApplication)
                {
                    throw new InvalidOperationException("The Application static instance has already assigned.");
                }

                ApplicationInstance.Application = this;
            }
        }

        protected override void OnPostUninitialize()
        {
            base.OnPostUninitialize();

            if (UninitializeOnUnityQuitting)
            {
                UnityApplication.quitting -= OnUnityApplicationQuitting;
            }

            if (ProvideStaticInstance)
            {
                ApplicationInstance.Application = null;
            }
        }

        protected virtual void OnUnityQuitting()
        {
        }

        private void OnUnityApplicationQuitting()
        {
            OnUnityQuitting();

            Uninitialize();
        }
    }
}
