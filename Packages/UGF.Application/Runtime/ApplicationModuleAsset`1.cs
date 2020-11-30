using System;
using UGF.Description.Runtime;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleAsset<TModule> : ApplicationModuleAsset<TModule, ApplicationModuleDescription> where TModule : class, IApplicationModule, IDescribed<ApplicationModuleDescription>
    {
        protected override ApplicationModuleDescription OnBuildDescription()
        {
            return new ApplicationModuleDescription(typeof(TModule));
        }
    }
}
