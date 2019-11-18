using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an abstract implementation of the <see cref="IApplicationModule"/> with async initialization support.
    /// </summary>
    public abstract class ApplicationModuleBaseAsync : ApplicationModuleBase
    {
        public abstract Task InitializeAsync();
    }
}
