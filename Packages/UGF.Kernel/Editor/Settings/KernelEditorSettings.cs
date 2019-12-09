using JetBrains.Annotations;
using UGF.CustomSettings.Editor;
using UGF.Kernel.Runtime;
using UnityEditor;

namespace UGF.Kernel.Editor.Settings
{
    public static class KernelEditorSettings
    {
        public static KernelConfigAsset Config
        {
            get { return m_settings.Data.Config; }
            set
            {
                m_settings.Data.Config = value;
                m_settings.Save();
            }
        }

        private static readonly CustomSettingsEditorPackage<KernelEditorSettingsData> m_settings = new CustomSettingsEditorPackage<KernelEditorSettingsData>
        (
            "UGF.Kernel",
            "KernelEditorSettings",
            CustomSettingsEditorUtility.DefaultPackageExternalFolder
        );

        [SettingsProvider, UsedImplicitly]
        private static SettingsProvider GetSettingsProvider()
        {
            return new CustomSettingsProvider<KernelEditorSettingsData>("Project/UGF/Kernel", m_settings, SettingsScope.Project);
        }
    }
}
