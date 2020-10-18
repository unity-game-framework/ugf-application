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
        public async Task InitializeAsync()
        {
            await OnInitializeAsync();
            await OnInitializeModulesAsync();
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

        protected override void OnPostUninitialize()
        {
            base.OnPostUninitialize();

            ClearModules();
        }

        protected virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
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

        public void AddModule<T>(T module) where T : class, IApplicationModule
        {
            AddModule(typeof(T), module);
        }

        public void AddModule<T>(IApplicationModule module) where T : class, IApplicationModule
        {
            AddModule(typeof(T), module);
        }

        public abstract void AddModule(Type registerType, IApplicationModule module);

        public bool RemoveModule<T>() where T : class, IApplicationModule
        {
            return RemoveModule(typeof(T));
        }

        public abstract bool RemoveModule(Type registerType);

        public abstract void ClearModules();

        public T GetModule<T>() where T : class, IApplicationModule
        {
            return TryGetModule(out T module) ? module : throw new ArgumentException($"Module not found by the specified type: '{typeof(T)}'.");
        }

        public bool TryGetModule<T>(out T module) where T : class, IApplicationModule
        {
            if (TryGetModule(typeof(T), out IApplicationModule value) && value is T result)
            {
                module = result;
                return true;
            }

            module = default;
            return false;
        }

        public abstract bool TryGetModule(Type registerType, out IApplicationModule module);

        public abstract IEnumerator<IApplicationModule> GetEnumerator();

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
