using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WeaponShopItem : MonoBehaviour
{
    public WeaponSkinSO weaponSkinSO;

    public TabButton weaponSkinTabButton;
    public TabGroup weaponSkinTabGroup;

    public GameObject unlockBtn;
    public GameObject selectBtn;
    public GameObject equippedBtn;

    public GameObject selectEdge;
    public GameObject lockUI;

    private Constant.ItemState itemState;

    private void OnEnable()
    {
        OnInit();
    }

    public void OnInit()
    {
        ResetAllUI();
        itemState = GetItemState();

        switch (itemState)
        {
            case Constant.ItemState.Lock:
                lockUI.SetActive(true);
                break;
            case Constant.ItemState.Equip:
                weaponSkinTabGroup.OnTabSelected(weaponSkinTabButton);
                break;
            default:
                break;
        }

        if (weaponSkinTabGroup.selectedTab == weaponSkinTabButton)
        {
            selectEdge.SetActive(true);
            OnChooseWeaponSkin();
        }
    }

    public void OnChooseWeaponSkin()
    {
        ResetAllBtn();
        itemState = GetItemState();
        switch (itemState)
        {
            case Constant.ItemState.Lock:
                unlockBtn.SetActive(true);
                break;
            case Constant.ItemState.NotEquip:
                selectBtn.SetActive(true);
                break;
            case Constant.ItemState.Equip:
                equippedBtn.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void OnUnlockWeaponSkin()
    {
        unlockBtn.SetActive(false);
        lockUI.SetActive(false);
        equippedBtn.SetActive(true);

        SelectWeaponSkin();
    }

    public void OnEquipWeaponSkin()
    {
        //EquipWeaponSkin();
    }

    public void OnSelectWeaponSkin()
    {       
        SelectWeaponSkin();
    }

    private void ResetAllUI()
    {
        selectEdge.SetActive(false);
        lockUI.SetActive(false);
    }

    private void SelectWeaponSkin()
    {
        // Load Item Data from Json File
        string itemJson = File.ReadAllText(PlayerDataController.Ins.itemData);
        ItemUnlockData itemData = JsonUtility.FromJson<ItemUnlockData>(itemJson);

        // Load Player Data from Json File
        string playerJson = File.ReadAllText(PlayerDataController.Ins.playerData);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerJson);

        // Reset current weapon
        int currentWeapon = playerData.weaponID;
        itemData.weaponSkinStates[currentWeapon] = (int)Constant.ItemState.NotEquip;
        playerData.weaponID = (int)weaponSkinSO.weaponID;

        // Reset current skin
        int currentWeaponSkin = playerData.weaponSkinID;
        itemData.weaponSkinStates[currentWeaponSkin] = (int)Constant.ItemState.NotEquip;
        playerData.weaponSkinID = (int)weaponSkinSO.weaponSkinID;

        // Set Skin state
        itemData.weaponSkinStates[(int)weaponSkinSO.weaponSkinID] = (int)Constant.ItemState.Equip;

        // Save Data
        itemJson = JsonUtility.ToJson(itemData);
        playerJson = JsonUtility.ToJson(playerData);

        File.WriteAllText(PlayerDataController.Ins.playerData, playerJson);
        File.WriteAllText(PlayerDataController.Ins.itemData, itemJson);
    }

    private void ResetAllBtn()
    {
        unlockBtn.SetActive(false);
        selectBtn.SetActive(false);
        equippedBtn.SetActive(false);
    }

    private Constant.ItemState GetItemState()
    {
        if (File.Exists(PlayerDataController.Ins.itemData))
        {
            string json = File.ReadAllText(PlayerDataController.Ins.itemData);
            Constant.ItemState itemState = (Constant.ItemState)JsonUtility.FromJson<ItemUnlockData>(json).weaponSkinStates[(int)weaponSkinSO.weaponSkinID];
            return itemState;
        }
        else
        {
            //config default item state
            ItemUnlockData data = new ItemUnlockData()
            {
                hatItemStates = new int[] { 0, 0, 0, 0, 0, 0 },
                pantItemStates = new int[] { 1, 0, 0, 0, 0, 0 },
                shieldItemStates = new int[] { 0, 0, 0, 0, 0, 0 },
                setItemStates = new int[] { 0, 0, 0, 0, 0 },
                weaponStates = new int[] { 1, 0, 0, 0, 0, 0 }
            };
            return 0;
        }

    }
}