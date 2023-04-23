using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public static class SaveSystem 
{
    public static string Path = Application.persistentDataPath + "/progress.win";
    public static void Save(Progress progress)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Path, FileMode.Create);
        ProgressData progressData = new ProgressData(progress);
        binaryFormatter.Serialize(fileStream, progressData);
        fileStream.Close();
    }

    public static ProgressData Load()
    {
        if (File.Exists(Path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(Path, FileMode.Open);
            ProgressData progressData = binaryFormatter.Deserialize(fileStream) as ProgressData;
            fileStream.Close();
            return progressData;
        }
        else
        {
            Debug.Log("No file");
            return null;
        }
            
    }

    public static void DeleteFile()
    {
        File.Delete(Path);
    }
}
