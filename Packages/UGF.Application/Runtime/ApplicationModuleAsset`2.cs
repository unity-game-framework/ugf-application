using System;
using UGF.Builder.Runtime;
using UGF.Description.Runtime;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleAsset<TModule, TDescription> : ApplicationModuleAsset, IDescribedBuilder<IApplication>, IDescriptionBuilder
        where TModule : class, IApplicationModule
        where TDescription : class, IDescription
    {
        protected override IApplicationModule OnBuild(IApplication arguments)
        {
            TDescription description = OnBuildDescription();

            if (description == null) throw new ArgumentNullException(nameof(description));

            return OnBuild(description, arguments);
        }

        protected abstract TDescription OnBuildDescription();
        protected abstract TModule OnBuild(TDescription description, IApplication application);

        T IBuilder<IApplication, IDescribed>.Build<T>(IApplication arguments)
        {
            return (T)Build(arguments);
        }

        IDescribed IBuilder<IApplication, IDescribed>.Build(IApplication arguments)
        {
            return (IDescribed)Build(arguments);
        }

        T IBuilder<IDescription>.Build<T>()
        {
            return (T)(object)OnBuildDescription();
        }

        IDescription IBuilder<IDescription>.Build()
        {
            return OnBuildDescription();
        }
    }
}
