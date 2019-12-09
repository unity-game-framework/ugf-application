using System;

namespace UGF.Application.Runtime
{
    public interface IApplicationModuleInfo
    {
        Type RegisterType { get; }
        IApplicationModuleBuilder Builder { get; }
    }
}
