using UGF.Application.Runtime;

namespace UGF.Module.Runtime
{
    public delegate IApplicationModule ModuleBuilderHandler(IApplication application, IModuleBuildArguments<object> arguments);
}
