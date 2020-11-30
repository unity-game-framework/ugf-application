namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleAsset<TModule> : ApplicationModuleAsset<TModule, ApplicationModuleDescription> where TModule : class, IApplicationModule
    {
        protected override IApplicationModuleDescription OnBuildDescription()
        {
            return new ApplicationModuleDescription(typeof(TModule));
        }
    }
}
