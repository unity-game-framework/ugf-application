using NUnit.Framework;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationBase
    {
        private class Application : ApplicationConfigured
        {
            public Application() : base(new ApplicationResources { new ApplicationConfig() })
            {
            }
        }

        private interface IModule : IApplicationModule
        {
        }

        private class Module : ApplicationModule<ApplicationModuleDescription>, IModule
        {
            public Module(IApplication application) : base(new ApplicationModuleDescription(typeof(IModule)), application)
            {
            }
        }

        [Test]
        public void Modules()
        {
            var application = new Application();
            var module = new Module(application);

            application.AddModule<IModule>(module);

            Assert.AreEqual(1, application.Count);
            Assert.True(application.HasModule(typeof(IModule)));
        }

        [Test]
        public void OnInitialize()
        {
            var application = new Application();
            var module0 = new Module(application);
            var module1 = new Module(application);

            application.AddModule<IModule>(module0);
            application.AddModule<IApplicationModule>(module1);
            application.Initialize();

            Assert.True(module0.IsInitialized);
            Assert.True(module1.IsInitialized);
        }

        [Test]
        public void OnUninitialize()
        {
            var application = new Application();
            var module0 = new Module(application);
            var module1 = new Module(application);

            application.AddModule<IModule>(module0);
            application.AddModule<IApplicationModule>(module1);
            application.Initialize();
            application.Uninitialize();

            Assert.False(module0.IsInitialized);
            Assert.False(module1.IsInitialized);
        }

        [Test]
        public void AddModule()
        {
            var application = new Application();
            var module = new Module(application);

            Assert.AreEqual(0, application.Count);

            application.AddModule<IModule>(module);

            Assert.AreEqual(1, application.Count);
            Assert.True(application.HasModule(module));
        }

        [Test]
        public void RemoveModule()
        {
            var application = new Application();
            var module = new Module(application);

            Assert.AreEqual(0, application.Count);

            application.AddModule<IModule>(module);

            Assert.AreEqual(1, application.Count);
            Assert.True(application.HasModule(module));

            application.RemoveModule<IModule>();

            Assert.AreEqual(0, application.Count);
            Assert.False(application.HasModule(module));
        }

        [Test]
        public void ClearModules()
        {
            var application = new Application();
            var module = new Module(application);

            Assert.AreEqual(0, application.Count);

            application.AddModule<IModule>(module);

            Assert.AreEqual(1, application.Count);
            Assert.True(application.HasModule(module));

            application.ClearModules();

            Assert.AreEqual(0, application.Count);
            Assert.False(application.HasModule(module));
        }

        [Test]
        public void GetModule()
        {
            var application = new Application();
            var module0 = new Module(application);

            application.AddModule<IModule>(module0);

            var module1 = application.GetModule<IModule>();

            Assert.NotNull(module1);
            Assert.AreEqual(module0, module1);
        }

        [Test]
        public void TryGetModule()
        {
            var application = new Application();
            var module0 = new Module(application);

            application.AddModule<IModule>(module0);

            bool result0 = application.TryGetModule(out IModule module1);
            bool result1 = application.TryGetModule(out IApplicationModule module2);

            Assert.True(result0);
            Assert.False(result1);
            Assert.NotNull(module1);
            Assert.Null(module2);
            Assert.AreEqual(module0, module1);
        }
    }
}
