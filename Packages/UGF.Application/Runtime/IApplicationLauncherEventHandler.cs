namespace UGF.Application.Runtime
{
    public interface IApplicationLauncherEventHandler
    {
        void OnLaunched(IApplication application);
        void OnStopped(IApplication application);
    }
}
