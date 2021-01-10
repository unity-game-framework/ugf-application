using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    public interface IApplicationLauncher
    {
        IApplicationLauncherResourceLoader ResourceLoader { get; }
        IApplication Application { get; }
        bool HasApplication { get; }
        bool IsLaunched { get; }

        event ApplicationHandler Launched;
        event ApplicationHandler Stopped;

        Task Launch();
        void Stop();
    }
}
