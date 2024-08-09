using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.Description.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Application.Runtime
{
    public interface IApplicationDescription : IDescription
    {
        IReadOnlyDictionary<GlobalId, IBuilder<IApplication, IApplicationModule>> Modules { get; }
    }
}
