using PlayerMake.Api;
using System.Threading.Tasks;

public class CreationApi : BaseApi
{
    public const string Resource = "creations";

    private readonly string _baseUrl;

    public CreationApi(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    public virtual async Task<CreationListResponse> ListCreationsAsync(CreationListRequest request)
    {
        var queryString = request.Params.GenerateQueryString();

        return await GetAsync<CreationListResponse>(new Request()
        {
            Url = $"{_baseUrl}/v1/{Resource}{queryString}"
        });
    }
}
