namespace UGF.Application.Runtime
{
    public interface IApplicationLauncherEventHandler
    {
        /// <summary>
        /// Invoked after application creation and initialization.
        /// </summary>
        /// <param name="application">The active and initialized application.</param>
        void OnLaunched(IApplication application);

        /// <summary>
        /// Invoked before uninitialize and destroy application.
        /// </summary>
        /// <param name="application">The active and initialized application.</param>
        void OnStopped(IApplication application);

        /// <summary>
        /// Invoked when Unity performs quitting and before active application stops.
        /// </summary>
        /// <param name="application">The active and initialized application.</param>
        void OnQuitting(IApplication application);
    }
}
