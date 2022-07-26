using System;
using UGF.Application.Runtime.Scenes;
using UnityEngine;

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
            if (!gameObject.scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(gameObject.scene));

            return gameObject.scene.TryGetApplication(out application);
        }
    }
}
