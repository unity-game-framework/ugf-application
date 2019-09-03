using System;
using UnityApplication = UnityEngine.Application;

namespace UGF.Application.Runtime
{
    public interface IApplicationUnityEventHandler
    {
        event Action Quitting;
        event Func<bool> WantsToQuit;
        event Action<bool> FocusChanged;
        event UnityApplication.LogCallback LogMessageReceived;
    }
}
