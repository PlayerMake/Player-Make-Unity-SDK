using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerMake.Api
{
    public class DeveloperApi : BaseApi
    {
        public const string Resource = "auth";

        private readonly PlayerMakeSettings _settings;

        public DeveloperApi(PlayerMakeSettings settings) : base(settings)
        {
            _settings = settings;
        }

        public virtual async Task<DeveloperGetResponse> GetDeveloperAsync(DeveloperGetRequest request, RequestCallbacks callbacks)
        {
            return await GetAsync<DeveloperGetResponse>(new Request()
            {
                Url = $"{_settings.ApiBaseUrl}/v1/{Resource}/profile/{request.Id}",
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } },
                Callbacks = callbacks
            });
        }


        public virtual async Task<DeveloperQuotaResponse> GetQuoataAsync(string apiKey, RequestCallbacks callbacks = null)
        {
            return await GetAsync<DeveloperQuotaResponse>(new Request()
            {
                Url = $"{_settings.ApiBaseUrl}/v1/creations/quota",
                Callbacks = callbacks,
                Headers = new Dictionary<string, string>()
                {
                    { "x-api-key", apiKey }
                }
            });
        }

        public virtual async Task<DeveloperValidationResponse> VerifyApiKeyAsync(string apiKey, RequestCallbacks callbacks = null)
        {
            return await GetAsync<DeveloperValidationResponse>(new Request()
            {
                Url = $"{_settings.ApiBaseUrl}/v1/{Resource}/profile-by-api-key",
                Callbacks = callbacks,
                Headers = new Dictionary<string, string>()
                {
                    { "x-api-key", apiKey }
                }
            });
        }
    }
}