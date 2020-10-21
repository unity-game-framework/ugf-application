using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Application.Runtime
{
    public class ApplicationLauncherResources : ApplicationLauncherResourceLoader
    {
        [SerializeField] private List<Object> m_resources = new List<Object>();

        public List<Object> Resources { get { return m_resources; } }

        public override async Task<IApplicationResources> LoadAsync()
        {
            var resources = new ApplicationResources();

            await ApplicationUtility.AddResources(resources, m_resources);

            return resources;
        }
    }
}
