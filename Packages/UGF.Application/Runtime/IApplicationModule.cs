using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an application module.
    /// </summary>
    public interface IApplicationModule : IInitialize
    {
        IApplication Application { get; }
    }
}
