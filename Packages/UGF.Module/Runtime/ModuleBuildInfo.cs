using System;

namespace UGF.Module.Runtime
{
    public class ModuleBuildInfo : IModuleBuildInfo
    {
        public bool Active { get; set; } = true;
        public IModuleBuilder Builder { get; }
        public IModuleBuildArguments<object> Arguments { get; }

        public ModuleBuildInfo(IModuleBuilder builder, IModuleBuildArguments<object> arguments)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }
    }
}
