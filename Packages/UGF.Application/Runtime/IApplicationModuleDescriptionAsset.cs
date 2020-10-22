namespace UGF.Application.Runtime
{
    public interface IApplicationModuleDescriptionAsset
    {
        T GetGetDescription<T>(IApplication application) where T : class, IApplicationModuleDescription;
        IApplicationModuleDescription GetDescription(IApplication application);
    }
}
