using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CoinController : Icoin
{
    public static event Action<int> CoinsChanged;

    private string _coinName;
    private int _coinAmount;

    public string name
    {
        get => _coinName;
        set => _coinName = value;
    }

    public int amount
    {
        get => _coinAmount;
        set
        {
            _coinAmount = value;
            CoinsChanged?.Invoke(_coinAmount); 
        }
    }

    public void AddCoins(int count)
    {
        if (count < 0)
            return;

        amount += count;
    }

    public void RemoveCoins(int count)
    {
        if (count < 0)
            return;

        if (_coinAmount >= count)
        {
            amount -= count;
        }
    }
}

