using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Application.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Application/Application Module Collection List", order = 2000)]
    public class ApplicationModuleCollectionListAsset : ApplicationModuleCollectionAsset
    {
        [SerializeField] private List<ApplicationModuleAsset> m_modules = new List<ApplicationModuleAsset>();

        public List<ApplicationModuleAsset> Modules { get { return m_modules; } }

        protected override void OnGetModules(ICollection<IApplicationModuleBuilder> modules)
        {
            for (int i = 0; i < m_modules.Count; i++)
            {
                ApplicationModuleAsset module = m_modules[i];

                if (module == null) throw new ArgumentException("Value not specified.");

                modules.Add(module);
            }
        }
    }
}
