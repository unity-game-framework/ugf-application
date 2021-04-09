using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Application.Runtime.Scenes
{
    [DefaultExecutionOrder(int.MinValue)]
    [AddComponentMenu("Unity Game Framework/Application/Application Scene Provider Instance", 2000)]
    public class ApplicationSceneProviderInstanceComponent : ProviderInstanceComponent<IProvider<Scene, IApplication>>
    {
        protected override IProvider<Scene, IApplication> OnCreateProvider()
        {
            return new Provider<Scene, IApplication>();
        }
    }
}
