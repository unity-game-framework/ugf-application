using System;
using UGF.Application.Runtime;

namespace UGF.Module.Runtime
{
    public abstract class ModuleBuilderAsset<TRegisterType> : ModuleBuilderAsset where TRegisterType : IApplicationModule
    {
        public override Type RegisterType { get; } = typeof(TRegisterType);
    }
}
