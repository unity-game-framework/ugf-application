using System.Collections;
using NUnit.Framework;
using UGF.Application.Runtime.Scenes;
using UnityEngine;
using UnityEngine.TestTools;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationAccess
    {
        private class Launcher : ApplicationLauncher
        {
            protected override IApplication OnCreateApplication(IApplicationResources resources)
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
            var launcher = new GameObject("launcher").AddComponent<Launcher>();

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
