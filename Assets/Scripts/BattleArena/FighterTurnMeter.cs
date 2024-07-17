using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterTurnMeter : MonoBehaviour
{
    private float _stepValue;
    private float _maxValue = 100f;

    private FighterUI _ui;

    private float _value;

    public bool CanOffensive => _value >= _maxValue;

    private void Start()
    {
        _ui = GetComponentInChildren<FighterUI>();
    }

    public void setValue(float speed) 
    {
        _stepValue = speed;
    }

    public void Increase() 
    {
        _value = Mathf.Clamp(_value + _stepValue, 0, _maxValue);
        _ui.UpdateTurnMeter(_value);
    }

    public void Reset()
    {
        _value = 0;
        _ui.UpdateTurnMeter(_value);
    }
}
