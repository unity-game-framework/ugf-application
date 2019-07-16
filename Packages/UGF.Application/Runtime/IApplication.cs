using System;
using System.Collections.Generic;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    public interface IApplication : IInitialize
    {
        IReadOnlyDictionary<Type, IApplicationModule> Modules { get; }

        void AddModule(IApplicationModule module);
        bool RemoveModule(Type moduleRegisterType);
        void ClearModules();
        T GetModule<T>() where T : IApplicationModule;
        bool TryGetModule<T>(out T module) where T : IApplicationModule;
    }
}
