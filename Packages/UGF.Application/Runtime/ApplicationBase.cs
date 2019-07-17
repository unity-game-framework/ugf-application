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

        public void AddModule<T>(IApplicationModule module) where T : IApplicationModule
        {
            AddModule(typeof(T), module);
        }

        public void AddModule(Type registerType, IApplicationModule module)
        {
            if (module == null) throw new ArgumentNullException(nameof(module));
            if (registerType == null) throw new ArgumentNullException(nameof(registerType));

            m_modules.Add(registerType, module);
        }

        public bool RemoveModule<T>() where T : IApplicationModule
        {
            return RemoveModule(typeof(T));
        }

        public bool RemoveModule(Type registerType)
        {
            if (registerType == null) throw new ArgumentNullException(nameof(registerType));

            return m_modules.Remove(registerType);
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

        public Type GetRegisterType(IApplicationModule module)
        {
            if (module == null) throw new ArgumentNullException(nameof(module));

            if (TryGetRegisterType(module, out Type type))
            {
                return type;
            }

            throw new ArgumentException($"The register not found for specified module: '{module}'.", nameof(module));
        }

        public bool TryGetRegisterType(IApplicationModule module, out Type registerType)
        {
            if (module == null) throw new ArgumentNullException(nameof(module));

            foreach (KeyValuePair<Type, IApplicationModule> pair in m_modules)
            {
                if (pair.Value == module)
                {
                    registerType = pair.Key;
                    return true;
                }
            }

            registerType = null;
            return false;
        }

        public Dictionary<Type, IApplicationModule>.Enumerator GetEnumerator()
        {
            return m_modules.GetEnumerator();
        }
    }
}
