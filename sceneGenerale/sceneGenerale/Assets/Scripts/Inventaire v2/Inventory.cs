using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Inventory
{
    private List<ItemAmount> itemList;

    public event EventHandler OnItemListChanged;


    /*
    public void Init(List<ItemAmount> itemList)
    {
        this.itemList = itemList;
    }

    public static Inventory CreateInstance(List<ItemAmount> itemList)
    {
        var data = ScriptableObject.CreateInstance<Inventory>();
        data.Init(itemList);

        data.AddItem(ItemAmount.CreateInstance(new Item("Sword"),2));
        Debug.Log(itemList.Count);

        return data;
    }
    */

    public Inventory(List<ItemAmount> itemList)
    {
        this.itemList = itemList;
    }

    public void AddItem(ItemAmount item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }

    public List<ItemAmount> GetItemList()
    {
        return itemList;
    }
}
