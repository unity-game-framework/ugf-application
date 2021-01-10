using UnityEngine;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application Singleton Builder", 2000)]
    public class ApplicationSingletonBuilderComponent : ApplicationOrderedBuilderComponent
    {
        [SerializeField] private bool m_provideStaticInstance = true;

        public bool ProvideStaticInstance { get { return m_provideStaticInstance; } set { m_provideStaticInstance = value; } }

        protected override IApplication OnBuild(IApplicationResources arguments)
        {
            return new ApplicationSingleton(arguments, UseReverseModulesUninitialization, m_provideStaticInstance);
        }
    }
}
