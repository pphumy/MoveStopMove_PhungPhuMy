using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Renderer
    public Renderer obstaclesRend;

    // Material to change
    public Material transparentMat;
    public Material normalMat;

    public void ChangeInviMat()
    {
        var materials = obstaclesRend.sharedMaterials;
        materials[0] = transparentMat;
        obstaclesRend.sharedMaterials = materials;
    }

    public void ChangeNormalMat()
    {
        var materials = obstaclesRend.sharedMaterials;
        materials[0] = normalMat;
        obstaclesRend.sharedMaterials = materials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            if (Cache.Ins.GetCharacterBoundaryFromGameObj(other.gameObject).isPlayer)
            {
                ChangeInviMat();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            if (Cache.Ins.GetCharacterBoundaryFromGameObj(other.gameObject).isPlayer)
            {
                ChangeNormalMat();
            }
        }
    }
}
