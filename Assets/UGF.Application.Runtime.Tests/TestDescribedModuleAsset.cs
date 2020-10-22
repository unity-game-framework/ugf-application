using System;
using UnityEngine;

namespace UGF.Application.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestDescribedModuleAsset")]
    public class TestDescribedModuleAsset : ApplicationModuleDescribedAsset<TestDescribedModule, TestModuleDescription>
    {
        protected override TestModuleDescription OnGetDescription(IApplication application)
        {
            return new TestModuleDescription();
        }

        protected override TestDescribedModule OnBuild(IApplication application, TestModuleDescription description)
        {
            return new TestDescribedModule(description);
        }
    }

    public class TestDescribedModule : ApplicationModuleBase
    {
        public TestModuleDescription Description { get; }

        public TestDescribedModule(TestModuleDescription description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }

    public class TestModuleDescription : IApplicationModuleDescription
    {
    }
}
