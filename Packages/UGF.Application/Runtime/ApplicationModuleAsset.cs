using UGF.Description.Runtime;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleAsset : DescribedWithDescriptionBuilderAsset<IApplication, IApplicationModule, IApplicationModuleDescription>, IApplicationModuleBuilder
    {
    }
}
