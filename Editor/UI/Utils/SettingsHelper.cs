using PlayerMake.Api;
using UnityEditor;
using UnityEngine;

public static class SettingsHelper
{
    public static PlayerMakeSettings GetOrCreateAndGetSettings()
    {
        var settings = Resources.Load<PlayerMakeSettings>("PlayerMakeSettings");

        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<PlayerMakeSettings>();

            EnsureResourcePathExists();

            AssetDatabase.CreateAsset(settings, "Assets/Player Make/Resources/PlayerMakeSettings.asset");
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        return settings;
    }

    private static void EnsureResourcePathExists()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Player Make"))
            AssetDatabase.CreateFolder("Assets", "Player Make");

        if (!AssetDatabase.IsValidFolder("Assets/Player Make/Resources"))
            AssetDatabase.CreateFolder("Assets/Player Make", "Resources");
    }
}