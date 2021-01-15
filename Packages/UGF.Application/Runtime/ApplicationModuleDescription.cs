using System;

namespace UGF.Application.Runtime
{
    public partial class ApplicationModuleDescription : IApplicationModuleDescription
    {
        public Type RegisterType { get; set; }

        public ApplicationModuleDescription()
        {
        }
    }
}
