namespace UGF.Application.Runtime
{
    public class ApplicationConfigLauncher : ApplicationUnityLauncher
    {
        protected override IApplication CreateApplication(IApplicationResources resources)
        {
            var config = resources.Get<IApplicationConfig>();

            return new ApplicationConfigured(config, ProvideStaticInstance);
        }
    }
}
