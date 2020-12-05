using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Application.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Application/Application Config Project", order = 2000)]
    public class ApplicationConfigProjectAsset : ApplicationResourceAsset
    {
        protected override Task<object> OnBuildAsync()
        {
            ApplicationResourceAsset asset = ApplicationSettings.Config ? ApplicationSettings.Config : throw new ArgumentException("Application config not specified in project settings.");

            return asset.BuildAsync();
        }
    }
}
