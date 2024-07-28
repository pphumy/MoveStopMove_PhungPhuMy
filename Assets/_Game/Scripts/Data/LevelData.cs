using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObj/Level", order = 1)]
public class LevelData : ScriptableObject
{
    public GameObject gamePlane;
    public int numOfBots;
}
