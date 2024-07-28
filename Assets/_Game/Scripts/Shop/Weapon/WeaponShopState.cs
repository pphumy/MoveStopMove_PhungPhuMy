using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WeaponShopState : MonoBehaviour
{
    public GameObject lockShop;
    public GameObject unlockShop;
    public GameObject cannotUnlockShop;

    public Text lockCostText;
    public Text cannotUnlockCostText;

    public int cost;

    public WeaponID weaponID;

    private void OnEnable()
    {
        ResetAllShopState();

        string itemJson = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
        ItemUnlockData itemData = JsonUtility.FromJson<ItemUnlockData>(itemJson);

        if (itemData.weaponStates[(int)weaponID] == (int)Constant.ItemState.Lock)
        {
            if (itemData.weaponStates[(int)weaponID - 1] == (int)Constant.ItemState.Lock)
            {
                cannotUnlockShop.SetActive(true);
                cannotUnlockCostText.text = cost.ToString();
            }
            else
            {
                lockShop.SetActive(true);
                lockCostText.text = cost.ToString();
            }
        }
        else
        {
            unlockShop.SetActive(true);
        }
    }

    public void OnUnlockWeapon()
    {
        if (cost < CoinController.Ins.GetCoins())
        {
            CoinController.Ins.DecreaseCoins(cost);
            lockShop.SetActive(false);
            unlockShop.SetActive(true);

            UnlockWeapon();
        }
    }

    private void UnlockWeapon()
    {
        string itemJson = File.ReadAllText(Application.dataPath + Constant.ITEM_STATE_PATH);
        ItemUnlockData itemData = JsonUtility.FromJson<ItemUnlockData>(itemJson);

        itemData.weaponStates[(int)weaponID] = (int)Constant.ItemState.NotEquip;

        itemJson = JsonUtility.ToJson(itemData);
        File.WriteAllText(Application.dataPath + Constant.ITEM_STATE_PATH, itemJson);
    }

    private void ResetAllShopState()
    {
        lockShop.SetActive(false);
        unlockShop.SetActive(false);
        cannotUnlockShop.SetActive(false);
    }
}
