using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationLauncherResourceLoaderBase : IApplicationLauncherResourceLoader
    {
        public abstract Task<IApplicationResources> LoadAsync();
    }
}
