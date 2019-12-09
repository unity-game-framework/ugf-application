using System;
using UGF.Application.Runtime;

namespace UGF.Module.Runtime
{
    public class ModuleBuilderDelegate<TRegisterType> : ModuleBuilder<TRegisterType> where TRegisterType : IApplicationModule
    {
        public ModuleBuilderHandler Handler { get; }

        public ModuleBuilderDelegate(ModuleBuilderHandler handler)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        protected override IApplicationModule OnBuild(IApplication application, IModuleBuildArguments<object> arguments)
        {
            return Handler(application, arguments);
        }
    }
}
