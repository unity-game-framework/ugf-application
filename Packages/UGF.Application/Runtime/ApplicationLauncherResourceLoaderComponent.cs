using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationLauncherResourceLoaderComponent : MonoBehaviour, IApplicationLauncherResourceLoader
    {
        public abstract Task<IApplicationResources> LoadAsync();
    }
}
