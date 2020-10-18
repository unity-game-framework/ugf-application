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

        public int Count { get { return m_modules.Count; } }

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

        protected virtual void OnCreateModules(IApplicationConfig config)
        {
            for (int i = 0; i < config.Modules.Count; i++)
            {
                IApplicationModuleInfo info = config.Modules[i];
                IApplicationModule module = info.Builder.Invoke(this);

                AddModule(info.RegisterType, module);
            }
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

        protected override bool OnHasModule(Type registerType)
        {
            return m_modules.ContainsKey(registerType);
        }

        protected override bool OnHasModule(IApplicationModule module)
        {
            return m_order.Contains(module);
        }

        protected override void OnAddModule(Type registerType, IApplicationModule module)
        {
            if (IsInitialized) throw new InvalidOperationException("Application modules can not be changed after initialization.");

            m_modules.Add(registerType, module);
            m_order.Add(module);
        }

        protected override bool OnRemoveModule(Type registerType)
        {
            if (IsInitialized) throw new InvalidOperationException("Application modules can not be changed after initialization.");

            if (m_modules.TryGetValue(registerType, out IApplicationModule module))
            {
                m_modules.Remove(registerType);
                m_order.Remove(module);
                return true;
            }

            return false;
        }

        protected override void OnClearModules()
        {
            if (IsInitialized) throw new InvalidOperationException("Application modules can not be changed after initialization.");

            m_modules.Clear();
            m_order.Clear();
        }

        protected override bool OnTryGetModule(Type registerType, out IApplicationModule module)
        {
            return m_modules.TryGetValue(registerType, out module);
        }

        protected override IEnumerator<IApplicationModule> OnGetEnumerator()
        {
            return m_order.GetEnumerator();
        }
    }
}
