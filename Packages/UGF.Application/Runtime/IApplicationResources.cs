using System;
using System.Collections.Generic;

namespace UGF.Application.Runtime
{
    public interface IApplicationResources : IEnumerable<object>
    {
        int Count { get; }

        bool Contains(object resource);
        void Add(object resource);
        void Remove(object resource);
        T Get<T>();
        object Get(Type type);
        bool TryGet<T>(out T resource);
        bool TryGet(Type type, out object resource);
    }
}
