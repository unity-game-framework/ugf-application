using System.Collections.Generic;

namespace UGF.Application.Runtime
{
    public interface IApplicationConfig
    {
        IReadOnlyList<IApplicationModuleInfo> Modules { get; }
    }
}
