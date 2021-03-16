using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application Launcher Resources", 2000)]
    public class ApplicationLauncherResourcesComponent : ApplicationLauncherResourceLoaderComponent
    {
        [SerializeField] private List<Object> m_resources = new List<Object>();

        public List<Object> Resources { get { return m_resources; } }

        public override async Task<IApplicationResources> LoadAsync()
        {
            var resources = new ApplicationResources();

            for (int i = 0; i < m_resources.Count; i++)
            {
                Object resource = m_resources[i];

                if (resource == null) throw new ArgumentNullException(nameof(resource), $"Resource not specified at index of the list: '{i}'.");

                if (resource is ApplicationResourceAsset asset)
                {
                    object result = await asset.BuildAsync();

                    resources.Add(result);
                }
                else
                {
                    resources.Add(resource);
                }
            }

            return resources;
        }
    }
}
