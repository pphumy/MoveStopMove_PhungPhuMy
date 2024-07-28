using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shield", menuName = "ScriptableObj/Shield", order = 1)]
public class ShieldSO : ScriptableObject
{
    public ShieldSkinID shieldSkinID;
    public GameObject shieldPrefab;
    public int shieldCost;
}
