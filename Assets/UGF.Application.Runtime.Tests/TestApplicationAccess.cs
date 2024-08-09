using System.Collections;
using NUnit.Framework;
using UGF.Application.Runtime.Scenes;
using UnityEngine;
using UnityEngine.TestTools;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationAccess
    {
        [UnityTest]
        public IEnumerator Access()
        {
            var gameObject = new GameObject("launcher");

            gameObject.AddComponent<ApplicationSceneProviderInstanceComponent>();

            var launcher = gameObject.AddComponent<ApplicationLauncherComponent>();

            launcher.Application = Resources.Load<ApplicationAsset>("TestApplication");

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
