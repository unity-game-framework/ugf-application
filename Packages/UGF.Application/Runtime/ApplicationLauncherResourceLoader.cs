using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    public class ApplicationLauncherResourceLoader : ApplicationLauncherResourceLoaderBase
    {
        public List<object> Resources { get; } = new List<object>();

        public override async Task<IApplicationResources> LoadAsync()
        {
            var resources = new ApplicationResources();

            for (int i = 0; i < Resources.Count; i++)
            {
                object resource = Resources[i];

                if (resource == null) throw new ArgumentNullException(nameof(resource), $"Resource not specified at index of the list: '{i}'.");

                if (resource is ApplicationResourceAsset asset)
                {
                    object result = await asset.BuildAsync();

                    resources.Add(result);
                }
                else
                {
                    resources.Add(resource);
                }
            }

            return resources;
        }
    }
}
