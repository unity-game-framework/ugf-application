using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.SceneManagement;

namespace UGF.Application.Runtime.Scenes
{
    public class ApplicationSceneProvider : IApplicationSceneProvider
    {
        public IReadOnlyDictionary<Scene, IApplication> Applications { get; }

        public event ApplicationSceneHandler Added;
        public event ApplicationSceneHandler Removed;
        public event Action Cleared;

        private readonly Dictionary<Scene, IApplication> m_applications = new Dictionary<Scene, IApplication>();

        public ApplicationSceneProvider()
        {
            Applications = new ReadOnlyDictionary<Scene, IApplication>(m_applications);
        }

        public void Add(Scene scene, IApplication application)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));
            if (application == null) throw new ArgumentNullException(nameof(application));

            m_applications.Add(scene, application);

            Added?.Invoke(scene, application);
        }

        public bool Remove(Scene scene)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));

            if (TryGet(scene, out IApplication application))
            {
                m_applications.Remove(scene);

                Removed?.Invoke(scene, application);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            m_applications.Clear();

            Cleared?.Invoke();
        }

        public T Get<T>(Scene scene) where T : class, IApplication
        {
            return (T)Get(scene);
        }

        public IApplication Get(Scene scene)
        {
            return TryGet(scene, out IApplication application) ? application : throw new ArgumentException($"Application not found by the specified scene: '{scene.name}'.");
        }

        public bool TryGet<T>(Scene scene, out T application) where T : class, IApplication
        {
            if (TryGet(scene, out IApplication value))
            {
                application = (T)value;
                return true;
            }

            application = default;
            return false;
        }

        public bool TryGet(Scene scene, out IApplication application)
        {
            if (!scene.IsValid()) throw new ArgumentException("Value should be valid.", nameof(scene));

            return m_applications.TryGetValue(scene, out application);
        }

        public Dictionary<Scene, IApplication>.Enumerator GetEnumerator()
        {
            return m_applications.GetEnumerator();
        }
    }
}
