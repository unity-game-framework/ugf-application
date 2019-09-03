using System;
using UnityEngine;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents <see cref="ApplicationUnity"/> launcher.
    /// </summary>
    public class ApplicationUnityLauncher : ApplicationLauncher
    {
        [SerializeField] private bool m_uninitializeOnUnityQuitting = true;
        [SerializeField] private bool m_provideStaticInstance = true;

        /// <summary>
        /// Gets or sets the value that determines whether application subscribed to Unity application quitting event to uninitialize it self.
        /// </summary>
        public bool UninitializeOnUnityQuitting { get { return m_uninitializeOnUnityQuitting; } set { m_uninitializeOnUnityQuitting = value; } }

        /// <summary>
        /// Gets or sets the value that determines whether application provide static instance via <see cref="ApplicationInstance"/>.
        /// </summary>
        public bool ProvideStaticInstance { get { return m_provideStaticInstance; } set { m_provideStaticInstance = value; } }

        protected override IApplication CreateApplication()
        {
            IApplicationUnityEventHandler eventHandler = GetUnityEventHandler();

            if (eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler), "The Unity Application event handler cannot be null.");
            }

            return new ApplicationUnity(m_uninitializeOnUnityQuitting, m_provideStaticInstance, eventHandler);
        }

        /// <summary>
        /// Invoked to create Unity Application event handler used by application.
        /// </summary>
        protected virtual IApplicationUnityEventHandler GetUnityEventHandler()
        {
            return new ApplicationUnityEventHandler();
        }
    }
}
