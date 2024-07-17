using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private InventoryObjects _inventory;

    [Inject] private Icoin _coin;

    private UIUpgrade _upgradeUI;

    public event Action<string> InfoAbt;

    private void Start()
    {
        _inventory = Resources.Load<InventoryObjects>("Inventories/Inventory");
        _upgradeUI = GetComponentInChildren<UIUpgrade>();
        _upgradeUI.ItemID += UpdateItem;
    }

    private void OnDestroy()
    {
        _upgradeUI.ItemID -= UpdateItem;
    }

    private void UpdateItem(int itemID) 
    {
        if (_coin.amount >= 10) 
        {
            for (int i = 0; i < _inventory.Container.Item.Length; i++)
            {
                if (_inventory.Container.Item[i].ID != -1)
                {
                    if (_inventory.Container.Item[i].item.Id == itemID)
                    {
                        _coin.RemoveCoins(10);
                        _inventory.Container.Item[i].item.ItemXP += 100;
                        InfoAbt?.Invoke("Item:" + _inventory.Container.Item[i].item.Name + " succesfull upgrade");
                    }
                }
            }
        }
        else 
        {
            InfoAbt?.Invoke("Dont enough money to upgrade item");
        }
    }
}
