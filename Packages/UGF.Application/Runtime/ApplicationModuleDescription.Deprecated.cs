using System;

namespace UGF.Application.Runtime
{
    public partial class ApplicationModuleDescription
    {
        [Obsolete("ApplicationModuleDescription constructor with 'registerType' argument has been deprecated. Use default constructor and properties initialization instead.")]
        public ApplicationModuleDescription(Type registerType)
        {
            RegisterType = registerType ?? throw new ArgumentNullException(nameof(registerType));
        }
    }
}
