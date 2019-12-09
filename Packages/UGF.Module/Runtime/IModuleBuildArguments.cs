using System.Collections.Generic;

namespace UGF.Module.Runtime
{
    public interface IModuleBuildArguments<out TValue> : IEnumerable<TValue>
    {
        T Get<T>();
        bool TryGet<T>(out T argument);
    }
}
