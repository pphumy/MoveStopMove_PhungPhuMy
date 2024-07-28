using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSkin : MonoBehaviour
{
    [Header("Skin")]
    public SkinnedMeshRenderer bodyRend;
    private BodyMaterialID bodyMatID;

    private PantSkinID pantSkinID;
    public SkinnedMeshRenderer pantRend;

    private HatSkinID hatSkinID;
    private Item hatItem;
    public Transform hatHolder;

    private ShieldSkinID shieldSkinID;
    private Item shieldItem;
    public Transform shieldHolder;

    public Transform tailHolder;
    private Item tailItem;
    private TailSkinID tailSkinID;

    public Transform wingHolder;
    private Item wingItem;
    private WingSkinID wingSkinID;

    public void OnInit()
    {
        SetItems();
    }

    public void SetItems()
    {
        InitHat();
        InitWing();
        InitTail();
        InitBody();
        InitPant();
        InitShield();
    }

    public void CheckUnlockOneTime()
    {
        CheckUnlockOneTimeHat();
        CheckUnlockOneTimePant();
        CheckUnlockOneTimeShield();
    }

    // Hat
    private void InitHat()
    {
        ChangeHat();
    }

    public void ChangeHat()
    {
        PlayerData playerData = PlayerDataController.Ins.LoadFromJson();
        hatSkinID = (HatSkinID)playerData.hatID;

        TryHat(hatSkinID);
    }

    public void TryHat(HatSkinID hatSkinID)
    {
        if (hatItem != null)
            Destroy(hatItem.gameObject);

        hatItem = ItemController.Ins.SetHat(hatSkinID, hatHolder);
    }

    private void CheckUnlockOneTimeHat()
    {
        PlayerData playerData = PlayerDataController.Ins.LoadFromJson();
        string itemJson = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
        ItemUnlockData itemData = JsonUtility.FromJson<ItemUnlockData>(itemJson);
        if (itemData.hatItemStates[(int)hatSkinID] == (int)Constant.ItemState.EquipOneTime)
        {
            itemData.hatItemStates[(int)hatSkinID] = (int)Constant.ItemState.Lock;
            hatSkinID = 0;
            playerData.hatID = 0;

            itemJson = JsonUtility.ToJson(itemData);

            PlayerDataController.Ins.SaveToJson(playerData);
            File.WriteAllText(Application.dataPath + Constant.ITEM_STATE_PATH, itemJson);
        }
    }

    // Pant
    private void InitPant()
    {
        ChangePant();
    }

    public void ChangePant()
    {
        PlayerData playerData = PlayerDataController.Ins.LoadFromJson();
        pantSkinID = (PantSkinID)playerData.pantID;

        TryPant(pantSkinID);
    }

    public void TryPant(PantSkinID pantSkinID)
    {
        Material pantMat = SkinController.Ins.GetPantMaterial(pantSkinID);
        var materials = pantRend.sharedMaterials;
        materials[0] = pantMat;
        pantRend.sharedMaterials = materials;
    }

    private void CheckUnlockOneTimePant()
    {
        PlayerData playerData = PlayerDataController.Ins.LoadFromJson();
        string itemJson = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
        ItemUnlockData itemData = JsonUtility.FromJson<ItemUnlockData>(itemJson);
        if (itemData.pantItemStates[(int)pantSkinID] == (int)Constant.ItemState.EquipOneTime)
        {
            itemData.pantItemStates[(int)pantSkinID] = (int)Constant.ItemState.Lock;
            pantSkinID = 0;
            playerData.pantID = 0;

            itemJson = JsonUtility.ToJson(itemData);

            PlayerDataController.Ins.SaveToJson(playerData);
            File.WriteAllText(Application.dataPath + Constant.ITEM_STATE_PATH, itemJson);
        }
    }

    //Shield
    private void InitShield()
    {
        ChangeShield();
    }

    public void ChangeShield()
    {
        PlayerData playerData = PlayerDataController.Ins.LoadFromJson();
        shieldSkinID = (ShieldSkinID)playerData.shieldID;

        TryShield(shieldSkinID);
    }

    public void TryShield(ShieldSkinID shieldSkinID)
    {
        if (shieldItem != null)
            Destroy(shieldItem.gameObject);

        shieldItem = ItemController.Ins.SetShield(shieldSkinID, shieldHolder);
    }

    private void CheckUnlockOneTimeShield()
    {
        PlayerData playerData = PlayerDataController.Ins.LoadFromJson();
        string itemJson = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
        ItemUnlockData itemData = JsonUtility.FromJson<ItemUnlockData>(itemJson);
        if (itemData.shieldItemStates[(int)shieldSkinID] == (int)Constant.ItemState.EquipOneTime)
        {
            itemData.shieldItemStates[(int)shieldSkinID] = (int)Constant.ItemState.Lock;
            shieldSkinID = 0;
            playerData.shieldID = 0;

            itemJson = JsonUtility.ToJson(itemData);

            PlayerDataController.Ins.SaveToJson(playerData);
            File.WriteAllText(Application.dataPath + Constant.ITEM_STATE_PATH, itemJson);
        }
    }

    // Body
    private void InitBody()
    {
        ChangeBody();
    }

    public void ChangeBody()
    {
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        bodyMatID = (BodyMaterialID)data.bodyID;

        TryBody(bodyMatID);
    }

    public void TryBody(BodyMaterialID bodyMatId)
    {
        Material bodyMat = SkinController.Ins.GetBodyMaterial(bodyMatId);
        var materials = bodyRend.sharedMaterials;
        materials[0] = bodyMat;
        bodyRend.sharedMaterials = materials;
    }

    // Tail
    private void InitTail()
    {
        ChangeTail();
    }

    public void ChangeTail()
    {
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        tailSkinID = (TailSkinID)data.tailID;

        TryTail(tailSkinID);
    }

    public void TryTail(TailSkinID tailSkinID)
    {
        if (tailItem != null)
            Destroy(tailItem.gameObject);

        tailItem = ItemController.Ins.SetTail(tailSkinID, tailHolder);
    }

    // Wing
    private void InitWing()
    {
        ChangeWing();
    }

    public void ChangeWing()
    {
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        wingSkinID = (WingSkinID)data.wingID;

        TryWing(wingSkinID);

    }

    public void TryWing(WingSkinID wingSkinID)
    {
        if (wingItem != null)
            Destroy(wingItem.gameObject);

        wingItem = ItemController.Ins.SetWing(wingSkinID, wingHolder);
    }
}
