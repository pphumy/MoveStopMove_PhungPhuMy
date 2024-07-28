using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponID
{
    WeaponHammer = 0,
    WeaponCandy = 1,
    WeaponArrow = 2,
}

public enum WeaponSkinID
{
    Hammer_Custom = 0,
    Hammer_1 = 1,
    Hammer_2 = 2,
    Candy_Custom = 3,
    Candy0_1 = 4,
    Candy0_2 = 5,
    Candy1_1 = 6,
    Candy2_1 = 7,
    Arrow_Custom = 8,
    Arrow_1 = 9
}

public enum PantSkinID
{
    no_pant = 0,
    comy = 1,
    dabao = 2,
    onion = 3,
    evil_set = 4,
    angle_set = 5,
}

public enum HatSkinID
{
    no_hat = 0,
    arrow = 1,
    crown = 2,
    ear = 3,
    flower = 4,
    hair = 5,
    headphone = 6,
    horn = 7,
    angle_hat = 8
}

public enum ShieldSkinID
{
    no_shield = 0,
    black = 1,
    captain = 2,
    angle_bow = 3
}

public enum BodyMaterialID
{
    Green,
    Red,
    Blue,
    Yellow,
    Pink,
    Purple,
    Orange
}

public enum TailSkinID
{
    no_tail,
    tail_evil_set
}

public enum WingSkinID
{
    no_wing,
    wing_evil_set,
    wing_angle_set
}

public enum SetSkinID
{
    no_set = 0,
    evil_set = 1,
    angle_set = 2
}

public class ItemController : Singleton<ItemController>
{
    public Weapon SetWeapon(WeaponID ID, WeaponSkinID SkinID, Transform weaponHolder)
    {
        Weapon weapon = Instantiate(SkinController.Ins.GetWeapon(ID), weaponHolder);
        weapon.InitSkin(SkinID);

        return weapon;
    }

    public Item SetHat(HatSkinID ID, Transform hatHolder)
    {
        if (SkinController.Ins.GetHatItem(ID) != null)
        {
                Item hat = Instantiate(SkinController.Ins.GetHatItem(ID), hatHolder);
            return hat;
        }
        return null;
    }

    public Item SetShield(ShieldSkinID ID, Transform shieldHolder)
    {
        if (SkinController.Ins.GetShieldItem(ID) != null)
        {
            Item shield = Instantiate(SkinController.Ins.GetShieldItem(ID), shieldHolder);
            return shield;
        }

        return null;
    }

    public Item SetTail(TailSkinID ID, Transform tailHolder)
    {
        if (SkinController.Ins.GetTailItem(ID) != null)
        {
            Item tail = Instantiate(SkinController.Ins.GetTailItem(ID), tailHolder);
            return tail;
        }
        return null;
    }

    public Item SetWing(WingSkinID ID, Transform wingHolder)
    {
        if (SkinController.Ins.GetWingItem(ID) != null)
        {
            Item wing = Instantiate(SkinController.Ins.GetWingItem(ID), wingHolder);
            return wing;
        }
        return null;
    }
}

