using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New creafting recipe", menuName = "Craft/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public Items[] ItemsForCraft;

    public Items CraftedItem;
}
