using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace UGF.Application.Runtime.Scenes
{
    public interface IApplicationSceneProvider
    {
        IReadOnlyDictionary<Scene, IApplication> Applications { get; }

        event ApplicationSceneHandler Added;
        event ApplicationSceneHandler Removed;
        event Action Cleared;

        void Add(Scene scene, IApplication application);
        bool Remove(Scene scene);
        void Clear();
        T Get<T>(Scene scene) where T : class, IApplication;
        IApplication Get(Scene scene);
        bool TryGet<T>(Scene scene, out T application) where T : class, IApplication;
        bool TryGet(Scene scene, out IApplication application);
    }
}
