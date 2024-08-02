using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerDataController : Singleton<PlayerDataController>
{

    public string playerData;
    public string itemData;

    private void Awake()
    {
        playerData = Path.Combine(Application.persistentDataPath, Constant.PLAYER_DATA_PATH);
        itemData = Path.Combine(Application.persistentDataPath, Constant.ITEM_STATE_PATH);
        SaveToJson(LoadFromJson());
        LoadFromJsonItem();
        SaveToJsonItem(LoadFromJsonItem());
    }

    public void SaveToJson(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(playerData, json);
    }
    public void SaveToJsonItem(ItemUnlockData data)
    {
        string jsonItem = JsonUtility.ToJson(data);
        File.WriteAllText(itemData, jsonItem);
    }

    public PlayerData LoadFromJson()
    {
        if (File.Exists(playerData))
        {
            string json = File.ReadAllText(playerData);
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
                coins = 3000
            };

            return data;
        }
    }
    
    public ItemUnlockData LoadFromJsonItem()
    {
        if (File.Exists(PlayerDataController.Ins.itemData))
        {
            string jsonItem = File.ReadAllText(itemData);
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
