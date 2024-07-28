using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantShop : Singleton<PantShop>
{
    public List<PantShopItem> pantShopItems;

    public void TryPant(PantSkinID pantSkinID)
    {
        UIManager.Ins.player.GetPlayerSkin().TryPant(pantSkinID);
    }

    public void ChoosePant()
    {
        UIManager.Ins.player.GetPlayerSkin().ChangePant();
    }

    public void ResetPant()
    {
        UIManager.Ins.player.GetPlayerSkin().SetItems();
    }

    public void ResetShop()
    {
        for (int i = 0; i < pantShopItems.Count; i++)
        {
            pantShopItems[i].OnInit();
        }
    }
}
