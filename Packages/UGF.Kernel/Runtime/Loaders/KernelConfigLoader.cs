using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Kernel.Runtime.Loaders
{
    public abstract class KernelConfigLoader : MonoBehaviour, IKernelConfigLoader
    {
        public abstract Task<IKernelConfig> Load();
    }
}
