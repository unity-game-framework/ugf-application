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

        public override Task<IApplicationResources> LoadAsync()
        {
            var loader = new ApplicationLauncherResourceLoader();

            loader.Resources.AddRange(m_resources);

            return loader.LoadAsync();
        }
    }
}
