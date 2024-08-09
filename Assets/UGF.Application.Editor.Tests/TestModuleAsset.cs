using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Application.Editor.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestModuleInfoAsset")]
    public class TestModuleAsset : ApplicationModuleAsset<TestModule, ApplicationModuleDescription>
    {
        [SerializeField] private string m_value = "Value";

        public string Value { get { return m_value; } set { m_value = value; } }

        protected override ApplicationModuleDescription OnBuildDescription()
        {
            return new ApplicationModuleDescription();
        }

        protected override TestModule OnBuild(ApplicationModuleDescription description, IApplication application)
        {
            return new TestModule(description, application);
        }
    }
}
