using UGF.Kernel.Runtime;
using UnityEngine;

namespace UGF.Kernel.Editor.Settings
{
    internal class KernelEditorSettingsData : ScriptableObject
    {
        [SerializeField] private KernelConfigAsset m_config;

        public KernelConfigAsset Config { get { return m_config; } set { m_config = value; } }
    }
}
