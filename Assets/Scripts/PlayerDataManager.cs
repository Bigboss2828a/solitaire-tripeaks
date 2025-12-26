using UnityEngine;


public static class PlayerDataManager
{
   // public static PlayerDataManager Instance;
    public static PlayerDataModel playerData;
/*    private static void Awake()
    {
            Instance = this;        
    }*/
    public static void Initialize()
    {
        LoadData();
    }
    public static void LoadData()
    {
        playerData = ReadWriteData.ReadData();
    }
    public static void SaveData()
    {
        ReadWriteData.SaveData(playerData);
    }

    public static void ExportData()
    {
        ReadWriteData.ExportData(playerData);
    }
    public static void DataChanged(PlayerDataModel model)
    {
        playerData = model;
    }
    public static PlayerDataModel GetData()
    {
        return playerData;
    }
    public static PlayerDataModel NewUser()
    {
        PlayerDataModel newModel = new PlayerDataModel();
        newModel.playerCoin = 225;

        newModel.playerName = "John Marston";
        return newModel;
    }
    public static bool DecreaseCoin(int givenValue)
    {
        
        if (playerData.playerCoin < givenValue)
        {
            return false;
        }
        playerData.playerCoin-= givenValue;
        SaveData();
        return true;
    }
    public static int Increase(int givenValue)
    {
        playerData.playerCoin += givenValue;
        SaveData();
        return playerData.playerCoin;
    }
}
