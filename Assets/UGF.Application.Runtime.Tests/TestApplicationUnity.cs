using System;
using NUnit.Framework;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationUnity
    {
        private class EventHandler : IApplicationUnityEventHandler
        {
            public event Action Quitting;

#pragma warning disable 67
            public event Func<bool> WantsToQuit;
            public event Action<bool> FocusChanged;
            public event UnityEngine.Application.LogCallback LogMessageReceived;
#pragma warning restore 67

            public void Quit()
            {
                Quitting?.Invoke();
            }
        }

        [Test]
        public void Create()
        {
            var eventHandler = new EventHandler();
            var application = new ApplicationUnity(true, true, eventHandler);

            Assert.False(ApplicationInstance.HasApplication);

            application.Initialize();

            Assert.True(application.IsInitialized);
            Assert.True(ApplicationInstance.HasApplication);
            Assert.AreEqual(application, ApplicationInstance.Application);

            eventHandler.Quit();

            Assert.False(application.IsInitialized);
            Assert.False(ApplicationInstance.HasApplication);

            eventHandler.Quit();
        }
    }
}
