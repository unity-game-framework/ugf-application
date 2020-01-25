namespace UGF.Application.Runtime
{
    public class ApplicationConfigLauncher : ApplicationUnityLauncher
    {
        protected override IApplication CreateApplication(IApplicationResources resources)
        {
            return new ApplicationConfigured(resources, ProvideStaticInstance);
        }
    }
}
