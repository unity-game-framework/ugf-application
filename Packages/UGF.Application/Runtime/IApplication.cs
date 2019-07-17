using System;
using System.Collections.Generic;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    public interface IApplication : IInitialize
    {
        IReadOnlyDictionary<Type, IApplicationModule> Modules { get; }

        void AddModule(Type registerType, IApplicationModule module);
        bool RemoveModule(Type registerType);
        void ClearModules();
        T GetModule<T>() where T : IApplicationModule;
        bool TryGetModule<T>(out T module) where T : IApplicationModule;
        Type GetRegisterType(IApplicationModule module);
        bool TryGetRegisterType(IApplicationModule module, out Type registerType);
    }
}
