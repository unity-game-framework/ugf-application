using System;
using UnityEngine;

namespace UGF.Application.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Application/Application Config Project", order = 2000)]
    public class ApplicationConfigProjectAsset : ApplicationResourceAsset
    {
        public override object GetResource()
        {
            ApplicationResourceAsset asset = ApplicationSettings.Config ? ApplicationSettings.Config : throw new ArgumentException("Application config not specified in project settings.");
            object resource = asset.GetResource();

            return resource;
        }
    }
}
