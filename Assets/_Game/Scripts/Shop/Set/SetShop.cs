using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShop : Singleton<SetShop>
{
    public Player player;
    public List<SetShopItem> setShopItems;

    public void TrySet(SetSkinID setSkinID)
    {
        Set set = SkinController.Ins.GetSet(setSkinID);
        player.GetPlayerSkin().TryPant(set.pantSkinID);
        player.GetPlayerSkin().TryBody(set.bodyMatID);
        player.GetPlayerSkin().TryHat(set.hatSkinID);
        player.GetPlayerSkin().TryTail(set.tailSkinID);
        player.GetPlayerSkin().TryWing(set.wingSkinID);
        player.GetPlayerSkin().TryShield(set.shieldSkinID);
    }

    public void ChooseSet()
    {
        player.GetPlayerSkin().ChangePant();
        player.GetPlayerSkin().ChangeBody();
        player.GetPlayerSkin().ChangeHat();
        player.GetPlayerSkin().ChangeTail();
        player.GetPlayerSkin().ChangeWing();
        player.GetPlayerSkin().ChangeShield();
    }

    public void ResetSet()
    {
        player.GetPlayerSkin().SetItems();
    }

    public void ResetShop()
    {
        for (int i = 0; i < setShopItems.Count; i++)
        {
            setShopItems[i].OnInit();
        }
    }
}
