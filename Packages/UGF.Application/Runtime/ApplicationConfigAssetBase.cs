using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationConfigAssetBase : ScriptableObject
    {
        public abstract IApplicationConfig GetConfig();
    }
}
