using GLTFast;
using PlayerMake.Api;
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
        private static DeveloperApi _developerApi;

        private static void Init()
        {
            if (_developerSettings == null)
                _developerSettings = Resources.Load<PlayerMakeSettings>("PlayerMakeSettings");

            if (_creationApi == null)
                _creationApi = new CreationApi(_developerSettings);

            if (_assetApi == null)
                _assetApi = new AssetApi(_developerSettings);

            if (_playerApi == null)
                _playerApi = new PlayerApi(_developerSettings);

            if (_developerApi == null)
                _developerApi = new DeveloperApi(_developerSettings);
        }

        public static async Task<PlayerLoginResponse> LoginPlayerWithCodeAsync(string code, RequestCallbacks callbacks = null)
        {
            Init();

            return await _playerApi.LoginAync(new PlayerLoginRequest() { 
                Code = code,
                ProjectId = _developerSettings.ProjectId
            }, callbacks);
        }

        public static async Task<Developer> GetDeveloperAsync(RequestCallbacks callbacks = null)
        {
            Init();

            return (await _developerApi.GetDeveloperAsync(new DeveloperGetRequest() { Id = _developerSettings.ProjectId }, callbacks)).Data;
        }

        public static async Task<(List<Asset>, Pagination)> ListAssetsAsync(
            int limit = 10,
            int skip = 0,
            RequestCallbacks callbacks = null
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
            }, callbacks);

            return (assetResponse.Data, assetResponse.Pagination);
        }

        public static async Task<(List<Creation>, Pagination)> ListCreationsAsync(
            string playerId = null,
            string assetId = null,
            string[] statuses = null,
            int limit = 10,
            int skip = 0,
            RequestCallbacks callbacks = null
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
            }, callbacks);

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
