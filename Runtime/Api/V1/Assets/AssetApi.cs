using PlayerMake.Api;
using System.Threading.Tasks;

public class AssetApi : BaseApi
{
    public const string Resource = "assets";

    private readonly PlayerMakeSettings _settings;

    public AssetApi(PlayerMakeSettings settings) : base(settings)
    {
        _settings = settings;
    }

    public virtual async Task<AssetListResponse> ListAssetsAsync(AssetListRequest request)
    {
        var queryString = request.Params.GenerateQueryString();

        return await GetAsync<AssetListResponse>(new Request()
        {
            Url = $"{_settings.ApiBaseUrl}/v1/{Resource}{queryString}"
        });
    }
}
