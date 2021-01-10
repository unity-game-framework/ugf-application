using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    public interface IApplicationLauncherResourceLoader
    {
        Task<IApplicationResources> LoadAsync();
    }
}
