using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    public class ApplicationOrdered : ApplicationBase
    {
        public int Count { get { return m_modules.Count; } }

        /// <summary>
        /// Gets or sets value that determines whether to use reverse order when uninitialize modules.
        /// </summary>
        public bool UseReverseModulesUninitialization { get; }

        private readonly Dictionary<Type, IApplicationModule> m_modules = new Dictionary<Type, IApplicationModule>();
        private readonly List<IApplicationModule> m_order = new List<IApplicationModule>();

        public ApplicationOrdered(IApplicationResources resources, bool useReverseModulesUninitialization = true) : base(resources)
        {
            UseReverseModulesUninitialization = useReverseModulesUninitialization;
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
            m_modules.Add(registerType, module);
            m_order.Add(module);
        }

        protected override bool OnRemoveModule(Type registerType)
        {
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
