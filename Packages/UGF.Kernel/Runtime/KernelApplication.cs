using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Logs.Runtime;
using UGF.Module.Runtime;

namespace UGF.Kernel.Runtime
{
    public class KernelApplication : ApplicationUnity, IKernelApplication
    {
        public IKernelConfig Config { get; }

        public KernelApplication(IKernelConfig config, bool provideStaticInstance = true) : base(provideStaticInstance)
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
                IModuleBuilder builder = Config.Modules[i].Builder;
                IApplicationModule module = Modules[builder.RegisterType];

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
                IModuleBuilder builder = Config.Modules[i].Builder;
                IApplicationModule module = Modules[builder.RegisterType];

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

        protected virtual void CreateModules(IKernelConfig config)
        {
            for (int i = 0; i < config.Modules.Count; i++)
            {
                IModuleBuildInfo info = config.Modules[i];

                if (info.Active)
                {
                    IModuleBuilder builder = info.Builder;
                    Type registerType = builder.RegisterType;
                    IApplicationModule module = builder.Build(this, info.Arguments);

                    AddModule(registerType, module);

                    Log.Debug($"Build Module: registerType:'{registerType}', module:'{module}'.");
                }
            }
        }
    }
}
