using System.Collections.Generic;

namespace UGF.Application.Runtime
{
    public class ApplicationConfig : IApplicationConfig
    {
        public List<IApplicationModuleAsset> Modules { get; } = new List<IApplicationModuleAsset>();

        IReadOnlyList<IApplicationModuleAsset> IApplicationConfig.Modules { get { return Modules; } }
    }
}
