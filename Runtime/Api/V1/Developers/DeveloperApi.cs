using PlayerMake.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

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
}