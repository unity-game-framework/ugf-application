using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationLauncher
    {
        private class Launcher : ApplicationLauncher
        {
            private Module m_module;
            private ModuleAsync m_moduleAsync;

            protected override void OnLaunch()
            {
                base.OnLaunch();

                Assert.True(IsLaunched);
                Assert.False(HasApplication);
            }

            protected override IApplication OnCreateApplication(IApplicationResources resources)
            {
                Assert.True(IsLaunched);
                Assert.False(HasApplication);

                m_module = new Module();
                m_moduleAsync = new ModuleAsync();

                var application = new Application();

                application.AddModule(m_module);
                application.AddModule(m_moduleAsync);

                return application;
            }

            protected override void OnInitializeApplication(IApplication application)
            {
                base.OnInitializeApplication(application);

                Assert.True(IsLaunched);
                Assert.False(HasApplication);
                Assert.True(application.GetModule<Module>().IsInit);
                Assert.False(application.GetModule<ModuleAsync>().IsInit);
            }

            protected override void OnLaunched(IApplication application)
            {
                base.OnLaunched(application);

                Assert.True(IsLaunched);
                Assert.True(HasApplication);
                Assert.True(application.GetModule<Module>().IsInit);
                Assert.True(application.GetModule<ModuleAsync>().IsInit);
            }

            protected override void OnStopped(IApplication application)
            {
                base.OnStopped(application);

                Assert.False(IsLaunched);
                Assert.True(HasApplication);
                Assert.True(application.GetModule<Module>().IsInit);
                Assert.True(application.GetModule<ModuleAsync>().IsInit);

                m_module = null;
                m_moduleAsync = null;
            }

            public void Quit()
            {
                SendMessage("OnApplicationQuit");
            }
        }

        private class Application : ApplicationConfigured
        {
            public Application() : base(new ApplicationResources { new ApplicationConfig() })
            {
            }
        }

        private class Module : ApplicationModuleBase
        {
            public bool IsInit { get; private set; }

            protected override void OnInitialize()
            {
                base.OnInitialize();

                IsInit = true;
            }

            protected override void OnUninitialize()
            {
                base.OnUninitialize();

                IsInit = false;
            }
        }

        private class ModuleAsync : ApplicationModuleBase, IApplicationModuleAsync
        {
            public bool IsInit { get; private set; }

            public async Task InitializeAsync()
            {
                await Task.Yield();

                IsInit = true;
            }

            protected override void OnUninitialize()
            {
                base.OnUninitialize();

                IsInit = false;
            }
        }

        [UnityTest]
        public IEnumerator LaunchAndStop()
        {
            var launcher = new GameObject("launcher").AddComponent<Launcher>();

            launcher.LaunchOnStart = false;

            yield return null;
            yield return null;

            Assert.False(launcher.IsLaunched);
            Assert.False(launcher.HasApplication);

            Task launchTask = launcher.Launch();

            while (!launchTask.IsCompleted)
            {
                yield return null;
            }

            Assert.True(launcher.IsLaunched);
            Assert.True(launcher.HasApplication);

            launcher.Stop();

            Assert.False(launcher.IsLaunched);
            Assert.False(launcher.HasApplication);

            Object.DestroyImmediate(launcher.gameObject);
        }

        [UnityTest]
        public IEnumerator LaunchAndDestroy()
        {
            var launcher = new GameObject("launcher").AddComponent<Launcher>();

            launcher.LaunchOnStart = false;

            yield return null;
            yield return null;

            Assert.False(launcher.IsLaunched);
            Assert.False(launcher.HasApplication);

            Task launchTask = launcher.Launch();

            while (!launchTask.IsCompleted)
            {
                yield return null;
            }

            Assert.True(launcher.IsLaunched);
            Assert.True(launcher.HasApplication);

            Object.DestroyImmediate(launcher.gameObject);

            Assert.False(launcher.IsLaunched);
            Assert.False(launcher.HasApplication);
        }

        [UnityTest]
        public IEnumerator LaunchAndQuit()
        {
            var launcher = new GameObject("launcher").AddComponent<Launcher>();

            launcher.LaunchOnStart = false;

            yield return null;
            yield return null;

            Assert.False(launcher.IsLaunched);
            Assert.False(launcher.HasApplication);

            Task launchTask = launcher.Launch();

            while (!launchTask.IsCompleted)
            {
                yield return null;
            }

            Assert.True(launcher.IsLaunched);
            Assert.True(launcher.HasApplication);

            launcher.Quit();

            Assert.False(launcher.IsLaunched);
            Assert.False(launcher.HasApplication);

            Object.DestroyImmediate(launcher.gameObject);
        }
    }
}
