using System;
using UGF.Logs.Runtime;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents application with config of modules.
    /// </summary>
    public class ApplicationConfigured : ApplicationOrdered
    {
        public IApplicationConfig Config { get; }

        public ApplicationConfigured(IApplicationResources resources, bool useReverseModulesUninitialization = true) : base(resources, useReverseModulesUninitialization)
        {
            Config = resources.Get<IApplicationConfig>() ?? throw new ArgumentException($"Config not found in specified resources: '{resources}'.");
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            for (int i = 0; i < Config.Modules.Count; i++)
            {
                IApplicationModuleBuilder builder = Config.Modules[i];
                IApplicationModule module = builder.Build(this);

                AddModule(module.Description.RegisterType, module);
            }

            Log.Debug("Configured application initialized", new
            {
                modulesCount = Count
            });
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Log.Debug("Configured application uninitialized", new
            {
                modulesCount = Count
            });

            ClearModules();
        }
    }
}
