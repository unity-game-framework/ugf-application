using UnityEngine;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application Configured Launcher")]
    public class ApplicationConfiguredLauncher : ApplicationOrderedLauncher
    {
        protected override IApplication OnCreateApplication(IApplicationResources resources)
        {
            return new ApplicationConfigured(resources, UseReverseModulesUninitialization);
        }
    }
}
