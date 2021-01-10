using UnityEngine;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application Configured Launcher", 2000)]
    public class ApplicationConfiguredLauncher : ApplicationOrderedLauncher
    {
        protected override IApplication OnCreateApplication(IApplicationResources resources)
        {
            return new ApplicationConfigured(resources, UseReverseModulesUninitialization);
        }
    }
}
