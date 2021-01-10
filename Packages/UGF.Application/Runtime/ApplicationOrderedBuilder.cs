namespace UGF.Application.Runtime
{
    public class ApplicationOrderedBuilder : ApplicationBuilder
    {
        public bool UseReverseModulesUninitialization { get; set; } = true;

        protected override IApplication OnBuild(IApplicationResources arguments)
        {
            return new ApplicationOrdered(arguments, UseReverseModulesUninitialization);
        }
    }
}
