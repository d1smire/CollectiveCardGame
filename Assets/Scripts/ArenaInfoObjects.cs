using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New arena parameters", menuName = "Arena/ArenaParams")]
public class ArenaInfoObjects : ScriptableObject
{
    public int ArenaID;
    public string ArenaName;
    public string ArenaDescription;
    public Sprite UIDisplay;
    public List<Fighter> Enemy;
    public int CoinReward;
    public string LinkOnArenaPrefab;
}
