using UGF.Application.Runtime;
using UGF.Description.Runtime;
using UnityEngine;

namespace UGF.Module.Runtime
{
    public abstract class ModuleBuilderAsset<TRegisterType, TDescription> : ModuleBuilderAsset<TRegisterType>
        where TRegisterType : IApplicationModule
        where TDescription : IDescription, new()
    {
        [SerializeField] private TDescription m_description = new TDescription();

        public TDescription Description { get { return m_description; } }

        protected override IApplicationModule OnBuild(IApplication application, IModuleBuildArguments<object> arguments)
        {
            return OnBuild(application, m_description);
        }

        protected abstract IApplicationModule OnBuild(IApplication application, TDescription description);
    }
}
