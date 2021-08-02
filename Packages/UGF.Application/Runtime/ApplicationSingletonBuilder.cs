namespace UGF.Application.Runtime
{
    public class ApplicationSingletonBuilder : ApplicationConfiguredBuilder
    {
        public bool ProvideStaticInstance { get; set; } = true;

        protected override IApplication OnBuild(IApplicationResources arguments)
        {
            return new ApplicationSingleton(arguments, UseReverseModulesUninitialization, ProvideStaticInstance);
        }
    }
}
