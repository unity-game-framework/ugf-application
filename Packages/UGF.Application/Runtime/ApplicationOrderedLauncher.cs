﻿using UnityEngine;

namespace UGF.Application.Runtime
{
    [AddComponentMenu("Unity Game Framework/Application/Application Ordered Launcher", 2000)]
    public class ApplicationOrderedLauncher : ApplicationLauncher
    {
        [SerializeField] private bool m_useReverseModulesUninitialization = true;

        public bool UseReverseModulesUninitialization { get { return m_useReverseModulesUninitialization; } set { m_useReverseModulesUninitialization = value; } }

        protected override IApplication OnCreateApplication(IApplicationResources resources)
        {
            return new ApplicationOrdered(resources, m_useReverseModulesUninitialization);
        }
    }
}
