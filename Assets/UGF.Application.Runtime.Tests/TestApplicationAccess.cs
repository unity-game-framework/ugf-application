using System.Collections;
using NUnit.Framework;
using UGF.Application.Runtime.Scenes;
using UnityEngine;
using UnityEngine.TestTools;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationAccess
    {
        private class Builder : ApplicationBuilderComponent
        {
            protected override IApplication OnBuild(IApplicationResources arguments)
            {
                return new Application();
            }
        }

        private class Application : ApplicationConfigured
        {
            public Application() : base(new ApplicationResources { new ApplicationConfig() })
            {
            }
        }

        [UnityTest]
        public IEnumerator Access()
        {
            var gameObject = new GameObject("launcher");

            gameObject.AddComponent<ApplicationSceneProviderInstanceComponent>();

            var launcher = gameObject.AddComponent<ApplicationLauncherComponent>();

            launcher.Builder = gameObject.AddComponent<Builder>();
            launcher.ResourceLoader = gameObject.AddComponent<ApplicationLauncherResourcesComponent>();

            yield return null;
            yield return null;

            var access = new GameObject("access").AddComponent<ApplicationSceneAccessComponent>();
            IApplication application = access.GetApplication();

            Assert.NotNull(application);

            launcher.Stop();

            Object.DestroyImmediate(launcher.gameObject);
            Object.DestroyImmediate(access.gameObject);
        }
    }
}
