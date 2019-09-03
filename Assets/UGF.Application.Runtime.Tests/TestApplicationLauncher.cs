using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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

            protected override IEnumerator PreloadResourcesAsync()
            {
                yield return null;

                Assert.True(IsLaunched);
                Assert.False(HasApplication);
            }

            protected override IApplication CreateApplication()
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

            protected override void InitializeApplication(IApplication application)
            {
                base.InitializeApplication(application);

                Assert.True(IsLaunched);
                Assert.False(HasApplication);
                Assert.True(application.GetModule<Module>().IsInit);
                Assert.False(application.GetModule<ModuleAsync>().IsInit);
            }

            protected override IEnumerator InitializeModulesAsync(IApplication application)
            {
                yield return base.InitializeModulesAsync(application);

                Assert.True(IsLaunched);
                Assert.False(HasApplication);
                Assert.True(application.GetModule<Module>().IsInit);
                Assert.True(application.GetModule<ModuleAsync>().IsInit);
            }

            protected override void OnLaunched(IApplication application)
            {
                base.OnLaunched(application);

                Assert.True(IsLaunched);
                Assert.True(HasApplication);
                Assert.True(application.GetModule<Module>().IsInit);
                Assert.True(application.GetModule<ModuleAsync>().IsInit);
            }

            protected override void OnStop(IApplication application)
            {
                base.OnStop(application);

                Assert.False(IsLaunched);
                Assert.True(HasApplication);
                Assert.True(application.GetModule<Module>().IsInit);
                Assert.True(application.GetModule<ModuleAsync>().IsInit);
            }

            protected override void OnStopped()
            {
                base.OnStopped();

                Assert.False(IsLaunched);
                Assert.False(HasApplication);
                Assert.False(m_module.IsInit);
                Assert.False(m_moduleAsync.IsInit);

                m_module = null;
                m_moduleAsync = null;
            }
        }

        private class Application : ApplicationBase
        {
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

        private class ModuleAsync : ApplicationModuleBaseAsync
        {
            public bool IsInit { get; private set; }

            protected override IEnumerator OnInitializeAsync()
            {
                yield return null;

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

            yield return launcher.Launch();

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

            yield return launcher.Launch();

            Assert.True(launcher.IsLaunched);
            Assert.True(launcher.HasApplication);

            Object.DestroyImmediate(launcher.gameObject);

            Assert.False(launcher.IsLaunched);
            Assert.False(launcher.HasApplication);
        }
    }
}
