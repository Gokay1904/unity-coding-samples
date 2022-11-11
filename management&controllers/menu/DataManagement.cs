using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManagement:MonoBehaviour
{
    [SerializeField]
    private static PlayerData _playerData;

    public static PlayerData playerData { get { if (_playerData == null) { _playerData = new PlayerData(); } return _playerData; } }

   
    private static DataManagement _dataManagement;
    public static DataManagement dataManagement{ get { if (_dataManagement == null) { _dataManagement = new DataManagement(); } return _dataManagement; } }
    
    public void Awake()
    {
            DontDestroyOnLoad(this);
            playerData.LoadData();
    }
  

    public void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);

        FileStream fileStream = new FileStream(path,FileMode.Create);

        using (StreamWriter writer  = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }
    public string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {  
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;  
            }
        }

        else
            {
                Debug.Log("File Not Found");
               return null;
            }
       

    }
    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }


}
