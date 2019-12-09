using System;
using UGF.Application.Runtime;

namespace UGF.Module.Runtime
{
    public interface IModuleBuilder
    {
        Type RegisterType { get; }

        IApplicationModule Build(IApplication application, IModuleBuildArguments<object> arguments);
    }
}
