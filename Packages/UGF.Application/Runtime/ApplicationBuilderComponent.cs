using UGF.Builder.Runtime;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationBuilderComponent : BuilderComponent<IApplicationResources, IApplication>, IApplicationBuilder
    {
    }
}
