using UGF.Builder.Runtime;

namespace UGF.Application.Runtime
{
    public abstract class ApplicationBuilder<TApplication> : Builder<IApplicationResources, TApplication>, IApplicationBuilder where TApplication : class, IApplication
    {
        T IBuilder<IApplicationResources, IApplication>.Build<T>(IApplicationResources arguments)
        {
            return (T)(object)Build(arguments);
        }

        IApplication IBuilder<IApplicationResources, IApplication>.Build(IApplicationResources arguments)
        {
            return Build(arguments);
        }
    }
}
