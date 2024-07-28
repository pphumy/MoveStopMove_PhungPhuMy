using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArrow : Bullet
{
    public List<MeshRenderer> meshRends;

    protected override void SpecialMove() { }

    public override void InitSkin(WeaponSkinID skinID)
    {
        for (int i = 0; i < meshRends.Count; i++)
        {
            Material weaponSkin = SkinController.Ins.GetWeaponMaterial(skinID);
            ChangeRendMaterial(weaponSkin, i);
        }
    }

    private void ChangeRendMaterial(Material weaponSkin, int index)
    {
        var materials = meshRends[index].sharedMaterials;
        materials[0] = weaponSkin;
        materials[1] = weaponSkin;
        materials[2] = weaponSkin;
        meshRends[index].sharedMaterials = materials;
    }
}
