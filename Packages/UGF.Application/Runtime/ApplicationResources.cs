using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.Application.Runtime
{
    public class ApplicationResources : IApplicationResources
    {
        public int Count { get { return m_collection.Count; } }
        public IReadOnlyDictionary<Type, object> Collection { get; }

        private readonly Dictionary<Type, object> m_collection = new Dictionary<Type, object>();

        public ApplicationResources()
        {
            Collection = new ReadOnlyDictionary<Type, object>(m_collection);
        }

        public void Add(object resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));

            m_collection.Add(resource.GetType(), resource);
        }

        public void Remove(object resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));

            m_collection.Remove(resource.GetType());
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public object Get(Type type)
        {
            if (!TryGet(type, out object resource))
            {
                throw new ArgumentException($"Resource by the specified type not found: '{resource}'.");
            }

            return resource;
        }

        public bool TryGet<T>(out T resource)
        {
            if (TryGet(typeof(T), out object value) && value is T cast)
            {
                resource = cast;
                return true;
            }

            resource = default;
            return false;
        }

        public bool TryGet(Type type, out object resource)
        {
            return m_collection.TryGetValue(type, out resource);
        }

        public Dictionary<Type, object>.Enumerator GetEnumerator()
        {
            return m_collection.GetEnumerator();
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            return m_collection.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_collection.Values.GetEnumerator();
        }
    }
}
