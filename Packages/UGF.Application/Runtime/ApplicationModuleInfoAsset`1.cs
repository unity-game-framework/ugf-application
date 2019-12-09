namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleInfoAsset<TRegisterType> : ApplicationModuleInfoAsset
    {
        public override IApplicationModuleInfo GetInfo()
        {
            return new ApplicationModuleInfo(typeof(TRegisterType), OnBuild);
        }

        protected abstract IApplicationModule OnBuild(IApplication application);
    }
}
