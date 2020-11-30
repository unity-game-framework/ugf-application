using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UnityEngine;

namespace UGF.Application.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Application/Application Config", order = 2000)]
    public class ApplicationConfigAsset : ApplicationResourceAsset
    {
        [SerializeField] private List<EnabledProperty<IApplicationModuleBuilder>> m_modules = new List<EnabledProperty<IApplicationModuleBuilder>>();

        public List<EnabledProperty<IApplicationModuleBuilder>> Modules { get { return m_modules; } }

        public override object GetResource()
        {
            var config = new ApplicationConfig();

            for (int i = 0; i < m_modules.Count; i++)
            {
                EnabledProperty<IApplicationModuleBuilder> module = m_modules[i];

                if (module.Enabled)
                {
                    if (module.Value == null) throw new ArgumentException("Module asset not specified.");

                    config.Modules.Add(module.Value);
                }
            }

            return config;
        }
    }
}
