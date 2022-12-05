using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public class Saver<T>
{   
    public static void TryLoad(string filename, ref T data)
    {
        var path = FileHandler.PathFile(filename);
                
        if (File.Exists(path))
        {            
            var dataString = File.ReadAllText(path);
            var saver = JsonUtility.FromJson<Saver<T>>(dataString);
            data = saver.data;
        }
        else
        {
            Debug.Log($"no file at {path}");
        }
    }

    public T data;

    public static void Save(string filename, T data)
    {       
        var wrapper = new Saver<T> { data = data };
        var dataString = JsonUtility.ToJson(wrapper);
        File.WriteAllText(FileHandler.PathFile(filename), dataString);
    }        
        
}
    
