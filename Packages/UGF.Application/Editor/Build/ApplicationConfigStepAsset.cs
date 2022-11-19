using UGF.Application.Runtime;
using UGF.Build.Editor;
using UnityEngine;

namespace UGF.Application.Editor.Build
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Application/Application Config Step", order = 2000)]
    public class ApplicationConfigStepAsset : BuildStepAsset
    {
        [SerializeField] private ApplicationConfigAsset m_config;

        public ApplicationConfigAsset Config { get { return m_config; } set { m_config = value; } }

        protected override IBuildStep OnBuild()
        {
            return new ApplicationConfigStep(m_config);
        }
    }
}
