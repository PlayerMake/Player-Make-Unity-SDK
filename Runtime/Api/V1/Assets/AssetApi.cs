using PlayerMake.Api;
using System.Threading.Tasks;

public class AssetApi : BaseApi
{
    public const string Resource = "assets";

    private readonly string _baseUrl;

    public AssetApi(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    public virtual async Task<AssetListResponse> ListAssetsAsync(AssetListRequest request)
    {
        var queryString = request.Params.GenerateQueryString();

        return await GetAsync<AssetListResponse>(new Request()
        {
            Url = $"{_baseUrl}/v1/{Resource}{queryString}"
        });
    }
}
