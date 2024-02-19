using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

public class SaveAndLoad
{
    public static async Task Save(string path, object data)
    {
        string persistentDataPath = Application.persistentDataPath;

        await Task.Run(() =>
        {
            try
            {
                string fullPath = Path.Combine(persistentDataPath, path);

                BinaryFormatter formatter = new BinaryFormatter();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    formatter.Serialize(memoryStream, data);
                    byte[] dataBytes = memoryStream.ToArray();

                    string folderName = Path.GetDirectoryName(fullPath);
                    Directory.CreateDirectory(folderName);

                    File.WriteAllBytes(fullPath, dataBytes);
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogWarning($"Error saving file at path {path}: {ex.Message}");
            }
        });
    }

    public static async Task<T> Load<T>(string path)
    {
        string persistentDataPath = Application.persistentDataPath;

        return await Task.Run(() =>
        {
            path = Path.Combine(persistentDataPath, path);

            try
            {
                if (!File.Exists(path))
                {
                    UnityEngine.Debug.LogWarning($"File does not exist at path: {path}");
                    return default(T);
                }

                byte[] dataBytes = File.ReadAllBytes(path);

                BinaryFormatter formatter = new BinaryFormatter();

                return (T)formatter.Deserialize(new MemoryStream(dataBytes));
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogWarning($"Error loading file at path {path}: {ex.Message}");
                return default(T);
            }
        });
    }
}