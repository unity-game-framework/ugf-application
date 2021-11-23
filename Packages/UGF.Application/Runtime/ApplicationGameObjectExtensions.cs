using System;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Application.Runtime
{
    public static class ApplicationGameObjectExtensions
    {
        public static IApplication GetApplication(this GameObject gameObject)
        {
            return TryGetApplication(gameObject, out IApplication application) ? application : throw new ArgumentException($"Application not found by the specified gameobject: '{gameObject}'.");
        }

        public static bool TryGetApplication(this GameObject gameObject, out IApplication application)
        {
            if (gameObject == null) throw new ArgumentNullException(nameof(gameObject));

            var provider = ProviderInstance.Get<IProvider<Scene, IApplication>>();

            return provider.TryGet(gameObject.scene, out application);
        }
    }
}
