using JetBrains.Annotations;
using UGF.Application.Runtime;
using UGF.CustomSettings.Editor;
using UnityEditor;

namespace UGF.Application.Editor.Settings
{
    public static class ApplicationEditorSettings
    {
        public static ApplicationConfigAssetBase Config
        {
            get { return m_settings.Data.Config; }
            set
            {
                m_settings.Data.Config = value;
                m_settings.SaveSettings();
            }
        }

        private static readonly CustomSettingsEditorPackage<ApplicationEditorSettingsData> m_settings = new CustomSettingsEditorPackage<ApplicationEditorSettingsData>
        (
            "UGF.Application",
            "ApplicationEditorSettings"
        );

        [SettingsProvider, UsedImplicitly]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<ApplicationEditorSettingsData>("Project/UGF/Application", m_settings, SettingsScope.Project);
        }
    }
}
