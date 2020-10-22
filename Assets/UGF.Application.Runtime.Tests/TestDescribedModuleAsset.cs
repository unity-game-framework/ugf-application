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
            return new TestDescribedModule(application, description);
        }
    }

    public class TestDescribedModule : ApplicationModuleDescribed<TestModuleDescription>
    {
        public TestDescribedModule(IApplication application, TestModuleDescription description) : base(application, description)
        {
        }
    }

    public class TestModuleDescription : IApplicationModuleDescription
    {
    }
}
