using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{


    public static void SavePlayer(PlayerData data)
    { 
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream,data);
        stream.Close();  
    }

    public  static PlayerData loadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            Debug.Log("loading");
            stream.Close();
            return data;
        
        }
        else
        {
            Debug.Log("Save File Not Found in "+ path);
            return null;
        }
    }
    
}
