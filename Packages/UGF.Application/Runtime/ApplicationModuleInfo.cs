using System;

namespace UGF.Application.Runtime
{
    public class ApplicationModuleInfo : IApplicationModuleInfo
    {
        public Type RegisterType { get; }
        public ApplicationModuleBuildHandler Builder { get; }

        public ApplicationModuleInfo(Type registerType, ApplicationModuleBuildHandler builder)
        {
            RegisterType = registerType ?? throw new ArgumentNullException(nameof(registerType));
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}
