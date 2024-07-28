using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hat", menuName = "ScriptableObj/Hat", order = 1)]
public class HatSO : ScriptableObject
{
    public HatSkinID hatSkinID;
    public GameObject hatPrefab;
    public int hatCost;
}
