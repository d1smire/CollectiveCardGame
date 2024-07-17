using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attributes
{
    CritChance,
    ATK,
    DEF,
    HP
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Items : ScriptableObject
{
    public int Id;
    public DropType dropType;
    public string Name;
    public Sprite UIDisplay;
    [TextArea(15, 20)]
    public string description;

    public EquipBuffs buffs;
    public string LinkOnPrefab;
    public float ItemXP;
    public int ItemLevel;
}

[System.Serializable]
public class EquipBuffs
{
    public Attributes buff;
    public float value;
}
