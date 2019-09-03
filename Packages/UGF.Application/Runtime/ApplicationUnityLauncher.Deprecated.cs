using System;

namespace UGF.Application.Runtime
{
    public partial class ApplicationUnityLauncher
    {
        /// <summary>
        /// Gets or sets the value that determines whether application subscribed to Unity application quitting event to uninitialize itself.
        /// </summary>
        [Obsolete("UninitializeOnUnityQuitting has been deprecated. Use StopOnQuit instead.")]
        public bool UninitializeOnUnityQuitting
        {
            get { return false; }
            // ReSharper disable once ValueParameterNotUsed
            set { }
        }
    }
}
