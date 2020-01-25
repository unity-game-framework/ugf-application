using System;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents an unity application implementation.
    /// </summary>
    public class ApplicationUnity : ApplicationBase
    {
        /// <summary>
        /// Gets the value that determines whether application provide static instance via <see cref="ApplicationInstance"/>.
        /// </summary>
        public bool ProvideStaticInstance { get; }

        /// <summary>
        /// Creates application with specified arguments.
        /// </summary>
        /// <param name="resources">The application resources.</param>
        /// <param name="provideStaticInstance">The value that determines whether to provide static instance via <see cref="ApplicationInstance"/>.</param>
        public ApplicationUnity(IApplicationResources resources = null, bool provideStaticInstance = false) : base(resources)
        {
            ProvideStaticInstance = provideStaticInstance;
        }

        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();

            if (ProvideStaticInstance)
            {
                if (ApplicationInstance.HasApplication)
                {
                    throw new InvalidOperationException("The Application static instance already assigned.");
                }

                ApplicationInstance.Application = this;
            }
        }

        protected override void OnPostUninitialize()
        {
            base.OnPostUninitialize();

            if (ProvideStaticInstance)
            {
                if (ApplicationInstance.Application != this)
                {
                    throw new InvalidOperationException("The Application static instance already assigned by another application.");
                }

                ApplicationInstance.Application = null;
            }
        }
    }
}
