using UGF.Description.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Initialize.Runtime;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Application.Runtime
{
    public interface IApplication : IInitializeAsync, IDescribed
    {
        IProvider<GlobalId, IApplicationModule> Provider { get; }

        T GetModule<T>() where T : class, IApplicationModule;
        T GetModule<T>(GlobalId id) where T : class, IApplicationModule;
    }
}
