using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : Singleton<WeaponShop>
{
    // Weapon
    public List<GameObject> weaponShops;
    private int currentWeaponIndex;

    // Player
    public Player player;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        // Open current weapon
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        currentWeaponIndex = data.weaponID;
        OpenWeaponShop(weaponShops[(int)data.weaponID]);
    }

    private void ResetShops()
    {
        foreach(GameObject weaponShop in weaponShops)
        {
            weaponShop.SetActive(false);
        }
    }

    private void OpenWeaponShop(GameObject weaponShop)
    {
        ResetShops();
        weaponShop.SetActive(true);
    }

    public void ChangeNext()
    {
        currentWeaponIndex++;

        if (currentWeaponIndex < weaponShops.Count)
        {
            OpenWeaponShop(weaponShops[currentWeaponIndex]);
        }
        else
        {
            currentWeaponIndex--;
        }
    }

    public void ChangePrev()
    {
        currentWeaponIndex--;

        if (currentWeaponIndex >= 0)
        {
            OpenWeaponShop(weaponShops[currentWeaponIndex]);
        }
        else
        {
            currentWeaponIndex++;
        }
    }

    public void ChooseWeapon()
    {
        player.ChangeWeapon();
    }
}
