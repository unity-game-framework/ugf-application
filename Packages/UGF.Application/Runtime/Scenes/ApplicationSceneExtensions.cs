using UnityEngine.SceneManagement;

namespace UGF.Application.Runtime.Scenes
{
    public static class ApplicationSceneExtensions
    {
        public static IApplication GetApplication(this Scene scene)
        {
            return ApplicationSceneProviderInstance.Provider.Get(scene);
        }

        public static bool TryGetApplication(this Scene scene, out IApplication application)
        {
            return ApplicationSceneProviderInstance.Provider.TryGet(scene, out application);
        }
    }
}
