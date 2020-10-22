namespace UGF.Application.Runtime
{
    public interface IApplicationModuleDescribed : IApplicationModule
    {
        IApplicationModuleDescription Description { get; }
    }
}
