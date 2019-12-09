using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationConfigLoader : MonoBehaviour
    {
        public abstract Task<IApplicationConfig> Load();
    }
}
