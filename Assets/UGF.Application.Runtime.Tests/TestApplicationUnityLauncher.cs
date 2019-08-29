using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationUnityLauncher
    {
        private class Launcher : ApplicationUnityLauncher
        {
            public Application Application { get; private set; }

            protected override IApplication CreateApplication()
            {
                Application = new Application(this);

                Application.AddModule<Module>(new Module());

                return Application;
            }

            protected override void OnLaunched(IApplication application)
            {
                base.OnLaunched(application);

                Assert.True(application.GetModule<Module>().Done);
            }

            private void OnDestroy()
            {
                Assert.False(Application.IsInitialized);
            }
        }

        private class Application : ApplicationUnity
        {
            private readonly Launcher m_launcher;

            public Application(Launcher launcher)
            {
                m_launcher = launcher;
            }

            protected override void OnPostUninitialize()
            {
                base.OnPostUninitialize();

                Assert.NotNull(m_launcher);
            }
        }

        private class Module : ApplicationModuleBaseAsync
        {
            public bool Done { get; private set; }

            protected override IEnumerator OnInitializeAsync()
            {
                yield return null;

                Done = true;
            }
        }

        [UnityTest]
        public IEnumerator Launch()
        {
            var launcher = new GameObject("Launcher").AddComponent<Launcher>();

            launcher.enabled = false;

            yield return launcher.Launch();

            Assert.NotNull(launcher.Application);
            Assert.NotNull(launcher.Application.GetModule<Module>());
            Assert.True(launcher.Application.GetModule<Module>().Done);
        }
    }
}
