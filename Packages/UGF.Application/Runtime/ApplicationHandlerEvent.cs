using System;
using UnityEngine.Events;

namespace UGF.Application.Runtime
{
    [Serializable]
    public class ApplicationHandlerEvent : UnityEvent<IApplication>
    {
    }
}
