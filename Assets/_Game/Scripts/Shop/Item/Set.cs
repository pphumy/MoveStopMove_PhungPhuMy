using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Set", menuName = "ScriptableObj/Set", order = 1)]
public class Set : ScriptableObject
{
    public SetSkinID setSkinID;

    public BodyMaterialID bodyMatID;
    public HatSkinID hatSkinID;
    public PantSkinID pantSkinID;
    public TailSkinID tailSkinID;
    public WingSkinID wingSkinID;
    public ShieldSkinID shieldSkinID;

    public int setCost;
}
