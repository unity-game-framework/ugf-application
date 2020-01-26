using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationResourceAsset : ScriptableObject
    {
        public abstract object GetResource();
    }
}
