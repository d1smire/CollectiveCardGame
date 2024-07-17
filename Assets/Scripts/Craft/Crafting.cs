using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Crafting : MonoBehaviour
{
    private CraftingRecipe[] _recipes;
    private InventoryObjects _inventory;
    private CraftingUI _craftingUI;

    public event Action<CraftingRecipe[]> GettingRecipes;
    public event Action<string> InfoAbt;

    private void Awake()
    {
        _craftingUI = GetComponent<CraftingUI>();
        _recipes = Resources.LoadAll<CraftingRecipe>("ScriptableObjects/Recipe");
        _inventory = Resources.Load<InventoryObjects>("Inventories/Inventory");
        _craftingUI.ButtonClicked += CraftSomething;
    }
    private void Start()
    {
        GettingRecipes?.Invoke(_recipes);
    }
    private void OnDestroy()
    {
        _craftingUI.ButtonClicked -= CraftSomething;
    }

    public void CraftSomething(int numRecipe) 
    {
        if (IsCrafted(numRecipe))
        {
            RemoveItemFromInventory(_recipes[numRecipe].ItemsForCraft, 1);
            AddItemToInventory(_recipes[numRecipe].CraftedItem, 1);
        }
        else
        {
            InfoAbt?.Invoke("Not enough resources to create item");
        }
    }

    private bool IsCrafted(int recipeNum) 
    {
        if (_inventory != null)
        {
            var amount = 0;
            for (int i = 0; i < _inventory.Container.Item.Length; i++) 
            {
                for (int j = 0; j < _recipes[recipeNum].ItemsForCraft.Length; j++)
                {
                    if (_inventory.Container.Item[i].ID == _recipes[recipeNum].ItemsForCraft[j].Id)
                    {
                        amount++;
                        if (_recipes[recipeNum].ItemsForCraft.Length == amount)
                        {
                            InfoAbt?.Invoke("Item: " + $"{_recipes[recipeNum].CraftedItem.name}" + " succesful crafted");
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        else 
            return false;
    }

    private void RemoveItemFromInventory(Items[] itemToRemove, int amount)
    {
        for (int i = 0; i < itemToRemove.Length; i++)
        {
            _inventory.RemoveItem(itemToRemove[i], amount);
        }
    }

    private void AddItemToInventory(Items itemToAdd, int amount) 
    {
        _inventory.AddItem(itemToAdd, amount);
    }
}
