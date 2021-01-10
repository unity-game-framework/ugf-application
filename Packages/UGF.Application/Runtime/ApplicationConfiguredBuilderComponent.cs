using UnityEngine;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application Configured Builder", 2000)]
    public class ApplicationConfiguredBuilderComponent : ApplicationOrderedBuilderComponent
    {
        protected override IApplication OnBuild(IApplicationResources arguments)
        {
            return new ApplicationConfigured(arguments, UseReverseModulesUninitialization);
        }
    }
}
