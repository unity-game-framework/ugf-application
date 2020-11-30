namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleAsset<TModule, TDescription> : ApplicationModuleAsset
        where TModule : class, IApplicationModule
        where TDescription : class, IApplicationModuleDescription
    {
        protected override IApplicationModule OnBuild(IApplication arguments, IApplicationModuleDescription description)
        {
            return OnBuild((TDescription)description, arguments);
        }

        protected abstract TModule OnBuild(TDescription description, IApplication application);
    }
}
