using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCandy : Weapon
{
    public override void InitSkin(WeaponSkinID _skinID)
    {
        skinID = _skinID;
        ChangeRendMat(skinID);
    }

    private void ChangeRendMat(WeaponSkinID skinID)
    {
        Material weaponSkin = SkinController.Ins.GetWeaponMaterial(skinID);
        var materials = meshRend.sharedMaterials;
        materials[0] = weaponSkin;
        materials[1] = weaponSkin;
        materials[2] = weaponSkin;
        meshRend.sharedMaterials = materials;
    }
}
