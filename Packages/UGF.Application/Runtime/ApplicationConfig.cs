using System.Collections.Generic;
using UGF.Description.Runtime;

namespace UGF.Application.Runtime
{
    public class ApplicationConfig : DescriptionBase, IApplicationConfig
    {
        public List<IApplicationModuleBuilder> Modules { get; } = new List<IApplicationModuleBuilder>();

        IReadOnlyList<IApplicationModuleBuilder> IApplicationConfig.Modules { get { return Modules; } }
    }
}
