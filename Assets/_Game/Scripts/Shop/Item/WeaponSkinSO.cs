using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Skin", menuName = "ScriptableObj/WeaponSkin", order = 1)]
public class WeaponSkinSO : ScriptableObject
{
    public WeaponID weaponID;
    public WeaponSkinID weaponSkinID;
    public int cost;
}
