using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationAccessComponent : MonoBehaviour
    {
        public T GetApplication<T>() where T : class, IApplication
        {
            return (T)GetApplication();
        }

        public IApplication GetApplication()
        {
            return OnGetApplication();
        }

        protected abstract IApplication OnGetApplication();
    }
}
