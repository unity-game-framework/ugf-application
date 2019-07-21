using System;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Provides static access to an application instance.
    /// </summary>
    public static class ApplicationInstance
    {
        /// <summary>
        /// Gets or sets an instance of the application.
        /// </summary>
        public static IApplication Application
        {
            get
            {
                if (m_application == null)
                {
                    throw new InvalidOperationException("The application has not been assigned.");
                }

                return m_application;
            }
            set { m_application = value; }
        }

        /// <summary>
        /// Gets the value that determines whether instance of the application is specified.
        /// </summary>
        public static bool HasApplication { get { return m_application != null; } }

        private static IApplication m_application;
    }
}
