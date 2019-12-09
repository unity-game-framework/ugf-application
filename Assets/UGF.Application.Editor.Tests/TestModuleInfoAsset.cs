using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Application.Editor.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestModuleInfoAsset")]
    public class TestModuleInfoAsset : ApplicationModuleInfoAsset<TestModuleInfoAsset>
    {
        [SerializeField] private string m_value = "Value";

        public string Value { get { return m_value; } set { m_value = value; } }

        protected override IApplicationModule OnBuild(IApplication application)
        {
            return null;
        }
    }
}
