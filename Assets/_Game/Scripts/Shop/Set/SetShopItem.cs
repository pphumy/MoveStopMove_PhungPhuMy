using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SetShopItem : MonoBehaviour
{
    // Tab Infor
    [Header("TabInfor")]
    public TabButton setTabButton;
    public TabGroup setTabGroup;

    // Item Infor
    [Header("SetInfor")]
    public Set setSO;
    private Constant.ItemState itemState;

    // Button Group
    [Header("ButtonGroup")]
    public Text costText;
    public GameObject purchaseBtn;
    public GameObject equipBtn;
    public GameObject unequipBtn;

    // Button UI
    [Header("ButtonUI")]
    public GameObject equipped;
    public GameObject locked;
    public GameObject selectEdge;

    private void OnEnable()
    {
        OnInit();
    }

    public void OnInit()
    {
        ResetAllUI();
        setTabGroup.PreSelect();
        itemState = GetItemState();
        switch (itemState)
        {
            case Constant.ItemState.Lock:
                locked.SetActive(true);
                break;
            case Constant.ItemState.Equip:
                equipped.SetActive(true);
                setTabGroup.OnTabSelected(setTabButton);
                OnChooseItem();
                break;
            case Constant.ItemState.EquipOneTime:
                equipped.SetActive(true);
                break;
            default:
                break;
        }

        if (setTabGroup.selectedTab == setTabButton)
        {
            selectEdge.SetActive(true);
            OnChooseItem();
        }
    }

    public void OnChooseItem()
    {
        ResetAllBtn();
        itemState = GetItemState();
        switch (itemState)
        {
            case Constant.ItemState.Lock:
                purchaseBtn.SetActive(true);
                costText.text = setSO.setCost.ToString();
                break;
            case Constant.ItemState.NotEquip:
                equipBtn.SetActive(true);
                break;
            case Constant.ItemState.Equip:
                unequipBtn.SetActive(true);
                break;
            default:
                break;
        }

        TrySet();
    }

    public void TrySet()
    {
        SetShop.Ins.TrySet(setSO.setSkinID);
    }

    public void OnUnequipSet()
    {
        unequipBtn.SetActive(false);
        equipBtn.SetActive(true);
        equipped.SetActive(false);

        UnEquipItem();
        itemState = GetItemState();
        SetShop.Ins.ChooseSet();
    }

    public void OnEquipSet()
    {
        equipBtn.SetActive(false);
        unequipBtn.SetActive(true);

        if (itemState == Constant.ItemState.NotEquip)
        {
            EquipItem();
        }
        SetShop.Ins.ResetShop();
        SetShop.Ins.ChooseSet();
        OnChooseItem();
    }

    public void OnPurchase()
    {
        if (CoinController.Ins.GetCoins() < setSO.setCost)
        {
            return;
        }
        else
        {
            CoinController.Ins.DecreaseCoins(setSO.setCost);
            purchaseBtn.SetActive(false);
            unequipBtn.SetActive(true);

            EquipItem();
            SetShop.Ins.ResetShop();
        }
    }

    public int GetCost()
    {
        return setSO.setCost;
    }

    private void ResetAllBtn()
    {
        purchaseBtn.SetActive(false);
        equipBtn.SetActive(false);
        unequipBtn.SetActive(false);
    }

    private void ResetAllUI()
    {
        locked.SetActive(false);
        equipped.SetActive(false);
    }

    private void EquipItem()
    {
        // Change State Item
        string itemJson = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
        ItemUnlockData itemData = JsonUtility.FromJson<ItemUnlockData>(itemJson);
        itemData.setItemStates[(int)setSO.setSkinID] = (int)Constant.ItemState.Equip;

        // Remove Current Item
        string playerJson = File.ReadAllText(Application.dataPath + Constant.PLAYER_DATA_PATH);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerJson);

        //Set
        int currentSet = playerData.setID;
        itemData.setItemStates[currentSet] = (int)Constant.ItemState.NotEquip;

        //Hat
        int currentHat = playerData.hatID;
        if (itemData.hatItemStates[currentHat] == (int)Constant.ItemState.Equip)
        {
            itemData.hatItemStates[currentHat] = (int)Constant.ItemState.NotEquip;
        }
        else if (itemData.hatItemStates[currentHat] == (int)Constant.ItemState.EquipOneTime)
        {
            itemData.hatItemStates[currentHat] = (int)Constant.ItemState.NotEquipOneTime;
        }

        //Pant
        int currentPant = playerData.pantID;
        if (itemData.pantItemStates[currentPant] == (int)Constant.ItemState.Equip)
        {
            itemData.pantItemStates[currentPant] = (int)Constant.ItemState.NotEquip;
        }
        else if (itemData.pantItemStates[currentPant] == (int)Constant.ItemState.EquipOneTime)
        {
            itemData.pantItemStates[currentPant] = (int)Constant.ItemState.NotEquipOneTime;
        }

        //Shield
        int currentShield = playerData.shieldID;
        if (itemData.shieldItemStates[currentShield] == (int)Constant.ItemState.Equip)
        {
            itemData.shieldItemStates[currentShield] = (int)Constant.ItemState.NotEquip;
        }
        else if (itemData.shieldItemStates[currentShield] == (int)Constant.ItemState.EquipOneTime)
        {
            itemData.shieldItemStates[currentShield] = (int)Constant.ItemState.NotEquipOneTime;
        }


        // Set New Item
        playerData.bodyID = (int)setSO.bodyMatID;
        playerData.setID = (int)setSO.setSkinID;
        playerData.hatID = (int)setSO.hatSkinID;
        playerData.pantID = (int)setSO.pantSkinID;
        playerData.shieldID = (int)setSO.shieldSkinID;
        playerData.wingID = (int)setSO.wingSkinID;
        playerData.tailID = (int)setSO.tailSkinID;

        itemJson = JsonUtility.ToJson(itemData);
        playerJson = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.dataPath + Constant.PLAYER_DATA_PATH, playerJson);
        File.WriteAllText(Application.dataPath + Constant.ITEM_STATE_PATH, itemJson);
    }

    private void UnEquipItem()
    {
        string itemJson = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
        ItemUnlockData itemData = JsonUtility.FromJson<ItemUnlockData>(itemJson);

        string playerJson = File.ReadAllText(Application.dataPath + Constant.PLAYER_DATA_PATH);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerJson);

        int currentSet = playerData.setID;
        itemData.setItemStates[currentSet] = (int)Constant.ItemState.NotEquip;

        playerData.bodyID = 0;
        playerData.setID = 0;
        playerData.hatID = 0;
        playerData.pantID = 0;
        playerData.shieldID = 0;
        playerData.wingID = 0;
        playerData.tailID = 0;

        itemJson = JsonUtility.ToJson(itemData);
        playerJson = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.dataPath + Constant.PLAYER_DATA_PATH, playerJson);
        File.WriteAllText(Application.dataPath + Constant.ITEM_STATE_PATH, itemJson);
    }

    private Constant.ItemState GetItemState()
    {
        if (File.Exists(Application.dataPath + Constant.ITEM_STATE_PATH))
        {
            string json = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
            Constant.ItemState itemState = (Constant.ItemState)JsonUtility.FromJson<ItemUnlockData>(json).setItemStates[(int)setSO.setSkinID];
            return itemState;
        }
        else
        {
            return 0;
        }
    }
}
