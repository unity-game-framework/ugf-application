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

        public override Task<IApplicationResources> LoadAsync()
        {
            var resources = new ApplicationResources();

            AddResources(resources);

            return Task.FromResult((IApplicationResources)resources);
        }

        protected void AddResources(IApplicationResources resources)
        {
            for (int i = 0; i < m_resources.Count; i++)
            {
                Object resource = m_resources[i];

                resources.Add(resource);
            }
        }
    }
}
