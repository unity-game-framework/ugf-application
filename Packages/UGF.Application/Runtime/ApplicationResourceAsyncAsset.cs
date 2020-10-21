using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationResourceAsyncAsset : ScriptableObject
    {
        public abstract Task<object> GetResourceAsync();
    }
}
