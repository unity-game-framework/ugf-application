using System;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleDescribedAsset<TModule, TDescription> : ApplicationModuleAsset<TModule>, IApplicationModuleDescriptionAsset
        where TModule : class, IApplicationModuleDescribed
        where TDescription : class, IApplicationModuleDescription
    {
        public T GetGetDescription<T>(IApplication application) where T : class, IApplicationModuleDescription
        {
            IApplicationModuleDescription description = GetDescription(application);

            return (T)description;
        }

        public TDescription GetDescription(IApplication application)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));

            return OnGetDescription(application);
        }

        protected override TModule OnBuildTyped(IApplication application)
        {
            TDescription description = GetDescription(application);
            TModule module = OnBuild(application, description);

            return module;
        }

        protected abstract TDescription OnGetDescription(IApplication application);
        protected abstract TModule OnBuild(IApplication application, TDescription description);

        IApplicationModuleDescription IApplicationModuleDescriptionAsset.GetDescription(IApplication application)
        {
            return GetDescription(application);
        }
    }
}
