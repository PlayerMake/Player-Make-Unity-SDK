using PlayerMake.Api;
using System.Threading.Tasks;

public class CreationApi : BaseApi
{
    public const string Resource = "creations";

    private readonly PlayerMakeSettings _settings;

    public CreationApi(PlayerMakeSettings settings) : base(settings)
    {
        _settings = settings;
    }

    public virtual async Task<CreationListResponse> ListCreationsAsync(CreationListRequest request, RequestCallbacks callbacks)
    {
        var queryString = request.Params.GenerateQueryString();

        return await GetAsync<CreationListResponse>(new Request()
        {
            Url = $"{_settings.ApiBaseUrl}/v1/{Resource}{queryString}",
            Callbacks = callbacks,
        });
    }
}
