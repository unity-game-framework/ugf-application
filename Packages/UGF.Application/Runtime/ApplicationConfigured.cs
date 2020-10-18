using System;

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

            for (int i = 0; i < Config.Modules.Count; i++)
            {
                IApplicationModuleInfo info = Config.Modules[i];
                IApplicationModule module = info.Builder.Invoke(this);

                AddModule(info.RegisterType, module);
            }
        }
    }
}
