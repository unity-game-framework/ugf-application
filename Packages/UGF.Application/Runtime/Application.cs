using System;
using System.Threading.Tasks;
using UGF.Builder.Runtime;
using UGF.Description.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Initialize.Runtime;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Providers;

// ReSharper disable SuspiciousTypeConversion.Global

namespace UGF.Application.Runtime
{
    public class Application : InitializableAsync, IApplication, IApplicationLauncherEventHandler
    {
        public ApplicationDescription Description { get; }
        public Provider<GlobalId, IApplicationModule> Provider { get; } = new Provider<GlobalId, IApplicationModule>();

        IProvider<GlobalId, IApplicationModule> IApplication.Provider { get { return Provider; } }

        private readonly ILog m_log;
        private readonly InitializeCollection<IApplicationModule> m_initialize = new InitializeCollection<IApplicationModule>(true);

        public Application(ApplicationDescription description)
        {
            m_log = Log.CreateWithLabel<Application>();

            Description = description ?? throw new ArgumentNullException(nameof(description));

            Provider.Added += OnModuleAdded;
            Provider.Removed += OnModuleRemoved;
            Provider.Cleared += OnModuleCleared;
        }

        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();

            if (Description.ProvideStaticInstance)
            {
                if (ApplicationInstance.HasApplication) throw new InvalidOperationException("Application static instance already assigned.");

                ApplicationInstance.Application = this;

                m_log.Debug("Singleton provide static instance", new
                {
                    application = this
                });
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach ((GlobalId id, IBuilder<IApplication, IApplicationModule> builder) in Description.Modules)
            {
                IApplicationModule module = builder.Build(this);

                Provider.Add(id, module);
            }

            m_initialize.Initialize();
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();
            await m_initialize.InitializeAsync();
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            m_initialize.Uninitialize();
        }

        protected override void OnPostUninitialize()
        {
            base.OnPostUninitialize();

            if (Description.ProvideStaticInstance)
            {
                if (ApplicationInstance.Application != this) throw new InvalidOperationException("Application static instance already assigned by another application.");

                ApplicationInstance.Application = null;

                m_log.Debug("Singleton clear static instance", new
                {
                    application = this
                });
            }
        }

        public T GetDescription<T>() where T : class, IDescription
        {
            return (T)GetDescription();
        }

        public IDescription GetDescription()
        {
            return Description;
        }

        public T GetModule<T>() where T : class, IApplicationModule
        {
            return Provider.Get<T>();
        }

        public T GetModule<T>(GlobalId id) where T : class, IApplicationModule
        {
            return Provider.Get<T>(id);
        }

        private void OnModuleAdded(IProvider provider, GlobalId id, IApplicationModule entry)
        {
            m_initialize.Add(entry);
        }

        private void OnModuleRemoved(IProvider provider, GlobalId id, IApplicationModule entry)
        {
            m_initialize.Remove(entry);
        }

        private void OnModuleCleared(IProvider provider)
        {
            m_initialize.Clear();
        }

        void IApplicationLauncherEventHandler.OnLaunched(IApplication application)
        {
            if (application != this) throw new ArgumentException("Application launcher event handler process another application.");

            foreach ((_, IApplicationModule module) in Provider)
            {
                if (module is IApplicationLauncherEventHandler handler)
                {
                    handler.OnLaunched(this);
                }
            }
        }

        void IApplicationLauncherEventHandler.OnStopped(IApplication application)
        {
            if (application != this) throw new ArgumentException("Application launcher event handler process another application.");

            foreach ((_, IApplicationModule module) in Provider)
            {
                if (module is IApplicationLauncherEventHandler handler)
                {
                    handler.OnStopped(this);
                }
            }
        }
    }
}
