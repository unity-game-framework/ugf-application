using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine.SceneManagement;

namespace UGF.Application.Runtime.Scenes
{
    public static class ApplicationSceneExtensions
    {
        public static IApplication GetApplication(this Scene scene)
        {
            return ProviderInstance.Get<IProvider<Scene, IApplication>>().Get(scene);
        }

        public static bool TryGetApplication(this Scene scene, out IApplication application)
        {
            return ProviderInstance.Get<IProvider<Scene, IApplication>>().TryGet(scene, out application);
        }
    }
}
