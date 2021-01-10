using UGF.Builder.Runtime;
using UGF.Description.Runtime;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleBuilder<TModule, TDescription> : DescribedWithDescriptionBuilder<IApplication, TModule, TDescription>, IApplicationModuleBuilder
        where TModule : class, IApplicationModule
        where TDescription : class, IApplicationModuleDescription
    {
        protected override TModule OnBuild(IApplication arguments, TDescription description)
        {
            return OnBuild(description, arguments);
        }

        protected abstract TModule OnBuild(TDescription description, IApplication application);

        IApplicationModule IBuilder<IApplication, IApplicationModule>.Build(IApplication arguments)
        {
            return OnBuild(arguments);
        }
    }
}
