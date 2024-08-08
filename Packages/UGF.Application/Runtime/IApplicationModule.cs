using UGF.Description.Runtime;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    public interface IApplicationModule : IInitialize, IDescribed
    {
        IApplication Application { get; }
    }
}
