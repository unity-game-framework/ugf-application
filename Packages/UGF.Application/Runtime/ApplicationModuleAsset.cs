using System;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleAsset : ScriptableObject, IApplicationModuleAsset
    {
        public abstract Type RegisterType { get; }

        public T Build<T>(IApplication application) where T : class, IApplicationModule
        {
            if (application == null) throw new ArgumentNullException(nameof(application));

            return (T)OnBuild(application);
        }

        public IApplicationModule Build(IApplication application)
        {
            if (application == null) throw new ArgumentNullException(nameof(application));

            return OnBuild(application);
        }

        protected abstract IApplicationModule OnBuild(IApplication application);
    }
}
