using UGF.CustomSettings.Runtime;

namespace UGF.Application.Runtime
{
    public static class ApplicationSettings
    {
        public static ApplicationResourceAsset Config
        {
            get { return Settings.Data.Config; }
            set
            {
                Settings.Data.Config = value;
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
