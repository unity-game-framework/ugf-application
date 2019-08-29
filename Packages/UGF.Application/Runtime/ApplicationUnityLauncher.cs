using UnityEngine;

namespace UGF.Application.Runtime
{
    public class ApplicationUnityLauncher : ApplicationLauncher
    {
        [SerializeField] private bool m_uninitializeOnUnityQuitting = true;
        [SerializeField] private bool m_provideStaticInstance = true;

        public bool UninitializeOnUnityQuitting { get { return m_uninitializeOnUnityQuitting; } set { m_uninitializeOnUnityQuitting = value; } }
        public bool ProvideStaticInstance { get { return m_provideStaticInstance; } set { m_provideStaticInstance = value; } }

        protected override IApplication CreateApplication()
        {
            return new ApplicationUnity(m_uninitializeOnUnityQuitting, m_provideStaticInstance);
        }
    }
}
