using UGF.Application.Runtime;

namespace UGF.Kernel.Runtime
{
    public interface IKernelApplication : IApplication
    {
        IKernelConfig Config { get; }
    }
}
