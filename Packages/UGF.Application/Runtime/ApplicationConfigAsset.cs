using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UnityEngine;

namespace UGF.Application.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Application/Application Config", order = 2000)]
    public class ApplicationConfigAsset : ApplicationResourceAsset
    {
        [SerializeField] private List<EnabledProperty<ApplicationModuleAsset>> m_modules = new List<EnabledProperty<ApplicationModuleAsset>>();
        [SerializeField] private List<ApplicationModuleCollectionAsset> m_collections = new List<ApplicationModuleCollectionAsset>();

        public List<EnabledProperty<ApplicationModuleAsset>> Modules { get { return m_modules; } }
        public List<ApplicationModuleCollectionAsset> Collections { get { return m_collections; } }

        protected override Task<object> OnBuildAsync()
        {
            var config = new ApplicationConfig();

            for (int i = 0; i < m_modules.Count; i++)
            {
                EnabledProperty<ApplicationModuleAsset> module = m_modules[i];

                if (module.Enabled)
                {
                    if (module.Value == null) throw new ArgumentException("Module asset not specified.");

                    config.Modules.Add(module.Value);
                }
            }

            for (int i = 0; i < m_collections.Count; i++)
            {
                ApplicationModuleCollectionAsset collection = m_collections[i];

                collection.GetModules(config.Modules);
            }

            return Task.FromResult<object>(config);
        }
    }
}
