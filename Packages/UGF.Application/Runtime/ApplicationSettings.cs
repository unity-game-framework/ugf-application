using System.Runtime.CompilerServices;
using UGF.CustomSettings.Runtime;

[assembly: InternalsVisibleTo("UGF.Application.Editor")]

namespace UGF.Application.Runtime
{
    public static class ApplicationSettings
    {
        public static ApplicationResourceAsset Config
        {
            get { return m_settings.Data.Config; }
            set
            {
                m_settings.Data.Config = value;
                m_settings.SaveSettings();
            }
        }

        internal static CustomSettingsPackage<ApplicationSettingsAsset> Settings { get { return m_settings; } }

        private static readonly CustomSettingsPackage<ApplicationSettingsAsset> m_settings = new CustomSettingsPackage<ApplicationSettingsAsset>
        (
            "UGF.Application",
            "ApplicationSettings"
        );
    }
}
