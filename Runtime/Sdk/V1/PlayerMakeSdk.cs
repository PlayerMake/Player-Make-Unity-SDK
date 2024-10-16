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

        private static void Init()
        {
            if (_developerSettings == null)
                _developerSettings = Resources.Load<PlayerMakeSettings>("PlayerMakeSettings");

            if (_creationApi == null)
                _creationApi = new CreationApi(_developerSettings.ApiBaseUrl);
        }

        public static async Task<List<Creation>> ListCreationsAsync(string userId = null)
        {
            Init();

            var creationListResponse = await _creationApi.ListCreationsAsync(new CreationListRequest()
            {
                Params = new CreationListQueryParams() { UserId = userId, ProjectId = _developerSettings.ProjectId }
            });

            return creationListResponse.Data;
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
