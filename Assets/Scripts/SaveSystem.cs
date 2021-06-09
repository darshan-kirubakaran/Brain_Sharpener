using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;

public static class SaveSystem
{
    public static void SavePlayer(GameSession gameSession, string fileName) // filename like "/player.fun"
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName + ".playerData";
        

        PlayerData data = LoadPlayer(fileName);
        if (data == null)
        {
            data = new PlayerData();
        }

        data.AddPlayerData(gameSession);

        Debug.Log(data);

        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(string fileName)
    {
        fileName = "/" + fileName + ".playerData";
        string path = Application.persistentDataPath + fileName;
        Debug.Log("Path = "+ path);

        if(File.Exists(path) && File.ReadAllText(path) != "")
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log("dataLength = " + data.scores.Count);

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
