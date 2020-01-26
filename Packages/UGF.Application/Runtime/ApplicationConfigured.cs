using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    public class ApplicationConfigured : ApplicationUnity
    {
        public IApplicationConfig Config { get; }

        public ApplicationConfigured(IApplicationResources resources, bool provideStaticInstance = false) : base(resources, provideStaticInstance)
        {
            Config = resources.Get<IApplicationConfig>();
        }

        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();

            CreateModules(Config);
        }

        protected override async Task OnInitializeAsync()
        {
            await OnInitializeModulesAsync();
        }

        protected override void OnPostUninitialize()
        {
            base.OnPostUninitialize();

            ClearModules();
        }

        protected override void OnInitializeModules()
        {
            for (int i = 0; i < Config.Modules.Count; i++)
            {
                IApplicationModuleInfo info = Config.Modules[i];
                IApplicationModule module = Modules[info.RegisterType];

                module.Initialize();
            }

            foreach (KeyValuePair<Type, IApplicationModule> pair in Modules)
            {
                if (!HasModule(Config, pair.Key))
                {
                    pair.Value.Initialize();
                }
            }
        }

        protected virtual async Task OnInitializeModulesAsync()
        {
            for (int i = 0; i < Config.Modules.Count; i++)
            {
                IApplicationModuleInfo info = Config.Modules[i];
                IApplicationModule module = Modules[info.RegisterType];

                if (module is IApplicationModuleAsync moduleAsync)
                {
                    await moduleAsync.InitializeAsync();
                }
            }

            foreach (KeyValuePair<Type, IApplicationModule> pair in Modules)
            {
                if (!HasModule(Config, pair.Key))
                {
                    IApplicationModule module = pair.Value;

                    if (module is IApplicationModuleAsync moduleAsync)
                    {
                        await moduleAsync.InitializeAsync();
                    }
                }
            }
        }

        protected override void OnUninitializeModules()
        {
            for (int i = Config.Modules.Count - 1; i >= 0; i--)
            {
                IApplicationModuleInfo info = Config.Modules[i];
                IApplicationModule module = Modules[info.RegisterType];

                module.Uninitialize();
            }

            foreach (KeyValuePair<Type, IApplicationModule> pair in Modules)
            {
                if (!HasModule(Config, pair.Key))
                {
                    pair.Value.Uninitialize();
                }
            }
        }

        protected virtual void CreateModules(IApplicationConfig config)
        {
            for (int i = 0; i < config.Modules.Count; i++)
            {
                IApplicationModuleInfo info = config.Modules[i];
                IApplicationModule module = info.Builder.Invoke(this);

                AddModule(info.RegisterType, module);
            }
        }

        private static bool HasModule(IApplicationConfig config, Type registerType)
        {
            for (int i = 0; i < config.Modules.Count; i++)
            {
                IApplicationModuleInfo module = config.Modules[i];

                if (module.RegisterType == registerType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
