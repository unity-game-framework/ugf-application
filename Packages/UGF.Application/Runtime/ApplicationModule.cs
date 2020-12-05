using System;
using UGF.Description.Runtime;
using UGF.Initialize.Runtime;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationModule<TDescription> : InitializeBase, IApplicationModule where TDescription : class, IApplicationModuleDescription
    {
        public TDescription Description { get; }
        public IApplication Application { get; }

        IApplicationModuleDescription IDescribed<IApplicationModuleDescription>.Description { get { return Description; } }

        protected ApplicationModule(TDescription description, IApplication application)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public T GetDescription<T>() where T : class, IDescription
        {
            return (T)GetDescription();
        }

        public IDescription GetDescription()
        {
            return Description;
        }
    }
}
