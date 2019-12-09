namespace UGF.Module.Runtime
{
    public interface IModuleBuildInfo
    {
        bool Active { get; }
        IModuleBuilder Builder { get; }
        IModuleBuildArguments<object> Arguments { get; }
    }
}
