using System.IO;
using UnityEditor;
using UnityEngine;

public static class CacheConfig
{
    public readonly static string BaseDirectory = Application.persistentDataPath + "/Player Make/Local Cache/";
    public readonly static string ModelBaseDirectory = BaseDirectory + "Creations/";
    public readonly static string IconBaseDirectory = BaseDirectory + "Icons/";

    public static void EnsureBaseDirectoriesExist()
    {
        CreateDirectoryIfNotExists(BaseDirectory);
        CreateDirectoryIfNotExists(ModelBaseDirectory);
        CreateDirectoryIfNotExists(IconBaseDirectory);
    }

    private static void CreateDirectoryIfNotExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
