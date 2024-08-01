using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerDataController : Singleton<PlayerDataController>
{
    private void Awake()
    {
        SaveToJson(LoadFromJson());
        LoadFromJsonItem();
        SaveToJsonItem(LoadFromJsonItem());
    }

    public void SaveToJson(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + Constant.PLAYER_DATA_PATH, json);
    }
    public void SaveToJsonItem(ItemUnlockData data)
    {
        string jsonItem = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + Constant.ITEM_STATE_PATH, jsonItem);
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
                coins = 1000
            };

            return data;
        }
    }
    
    public ItemUnlockData LoadFromJsonItem()
    {
        if (File.Exists(Application.dataPath + Constant.ITEM_STATE_PATH))
        {
            string jsonItem = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
            ItemUnlockData dataItem = JsonUtility.FromJson<ItemUnlockData>(jsonItem);

            return dataItem;
        }
        else
        {
            // Set new information
            ItemUnlockData dataItem = new ItemUnlockData
            {
                hatItemStates = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                hatUnlockOneTime = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                pantItemStates = new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                pantUnlockOneTime = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                shieldItemStates = new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                shieldUnlockOneTime = new int [] { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                setItemStates = new int [] { 0, 0, 0, 0, 0, 0 },
                weaponSkinStates = new int [] { 3, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                weaponStates = new int[] { 1, 0, 0, 0, 0 },
            };

            return dataItem;
        }
    }

}
