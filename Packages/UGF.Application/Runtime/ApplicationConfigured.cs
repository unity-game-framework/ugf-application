using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents application with config of modules to automatically create during initialization.
    /// </summary>
    /// <remarks>
    /// Only modules from config will be created, initialized and uninitialized.
    /// Application modules can not be changed after initialization.
    /// </remarks>
    public class ApplicationConfigured : ApplicationUnity
    {
        public IApplicationConfig Config { get; }

        /// <summary>
        /// Gets or sets value that determines whether to use reverse order when uninitialize modules.
        /// </summary>
        public bool UseReverseModulesUninitialization { get; set; } = true;

        private readonly Dictionary<Type, IApplicationModule> m_modules = new Dictionary<Type, IApplicationModule>();
        private readonly List<IApplicationModule> m_order = new List<IApplicationModule>();

        public ApplicationConfigured(IApplicationConfig config, bool provideStaticInstance = false) : base(provideStaticInstance)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();

            OnCreateModules(Config);
        }

        protected override void OnInitializeModules()
        {
            for (int i = 0; i < m_order.Count; i++)
            {
                IApplicationModule module = m_order[i];

                module.Initialize();
            }
        }

        protected override async Task OnInitializeModulesAsync()
        {
            for (int i = 0; i < m_order.Count; i++)
            {
                IApplicationModule module = m_order[i];

                if (module is IApplicationModuleAsync moduleAsync)
                {
                    await moduleAsync.InitializeAsync();
                }
            }
        }

        protected override void OnUninitializeModules()
        {
            if (UseReverseModulesUninitialization)
            {
                for (int i = m_order.Count - 1; i >= 0; i--)
                {
                    IApplicationModule module = m_order[i];

                    module.Uninitialize();
                }
            }
            else
            {
                for (int i = 0; i < m_order.Count; i++)
                {
                    IApplicationModule module = m_order[i];

                    module.Uninitialize();
                }
            }
        }

        public override void AddModule(Type registerType, IApplicationModule module)
        {
            if (registerType == null) throw new ArgumentNullException(nameof(registerType));
            if (module == null) throw new ArgumentNullException(nameof(module));
            if (IsInitialized) throw new InvalidOperationException("Application modules can not be changed after initialization.");

            m_modules.Add(registerType, module);
            m_order.Add(module);
        }

        public override bool RemoveModule(Type registerType)
        {
            if (registerType == null) throw new ArgumentNullException(nameof(registerType));
            if (IsInitialized) throw new InvalidOperationException("Application modules can not be changed after initialization.");

            if (m_modules.TryGetValue(registerType, out IApplicationModule module))
            {
                m_modules.Remove(registerType);
                m_order.Remove(module);
                return true;
            }

            return false;
        }

        public override void ClearModules()
        {
            if (IsInitialized) throw new InvalidOperationException("Application modules can not be changed after initialization.");

            m_modules.Clear();
            m_order.Clear();
        }

        public override bool TryGetModule(Type registerType, out IApplicationModule module)
        {
            if (registerType == null) throw new ArgumentNullException(nameof(registerType));

            return m_modules.TryGetValue(registerType, out module);
        }

        public override IEnumerator<IApplicationModule> GetEnumerator()
        {
            return m_order.GetEnumerator();
        }

        protected virtual void OnCreateModules(IApplicationConfig config)
        {
            for (int i = 0; i < config.Modules.Count; i++)
            {
                IApplicationModuleInfo info = config.Modules[i];
                IApplicationModule module = info.Builder.Invoke(this);

                AddModule(info.RegisterType, module);
            }
        }
    }
}
