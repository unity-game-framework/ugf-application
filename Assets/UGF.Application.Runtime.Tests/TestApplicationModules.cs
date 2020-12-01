using System;
using System.Collections.Generic;
using NUnit.Framework;
using UGF.Builder.Runtime;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationModules
    {
        private class ModuleA : ModuleBase
        {
            public ModuleA(IApplication application, Action init = null, Action uninit = null) : base(typeof(ModuleA), application, init, uninit)
            {
            }
        }

        private class ModuleB : ModuleBase
        {
            public ModuleB(IApplication application, Action init = null, Action uninit = null) : base(typeof(ModuleB), application, init, uninit)
            {
            }
        }

        private class ModuleC : ModuleBase
        {
            public ModuleC(IApplication application, Action init = null, Action uninit = null) : base(typeof(ModuleC), application, init, uninit)
            {
            }
        }

        private abstract class ModuleBase : ApplicationModule<ApplicationModuleDescription>
        {
            private readonly Action m_init;
            private readonly Action m_uninit;

            protected ModuleBase(Type type, IApplication application, Action init = null, Action uninit = null) : base(new ApplicationModuleDescription(type), application)
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

        private class ModuleBuilder : Builder<IApplication, IApplicationModule>, IApplicationModuleBuilder
        {
            public Func<IApplication, IApplicationModule> Func { get; set; }

            public ModuleBuilder(Func<IApplication, IApplicationModule> func)
            {
                Func = func ?? throw new ArgumentNullException(nameof(func));
            }

            protected override IApplicationModule OnBuild(IApplication arguments)
            {
                return Func(arguments);
            }
        }

        private static IApplicationModuleBuilder Create<T>(Func<IApplication, IApplicationModule> func) where T : class, IApplicationModule
        {
            return new ModuleBuilder(func);
        }

        [Test]
        public void CreateModules()
        {
            var config = new ApplicationConfig()
            {
                Modules =
                {
                    Create<ModuleA>(application1 => new ModuleA(application1)),
                    Create<ModuleB>(application1 => new ModuleB(application1))
                }
            };

            var application = new ApplicationConfigured(new ApplicationResources { config });

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
                    Create<ModuleA>(application1 => new ModuleA(application1, () => order.Add("moduleA"))),
                    Create<ModuleB>(application1 => new ModuleB(application1, () => order.Add("moduleB")))
                }
            };

            var application = new ApplicationConfigured(new ApplicationResources { config });

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
                    Create<ModuleA>(application1 => new ModuleA(application1, () => order.Add("moduleA"))),
                    Create<ModuleB>(application1 => new ModuleB(application1, () => order.Add("moduleB")))
                }
            };

            var application = new ApplicationConfigured(new ApplicationResources { config });

            application.AddModule(new ModuleC(application, () => order.Add("moduleC")));
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
                    Create<ModuleA>(application1 => new ModuleA(application1, uninit: () => order.Add("moduleA"))),
                    Create<ModuleB>(application1 => new ModuleB(application1, uninit: () => order.Add("moduleB")))
                }
            };

            var application = new ApplicationConfigured(new ApplicationResources { config });

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
                    Create<ModuleA>(application1 => new ModuleA(application1, uninit: () => order.Add("moduleA"))),
                    Create<ModuleB>(application1 => new ModuleB(application1, uninit: () => order.Add("moduleB")))
                }
            };

            var application = new ApplicationConfigured(new ApplicationResources { config });

            application.Initialize();

            var moduleC = new ModuleC(application, uninit: () => order.Add("moduleC"));

            moduleC.Initialize();

            application.AddModule(moduleC);

            application.Uninitialize();

            Assert.AreEqual(3, order.Count);
            Assert.AreEqual("moduleC", order[0]);
            Assert.AreEqual("moduleB", order[1]);
            Assert.AreEqual("moduleA", order[2]);
        }
    }
}
