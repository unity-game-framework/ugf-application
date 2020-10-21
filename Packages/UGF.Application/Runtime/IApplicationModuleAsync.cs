using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    public interface IApplicationModuleAsync : IApplicationModule
    {
        Task InitializeAsync();
    }
}
