using System;

namespace UGF.Application.Runtime
{
    public class ApplicationModuleDescribed<TDescription> : ApplicationModuleBase, IApplicationModuleDescribed where TDescription : class, IApplicationModuleDescription
    {
        public TDescription Description { get; }

        IApplicationModuleDescription IApplicationModuleDescribed.Description { get { return Description; } }

        public ApplicationModuleDescribed(IApplication application, TDescription description) : base(application)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
