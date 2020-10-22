using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an abstract implementation of the <see cref="IApplication"/>.
    /// </summary>
    public abstract class ApplicationBase : InitializeBase, IApplication, IEnumerable<IApplicationModule>, IApplicationLauncherEventHandler
    {
        public IApplicationResources Resources { get; }

        protected ApplicationBase(IApplicationResources resources)
        {
            Resources = resources ?? throw new ArgumentNullException(nameof(resources));
        }

        public async Task InitializeAsync()
        {
            await OnInitializeAsync();
            await OnInitializeModulesAsync();
        }

        public bool HasModule<T>() where T : class, IApplicationModule
        {
            return HasModule(typeof(T));
        }

        public bool HasModule(Type registerType)
        {
            if (registerType == null) throw new ArgumentNullException(nameof(registerType));

            return OnHasModule(registerType);
        }

        public bool HasModule(IApplicationModule module)
        {
            if (module == null) throw new ArgumentNullException(nameof(module));

            return OnHasModule(module);
        }

        public void AddModule<T>(T module) where T : class, IApplicationModule
        {
            AddModule(typeof(T), module);
        }

        public void AddModule<T>(IApplicationModule module) where T : class, IApplicationModule
        {
            AddModule(typeof(T), module);
        }

        public void AddModule(Type registerType, IApplicationModule module)
        {
            if (registerType == null) throw new ArgumentNullException(nameof(registerType));
            if (module == null) throw new ArgumentNullException(nameof(module));
            if (module.Application != this) throw new ArgumentException($"Can not add module from another application: '{module.Application}'.");

            OnAddModule(registerType, module);
        }

        public bool RemoveModule<T>() where T : class, IApplicationModule
        {
            return RemoveModule(typeof(T));
        }

        public bool RemoveModule(Type registerType)
        {
            if (registerType == null) throw new ArgumentNullException(nameof(registerType));

            return OnRemoveModule(registerType);
        }

        public void ClearModules()
        {
            OnClearModules();
        }

        public T GetModule<T>() where T : class, IApplicationModule
        {
            return TryGetModule(out T module) ? module : throw new ArgumentException($"Module not found by the specified type: '{typeof(T)}'.");
        }

        public bool TryGetModule<T>(out T module) where T : class, IApplicationModule
        {
            if (TryGetModule(typeof(T), out IApplicationModule value))
            {
                module = (T)value;
                return true;
            }

            module = default;
            return false;
        }

        public bool TryGetModule(Type registerType, out IApplicationModule module)
        {
            if (registerType == null) throw new ArgumentNullException(nameof(registerType));

            return OnTryGetModule(registerType, out module);
        }

        public IEnumerator<IApplicationModule> GetEnumerator()
        {
            return OnGetEnumerator();
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            OnInitializeModules();
        }

        protected virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected override void OnPreUninitialize()
        {
            base.OnPreUninitialize();

            OnUninitializeModules();
        }

        /// <summary>
        /// Invoked on modules initialization.
        /// </summary>
        protected abstract void OnInitializeModules();

        /// <summary>
        /// Invoked after all modules created and initialized.
        /// </summary>
        protected abstract Task OnInitializeModulesAsync();

        /// <summary>
        /// Invoked on modules uninitialization.
        /// </summary>
        protected abstract void OnUninitializeModules();

        protected abstract bool OnHasModule(Type registerType);
        protected abstract bool OnHasModule(IApplicationModule module);
        protected abstract void OnAddModule(Type registerType, IApplicationModule module);
        protected abstract bool OnRemoveModule(Type registerType);
        protected abstract void OnClearModules();
        protected abstract bool OnTryGetModule(Type registerType, out IApplicationModule module);
        protected abstract IEnumerator<IApplicationModule> OnGetEnumerator();

        /// <summary>
        /// Invoked after application creation and initialization.
        /// </summary>
        protected virtual void OnLaunched()
        {
        }

        /// <summary>
        /// Invoked before uninitialize and destroy application.
        /// </summary>
        protected virtual void OnStopped()
        {
        }

        /// <summary>
        /// Invoked when Unity performs quitting and before active application stops.
        /// </summary>
        protected virtual void OnQuitting()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
