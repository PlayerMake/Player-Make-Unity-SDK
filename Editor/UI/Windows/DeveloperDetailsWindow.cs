using PlayerMake.Api;
using PlayerMake.Editor.Utils;
using RuntimeSounds.Editor.UI.Components;
using UnityEditor;
using UnityEngine;

namespace PlayerMake.Editor.UI.Windows
{
    public class DeveloperDetailsWindow : EditorWindow
    {
        PlayerMakeSettings settings;

        private Debouncer debouncer;
        private LoadingIndicator loadingIndicator;

        [MenuItem("Tools/Player Make", false, 0)]
        public static void Generate()
        {
            var window = GetWindow<DeveloperDetailsWindow>("Player Make");
            window.minSize = new Vector2(400, 240);
        }

        private void OnEnable()
        {
            debouncer = new Debouncer(0.8f);
            loadingIndicator = new LoadingIndicator();
            settings = SettingsHelper.GetOrCreateAndGetSettings();

            UserVerification.GetQuota(settings.ApiKey);
            UserVerification.VerifyApiKey(settings, settings.ApiKey);
        }

        private void OnGUI()
        {
            GUILayout.Space(10);

            using (new GUILayout.HorizontalScope())
            {
                if (string.IsNullOrEmpty(settings.ApiKey))
                {
                    GUILayout.Label($"Welcome to Player Make!", new GUIStyle()
                    {
                        fontStyle = FontStyle.Bold,
                        normal = new GUIStyleState()
                        {
                            textColor = Color.white
                        },
                        margin = new RectOffset(5, 0, 0, 0),
                        alignment = TextAnchor.MiddleCenter,
                    });
                }
                else
                {
                    GUILayout.Label($"Player Make Settings", new GUIStyle()
                    {
                        fontStyle = FontStyle.Bold,
                        normal = new GUIStyleState()
                        {
                            textColor = Color.white,
                        },
                        fontSize = 14,
                        margin = new RectOffset(5, 0, 0, 0),
                        alignment = TextAnchor.MiddleCenter,
                    });
                }
            }

            EditorGUILayout.BeginVertical(new GUIStyle()
            {
                margin = new RectOffset(10, 10, 10, 0)
            });

            if (string.IsNullOrEmpty(settings.ApiKey))
            {
                EditorGUILayout.LabelField($"Please use the button below to login to Player Make, generate an API Key, and add it below.", new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    wordWrap = true,
                }, GUILayout.ExpandWidth(true));

                EditorGUILayout.BeginVertical(new GUIStyle()
                {
                    margin = new RectOffset(0, 0, 6, 0)
                });

                EditorGUILayout.EndVertical();

                if (GUILayout.Button("Get API Key and Project ID", GUILayout.ExpandWidth(true)))
                {
                    Application.OpenURL("https://playermake.com/generate-api-key");
                }

                EditorGUILayout.BeginVertical(new GUIStyle()
                {
                    margin = new RectOffset(0, 0, 6, 0)
                });

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.LabelField("Project Settings", new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                normal = new GUIStyleState()
                {
                    textColor = Color.white
                },
                margin = new RectOffset(5, 0, 6, 0),
            });

            var newProjectId = EditorGUILayout.TextField("Project ID", settings.ProjectId);

            if (newProjectId != settings.ProjectId)
            {
                settings.ProjectId = newProjectId;
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            var newApiKey = EditorGUILayout.TextField("API Key", settings.ApiKey);

            if (!UserVerification.apiKeyValid && !UserVerification.apiKeyValidationLoading && !string.IsNullOrEmpty(settings.ApiKey))
            {
                EditorGUILayout.LabelField("API Key is invalid.", new GUIStyle(EditorStyles.label)
                {
                    normal = new GUIStyleState
                    {
                        textColor = Color.red
                    }
                });
            }

            if (UserVerification.apiKeyValid && !UserVerification.apiKeyValidationLoading && !string.IsNullOrEmpty(settings.ApiKey))
            {
                EditorGUILayout.LabelField("API Key is valid.", new GUIStyle(EditorStyles.label)
                {
                    normal = new GUIStyleState
                    {
                        textColor = Color.green
                    }
                });
            }

            if (UserVerification.apiKeyValidationLoading)
            {
                loadingIndicator.Render(new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleRight,
                }, GUILayout.ExpandWidth(true));
            }

            if (newApiKey != settings.ApiKey)
            {
                debouncer.Execute(() => {
                    UserVerification.VerifyApiKey(settings, newApiKey);
                    UserVerification.GetQuota(newApiKey);
                });
            }

            EditorGUILayout.BeginVertical(new GUIStyle()
            {
                margin = new RectOffset(10, 10, 10, 0)
            });

            EditorGUILayout.EndVertical();

            EditorGUILayout.LabelField("Cache Config", new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                normal = new GUIStyleState()
                {
                    textColor = Color.white
                },
                margin = new RectOffset(5, 0, 10, 0),
            });

            var iconCacheTotalFileSizeLimit = EditorGUILayout.IntField("Icon total file size (MB)", settings.IconCacheTotalFileSizeLimitMb);

            if (iconCacheTotalFileSizeLimit != settings.IconCacheTotalFileSizeLimitMb)
            {
                settings.IconCacheTotalFileSizeLimitMb = iconCacheTotalFileSizeLimit;
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            var iconCacheTotalCountLimit = EditorGUILayout.IntField("Icon max file count", settings.IconCacheFileCountLimit);

            if (iconCacheTotalCountLimit != settings.IconCacheFileCountLimit)
            {
                settings.IconCacheFileCountLimit = iconCacheTotalCountLimit;
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            var modelCacheTotalFileSizeLimit = EditorGUILayout.IntField("Model total file size (MB)", settings.ModelCacheTotalFileSizeLimitMb);

            if (modelCacheTotalFileSizeLimit != settings.ModelCacheTotalFileSizeLimitMb)
            {
                settings.ModelCacheTotalFileSizeLimitMb = modelCacheTotalFileSizeLimit;
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            var modelCacheTotalCountLimit = EditorGUILayout.IntField("Model max file count", settings.ModelCacheFileCountLimit);

            if (modelCacheTotalCountLimit != settings.ModelCacheFileCountLimit)
            {
                settings.ModelCacheFileCountLimit = modelCacheTotalCountLimit;
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            EditorGUILayout.BeginVertical(new GUIStyle()
            {
                margin = new RectOffset(10, 10, 10, 0)
            });

            EditorGUILayout.EndVertical();

            EditorGUILayout.LabelField("Account Stats", new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                normal = new GUIStyleState()
                {
                    textColor = Color.white
                },
                margin = new RectOffset(5, 0, 10, 0),
            });

            var tier = UserVerification.quota?.Tier == null ? "Unknown" : UserVerification.quota.Tier;
            var requestPercentageUsage = UserVerification.quota?.DownloadQuota == null || UserVerification.quota?.DownloadQuota == 0 ? 1 : (UserVerification.quota.RequestCount / UserVerification.quota.DownloadQuota);
            var generationPercentageUsage = UserVerification.quota?.GenerationQuota == null || UserVerification.quota?.GenerationQuota == 0 ? 1 : (UserVerification.quota.GenerationCount / UserVerification.quota.GenerationQuota);

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Account Tier: ", GUILayout.Width(250));

            if (!UserVerification.quotaLoading)
            {
                EditorGUILayout.LabelField(string.IsNullOrEmpty(tier) ? "Unknown" : char.ToUpper(tier[0]) + tier.Substring(1), new GUIStyle(EditorStyles.label)
                {
                    normal = new GUIStyleState()
                    {
                        textColor = tier == "custom" ? Color.mediumPurple : (tier == "free" ? Color.yellow : Color.white)
                    }
                });
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(); 

            EditorGUILayout.LabelField("Total player creation downloads:", GUILayout.Width(250));

            if (!UserVerification.quotaLoading)
            {
                EditorGUILayout.LabelField((UserVerification.quota?.RequestCount ?? 0) + "/" + (UserVerification.quota?.DownloadQuota ?? 0), new GUIStyle(EditorStyles.label)
                {
                    normal = new GUIStyleState()
                    {
                        textColor = requestPercentageUsage >= 1 ? Color.red : (requestPercentageUsage >= 0.8 ? Color.yellow : Color.green)
                    }
                });
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Total player creations made:", GUILayout.Width(250));

            if (!UserVerification.quotaLoading)
            {
                EditorGUILayout.LabelField((UserVerification.quota?.GenerationCount ?? 0) + "/" + (UserVerification.quota?.GenerationQuota ?? 0), new GUIStyle(EditorStyles.label)
                {
                    normal = new GUIStyleState()
                    {
                        textColor = generationPercentageUsage >= 1 ? Color.red : (generationPercentageUsage >= 0.8 ? Color.yellow : Color.green)
                    }
                });
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }
    }
}
