using System.Threading.Tasks;

namespace PlayerMake.Api
{
    public class AssetApi : BaseApi
    {
        public const string Resource = "assets";

        private readonly PlayerMakeSettings _settings;

        public AssetApi(PlayerMakeSettings settings) : base(settings)
        {
            _settings = settings;
        }

        public virtual async Task<AssetListResponse> ListAssetsAsync(AssetListRequest request, RequestCallbacks callbacks = null)
        {
            var queryString = request.Params.GenerateQueryString();

            return await GetAsync<AssetListResponse>(new Request()
            {
                Url = $"{_settings.ApiBaseUrl}/v1/{Resource}{queryString}",
                Callbacks = callbacks
            });
        }
    }
}