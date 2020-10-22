using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Application.Editor.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestModuleInfoAsset")]
    public class TestModuleAsset : ApplicationModuleAsset<TestModule>
    {
        [SerializeField] private string m_value = "Value";

        public string Value { get { return m_value; } set { m_value = value; } }

        protected override TestModule OnBuildTyped(IApplication application)
        {
            return new TestModule(application);
        }
    }
}
