using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSkin : MonoBehaviour
{
    public SkinnedMeshRenderer bodyRend;
    public SkinnedMeshRenderer pantRend;

    private Item hatItem;
    public Transform hatHolder;

    public void OnInit()
    {
        SetItems();
    }

    public void SetItems()
    {
        InitBody();
        InitPant();
        InitHat();
    }

    #region Initialize Skin
    private void InitPant()
    {
        Material pantMat = SkinController.Ins.GetRandomPantMaterial();
        var materials = pantRend.sharedMaterials;
        materials[0] = pantMat;
        pantRend.sharedMaterials = materials;
    }

    private void InitHat()
    {
        if (hatItem != null)
            Destroy(hatItem.gameObject);

        HatSkinID hatSkinID = SkinController.Ins.GetRandomHatItem();

        hatItem = ItemController.Ins.SetHat(hatSkinID, hatHolder);
    }

    private void InitBody()
    {
        Material bodyMat = SkinController.Ins.GetRandomBodyMaterial();
        var materials = bodyRend.sharedMaterials;
        materials[0] = bodyMat;
        bodyRend.sharedMaterials = materials;
    }
    #endregion
}
