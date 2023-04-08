using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleCollectionAsset : ScriptableObject
    {
        public void GetModules(ICollection<IApplicationModuleBuilder> modules)
        {
            if (modules == null) throw new ArgumentNullException(nameof(modules));
        }

        protected abstract void OnGetModules(ICollection<IApplicationModuleBuilder> modules);
    }
}
