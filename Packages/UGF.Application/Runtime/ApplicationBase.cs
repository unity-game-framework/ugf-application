using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationBase : InitializeBase, IApplication
    {
        public IReadOnlyDictionary<Type, IApplicationModule> Modules { get; }

        private readonly Dictionary<Type, IApplicationModule> m_modules = new Dictionary<Type, IApplicationModule>();

        protected ApplicationBase()
        {
            Modules = new ReadOnlyDictionary<Type, IApplicationModule>(m_modules);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (KeyValuePair<Type, IApplicationModule> pair in m_modules)
            {
                pair.Value.Initialize();
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            foreach (KeyValuePair<Type, IApplicationModule> pair in m_modules)
            {
                pair.Value.Uninitialize();
            }
        }

        public void AddModule(IApplicationModule module)
        {
            if (module == null) throw new ArgumentNullException(nameof(module));

            m_modules.Add(module.RegisterType, module);
        }

        public bool RemoveModule(Type moduleRegisterType)
        {
            if (moduleRegisterType == null) throw new ArgumentNullException(nameof(moduleRegisterType));

            return m_modules.Remove(moduleRegisterType);
        }

        public void ClearModules()
        {
            m_modules.Clear();
        }

        public T GetModule<T>() where T : IApplicationModule
        {
            return (T)m_modules[typeof(T)];
        }

        public bool TryGetModule<T>(out T module) where T : IApplicationModule
        {
            if (m_modules.TryGetValue(typeof(T), out IApplicationModule applicationModule) && applicationModule is T resultModule)
            {
                module = resultModule;
                return true;
            }

            module = default;
            return false;
        }
    }
}
