using System.Collections.Generic;
using UGF.Description.Runtime;
using UGF.Module.Runtime;

namespace UGF.Kernel.Runtime
{
    public interface IKernelConfig : IDescription
    {
        string Name { get; }
        IReadOnlyList<IModuleBuildInfo> Modules { get; }
    }
}
