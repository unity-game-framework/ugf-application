using System;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an abstract implementation of the <see cref="IApplicationModule"/>.
    /// </summary>
    public abstract class ApplicationModuleBase : InitializeBase, IApplicationModule
    {
        public IApplication Application { get; }

        protected ApplicationModuleBase(IApplication application)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
        }
    }
}
