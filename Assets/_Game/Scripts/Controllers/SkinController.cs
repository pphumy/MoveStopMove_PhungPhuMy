using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : Singleton<SkinController>
{
    public List<Weapon> weaponPrefabs;

    public List<Material> weaponMaterials;

    public List<Material> pantMaterials;

    public List<Item> hatPrefabs;

    public List<Item> shieldPrefabs;

    public List<Material> bodyMaterials;

    public List<Item> tailItems;

    public List<Item> wingItems;

    public List<Set> setsList;

    public Material GetWeaponMaterial(WeaponSkinID weaponSkinId)
    {
        return weaponMaterials[(int)weaponSkinId];
    }

    public Weapon GetWeapon(WeaponID weaponID)
    {
        return weaponPrefabs[(int)weaponID];
    }

    public Material GetPantMaterial(PantSkinID pantSkinID)
    {
        return pantMaterials[(int)pantSkinID];
    }

    public Item GetHatItem(HatSkinID hatSkinID)
    {
        return hatPrefabs[(int)hatSkinID];
    }

    public Item GetShieldItem(ShieldSkinID shieldSkinID)
    {
        return shieldPrefabs[(int)shieldSkinID];
    }

    public Material GetBodyMaterial(BodyMaterialID bodyMaterialID)
    {
        return bodyMaterials[(int)bodyMaterialID];
    }

    public Item GetTailItem(TailSkinID tailSkinID)
    {
        return tailItems[(int)tailSkinID];
    }

    public Item GetWingItem(WingSkinID wingSkinID)
    {
        return wingItems[(int)wingSkinID];
    }

    public Set GetSet(SetSkinID setSkinID)
    {
        return setsList[(int)setSkinID];
    }

    public Material GetRandomPantMaterial()
    {
        PantSkinID pantSkinID = (PantSkinID)Random.Range(0, pantMaterials.Count);
        return pantMaterials[(int)pantSkinID];
    }

    public HatSkinID GetRandomHatItem()
    {
        HatSkinID hatSkinID = (HatSkinID)Random.Range(0, hatPrefabs.Count);
        return hatSkinID;
    }

    public Material GetRandomBodyMaterial()
    {
        BodyMaterialID bodyMaterialID = (BodyMaterialID)Random.Range(0, bodyMaterials.Count);
        return bodyMaterials[(int)bodyMaterialID];
    }



}
