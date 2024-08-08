using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.Description.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Application.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Application/Application Description", order = 2000)]
    public class ApplicationDescriptionAsset : DescribedWithDescriptionBuilderAsset<Application, ApplicationDescription>
    {
        [SerializeField] private bool m_provideStaticInstance = true;
        [SerializeField] private List<AssetIdReference<ApplicationModuleAsset>> m_modules = new List<AssetIdReference<ApplicationModuleAsset>>();

        public bool ProvideStaticInstance { get { return m_provideStaticInstance; } set { m_provideStaticInstance = value; } }
        public List<AssetIdReference<ApplicationModuleAsset>> Modules { get { return m_modules; } }

        protected override ApplicationDescription OnBuildDescription()
        {
            var modules = new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>();

            for (int i = 0; i < m_modules.Count; i++)
            {
                AssetIdReference<ApplicationModuleAsset> reference = m_modules[i];

                modules.Add(reference.Guid, reference.Asset);
            }

            return new ApplicationDescription(
                m_provideStaticInstance,
                modules
            );
        }

        protected override Application OnBuild(ApplicationDescription description)
        {
            return new Application(description);
        }
    }
}
