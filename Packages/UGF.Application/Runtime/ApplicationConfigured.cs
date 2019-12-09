using System;
using System.Collections.Generic;

namespace UGF.Application.Runtime
{
    public class ApplicationConfigured : ApplicationUnity
    {
        public IApplicationConfig Config { get; }

        public ApplicationConfigured(IApplicationConfig config, bool provideStaticInstance = true) : base(provideStaticInstance)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();

            CreateModules(Config);
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

            if (Modules.Count != Config.Modules.Count)
            {
                foreach (KeyValuePair<Type, IApplicationModule> pair in Modules)
                {
                    if (!pair.Value.IsInitialized)
                    {
                        pair.Value.Initialize();
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

            if (Modules.Count != Config.Modules.Count)
            {
                foreach (KeyValuePair<Type, IApplicationModule> pair in Modules)
                {
                    if (pair.Value.IsInitialized)
                    {
                        pair.Value.Uninitialize();
                    }
                }
            }
        }

        protected virtual void CreateModules(IApplicationConfig config)
        {
            for (int i = 0; i < config.Modules.Count; i++)
            {
                IApplicationModuleInfo info = config.Modules[i];
                IApplicationModule module = info.Builder.Build(this);

                AddModule(info.RegisterType, module);
            }
        }
    }
}
