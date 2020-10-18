using System;
using NUnit.Framework;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationInstance
    {
        private class Target : ApplicationConfigured
        {
            public Target() : base(new ApplicationConfig())
            {
            }
        }

        [Test]
        public void Application()
        {
            var target = new Target();

            Assert.Throws<InvalidOperationException>(() => Assert.Null(ApplicationInstance.Application));

            ApplicationInstance.Application = target;

            Assert.NotNull(ApplicationInstance.Application);
            Assert.AreEqual(target, ApplicationInstance.Application);

            ApplicationInstance.Application = null;
        }

        [Test]
        public void HasApplication()
        {
            var target = new Target();

            Assert.False(ApplicationInstance.HasApplication);

            ApplicationInstance.Application = target;

            Assert.True(ApplicationInstance.HasApplication);

            ApplicationInstance.Application = null;
        }
    }
}
