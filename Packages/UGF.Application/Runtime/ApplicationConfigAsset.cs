using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Application.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Application/ApplicationConfig", order = 2000)]
    public class ApplicationConfigAsset : ApplicationConfigAssetBase
    {
        [SerializeField] private List<ModuleInfo> m_modules = new List<ModuleInfo>();

        public List<ModuleInfo> Modules { get { return m_modules; } }

        [Serializable]
        public class ModuleInfo
        {
            [SerializeField] private bool m_active = true;
            [SerializeField] private ApplicationModuleAsset m_module;

            public bool Active { get { return m_active; } set { m_active = value; } }
            public ApplicationModuleAsset Module { get { return m_module; } set { m_module = value; } }
        }

        public override IApplicationConfig GetConfig()
        {
            var config = new ApplicationConfig();

            for (int i = 0; i < m_modules.Count; i++)
            {
                ModuleInfo module = m_modules[i];

                if (module.Active)
                {
                    if (module.Module == null) throw new ArgumentException("Module asset not specified.");

                    config.Modules.Add(module.Module);
                }
            }

            return config;
        }
    }
}
