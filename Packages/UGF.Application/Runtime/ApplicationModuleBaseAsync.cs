using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an abstract implementation of the <see cref="IApplicationModule"/> with async initialization support.
    /// </summary>
    public class ApplicationModuleBaseAsync : InitializeBaseAsync, IApplicationModule
    {
    }
}
