using UnityEngine;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application", 2000)]
    public class ApplicationComponent : MonoBehaviour
    {
        [SerializeField] private ApplicationDescriptionAsset m_description;

        public ApplicationDescriptionAsset Description { get { return m_description; } set { m_description = value; } }
    }
}
