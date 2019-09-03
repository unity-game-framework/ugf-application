using System;
using UnityApplication = UnityEngine.Application;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents default wrapper around Unity Application events.
    /// </summary>
    public class ApplicationUnityEventHandler : IApplicationUnityEventHandler
    {
        public event Action Quitting { add { UnityApplication.quitting += value; } remove { UnityApplication.quitting -= value; } }
        public event Func<bool> WantsToQuit { add { UnityApplication.wantsToQuit += value; } remove { UnityApplication.wantsToQuit -= value; } }
        public event Action<bool> FocusChanged { add { UnityApplication.focusChanged += value; } remove { UnityApplication.focusChanged -= value; } }
        public event UnityApplication.LogCallback LogMessageReceived { add { UnityApplication.logMessageReceived += value; } remove { UnityApplication.logMessageReceived -= value; } }
    }
}
