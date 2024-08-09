using System.Threading.Tasks;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    public interface IApplicationLauncher : IInitialize
    {
        IApplication Application { get; }
        bool HasApplication { get; }
        bool IsLaunched { get; }

        event ApplicationHandler Launched;
        event ApplicationHandler Stopped;

        Task LaunchAsync();
        void Stop();
    }
}
