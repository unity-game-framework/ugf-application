using System;
using UGF.Module.Runtime;
using UnityEngine;

namespace UGF.Kernel.Runtime
{
    [Serializable]
    public class KernelModuleInfo : IModuleBuildInfo
    {
        [SerializeField] private bool m_active = true;
        [SerializeField] private ModuleBuilderAsset m_builder;

        public bool Active { get { return m_active; } set { m_active = value; } }
        public ModuleBuilderAsset Builder { get { return m_builder; } set { m_builder = value; } }
        public IModuleBuildArguments<object> Arguments { get; } = ModuleBuildArguments<object>.Empty;

        IModuleBuilder IModuleBuildInfo.Builder { get { return m_builder; } }
    }
}
