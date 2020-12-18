using System;

namespace UGF.Application.Runtime.Scenes
{
    public static class ApplicationSceneProviderInstance
    {
        public static IApplicationSceneProvider Provider { get { return m_provider; } set { m_provider = value ?? throw new ArgumentNullException(nameof(value)); } }

        private static IApplicationSceneProvider m_provider = new ApplicationSceneProvider();
    }
}
