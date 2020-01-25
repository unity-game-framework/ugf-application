using System;
using System.Collections;
using System.Collections.Generic;

namespace UGF.Application.Runtime
{
    public class ApplicationResources : IApplicationResources
    {
        public int Count { get { return m_resources.Count; } }

        private readonly List<object> m_resources = new List<object>();

        public bool Contains(object resource)
        {
            return m_resources.Contains(resource);
        }

        public void Add(object resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));

            m_resources.Add(resource);
        }

        public void Remove(object resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));

            m_resources.Remove(resource);
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public object Get(Type type)
        {
            if (!TryGet(type, out object resource))
            {
                throw new ArgumentException($"Resource by the specified type not found: '{type}'.");
            }

            return resource;
        }

        public bool TryGet<T>(out T resource)
        {
            if (TryGet(typeof(T), out object value))
            {
                resource = (T)value;
                return true;
            }

            resource = default;
            return false;
        }

        public bool TryGet(Type type, out object resource)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            for (int i = 0; i < m_resources.Count; i++)
            {
                resource = m_resources[i];

                if (type.IsInstanceOfType(resource))
                {
                    return true;
                }
            }

            resource = null;
            return false;
        }

        public List<object>.Enumerator GetEnumerator()
        {
            return m_resources.GetEnumerator();
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            return m_resources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_resources.GetEnumerator();
        }
    }
}
