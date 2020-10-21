namespace UGF.Application.Runtime
{
    public class ApplicationConfiguredLauncher : ApplicationOrderedLauncher
    {
        protected override IApplication OnCreateApplication(IApplicationResources resources)
        {
            return new ApplicationConfigured(resources, UseReverseModulesUninitialization);
        }
    }
}
