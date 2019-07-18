using System;
using UnityApplication = UnityEngine.Application;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an unity application implementation.
    /// </summary>
    public class ApplicationUnity : ApplicationBase
    {
        /// <summary>
        /// Gets the value that determines whether application subscribed to Unity application quitting event to uninitialize it self.
        /// </summary>
        public bool UninitializeOnUnityQuitting { get; }

        /// <summary>
        /// Gets the value that determines whether application provide static instance via <see cref="ApplicationInstance"/>.
        /// </summary>
        public bool ProvideStaticInstance { get; }

        /// <summary>
        /// Creates application with specified arguments.
        /// </summary>
        /// <param name="uninitializeOnUnityQuitting">The value that determines whether to subscribe to Unity quitting event ot uninitialize it self.</param>
        /// <param name="provideStaticInstance">The value that determines whether to provide static instance via <see cref="ApplicationInstance"/>.</param>
        public ApplicationUnity(bool uninitializeOnUnityQuitting = true, bool provideStaticInstance = true)
        {
            UninitializeOnUnityQuitting = uninitializeOnUnityQuitting;
            ProvideStaticInstance = provideStaticInstance;
        }

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

        /// <summary>
        /// Invoked when Unity application performs quitting.
        /// </summary>
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
