using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerDataController : Singleton<PlayerDataController>
{
    private void Awake()
    {
        SaveToJson(LoadFromJson());
    }

    public void SaveToJson(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + Constant.PLAYER_DATA_PATH, json);
    }

    public PlayerData LoadFromJson()
    {
        if (File.Exists(Application.dataPath + Constant.PLAYER_DATA_PATH))
        {
            string json = File.ReadAllText(Application.dataPath + Constant.PLAYER_DATA_PATH);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            return data;
        }
        else
        {
            // Set new information
            PlayerData data = new PlayerData
            {
                name = "Player",
                level = 1,
                setID = 0,
                pantID = 0,
                hatID = 0,
                weaponID = 0,
                shieldID = 0,
                coins = 0
            };

            return data;
        }
    }

}
