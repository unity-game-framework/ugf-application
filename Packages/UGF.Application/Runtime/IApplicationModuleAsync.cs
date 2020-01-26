using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    public interface IApplicationModuleAsync
    {
        Task InitializeAsync();
    }
}
