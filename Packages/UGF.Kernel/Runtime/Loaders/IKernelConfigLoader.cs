using System.Threading.Tasks;

namespace UGF.Kernel.Runtime.Loaders
{
    public interface IKernelConfigLoader
    {
        Task<IKernelConfig> Load();
    }
}
