using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an abstract implementation of the <see cref="IApplication"/>.
    /// </summary>
    public abstract class ApplicationBase : InitializeBase, IApplication
    {
        public IApplicationResources Resources { get; }
        public IReadOnlyDictionary<Type, IApplicationModule> Modules { get; }

        private readonly Dictionary<Type, IApplicationModule> m_modules = new Dictionary<Type, IApplicationModule>();

        protected ApplicationBase(IApplicationResources resources)
        {
            Resources = resources ?? throw new ArgumentNullException(nameof(resources));
            Modules = new ReadOnlyDictionary<Type, IApplicationModule>(m_modules);
        }

        public async Task InitializeAsync()
        {
            await OnInitializeAsync();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            OnInitializeModules();
        }

        protected virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            OnUninitializeModules();
        }

        /// <summary>
        /// Invoked on modules initialization.
        /// </summary>
        protected virtual void OnInitializeModules()
        {
            foreach (KeyValuePair<Type, IApplicationModule> pair in m_modules)
            {
                pair.Value.Initialize();
            }
        }

        /// <summary>
        /// Invoked on modules uninitialization.
        /// </summary>
        protected virtual void OnUninitializeModules()
        {
            foreach (KeyValuePair<Type, IApplicationModule> pair in m_modules)
            {
                pair.Value.Uninitialize();
            }
        }

        public void AddModule<T>(T module) where T : class, IApplicationModule
        {
            AddModule(typeof(T), module);
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
            if (!TryGetModule(out T module))
            {
                throw new ArgumentException($"No module found by the specified type: '{typeof(T)}'.");
            }

            return module;
        }

        public bool TryGetModule<T>(out T module) where T : IApplicationModule
        {
            if (m_modules.TryGetValue(typeof(T), out IApplicationModule result) && result is T cast)
            {
                module = cast;
                return true;
            }

            module = default;
            return false;
        }

        public Type GetRegisterType(IApplicationModule module)
        {
            if (module == null) throw new ArgumentNullException(nameof(module));

            if (!TryGetRegisterType(module, out Type type))
            {
                throw new ArgumentException($"The register type not found for specified module: '{module}'.", nameof(module));
            }

            return type;
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
