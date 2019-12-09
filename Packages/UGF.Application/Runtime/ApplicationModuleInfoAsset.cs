using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationModuleInfoAsset : ScriptableObject
    {
        public abstract IApplicationModuleInfo GetInfo();
    }
}
