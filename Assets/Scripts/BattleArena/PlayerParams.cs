using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharType
{
    player,
    enemy
}

public enum CharRole
{
    archer,
    melee,
    wizard,
}

[CreateAssetMenu(fileName = "New params", menuName = "Characters/Characteristics")]
public class PlayerParams : ScriptableObject
{
    public CharType CharType;
    public CharRole CharRoles;
    
    public float Health;
    public float ATK;
    public float DEF;
    public float Speed;
    public int CritChance;
    public int EnergyRecovery;
    public float EnergyForUltimates;
    public float NeedEnergyToUseUltimates;

    public float CharXP;
    public int CharLevel;
}
