using NUnit.Framework;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationUnity
    {
        [Test]
        public void Create()
        {
            var application = new ApplicationUnity(true);

            Assert.False(ApplicationInstance.HasApplication);

            application.Initialize();

            Assert.True(application.IsInitialized);
            Assert.True(ApplicationInstance.HasApplication);
            Assert.AreEqual(application, ApplicationInstance.Application);

            application.Uninitialize();

            Assert.False(application.IsInitialized);
            Assert.False(ApplicationInstance.HasApplication);
        }
    }
}
