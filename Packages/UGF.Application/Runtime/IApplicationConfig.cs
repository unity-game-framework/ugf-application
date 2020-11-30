using System.Collections.Generic;
using UGF.Description.Runtime;

namespace UGF.Application.Runtime
{
    public interface IApplicationConfig : IDescription
    {
        IReadOnlyList<IApplicationModuleBuilder> Modules { get; }
    }
}
