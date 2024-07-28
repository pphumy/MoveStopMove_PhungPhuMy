using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldShop : Singleton<ShieldShop>
{
    public List<ShieldShopItem> shieldShopItems;

    public void TryShield(ShieldSkinID shieldSkinID)
    {
        UIManager.Ins.player.GetPlayerSkin().TryShield(shieldSkinID);
    }

    public void ChooseShield()
    {
        UIManager.Ins.player.GetPlayerSkin().ChangeShield();
    }

    public void ResetShield()
    {
        UIManager.Ins.player.GetPlayerSkin().SetItems();
    }

    public void ResetShop()
    {
        for (int i = 0; i < shieldShopItems.Count; i++)
        {
            shieldShopItems[i].OnInit();
        }
    }
}
