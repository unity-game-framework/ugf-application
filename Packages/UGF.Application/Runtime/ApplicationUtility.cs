using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    public static class ApplicationUtility
    {
        public static async Task AddResources(IApplicationResources resources, IEnumerable<object> resourcesToAdd)
        {
            if (resources == null) throw new ArgumentNullException(nameof(resources));
            if (resourcesToAdd == null) throw new ArgumentNullException(nameof(resourcesToAdd));

            foreach (object element in resourcesToAdd)
            {
                object resource = element switch
                {
                    ApplicationResourceAsset asset => asset.GetResource(),
                    ApplicationResourceAsyncAsset assetAsync => await assetAsync.GetResourceAsync(),
                    _ => element
                } ?? throw new ArgumentException($"Resource not provided: '{element}'.");

                resources.Add(resource);
            }
        }
    }
}
