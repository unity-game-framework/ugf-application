using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationBase
    {
        private class Application : ApplicationBase
        {
        }

        private interface IModule : IApplicationModule
        {
        }

        private class Module : ApplicationModuleBase, IModule
        {
        }

        [Test]
        public void Modules()
        {
            var application = new Application();
            var module = new Module();

            application.AddModule<IModule>(module);

            Assert.NotNull(application.Modules);
            Assert.AreEqual(1, application.Modules.Count);
            Assert.True(application.Modules.ContainsKey(typeof(IModule)));
        }

        [Test]
        public void OnInitialize()
        {
            var application = new Application();
            var module0 = new Module();
            var module1 = new Module();

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
            var module0 = new Module();
            var module1 = new Module();

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
            var module = new Module();

            Assert.AreEqual(0, application.Modules.Count);

            application.AddModule<IModule>(module);

            Assert.AreEqual(1, application.Modules.Count);
            Assert.True(application.Modules.Values.Any(x => x == module));
        }

        [Test]
        public void RemoveModule()
        {
            var application = new Application();
            var module = new Module();

            Assert.AreEqual(0, application.Modules.Count);

            application.AddModule<IModule>(module);

            Assert.AreEqual(1, application.Modules.Count);
            Assert.True(application.Modules.Values.Any(x => x == module));

            application.RemoveModule<IModule>();

            Assert.AreEqual(0, application.Modules.Count);
            Assert.False(application.Modules.Values.Any(x => x == module));
        }

        [Test]
        public void ClearModules()
        {
            var application = new Application();
            var module = new Module();

            Assert.AreEqual(0, application.Modules.Count);

            application.AddModule<IModule>(module);

            Assert.AreEqual(1, application.Modules.Count);
            Assert.True(application.Modules.Values.Any(x => x == module));

            application.ClearModules();

            Assert.AreEqual(0, application.Modules.Count);
            Assert.False(application.Modules.Values.Any(x => x == module));
        }

        [Test]
        public void GetModule()
        {
            var application = new Application();
            var module0 = new Module();

            application.AddModule<IModule>(module0);

            var module1 = application.GetModule<IModule>();

            Assert.NotNull(module1);
            Assert.AreEqual(module0, module1);
        }

        [Test]
        public void TryGetModule()
        {
            var application = new Application();
            var module0 = new Module();

            application.AddModule<IModule>(module0);

            bool result0 = application.TryGetModule(out IModule module1);
            bool result1 = application.TryGetModule(out IApplicationModule module2);

            Assert.True(result0);
            Assert.False(result1);
            Assert.NotNull(module1);
            Assert.Null(module2);
            Assert.AreEqual(module0, module1);
        }

        [Test]
        public void GetRegisterType()
        {
            var application = new Application();
            var module = new Module();

            application.AddModule<IModule>(module);

            Type type = application.GetRegisterType(module);

            Assert.NotNull(type);
            Assert.AreEqual(typeof(IModule), type);
        }

        [Test]
        public void TryGetRegisterType()
        {
            var application = new Application();
            var module = new Module();

            application.AddModule<IModule>(module);

            bool result0 = application.TryGetRegisterType(module, out Type type0);
            bool result1 = application.TryGetRegisterType(new Module(), out Type type1);

            Assert.True(result0);
            Assert.False(result1);
            Assert.NotNull(type0);
            Assert.Null(type1);
            Assert.AreEqual(typeof(IModule), type0);
        }

        [Test]
        public void GetEnumerator()
        {
            var application = new Application();

            foreach (KeyValuePair<Type, IApplicationModule> pair in application)
            {
                Assert.NotNull(pair.Key);
            }
        }
    }
}
