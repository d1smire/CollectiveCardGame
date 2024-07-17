using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FighterAnimator), typeof(FighterAttack), typeof(FighterMove))]
public class Fighter : MonoBehaviour
{
    public PlayerParams playerParams;

    [SerializeField] private Fighter _target; 
    [SerializeField] private float _fightDistance;

    private float _maxHP;
    private float _def;
    private float _health;
    private GameObject _stayPoint;
    private FighterAnimator _animator;
    private FighterAttack _attack;
    private FighterMove _move;
    private FighterTurnMeter _turnMeter;
    private FighterUI _ui; 

    public event UnityAction<Fighter> TurnMeterFilled;
    public event UnityAction<Fighter> Died;

    private void Awake()
    {
        _animator = GetComponent<FighterAnimator>();
        _attack = GetComponent<FighterAttack>();
        _move = GetComponent<FighterMove>();
        _turnMeter = GetComponent<FighterTurnMeter>();
        _ui = GetComponentInChildren<FighterUI>();
        InitializeFighter();
    }

    private void InitializeFighter() 
    {
        _health = playerParams.Health;
        _attack.setDamage(playerParams.ATK);
        _turnMeter.setValue(playerParams.Speed);

        _maxHP = _health;
        _def = playerParams.DEF;
        _ui.SetMaxHp(_maxHP);
    }

    private void Start()
    {
        _ui.UpdateHealth(_health);
    }

    public void TurnMeter(bool test)
    {
        if (!test)
        {
            _turnMeter.Increase();
            
            if (_turnMeter.CanOffensive)
            {
                TurnMeterFilled?.Invoke(this);
            }
        }
        else 
        {
            _turnMeter.Reset();
        }
    }

    public void CurrentStayPoint(GameObject spawnPoint) 
    {
        _stayPoint = spawnPoint;
    }

    public void GetDMG(float damage) 
    {
        var damage_after_defense = damage * (1 - _def / 100);
        _health -= damage_after_defense;

        _ui.UpdateHealth(_health);
        if (_health <= 0) 
        {
            Die();
        }
    }

    private void Die() 
    {
        _animator.Dead();
        Died?.Invoke(this);
    }

    public Coroutine StartOffensive(Fighter target) 
    {
        return StartCoroutine(Offensive(target));
    }

    private IEnumerator Offensive(Fighter target)
    {
        _animator.Run(); 
        yield return _move.StartLookAtRotation(target.transform);
        yield return _move.StartMove(target.transform,_fightDistance); // ідемо до ворога

        _animator.Idle();
        _attack.SetTarget(target);
        yield return new WaitForSeconds(_animator.StartAttack()); // удар по ворозі

        _animator.Run();
        yield return _move.StartLookAtRotation(_stayPoint.transform);
        yield return _move.StartMove(_stayPoint.transform); // повертаємося назад

        yield return _move.StartRotation(_stayPoint.transform.rotation);
        _animator.Idle(); // повертаємося в початкову точку 
    }
}