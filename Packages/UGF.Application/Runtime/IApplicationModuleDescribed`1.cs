namespace UGF.Application.Runtime
{
    public interface IApplicationModuleDescribed<out TDescription> : IApplicationModuleDescribed where TDescription : class, IApplicationModuleDescription
    {
        new TDescription Description { get; }
    }
}
