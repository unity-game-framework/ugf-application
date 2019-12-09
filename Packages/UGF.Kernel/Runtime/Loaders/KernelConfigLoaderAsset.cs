using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Kernel.Runtime.Loaders
{
    public class KernelConfigLoaderAsset : KernelConfigLoader
    {
        [SerializeField] private KernelConfigAsset m_config;

        public KernelConfigAsset Config { get { return m_config; } set { m_config = value; } }

        public override Task<IKernelConfig> Load()
        {
            return Task.FromResult<IKernelConfig>(m_config.Description);
        }
    }
}
