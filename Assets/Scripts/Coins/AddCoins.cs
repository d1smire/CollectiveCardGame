using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AddCoins : MonoBehaviour
{
    [Inject] private Icoin _coins;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            _coins.AddCoins(100);
        }
    }
}
