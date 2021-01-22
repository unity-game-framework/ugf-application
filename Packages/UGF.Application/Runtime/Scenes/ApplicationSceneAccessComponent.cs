using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Application.Runtime.Scenes
{
    [AddComponentMenu("Unity Game Framework/Application/Application Scene Access", 2000)]
    public class ApplicationSceneAccessComponent : ApplicationAccessComponent
    {
        protected override IApplication OnGetApplication()
        {
            var provider = ProviderInstance.Get<IProvider<Scene, IApplication>>();
            IApplication application = provider.Get(gameObject.scene);

            return application;
        }
    }
}
