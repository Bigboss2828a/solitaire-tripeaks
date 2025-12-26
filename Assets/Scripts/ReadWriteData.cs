using UnityEngine;
using System.IO;
public static class ReadWriteData 
{
    public static string filePath = Application.persistentDataPath + "/JosnFile.json";
    public static PlayerDataModel ReadData()
    {
        if (File.Exists(filePath))
        {
            return _ReadData();
        }
        return NoFileFound();
    }
    public static void SaveData(PlayerDataModel givenModel)
    {
        string jsoned = JsonUtility.ToJson(givenModel,true);
        File.WriteAllText(filePath, jsoned);
        Debug.Log("Model Saved to " + filePath);
    }
    public static void ExportData(PlayerDataModel givenModel)
    {
        SaveData(givenModel);
        string jsoned = JsonUtility.ToJson(givenModel, true);
     //   AndroidShare.Share(jsoned,"JsonFile");
    }
    private static PlayerDataModel _ReadData()
    {
        string json = File.ReadAllText(filePath); 
        PlayerDataModel model = JsonUtility.FromJson<PlayerDataModel>(json);
        return model;
        Debug.Log("Got Model from " + filePath);

    }
    private static PlayerDataModel NoFileFound()// As if its a new user
    {

        return PlayerDataManager.NewUser();
    }
}
