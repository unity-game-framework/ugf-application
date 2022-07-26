using System;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine.SceneManagement;

namespace UGF.Application.Runtime.Scenes
{
    public static class ApplicationSceneExtensions
    {
        public static IApplication GetApplication(this Scene scene)
        {
            return TryGetApplication(scene, out IApplication application) ? application : throw new ArgumentException($"Application not found by the specified scene: '{scene}'.");
        }

        public static bool TryGetApplication(this Scene scene, out IApplication application)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));

            application = default;
            return ProviderInstance.TryGet(out IProvider<Scene, IApplication> provider) && provider.TryGet(scene, out application);
        }
    }
}
