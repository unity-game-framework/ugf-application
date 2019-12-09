using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public class ApplicationConfigLoaderFromAsset : ApplicationConfigLoader
    {
        [SerializeField] private ApplicationConfigAssetBase m_config;

        public ApplicationConfigAssetBase Config { get { return m_config; } set { m_config = value; } }

        public override Task<IApplicationConfig> Load()
        {
            if (m_config == null) throw new ArgumentNullException(nameof(m_config), "The config asset not specified.");

            IApplicationConfig config = m_config.GetConfig();

            return Task.FromResult(config);
        }
    }
}
