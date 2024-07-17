using System.Collections.Generic;
using UnityEngine;

public class DropFromBox : MonoBehaviour
{
    public ItemDataBaseObj ItemsDatabase;

    public Items ChooseRandomItemByType(DropType dropType)
    {
        var itemsOfType = new List<Items>();

        for (int i = 0; i < ItemsDatabase.Items.Length; i++)
        {
            if (dropType == ItemsDatabase.Items[i].dropType)
            {
                itemsOfType.Add(ItemsDatabase.Items[i]);
            }
        }

        if (itemsOfType.Count > 0)
        {
            int randomIndex = Random.Range(0, itemsOfType.Count);
            return itemsOfType[randomIndex];
        }

        return null;
    }
}
