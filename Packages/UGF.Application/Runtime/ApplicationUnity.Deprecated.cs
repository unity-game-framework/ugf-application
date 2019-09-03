using System;

namespace UGF.Application.Runtime
{
    public partial class ApplicationUnity
    {
        /// <summary>
        /// Gets the value that determines whether application subscribed to Unity application quitting event to uninitialize itself.
        /// </summary>
        [Obsolete("UninitializeOnUnityQuitting has been deprecated.")]
        public bool UninitializeOnUnityQuitting { get; } = false;

        /// <summary>
        /// Creates application with specified arguments.
        /// </summary>
        /// <param name="uninitializeOnUnityQuitting">The value that determines whether to subscribe to Unity quitting event ot uninitialize it self.</param>
        /// <param name="provideStaticInstance">The value that determines whether to provide static instance via <see cref="ApplicationInstance"/>.</param>
        [Obsolete("ApplicationUnity constructor with uninitializeOnUnityQuitting argument has been deprecated. Use another overload instead.")]
        public ApplicationUnity(bool uninitializeOnUnityQuitting = true, bool provideStaticInstance = true)
        {
            ProvideStaticInstance = provideStaticInstance;
        }

        /// <summary>
        /// Invoked when Unity application performs quitting.
        /// </summary>
        [Obsolete("OnUnityQuitting has been deprecated. Use ApplicationUnityLauncher.OnQuitting instead.")]
        protected virtual void OnUnityQuitting()
        {
        }
    }
}
