using System.Collections.Generic;

namespace UGF.Application.Runtime
{
    public class ApplicationConfig : IApplicationConfig
    {
        public List<IApplicationModuleInfo> Modules { get; } = new List<IApplicationModuleInfo>();

        IReadOnlyList<IApplicationModuleInfo> IApplicationConfig.Modules { get { return Modules; } }
    }
}
