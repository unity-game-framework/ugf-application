using System;

namespace UGF.Application.Runtime
{
    public class ApplicationSingleton : ApplicationConfigured
    {
        /// <summary>
        /// Gets the value that determines whether application provide static instance via <see cref="ApplicationInstance"/>.
        /// </summary>
        public bool ProvideStaticInstance { get; }

        public ApplicationSingleton(IApplicationResources resources, bool useReverseModulesUninitialization = true, bool provideStaticInstance = true) : base(resources, useReverseModulesUninitialization)
        {
            ProvideStaticInstance = provideStaticInstance;
        }

        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();

            if (ProvideStaticInstance)
            {
                if (ApplicationInstance.HasApplication) throw new InvalidOperationException("Application static instance already assigned.");

                ApplicationInstance.Application = this;
            }
        }

        protected override void OnPostUninitialize()
        {
            base.OnPostUninitialize();

            if (ProvideStaticInstance)
            {
                if (ApplicationInstance.Application != this) throw new InvalidOperationException("Application static instance already assigned by another application.");

                ApplicationInstance.Application = null;
            }
        }
    }
}
