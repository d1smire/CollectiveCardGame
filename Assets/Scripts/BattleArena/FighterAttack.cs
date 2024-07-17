using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAttack : MonoBehaviour
{
    private float _damage;

    private Fighter _target;

    public void setDamage(float damage) 
    {
        _damage = damage;
    }
    public void SetTarget(Fighter fighter) 
    {
        _target = fighter;
        Attack();
    }

    public void Attack() 
    {
        _target.GetDMG(_damage);
    }
}
