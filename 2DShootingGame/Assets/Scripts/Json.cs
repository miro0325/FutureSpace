using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
//using Newtonsoft.Json;


public class Json : MonoBehaviour
{
    // Start is called before the first frame update
    


    
   
    void Awake()
    {
        SaveData saveData = SaveSystem.Load("data");
        if(saveData == null)
        {
            saveData = new SaveData();
            SaveSystem.Save(saveData, "data");
        }
    }

    // Update is called once per frame
    void Update()
    {
       //if(Input.GetMouseButtonDown(0))
       // {
       //     SaveData saveData = SaveSystem.Load("data");
       //     saveData.rankingScore.Add(10);
       //     saveData.rankingName.Add("test");
       //     SaveSystem.Save(saveData, "data");

       // }
    }
}


[System.Serializable]
public class SaveData
{
    public SaveData()
    {

    }
    [SerializeField]
    public List<int> rankingScore = new List<int>();
    public List<string> rankingName = new List<string>();
}


public static class SaveSystem
{
    private static string SavePath => Application.dataPath;
    public static void Save(SaveData saveData, string saveFileName)
    {
        if (!Directory.Exists(Application.dataPath + "/Data/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data/");
        }
        FileStream stream = new FileStream(Application.dataPath + "/Data/" + saveFileName + ".json", FileMode.OpenOrCreate);

        string saveJson = JsonUtility.ToJson(saveData);
        byte[] bytes = Encoding.UTF8.GetBytes(saveJson);

        stream.Write(bytes, 0, bytes.Length);
        stream.Close();
    }

    public static SaveData Load(string saveFileName)
    {
        string saveFilePath = SavePath + "/Data/" + saveFileName + ".json";

        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("No such saveFile exists");
            return null;
        }
        string saveFile = File.ReadAllText(saveFilePath);
        byte[] bytes = Encoding.Default.GetBytes(saveFile);


        saveFile = Encoding.UTF8.GetString(bytes);

        SaveData saveData = JsonUtility.FromJson<SaveData>(saveFile);
        return saveData;
    }
}

