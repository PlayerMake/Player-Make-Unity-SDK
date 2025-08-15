using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public enum CacheType
{
    Icon = 1,
    Model = 2,
}

public static class CacheManager
{
    private const decimal BytesInMb = 1048576;

    public static async Task<byte[]> LoadAsync(string id, CacheType type)
    {
        string filePath = (type == CacheType.Icon ? CacheConfig.IconBaseDirectory : CacheConfig.ModelBaseDirectory) +
            $"/{id}."
            + (type == CacheType.Icon ? "png" : "glb");

        CacheConfig.EnsureBaseDirectoriesExist();

        if (!File.Exists(filePath))
            return null;

        return await File.ReadAllBytesAsync(filePath);
    }

    public static async Task SaveAsync(byte[] bytes, string id, CacheType type)
    {
        var path = (type == CacheType.Icon ? CacheConfig.IconBaseDirectory : CacheConfig.ModelBaseDirectory) 
            + $"/{id}."
            + (type == CacheType.Icon ? "png" : "glb");

        CacheConfig.EnsureBaseDirectoriesExist();

        await File.WriteAllBytesAsync(path, bytes);
    }

    public static async Task RemoveItemFromCache(string id)
    {
        CacheConfig.EnsureBaseDirectoriesExist();

        var iconPath = CacheConfig.IconBaseDirectory + $"/{id}.png";
        var modelPath = CacheConfig.ModelBaseDirectory + $"/{id}.glb";

        await Task.Run(() => File.Delete(iconPath));
        await Task.Run(() => File.Delete(modelPath));

    }

    public static async Task ClearCache()
    {
        CacheConfig.EnsureBaseDirectoriesExist();

        await DeleteAllFilesInDirectory(CacheConfig.IconBaseDirectory);
        await DeleteAllFilesInDirectory(CacheConfig.ModelBaseDirectory);
    }

    public static async Task EnforceIconCacheLimitsAsync(decimal maxTotalSizeMb, int maxTotalFileCount)
    {
        CacheConfig.EnsureBaseDirectoriesExist();

        var allFiles = Directory.GetFiles(CacheConfig.IconBaseDirectory)
            .Select(path => new FileInfo(path))
            .OrderBy(f => f.CreationTimeUtc)
            .ToList();

        await EnforceCacheLimitsAsync(allFiles, maxTotalSizeMb * BytesInMb, maxTotalFileCount);
    }

    public static async Task EnforceModelCacheLimitsAsync(decimal maxTotalSizeMb, int maxTotalFileCount)
    {
        CacheConfig.EnsureBaseDirectoriesExist();

        var allFiles = Directory.GetFiles(CacheConfig.ModelBaseDirectory)
            .Select(path => new FileInfo(path))
            .OrderBy(f => f.CreationTimeUtc)
            .ToList();

        await EnforceCacheLimitsAsync(allFiles, maxTotalSizeMb * BytesInMb, maxTotalFileCount);
    }

    private static async Task EnforceCacheLimitsAsync(
        List<FileInfo> files,
        decimal maxTotalSizeBytes,
        int maxTotalFileCount
        )
    {
        long totalSize = files.Sum(f => f.Length);
        int totalCount = files.Count;

        foreach (var file in files)
        {
            if (totalSize < maxTotalSizeBytes && totalCount < maxTotalFileCount)
                break;

            try
            {
                long sizeBeforeDelete = file.Length;
                await Task.Run(() => file.Delete());

                totalSize -= sizeBeforeDelete;
                totalCount--;
            }
            catch (IOException) { }
        }
    }

    private static async Task DeleteAllFilesInDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            return;

        var files = Directory.GetFiles(directoryPath);

        foreach (var file in files)
        {
            try
            {
                await Task.Run(() => File.Delete(file));
            }
            catch (IOException) { }
        }
    }
}