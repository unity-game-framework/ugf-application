using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an abstract implementation of the <see cref="IApplication"/>.
    /// </summary>
    public abstract class ApplicationBase : InitializeBase, IApplication, IEnumerable<KeyValuePair<Type, IApplicationModule>>, IApplicationLauncherEventHandler
    {
        public IApplicationResources Resources { get; }
        public IReadOnlyDictionary<Type, IApplicationModule> Modules { get; }

        private readonly Dictionary<Type, IApplicationModule> m_modules = new Dictionary<Type, IApplicationModule>();

        protected ApplicationBase(IApplicationResources resources = null)
        {
            Resources = resources ?? new ApplicationResources();
            Modules = new ReadOnlyDictionary<Type, IApplicationModule>(m_modules);
        }

        public async Task InitializeAsync()
        {
            await OnInitializeAsync();
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            OnInitializeModules();
        }

        protected override void OnPreUninitialize()
        {
            base.OnPreUninitialize();

            OnUninitializeModules();
        }

        protected virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
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

        protected virtual void OnLaunched()
        {
            foreach (KeyValuePair<Type, IApplicationModule> pair in m_modules)
            {
                if (pair.Value is IApplicationLauncherEventHandler handler)
                {
                    handler.OnLaunched(this);
                }
            }
        }

        protected virtual void OnStopped()
        {
            foreach (KeyValuePair<Type, IApplicationModule> pair in m_modules)
            {
                if (pair.Value is IApplicationLauncherEventHandler handler)
                {
                    handler.OnStopped(this);
                }
            }
        }

        protected virtual void OnQuitting()
        {
            foreach (KeyValuePair<Type, IApplicationModule> pair in m_modules)
            {
                if (pair.Value is IApplicationLauncherEventHandler handler)
                {
                    handler.OnQuitting(this);
                }
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_modules.GetEnumerator();
        }

        IEnumerator<KeyValuePair<Type, IApplicationModule>> IEnumerable<KeyValuePair<Type, IApplicationModule>>.GetEnumerator()
        {
            return m_modules.GetEnumerator();
        }

        void IApplicationLauncherEventHandler.OnLaunched(IApplication application)
        {
            if (application != this) throw new ArgumentException("Application launcher event handler process another application.");

            OnLaunched();
        }

        void IApplicationLauncherEventHandler.OnStopped(IApplication application)
        {
            if (application != this) throw new ArgumentException("Application launcher event handler process another application.");

            OnStopped();
        }

        void IApplicationLauncherEventHandler.OnQuitting(IApplication application)
        {
            if (application != this) throw new ArgumentException("Application launcher event handler process another application.");

            OnQuitting();
        }
    }
}
