namespace UGF.Application.Runtime
{
    public abstract class ApplicationConfigAssetBase : ApplicationResourceAsset
    {
        public abstract IApplicationConfig GetConfig();

        public override object GetResource()
        {
            return GetConfig();
        }
    }
}
