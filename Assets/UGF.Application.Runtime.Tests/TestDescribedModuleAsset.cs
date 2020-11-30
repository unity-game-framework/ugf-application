using System;
using UnityEngine;

namespace UGF.Application.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestDescribedModuleAsset")]
    public class TestDescribedModuleAsset : ApplicationModuleAsset<TestDescribedModule, TestModuleDescription>
    {
        protected override IApplicationModuleDescription OnBuildDescription()
        {
            return new TestModuleDescription(typeof(TestDescribedModule));
        }

        protected override TestDescribedModule OnBuild(TestModuleDescription description, IApplication application)
        {
            return new TestDescribedModule(description, application);
        }
    }

    public class TestDescribedModule : ApplicationModule<TestModuleDescription>
    {
        public TestDescribedModule(TestModuleDescription description, IApplication application) : base(description, application)
        {
        }
    }

    public class TestModuleDescription : ApplicationModuleDescription
    {
        public TestModuleDescription(Type registerType) : base(registerType)
        {
        }
    }
}
