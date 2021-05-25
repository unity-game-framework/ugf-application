using UGF.Application.Runtime;
using UGF.CustomSettings.Editor;
using UnityEditor;

namespace UGF.Application.Editor
{
    internal static class ApplicationSettingsProvider
    {
        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<ApplicationSettingsAsset>("Project/Unity Game Framework/Application", ApplicationSettings.Settings, SettingsScope.Project);
        }
    }
}
