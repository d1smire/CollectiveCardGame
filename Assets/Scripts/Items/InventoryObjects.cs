using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
public class InventoryObjects : ScriptableObject
{
    public string savePath;
    public ItemDataBaseObj database;
    public Inventory Container;

    public void AddItem(Items _items, int _amount)
    {

        for (int i = 0; i < Container.Item.Length; i++)
        {
            if (Container.Item[i].ID == _items.Id)
            {
                Container.Item[i].addAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_items, _amount);
    }

    public void RemoveItem(Items _items, int _amount)
    {
        for (int i = 0; i < Container.Item.Length; i++)
        {
            if (Container.Item[i].ID == _items.Id)
            {
                Container.Item[i].removeAmount(_amount);
                if (Container.Item[i].amount <= 0)
                {
                    RemoveItemFromSlot(i);
                }
                return;
            }
        }
    }

    private void RemoveItemFromSlot(int index)
    {
        Container.Item[index].UpdateSlot(-1, null, 0);
    }

    public InventorySlot SetEmptySlot(Items _item, int _amount)
    {
        for (int i = 0; i < Container.Item.Length; i++)
        {
            if (Container.Item[i].ID <= -1)
            {
                Container.Item[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Item[i];
            }
        }
        return null;
    }
    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }

    [ContextMenu("Save")]
    public void Save()
    {
        string saveData = JsonUtility.ToJson(Container, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();

        //IFormatter formatter = new BinaryFormatter();
        //Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Write);
        //formatter.Serialize(stream, Container);
        //stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), Container);
            file.Close();

            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            //Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            //for (int i = 0; i < Container.Item.Length; i++)
            //{
            //    Container.Item[i].UpdateSlot(newContainer.Item[i].ID, newContainer.Item[i].item, newContainer.Item[i].amount);
            //}
            //stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Item = new InventorySlot[96];
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Items item;
    public int amount;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, Items _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, Items _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void addAmount(int Value)
    {
        amount += Value;
    }
    public void removeAmount(int Value)
    {
        amount -= Value;
    }
}
