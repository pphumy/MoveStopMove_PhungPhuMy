using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCandy : Bullet
{
    protected override void SpecialMove() { }

    public override void InitSkin(WeaponSkinID skinID)
    {
        Material weaponSkin = SkinController.Ins.GetWeaponMaterial(skinID);
        ChangeRendMat(weaponSkin);
    }

    private void ChangeRendMat(Material weaponSkin)
    {
        var materials = meshRend.sharedMaterials;
        materials[0] = weaponSkin;
        materials[1] = weaponSkin;
        materials[2] = weaponSkin;
        meshRend.sharedMaterials = materials;
    }

}
