using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Inventory
{
    private List<ItemAmount> itemList;
    private List<ItemAmount> favList;

    public event EventHandler OnItemListChanged;

    public int sizeMaxStack;

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

    void Awake()
    {
        sizeMaxStack = 5;
    }

    public Inventory(List<ItemAmount> itemList, List<ItemAmount> favList)
    {
        this.itemList = itemList;
        this.favList = favList;
    }

    public bool AddItem(ItemAmount item) //retourne un bool qui indique si il avait asser de place dans l'inventaire pour que l'item soit ajouté
    {
        bool itemAlreadyInInventory = false;
        int n = UI_Inventory.xSizeMaxInv * UI_Inventory.ySizeMaxInv; //nombre de slots
        foreach (ItemAmount inventoryItem in itemList)
        { 
            if (inventoryItem.Item.id == item.Item.id)
            {
                itemAlreadyInInventory = true;
                int i = item.Amount;
                int j = inventoryItem.Amount;
                if (i + j <= sizeMaxStack)
                {
                    inventoryItem.Amount += item.Amount;
                }
                else
                {
                    if (itemList.Count == n) return false;
                    inventoryItem.Amount = sizeMaxStack;
                    item.Amount = (i + j) - sizeMaxStack;
                    itemList.Add(item);
                }
            }
        }
        if (!itemAlreadyInInventory)
        {
            if (itemList.Count < n)
            {
                itemList.Add(item);
            }
            else
            {
                return false;
            }   
        }
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
        return true;
    }

    public bool DelItem(ItemAmount item) //supprime un item de l'inventaire et retourne false si il n'était pas présent dans l'inventaire
    {
        int n = 0; //indice du slot en train d'être traité
        foreach (ItemAmount inventoryItem in itemList.ToList()) //on parcours une copie de la liste pour pouvoir supprimer des elements pendant l'itération
        {
            if (inventoryItem.Item.id == item.Item.id)
            {
                int i = item.Amount;
                int j = inventoryItem.Amount;
                if (j > i) 
                {
                    itemList[n].Amount -= item.Amount;
                    return true;
                }
                else if (j == i)
                {
                    itemList.RemoveAt(n);
                    return true;
                }
                else
                {
                    itemList.RemoveAt(n);
                    item.Amount = i - j;
                }
            }
            n++; 
        }
        return false;
    }

    public List<ItemAmount> GetItemList()
    {
        return itemList;
    }

    public List<ItemAmount> GetFavList()
    {
        return favList;
    }

    public bool AddToFav(ItemAmount item) //retourne un bool qui indique si il avait asser de place dans les favoris pour que l'item soit ajouté
    {
        int n = UI_Inventory.xSizeMaxInv; //nombre de slots
        if (favList.Count < n)
        {
            favList.Add(item);
        }
        else
        {
            return false;
        }
        return true;
    }
}
