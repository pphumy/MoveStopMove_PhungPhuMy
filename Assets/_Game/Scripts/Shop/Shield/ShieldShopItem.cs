using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShieldShopItem : MonoBehaviour
{
    // Tab Infor
    [Header("TabInfor")]
    public TabButton shieldTabButton;
    public TabGroup shieldTabGroup;

    // Item Infor
    [Header("ShieldInfor")]
    public ShieldSO shieldSO;
    private Constant.ItemState itemState;
    private Constant.ItemUnlockOneTime itemUnlockOneTime;

    // Button Group
    [Header("ButtonGroup")]
    public Text costText;
    public GameObject purchaseBtn;
    public GameObject equipBtn;
    public GameObject unequipBtn;
    public GameObject oneTimeText;
    public GameObject tryBtn;
    public GameObject oneTimeUnequipText;

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
        shieldTabGroup.PreSelect();
        itemState = GetItemState();
        itemUnlockOneTime = GetItemUnlockOneTime();

        switch (itemState)
        {
            case Constant.ItemState.Lock:
                locked.SetActive(true);
                break;
            case Constant.ItemState.Equip:
                equipped.SetActive(true);
                shieldTabGroup.OnTabSelected(shieldTabButton);
                break;
            case Constant.ItemState.EquipOneTime:
                equipped.SetActive(true);
                shieldTabGroup.OnTabSelected(shieldTabButton);
                break;
            default:
                break;
        }

        if (shieldTabGroup.selectedTab == shieldTabButton)
        {
            selectEdge.SetActive(true);
            OnChooseItem();
        }
    }

    public void OnChooseItem()
    {
        ResetAllBtn();
        itemState = GetItemState();
        itemUnlockOneTime = GetItemUnlockOneTime();
        switch (itemState)
        {
            case Constant.ItemState.Lock:
                purchaseBtn.SetActive(true);
                costText.text = shieldSO.shieldCost.ToString();
                if (itemUnlockOneTime == Constant.ItemUnlockOneTime.NotUsed)
                {
                    tryBtn.SetActive(true);
                }
                break;
            case Constant.ItemState.NotEquip:
                equipBtn.SetActive(true);

                break;
            case Constant.ItemState.NotEquipOneTime:
                equipBtn.SetActive(true);
                oneTimeText.SetActive(true);
                break;
            case Constant.ItemState.Equip:
                unequipBtn.SetActive(true);
                break;
            case Constant.ItemState.EquipOneTime:
                unequipBtn.SetActive(true);

                oneTimeUnequipText.SetActive(true);
                break;
            default:
                break;
        }

        TryShield();
    }


    public void TryShield()
    {
        ShieldShop.Ins.TryShield(shieldSO.shieldSkinID);
    }

    public void OnUnequipShield()
    {
        unequipBtn.SetActive(false);
        equipBtn.SetActive(true);
        equipped.SetActive(false);

        UnEquipItem();
        itemState = GetItemState();
        if (itemState == Constant.ItemState.NotEquipOneTime) oneTimeText.SetActive(true);
        ShieldShop.Ins.ChooseShield();
        OnChooseItem();
    }

    public void OnEquipShield()
    {
        equipBtn.SetActive(false);
        unequipBtn.SetActive(true);

        if (itemState == Constant.ItemState.NotEquip)
        {
            EquipItem(false);
        }
        else if (itemState == Constant.ItemState.NotEquipOneTime)
        {
            EquipItem(true);
            oneTimeText.SetActive(true);
        }
        ShieldShop.Ins.ResetShop();
        ShieldShop.Ins.ChooseShield();
        OnChooseItem();
    }

    public void OnPurchase()
    {
        if (CoinController.Ins.GetCoins() < shieldSO.shieldCost)
        {
            return;
        }
        else
        {
            CoinController.Ins.DecreaseCoins(shieldSO.shieldCost);
            purchaseBtn.SetActive(false);
            unequipBtn.SetActive(true);

            EquipItem(false);
            ShieldShop.Ins.ResetShop();

        }
    }

    public void OnUnlockOneTime()
    {
        tryBtn.SetActive(false);
        purchaseBtn.SetActive(false);
        unequipBtn.SetActive(true);
        oneTimeUnequipText.SetActive(true);
        RemoveUnlockOneTime();
        EquipItem(true);
        ShieldShop.Ins.ResetShop();
    }

    public int GetCost()
    {
        return shieldSO.shieldCost;
    }

    private void ResetAllBtn()
    {
        purchaseBtn.SetActive(false);
        equipBtn.SetActive(false);
        unequipBtn.SetActive(false);
        oneTimeText.SetActive(false);
        tryBtn.SetActive(false);
        oneTimeUnequipText.SetActive(false);
    }

    private void ResetAllUI()
    {
        locked.SetActive(false);
        equipped.SetActive(false);
    }

    private void RemoveUnlockOneTime()
    {
        string itemJson = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
        ItemUnlockData itemData = JsonUtility.FromJson<ItemUnlockData>(itemJson);

        itemData.shieldUnlockOneTime[(int)shieldSO.shieldSkinID] = (int)Constant.ItemUnlockOneTime.Used;

        itemJson = JsonUtility.ToJson(itemData);
        File.WriteAllText(Application.dataPath + Constant.ITEM_STATE_PATH, itemJson);
    }

    private void EquipItem(bool isOneTime)
    {
        // Change State Item
        string itemJson = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
        ItemUnlockData itemData = JsonUtility.FromJson<ItemUnlockData>(itemJson);
        if (isOneTime)
        {
            itemData.shieldItemStates[(int)shieldSO.shieldSkinID] = (int)Constant.ItemState.EquipOneTime;
        }
        else
        {
            itemData.shieldItemStates[(int)shieldSO.shieldSkinID] = (int)Constant.ItemState.Equip;
        }

        // Remove Current Item
        string playerJson = File.ReadAllText(Application.dataPath + Constant.PLAYER_DATA_PATH);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerJson);

        int currentItem = playerData.shieldID;

        if (itemData.shieldItemStates[currentItem] == (int)Constant.ItemState.Equip)
        {
            itemData.shieldItemStates[currentItem] = (int)Constant.ItemState.NotEquip;
        }
        else if (itemData.shieldItemStates[currentItem] == (int)Constant.ItemState.EquipOneTime)
        {
            itemData.shieldItemStates[currentItem] = (int)Constant.ItemState.NotEquipOneTime;
        }

        // Set New Item
        playerData.shieldID = (int)shieldSO.shieldSkinID;

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

        int currentItem = playerData.shieldID;

        if (itemData.shieldItemStates[currentItem] == (int)Constant.ItemState.Equip)
        {
            itemData.shieldItemStates[currentItem] = (int)Constant.ItemState.NotEquip;
        }
        else if (itemData.shieldItemStates[currentItem] == (int)Constant.ItemState.EquipOneTime)
        {
            itemData.shieldItemStates[currentItem] = (int)Constant.ItemState.NotEquipOneTime;
        }

        playerData.shieldID = 0;

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
            Constant.ItemState itemState = (Constant.ItemState)JsonUtility.FromJson<ItemUnlockData>(json).shieldItemStates[(int)shieldSO.shieldSkinID];
            return itemState;
        }
        else
        {
            return 0;
        }
    }

    private Constant.ItemUnlockOneTime GetItemUnlockOneTime()
    {
        if (File.Exists(Application.dataPath + Constant.ITEM_STATE_PATH))
        {
            string json = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
            Constant.ItemUnlockOneTime itemUnlockOneTime = (Constant.ItemUnlockOneTime)JsonUtility.FromJson<ItemUnlockData>(json).shieldUnlockOneTime[(int)shieldSO.shieldSkinID];
            return itemUnlockOneTime;
        }
        else
        {
            return 0;
        }
    }

}
