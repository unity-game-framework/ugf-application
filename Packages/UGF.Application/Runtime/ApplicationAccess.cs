using UnityEngine;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationAccess : MonoBehaviour
    {
        public IApplication GetApplication()
        {
            return OnGetApplication();
        }

        protected abstract IApplication OnGetApplication();
    }
}
