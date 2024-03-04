using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    public class ApplicationOrdered : ApplicationBase
    {
        public int Count { get { return m_modules.Count; } }

        /// <summary>
        /// Gets or sets value that determines whether to use reverse order when uninitialize modules.
        /// </summary>
        public bool UseReverseModulesUninitialization { get { return m_initialize.ReverseUninitializationOrder; } }

        private readonly Dictionary<Type, IApplicationModule> m_modules = new Dictionary<Type, IApplicationModule>();
        private readonly InitializeCollection<IApplicationModule> m_initialize;

        public ApplicationOrdered(IApplicationResources resources, bool useReverseModulesUninitialization = true) : base(resources)
        {
            m_initialize = new InitializeCollection<IApplicationModule>(useReverseModulesUninitialization);
        }

        public new List<IApplicationModule>.Enumerator GetEnumerator()
        {
            return m_initialize.GetEnumerator();
        }

        protected override void OnInitializeModules()
        {
            m_initialize.Initialize();
        }

        protected override async Task OnInitializeModulesAsync()
        {
            for (int i = 0; i < m_initialize.Count; i++)
            {
                IApplicationModule module = m_initialize[i];

                if (module is IApplicationModuleAsync moduleAsync)
                {
                    await moduleAsync.InitializeAsync();
                }
            }
        }

        protected override void OnUninitializeModules()
        {
            m_initialize.Uninitialize();
        }

        protected override bool OnHasModule(Type registerType)
        {
            return m_modules.ContainsKey(registerType);
        }

        protected override bool OnHasModule(IApplicationModule module)
        {
            return m_initialize.Contains(module);
        }

        protected override void OnAddModule(Type registerType, IApplicationModule module)
        {
            m_modules.Add(registerType, module);
            m_initialize.Add(module);
        }

        protected override bool OnRemoveModule(Type registerType)
        {
            if (m_modules.Remove(registerType, out IApplicationModule module))
            {
                m_initialize.Remove(module);
                return true;
            }

            return false;
        }

        protected override void OnClearModules()
        {
            m_modules.Clear();
            m_initialize.Clear();
        }

        protected override bool OnTryGetModule(Type registerType, out IApplicationModule module)
        {
            if (!m_modules.TryGetValue(registerType, out module))
            {
                foreach ((_, IApplicationModule applicationModule) in m_modules)
                {
                    if (registerType.IsInstanceOfType(applicationModule))
                    {
                        module = applicationModule;
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

        protected override IEnumerator<IApplicationModule> OnGetEnumerator()
        {
            return GetEnumerator();
        }

        protected override void OnLaunched()
        {
            base.OnLaunched();

            foreach (IApplicationModule module in m_initialize)
            {
                if (module is IApplicationLauncherEventHandler handler)
                {
                    handler.OnLaunched(this);
                }
            }
        }

        protected override void OnStopped()
        {
            base.OnStopped();

            foreach (IApplicationModule module in m_initialize)
            {
                if (module is IApplicationLauncherEventHandler handler)
                {
                    handler.OnStopped(this);
                }
            }
        }
    }
}
