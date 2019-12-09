using System;
using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Module.Runtime
{
    public abstract class ModuleBuilderAsset : ScriptableObject, IModuleBuilder
    {
        public abstract Type RegisterType { get; }

        public IApplicationModule Build(IApplication application, IModuleBuildArguments<object> arguments)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));

            return OnBuild(application, arguments);
        }

        protected abstract IApplicationModule OnBuild(IApplication application, IModuleBuildArguments<object> arguments);
    }
}
