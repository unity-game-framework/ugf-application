using System.Collections.Generic;
using System.Threading.Tasks;

namespace UGF.Application.Runtime
{
    public static class ApplicationUtility
    {
        public static async Task AddResources(IApplicationResources resources, IEnumerable<object> resourcesToAdd)
        {
            foreach (object element in resources)
            {
                switch (element)
                {
                    case ApplicationResourceAsset asset:
                    {
                        object resource = asset.GetResource();

                        resources.Add(resource);
                        break;
                    }
                    case ApplicationResourceAsyncAsset assetAsync:
                    {
                        object resource = await assetAsync.GetResourceAsync();

                        resources.Add(resource);
                        break;
                    }
                    default:
                    {
                        resources.Add(element);
                        break;
                    }
                }
            }
        }
    }
}
