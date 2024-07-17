using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAnimator : MonoBehaviour
{
    private Animator _animator;

    private const string IdleParametr = "Idle";
    private const string DeadParametr = "Dead";
    private const string SpeedParametr = "Speed";
    private const string RunParametr = "Run";
    private const string LightAttackParametr = "LightAttack";
    private const string HeavyAttackParametr = "HeavyAttack";
    private const string UltimateParametr = "Ultimate";

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public float StartAttack()
    {
        _animator.SetTrigger(LightAttackParametr);
        AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        return clipInfo[0].clip.length;
    }

    public void Run() 
    {
        _animator.SetBool(IdleParametr, false);
        _animator.SetBool(RunParametr, true);
    }
    public void Dead()
    {
        _animator.SetBool(IdleParametr, false);
        _animator.SetBool(DeadParametr, true);
    }
    public void Idle()
    {
        _animator.SetBool(IdleParametr, true);
        _animator.SetBool(RunParametr, false);
    }
}
