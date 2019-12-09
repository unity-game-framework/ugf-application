namespace UGF.Application.Runtime
{
    public interface IApplicationModuleBuilder
    {
        IApplicationModule Build(IApplication application);
    }
}
