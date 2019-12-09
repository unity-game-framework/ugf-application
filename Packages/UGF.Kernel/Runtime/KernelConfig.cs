using System;
using System.Collections.Generic;
using UGF.Module.Runtime;
using UnityEngine;

namespace UGF.Kernel.Runtime
{
    [Serializable]
    public class KernelConfig : IKernelConfig
    {
        [SerializeField] private string m_name = "Default";
        [SerializeField] private List<KernelModuleInfo> m_modules = new List<KernelModuleInfo>();

        public string Name { get { return m_name; } set { m_name = value; } }
        public List<KernelModuleInfo> Modules { get { return m_modules; } set { m_modules = value; } }

        IReadOnlyList<IModuleBuildInfo> IKernelConfig.Modules { get { return m_modules; } }
    }
}
