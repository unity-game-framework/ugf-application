using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UGF.Application.Runtime.Tests
{
    public class TestApplicationBase
    {
        private class Application : ApplicationBase
        {
        }

        [Test]
        public void GetEnumerator()
        {
            var application = new Application();

            foreach (KeyValuePair<Type, IApplicationModule> pair in application)
            {
                Assert.NotNull(pair.Key);
            }
        }
    }
}
