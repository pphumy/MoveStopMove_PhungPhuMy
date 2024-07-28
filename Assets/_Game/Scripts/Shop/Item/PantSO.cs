using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pant", menuName = "ScriptableObj/Pant", order = 1)]
public class PantSO : ScriptableObject
{
    public PantSkinID pantSkinID;
    public Material pantMat;
    public int pantCost;
}
