namespace UGF.Application.Runtime
{
    public class ApplicationConfiguredBuilder : ApplicationOrderedBuilder
    {
        protected override IApplication OnBuild(IApplicationResources arguments)
        {
            return new ApplicationConfigured(arguments, UseReverseModulesUninitialization);
        }
    }
}
