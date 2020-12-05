using UGF.CustomSettings.Runtime;

namespace UGF.Application.Runtime
{
    public static class ApplicationSettings
    {
        public static ApplicationResourceAsset Config
        {
            get { return Settings.GetData().Config; }
            set
            {
                Settings.GetData().Config = value;
                Settings.SaveSettings();
            }
        }

        public static CustomSettingsPackage<ApplicationSettingsAsset> Settings { get; } = new CustomSettingsPackage<ApplicationSettingsAsset>
        (
            "UGF.Application",
            "ApplicationSettings"
        );
    }
}
