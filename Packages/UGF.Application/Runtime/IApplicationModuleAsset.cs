using System;

namespace UGF.Application.Runtime
{
    public interface IApplicationModuleAsset
    {
        Type RegisterType { get; }

        T Build<T>(IApplication application) where T : class, IApplicationModule;
        IApplicationModule Build(IApplication application);
    }
}
