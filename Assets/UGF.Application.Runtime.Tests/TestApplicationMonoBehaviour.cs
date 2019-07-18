using NUnit.Framework;
using UnityEngine;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationMonoBehaviour : MonoBehaviour
    {
        private TestApplication m_application;

        private class TestApplication : ApplicationUnity
        {
            private readonly MonoBehaviour m_monoBehaviour;

            public TestApplication(MonoBehaviour monoBehaviour)
            {
                m_monoBehaviour = monoBehaviour;
            }

            protected override void OnPostUninitialize()
            {
                base.OnPostUninitialize();

                Assert.NotNull(m_monoBehaviour);
            }
        }

        private void Awake()
        {
            m_application = new TestApplication(this);
            m_application.Initialize();
        }

        private void OnDestroy()
        {
            Assert.False(m_application.IsInitialized);
        }
    }
}
