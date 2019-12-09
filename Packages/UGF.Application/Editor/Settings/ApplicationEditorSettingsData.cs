using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Application.Editor.Settings
{
    internal class ApplicationEditorSettingsData : ScriptableObject
    {
        [SerializeField] private ApplicationConfigAssetBase m_config;

        public ApplicationConfigAssetBase Config { get { return m_config; } set { m_config = value; } }
    }
}
