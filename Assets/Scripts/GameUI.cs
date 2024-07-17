using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    
    [SerializeField] private Image _moneyIcon;
    [SerializeField] private TextMeshProUGUI _moneyText;


    private void Start()
    {
        CoinController.CoinsChanged += ChangeCoinsOnUI;
    }

    private void OnDestroy()
    {
        CoinController.CoinsChanged -= ChangeCoinsOnUI;
    }

    public void ChangeCoinsOnUI(int count) 
    {
        _moneyText.text = count.ToString();
    }
}
