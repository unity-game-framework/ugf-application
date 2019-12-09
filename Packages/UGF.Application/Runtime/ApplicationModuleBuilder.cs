using System;

namespace UGF.Application.Runtime
{
    public class ApplicationModuleBuilder : IApplicationModuleBuilder
    {
        private readonly ApplicationModuleBuildHandler m_handler;

        public ApplicationModuleBuilder(ApplicationModuleBuildHandler handler)
        {
            m_handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public IApplicationModule Build(IApplication application)
        {
            return m_handler(application);
        }
    }
}
