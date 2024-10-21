using GLTFast;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerMake.V1
{
    public static class PlayerMakeSdk
    {
        private static PlayerMakeSettings _developerSettings;
        private static CreationApi _creationApi;
        private static AssetApi _assetApi;
        private static PlayerApi _playerApi;

        private static void Init()
        {
            if (_developerSettings == null)
                _developerSettings = Resources.Load<PlayerMakeSettings>("PlayerMakeSettings");

            if (_creationApi == null)
                _creationApi = new CreationApi(_developerSettings.ApiBaseUrl);

            if (_assetApi == null)
                _assetApi = new AssetApi(_developerSettings.ApiBaseUrl);

            if (_playerApi == null)
                _playerApi = new PlayerApi(_developerSettings.ApiBaseUrl);
        }

        public static async Task<PlayerLoginResponse> LoginPlayerWithCodeAsync(string code)
        {
            Init();

            return await _playerApi.LoginAync(new PlayerLoginRequest() { Code = code });
        }

        public static async Task<(List<Asset>, Pagination)> ListAssetsAsync(
            int limit = 10,
            int skip = 0
            )
        {
            Init();

            var assetResponse = await _assetApi.ListAssetsAsync(new AssetListRequest()
            {
                Params = new AssetListQueryParams()
                {
                    ProjectId = _developerSettings.ProjectId,
                    Limit = limit,
                    Skip = skip
                }
            });

            return (assetResponse.Data, assetResponse.Pagination);
        }

        public static async Task<(List<Creation>, Pagination)> ListCreationsAsync(
            string playerId = null,
            string assetId = null,
            string[] statuses = null,
            int limit = 10,
            int skip = 0
            )
        {
            Init();

            var creationResponse = await _creationApi.ListCreationsAsync(new CreationListRequest()
            {
                Params = new CreationListQueryParams() {
                    PlayerId = playerId,
                    AssetId = assetId,
                    ProjectId = _developerSettings.ProjectId,
                    Status = statuses,
                    Limit = limit,
                    Skip = skip
                }
            });

            return (creationResponse.Data, creationResponse.Pagination);
        }

        public static async Task<GameObject> InstantiateAsync<T>(T downloadableModel, string name = null, Transform parent = null) where T : IDownloadableModel
        {
            Init();

            var importer = new GltfImport();

            if (!await importer.Load(downloadableModel.Url))
                return null;

            var gameObject = new GameObject(name);

            await importer.InstantiateMainSceneAsync(gameObject.transform);

            gameObject.transform.SetParent(parent);

            return gameObject;
        }

        public static async Task<List<IconDownloadResponse>> LoadIconAsync<T>(T downloadableIcon) where T : IDownloadableIcon
        {
            Init();

            return await LoadIconsAsync(new List<T>() { downloadableIcon });
        }

        public static async Task<List<IconDownloadResponse>> LoadIconsAsync<T>(List<T> downloadableIcons) where T : IDownloadableIcon
        {
            Init();

            return (await Task.WhenAll(downloadableIcons
                .Select(async downloadableIcon =>
                    new IconDownloadResponse()
                    {
                        Id = downloadableIcon.Id,
                        Image = await FileApi.DownloadImageAsync(downloadableIcon.IconUrl)
                    }
                ))).ToList();
        }
    }
}
