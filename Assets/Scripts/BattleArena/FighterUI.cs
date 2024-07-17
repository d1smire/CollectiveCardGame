using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterUI : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;
    [SerializeField] private Slider sliderTurnMeter;

    private float _maxHealth;

    public void SetMaxHp(float Hp) 
    {
        _maxHealth = Hp;
    }

    public void UpdateHealth(float currentHealth)
    {
        if (sliderHP != null)
        {
            sliderHP.value = currentHealth / _maxHealth;
        }
        else
        {
            Debug.LogError("Slider for HP is not assigned!");
        }
    }

    public void UpdateTurnMeter(float turnMeter)
    {
        if (sliderTurnMeter != null)
        {
            sliderTurnMeter.value = turnMeter;
        }
        else
        {
            Debug.LogError("Slider for Turn Meter is not assigned!");
        }
    }
}
