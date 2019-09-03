using System;
using UnityApplication = UnityEngine.Application;

namespace UGF.Application.Runtime
{
    /// <summary>
    /// Represents Unity <see cref="UnityEngine.Application"/> event handler.
    /// </summary>
    public interface IApplicationUnityEventHandler
    {
        /// <summary>
        /// This event occurs when the player application is quitting.
        /// </summary>
        event Action Quitting;

        /// <summary>
        /// This event occurs when the player application wants to quit.
        /// </summary>
        event Func<bool> WantsToQuit;

        /// <summary>
        /// This event occurs when the player focus gained or lost.
        /// </summary>
        event Action<bool> FocusChanged;

        /// <summary>
        /// This event occurs when a log message is received.
        /// </summary>
        event UnityApplication.LogCallback LogMessageReceived;
    }
}
