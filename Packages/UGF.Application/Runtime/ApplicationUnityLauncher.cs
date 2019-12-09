using UnityEngine;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents <see cref="ApplicationUnity"/> launcher.
    /// </summary>
    public class ApplicationUnityLauncher : ApplicationLauncher
    {
        [SerializeField] private bool m_provideStaticInstance = true;

        /// <summary>
        /// Gets or sets the value that determines whether application provide static instance via <see cref="ApplicationInstance"/>.
        /// </summary>
        public bool ProvideStaticInstance { get { return m_provideStaticInstance; } set { m_provideStaticInstance = value; } }

        protected override IApplication CreateApplication()
        {
            return new ApplicationUnity(m_provideStaticInstance);
        }
    }
}
