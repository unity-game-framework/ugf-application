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
        public static IApplication Application { get; set; }

        /// <summary>
        /// Gets the value that determines whether instance of the application is specified.
        /// </summary>
        public static bool HasApplication { get { return Application != null; } }
    }
}
