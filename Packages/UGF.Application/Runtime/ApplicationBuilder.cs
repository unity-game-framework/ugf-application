using UGF.Builder.Runtime;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationBuilder : Builder<IApplicationResources, IApplication>, IApplicationBuilder
    {
        T IBuilder<IApplicationResources, IApplication>.Build<T>(IApplicationResources arguments)
        {
            return (T)Build(arguments);
        }

        IApplication IBuilder<IApplicationResources, IApplication>.Build(IApplicationResources arguments)
        {
            return Build(arguments);
        }
    }
}
