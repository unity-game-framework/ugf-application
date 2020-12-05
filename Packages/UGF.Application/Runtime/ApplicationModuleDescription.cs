using System;

namespace UGF.Application.Runtime
{
    public class ApplicationModuleDescription : IApplicationModuleDescription
    {
        public Type RegisterType { get; }

        public ApplicationModuleDescription(Type registerType)
        {
            RegisterType = registerType ?? throw new ArgumentNullException(nameof(registerType));
        }
    }
}
