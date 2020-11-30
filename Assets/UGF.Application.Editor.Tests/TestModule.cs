using UGF.Application.Runtime;

namespace UGF.Application.Editor.Tests
{
    public class TestModule : ApplicationModule<ApplicationModuleDescription>
    {
        public TestModule(ApplicationModuleDescription description, IApplication application) : base(description, application)
        {
        }
    }
}
