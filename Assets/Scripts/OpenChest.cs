using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public enum DropType
{
    Common,       //50%
    Rare,        //25%
    Epic,       //20%
    Legendary  //5%
}

public class OpenChest : MonoBehaviour
{
    private InventoryObjects _inventory;
    [SerializeField] private int _chestCost;

    [Inject] private Icoin _coins;

    private DropFromBox _dropFromBox;
    private RandomDropByType _randomDropGenerator;

    public string ErrorMessage;
    public event Action<Items> ChestOpen;
    public event Action<string> CantOpen;   

    private void Start()
    {
        _inventory = Resources.Load<InventoryObjects>("Inventories/Inventory");
        _randomDropGenerator = new RandomDropByType();
        _dropFromBox = GetComponent<DropFromBox>();
    }

    public void BuyAndOpen(int amount) 
    {
        if (amount != 10 && _coins.amount >= _chestCost)
        {
            _coins.RemoveCoins(_chestCost);
            GenerateRandomDrop();
        }
        else if (amount == 10 && _coins.amount >= _chestCost * amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _coins.RemoveCoins(_chestCost);
                GenerateRandomDrop();
            }
        }
        else
        {
            CantOpen?.Invoke(ErrorMessage);
        }
    }
    private void GenerateRandomDrop()
    {
        var randomDrop = _randomDropGenerator.GetRandomDrop();
        var item = _dropFromBox.ChooseRandomItemByType(randomDrop);
        if (item != null)
        {
            _inventory.AddItem(item, 1);
            ChestOpen?.Invoke(item);
        }
    }
}

public class RandomDropByType // рандомайзер але так більше не робити бо -бланс
{
    private static System.Random random = new System.Random();

    public DropType GetRandomDrop()
    {
        int randomNumber = random.Next(1, 101);

        if (randomNumber <= 50)
        {
            return DropType.Common; 
        }
        else if (randomNumber <= 75)
        {
            return DropType.Rare; 
        }
        else if (randomNumber <= 95)
        {
            return DropType.Epic; 
        }
        else
        {
            return DropType.Legendary;
        }
    }
}
