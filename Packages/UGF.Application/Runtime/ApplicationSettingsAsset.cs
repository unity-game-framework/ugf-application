using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public class ApplicationSettingsAsset : CustomSettingsData
    {
        [SerializeField] private ApplicationResourceAsset m_config;

        public ApplicationResourceAsset Config { get { return m_config; } set { m_config = value; } }
    }
}
