using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemAmount
{
    public Item Item;
    public int Amount;
    public int durability;

    /*
    public void Init(Item Item, int Amount)
    {
        this.Item = Item;
        this.Amount = Amount;
    }

    public static ItemAmount CreateInstance(Item Item, int Amount)
    {
        var data = ScriptableObject.CreateInstance<ItemAmount>();
        data.Init(Item,Amount);
        return data;
    }
    */

    public ItemAmount(Item Item, int Amount)
    {
        this.Item = Item;
        this.Amount = Amount;
    }


}

[CreateAssetMenu]
public class RecetteCraft : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

}


