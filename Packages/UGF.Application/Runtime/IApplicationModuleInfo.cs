using System;

namespace UGF.Application.Runtime
{
    public interface IApplicationModuleInfo
    {
        Type RegisterType { get; }
        ApplicationModuleBuildHandler Builder { get; }
    }
}
