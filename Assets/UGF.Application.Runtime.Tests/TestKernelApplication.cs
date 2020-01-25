using System;
using System.Collections.Generic;
using NUnit.Framework;
using UGF.Testing.Runtime.Tests;

namespace UGF.Application.Runtime.Tests
{
    public class TestKernelApplication : TestNoLogs
    {
        private class ModuleA : ModuleBase
        {
            public ModuleA(Action init = null, Action uninit = null) : base(init, uninit)
            {
            }
        }

        private class ModuleB : ModuleBase
        {
            public ModuleB(Action init = null, Action uninit = null) : base(init, uninit)
            {
            }
        }

        private class ModuleC : ModuleBase
        {
            public ModuleC(Action init = null, Action uninit = null) : base(init, uninit)
            {
            }
        }

        private abstract class ModuleBase : ApplicationModuleBase
        {
            private readonly Action m_init;
            private readonly Action m_uninit;

            protected ModuleBase(Action init = null, Action uninit = null)
            {
                m_init = init;
                m_uninit = uninit;
            }

            protected override void OnInitialize()
            {
                base.OnInitialize();

                m_init?.Invoke();
            }

            protected override void OnUninitialize()
            {
                base.OnUninitialize();

                m_uninit?.Invoke();
            }
        }

        private class ModuleInfo : IApplicationModuleInfo
        {
            public Type RegisterType { get; }
            public ApplicationModuleBuildHandler Builder { get; }

            public ModuleInfo(IApplicationModule module)
            {
                RegisterType = module.GetType();
                Builder = application => module;
            }
        }

        [Test]
        public void CreateModules()
        {
            var config = new ApplicationConfig()
            {
                Modules =
                {
                    new ModuleInfo(new ModuleA()),
                    new ModuleInfo(new ModuleB())
                }
            };

            var resources = new ApplicationResources { config };
            var application = new ApplicationConfigured(resources, false);

            application.Initialize();

            bool result0 = application.TryGetModule(out ModuleA moduleA);
            bool result1 = application.TryGetModule(out ModuleB moduleB);

            Assert.True(result0);
            Assert.True(result1);
            Assert.NotNull(moduleA);
            Assert.NotNull(moduleB);
        }

        [Test]
        public void InitializeOrder()
        {
            var order = new List<string>();

            var config = new ApplicationConfig()
            {
                Modules =
                {
                    new ModuleInfo(new ModuleA(() => order.Add("moduleA"))),
                    new ModuleInfo(new ModuleB(() => order.Add("moduleB")))
                }
            };

            var resources = new ApplicationResources { config };
            var application = new ApplicationConfigured(resources, false);

            application.Initialize();

            Assert.AreEqual(2, order.Count);
            Assert.AreEqual("moduleA", order[0]);
            Assert.AreEqual("moduleB", order[1]);
        }

        [Test]
        public void InitializeOrder2()
        {
            var order = new List<string>();

            var config = new ApplicationConfig()
            {
                Modules =
                {
                    new ModuleInfo(new ModuleA(() => order.Add("moduleA"))),
                    new ModuleInfo(new ModuleB(() => order.Add("moduleB")))
                }
            };

            var resources = new ApplicationResources { config };
            var application = new ApplicationConfigured(resources, false);

            application.AddModule(new ModuleC(() => order.Add("moduleC")));
            application.Initialize();

            Assert.AreEqual(3, order.Count);
            Assert.AreEqual("moduleA", order[0]);
            Assert.AreEqual("moduleB", order[1]);
            Assert.AreEqual("moduleC", order[2]);
        }

        [Test]
        public void UninitializeOrder()
        {
            var order = new List<string>();

            var config = new ApplicationConfig()
            {
                Modules =
                {
                    new ModuleInfo(new ModuleA(uninit: () => order.Add("moduleA"))),
                    new ModuleInfo(new ModuleB(uninit: () => order.Add("moduleB")))
                }
            };

            var resources = new ApplicationResources { config };
            var application = new ApplicationConfigured(resources, false);

            application.Initialize();
            application.Uninitialize();

            Assert.AreEqual(2, order.Count);
            Assert.AreEqual("moduleB", order[0]);
            Assert.AreEqual("moduleA", order[1]);
        }

        [Test]
        public void UninitializeOrder2()
        {
            var order = new List<string>();

            var config = new ApplicationConfig()
            {
                Modules =
                {
                    new ModuleInfo(new ModuleA(uninit: () => order.Add("moduleA"))),
                    new ModuleInfo(new ModuleB(uninit: () => order.Add("moduleB")))
                }
            };

            var resources = new ApplicationResources { config };
            var application = new ApplicationConfigured(resources, false);

            application.Initialize();

            var moduleC = new ModuleC(uninit: () => order.Add("moduleC"));

            moduleC.Initialize();

            application.AddModule(moduleC);

            application.Uninitialize();

            Assert.AreEqual(3, order.Count);
            Assert.AreEqual("moduleB", order[0]);
            Assert.AreEqual("moduleA", order[1]);
            Assert.AreEqual("moduleC", order[2]);
        }
    }
}
