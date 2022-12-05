using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileHandler
{
    public static string PathFile(string filename)
    {
        return $"{Application.persistentDataPath}/{filename}";
    }

    public static void Reset(string filename)
    {
        var path = PathFile(filename);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static bool HasFile(string filename)
    {
        return File.Exists(PathFile(filename));
    }
}
