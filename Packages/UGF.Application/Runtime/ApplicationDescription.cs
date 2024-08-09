using System;
using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.Description.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Application.Runtime
{
    public class ApplicationDescription : DescriptionBase, IApplicationDescription
    {
        public bool ProvideStaticInstance { get; }
        public IReadOnlyDictionary<GlobalId, IBuilder<IApplication, IApplicationModule>> Modules { get; }

        public ApplicationDescription(
            bool provideStaticInstance,
            IReadOnlyDictionary<GlobalId, IBuilder<IApplication, IApplicationModule>> modules)
        {
            ProvideStaticInstance = provideStaticInstance;
            Modules = modules ?? throw new ArgumentNullException(nameof(modules));
        }
    }
}
