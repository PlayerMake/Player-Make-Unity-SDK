using UnityEditor;
using UnityEngine;

namespace PlayerMake.Editor.UI.Windows
{
    public class DeveloperDetailsWindow : EditorWindow
    {
        [MenuItem("Tools/Player Make", false, 0)]
        public static void Generate()
        {
            var window = GetWindow<DeveloperDetailsWindow>("Player Make");
            window.minSize = new Vector2(700, 240);
        }

        private void OnEnable()
        {
            var sdkSettings = Resources.Load<PlayerMakeSettings>("PlayerMakeSettings");

            if (sdkSettings == null)
            {
                sdkSettings = CreateInstance<PlayerMakeSettings>();

                EnsureResourcePathExists();

                AssetDatabase.CreateAsset(sdkSettings, "Assets/Player Make/Resources/PlayerMakeSettings.asset");
                EditorUtility.SetDirty(sdkSettings);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private void EnsureResourcePathExists()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Player Make"))
                AssetDatabase.CreateFolder("Assets", "Player Make");

            if (!AssetDatabase.IsValidFolder("Assets/Player Make/Resources"))
                AssetDatabase.CreateFolder("Assets/Player Make", "Resources");
        }

        private void OnGUI()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label($"Welcome to the Player Make SDK!");
            }
        }
    }
}
