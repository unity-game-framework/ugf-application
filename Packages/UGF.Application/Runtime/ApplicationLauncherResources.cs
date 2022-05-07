using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Application.Runtime
{
    public class ApplicationLauncherResources
    {
        public string ResourcesPath { get; }
        public ApplicationLauncherComponent Component { get { return m_component ? m_component : throw new ArgumentException("Value not specified."); } }
        public IApplication Application { get { return Component.Launcher.Application; } }

        private ApplicationLauncherComponent m_component;

        public ApplicationLauncherResources(string resourcesPath)
        {
            if (string.IsNullOrEmpty(resourcesPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(resourcesPath));

            ResourcesPath = resourcesPath;
        }

        public async Task CreateAsync()
        {
            var asset = (ApplicationLauncherComponent)await Resources.LoadAsync<ApplicationLauncherComponent>(ResourcesPath).WaitAsync();

            m_component = Object.Instantiate(asset);
            m_component.Initialize();

            await m_component.LaunchAsync();
        }

        public void Destroy()
        {
            m_component.Uninitialize();

            Object.Destroy(m_component.gameObject);

            m_component = null;
        }
    }
}
