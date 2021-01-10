using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationLauncherResourceLoader : MonoBehaviour, IApplicationLauncherResourceLoader
    {
        public abstract Task<IApplicationResources> LoadAsync();
    }
}
