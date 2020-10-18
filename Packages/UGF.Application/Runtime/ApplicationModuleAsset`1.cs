using System;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleAsset<TModule> : ApplicationModuleAsset where TModule : class, IApplicationModule
    {
        public override Type RegisterType { get; } = typeof(TModule);

        protected override IApplicationModule OnBuild(IApplication application)
        {
            return OnBuildTyped(application);
        }

        protected abstract TModule OnBuildTyped(IApplication application);
    }
}
