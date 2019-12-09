using System;

namespace UGF.Application.Runtime
{
    public class ApplicationModuleInfo : IApplicationModuleInfo
    {
        public Type RegisterType { get; }
        public IApplicationModuleBuilder Builder { get; }

        public ApplicationModuleInfo(Type registerType, ApplicationModuleBuildHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            RegisterType = registerType ?? throw new ArgumentNullException(nameof(registerType));
            Builder = new ApplicationModuleBuilder(handler);
        }

        public ApplicationModuleInfo(Type registerType, IApplicationModuleBuilder builder)
        {
            RegisterType = registerType ?? throw new ArgumentNullException(nameof(registerType));
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}
