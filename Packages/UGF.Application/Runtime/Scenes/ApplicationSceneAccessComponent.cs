using UnityEngine;

namespace UGF.Application.Runtime.Scenes
{
    [AddComponentMenu("Unity Game Framework/Application/Application Scene Access", 2000)]
    public class ApplicationSceneAccessComponent : ApplicationAccessComponent
    {
        protected override IApplication OnGetApplication()
        {
            IApplicationSceneProvider provider = ApplicationSceneProviderInstance.Provider;
            IApplication application = provider.Get(gameObject.scene);

            return application;
        }
    }
}
