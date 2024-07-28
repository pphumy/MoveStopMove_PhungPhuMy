using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatShop : Singleton<HatShop>
{
    public List<HatShopItem> hatShopItems;

    public void TryHat(HatSkinID hatSkinID)
    {
        UIManager.Ins.player.GetPlayerSkin().TryHat(hatSkinID);
    }

    public void ChooseHat()
    {
        UIManager.Ins.player.GetPlayerSkin().ChangeHat();
    }

    public void ResetHat()
    {
        UIManager.Ins.player.GetPlayerSkin().SetItems();
    }

    public void ResetShop()
    {
        for (int i = 0; i < hatShopItems.Count; i++)
        {
            hatShopItems[i].OnInit();
        }
    }
}
