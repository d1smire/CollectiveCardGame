using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Items/Database")]
public class ItemDataBaseObj : ScriptableObject, ISerializationCallbackReceiver
{
    public Items[] Items;
    public Dictionary<int, Items> GetItem = new Dictionary<int, Items>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            //Items[i].Id = i;
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, Items>();
    }
}