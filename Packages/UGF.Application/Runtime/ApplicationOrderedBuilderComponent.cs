using UnityEngine;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application Ordered Builder", 2000)]
    public class ApplicationOrderedBuilderComponent : ApplicationBuilderComponent
    {
        [SerializeField] private bool m_useReverseModulesUninitialization = true;

        public bool UseReverseModulesUninitialization { get { return m_useReverseModulesUninitialization; } set { m_useReverseModulesUninitialization = value; } }

        protected override IApplication OnBuild(IApplicationResources arguments)
        {
            return new ApplicationOrdered(arguments, m_useReverseModulesUninitialization);
        }
    }
}
