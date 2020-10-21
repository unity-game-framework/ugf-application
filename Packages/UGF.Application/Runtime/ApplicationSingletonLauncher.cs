using UnityEngine;

namespace UGF.Application.Runtime
{
    public class ApplicationSingletonLauncher : ApplicationConfiguredLauncher
    {
        [SerializeField] private bool m_provideStaticInstance = true;

        public bool ProvideStaticInstance { get { return m_provideStaticInstance; } set { m_provideStaticInstance = value; } }

        protected override IApplication OnCreateApplication(IApplicationResources resources)
        {
            return new ApplicationSingleton(resources, UseReverseModulesUninitialization, m_provideStaticInstance);
        }
    }
}
