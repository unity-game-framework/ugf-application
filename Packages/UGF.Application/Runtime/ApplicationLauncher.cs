using System;
using System.Threading.Tasks;
using UGF.Initialize.Runtime;
using UGF.Logs.Runtime;

namespace UGF.Application.Runtime
{
    public class ApplicationLauncher : InitializeBase, IApplicationLauncher
    {
        public IApplicationBuilder Builder { get; }
        public IApplicationLauncherResourceLoader ResourceLoader { get; }
        public IApplication Application { get { return m_application ?? throw new InvalidOperationException("Application not exists."); } }
        public bool HasApplication { get { return m_application != null; } }
        public bool IsLaunched { get { return m_state; } }

        public event ApplicationHandler Launched;
        public event ApplicationHandler Stopped;

        private InitializeState m_state;
        private IApplication m_application;

        public ApplicationLauncher(IApplicationBuilder builder, IApplicationLauncherResourceLoader resourceLoader)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
            ResourceLoader = resourceLoader ?? throw new ArgumentNullException(nameof(resourceLoader));
        }

        public async Task LaunchAsync()
        {
            m_state = m_state.Initialize();

            OnLaunch();

            IApplicationResources resources = await ResourceLoader.LoadAsync() ?? throw new ArgumentNullException(nameof(resources), "Application Resources can not be null.");
            IApplication application = OnCreateApplication(resources) ?? throw new ArgumentNullException(nameof(application), "Application can not be null.");

            OnInitializeApplication(application);

            await OnInitializeApplicationAsync(application);

            m_application = application;

            OnLaunched(application);

            Launched?.Invoke(application);
        }

        public void Stop()
        {
            m_state = m_state.Uninitialize();

            IApplication application = Application;

            OnStopped(application);

            Stopped?.Invoke(application);

            UninitializeApplication(application);

            m_application = null;
        }

        protected virtual void OnLaunch()
        {
            Log.Debug("Application launching.");
        }

        protected virtual IApplication OnCreateApplication(IApplicationResources resources)
        {
            return Builder.Build(resources);
        }

        protected virtual void OnInitializeApplication(IApplication application)
        {
            Log.Debug("Application initialization", new
            {
                application
            });

            application.Initialize();
        }

        protected virtual Task OnInitializeApplicationAsync(IApplication application)
        {
            Log.Debug("Application async initialization", new
            {
                application
            });

            return application.InitializeAsync();
        }

        protected virtual void UninitializeApplication(IApplication application)
        {
            Log.Debug("Application uninitialize", new
            {
                application
            });

            application.Uninitialize();
        }

        protected virtual void OnLaunched(IApplication application)
        {
            if (application is IApplicationLauncherEventHandler handler)
            {
                handler.OnLaunched(application);
            }

            Log.Debug("Application launched", new
            {
                application
            });
        }

        protected virtual void OnStopped(IApplication application)
        {
            if (application is IApplicationLauncherEventHandler handler)
            {
                handler.OnStopped(application);
            }

            Log.Debug("Application stopped", new
            {
                application
            });
        }
    }
}
