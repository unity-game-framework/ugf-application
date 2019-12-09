using System;
using System.Collections;
using System.Collections.Generic;

namespace UGF.Module.Runtime
{
    public class ModuleBuildArguments<TValue> : IModuleBuildArguments<TValue>
    {
        public List<TValue> Values { get; } = new List<TValue>();

        public static ModuleBuildArguments<TValue> Empty { get; } = new ModuleBuildArguments<TValue>();

        public T Get<T>()
        {
            if (!TryGet(out T argument))
            {
                throw new ArgumentException($"The value of the specified type not found: '{typeof(T)}'.", nameof(T));
            }

            return argument;
        }

        public bool TryGet<T>(out T argument)
        {
            for (int i = 0; i < Values.Count; i++)
            {
                TValue value = Values[i];

                if (value is T cast)
                {
                    argument = cast;
                    return true;
                }
            }

            argument = default;
            return false;
        }

        public List<TValue>.Enumerator GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return ((IEnumerable<TValue>)Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Values).GetEnumerator();
        }
    }
}
