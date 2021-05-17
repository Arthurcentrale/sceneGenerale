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

    public Player player;

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

    public Inventory(List<ItemAmount> itemList, List<ItemAmount> favList,Player player)
    {
        this.itemList = itemList;
        this.favList = favList;
        this.player = player;
        this.sizeMaxStack = 5;
    }

    public bool AddItem(ItemAmount item) //retourne un bool qui indique si il avait assez de place dans l'inventaire pour que l'item soit ajouté
    {
        int x = item.Amount; // le total d'objet à placer
        if (player.uiInventory.NbrPlace(item.Item) < x) // Pas assez de place
        {
            return false;
        }
        else
        {
            int p; // place disponible dans chaque slot
            foreach (ItemAmount inventoryItem in itemList)
            {
                if (x == 0) return true;
                else
                {
                    if (inventoryItem.Item.id == item.Item.id)
                    {
                        p = (sizeMaxStack / item.Item.Weight) - inventoryItem.Amount;
                        if (p >= x)
                        {
                            inventoryItem.Amount += x;
                            x = 0;
                        }
                        else
                        {
                            inventoryItem.Amount += p;
                            x -= p;
                        }
                    }
                }
            }
            if (x > 0) // si il reste des items à placer dans des slots vides
            {
                p = sizeMaxStack / item.Item.Weight; // on redefinie p comme le nombre d'item plaçable dans un slot
                for (int i=0; i<x/p; i++)
                {
                    itemList.Add(new ItemAmount(Item: item.Item, Amount: p));
                }
                if (x%p > 0)
                {
                    itemList.Add(new ItemAmount(Item: item.Item, Amount: x % p));
                }
            }
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
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
                    OnItemListChanged?.Invoke(this, EventArgs.Empty);
                    return true;
                }
                else if (j == i)
                {
                    itemList.RemoveAt(n);
                    OnItemListChanged?.Invoke(this, EventArgs.Empty);
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
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public void DelFavAtIndex(int i) //supprime un item des favoris
    {
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        favList.RemoveAt(i);
    }
}
