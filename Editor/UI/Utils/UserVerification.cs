using PlayerMake.Api;
using PlayerMake.V1;
using UnityEditor;

public static class UserVerification
{
    public static bool apiKeyValid = false;
    public static bool apiKeyValidationLoading = false;

    public static DeveloperQuota quota;
    public static bool quotaLoading = false;

    public static void GetQuota(string apiKey)
    {
        quotaLoading = true;

        if (string.IsNullOrEmpty(apiKey))
        {
            quota = null;
            quotaLoading = false;
            return;
        }

        try
        {
            PlayerMakeSdk
                .GetQuotaAsync(apiKey)
                .ContinueWith(p =>
                {
                    quotaLoading = false;
                    quota = p.Result;
                });
        }
        catch
        {
            quotaLoading = false;
        }
    }
    public static void VerifyApiKey(PlayerMakeSettings settings, string apiKey)
    {
        apiKeyValidationLoading = true;
        settings.ApiKey = apiKey;
        EditorUtility.SetDirty(settings);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        if (string.IsNullOrEmpty(apiKey))
        {
            apiKeyValidationLoading = false;
            apiKeyValid = false;
            return;
        }

        try
        {
            PlayerMakeSdk
                .VerifyApiKeyAsync(apiKey)
                .ContinueWith(p =>
                {
                    apiKeyValidationLoading = false;

                    if (p.Result == null)
                    {
                        apiKeyValid = false;
                    }
                    else
                    {
                        apiKeyValid = true;
                    }

                });
        }
        catch
        {
            apiKeyValidationLoading = false;
            apiKeyValid = false;
        }
    }
}