using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;

[Serializable]
public class Inventory
{
    private List<ItemAmount> itemList;
    private List<bool> favList;          //liste de bool de la même taille de itemList qui indique quel item de l'inventaire est dans les fav (donc un max de 4 true)

    public Player player;

    public delegate void Inventory_OnItemListChanged();
    public event Inventory_OnItemListChanged OnItemListChanged;

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


    public Inventory(List<ItemAmount> itemList_, List<bool> favList_, Player player_)
    {
        this.itemList = itemList_;

        if (favList_.Count == 0)
        {
            this.favList = new List<bool>() { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        }

        else
        {
            this.favList = favList_;
        }
        
        this.player = player_;
        this.sizeMaxStack = 5;
    }

    public bool AddItem(ItemAmount item) //retourne un bool qui indique si il y avait assez de place dans l'inventaire pour que l'item soit ajouté
    {
        int x = item.Amount; // le total d'objet à placer

        if (player.uiInventory.NbrPlace(item.Item) < x) // Pas assez de place
        {
            Popup popup = GameObject.Find("Popup").GetComponent<Popup>();
            popup.popup("Inventaire Plein");
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
                for (int i = 0; i < x / p; i++)
                {
                    itemList.Add(new ItemAmount(Item: item.Item, Amount: p));
                }
                if (x % p > 0)
                {
                    itemList.Add(new ItemAmount(Item: item.Item, Amount: x % p));
                }
            }
            //Debug.Log("Ajouté" + item.Amount + "   " + item.Item.name);
            OnItemListChanged?.Invoke();
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
                    OnItemListChanged?.Invoke();
                    return true;
                }
                else if (j == i)
                {
                    itemList.RemoveAt(n);
                    DecaleFav(n);
                    OnItemListChanged?.Invoke();
                    return true;
                }
                else
                {
                    itemList.RemoveAt(n);
                    DecaleFav(n);
                    item.Amount = i - j;
                }
            }
            n++;
        }
        return false;
    }

    public void DelItemAtPos(int slot)  //supprime les items de l'inventaire à un certain emplacement
    {
        itemList.RemoveAt(slot);
        DecaleFav(slot);
        OnItemListChanged?.Invoke();
    }

    public List<ItemAmount> GetItemList()
    {
        return itemList;
    }

    public List<bool> GetFavList()
    {
        return favList;
    }

    public bool AddToFav(int slot) //retourne un bool qui indique si l'item était déjà dans les favoris ou si les favoris était plein
    {
        int n = UI_Inventory.nbrFavoris; //nombre de favoris (4 en général)

        //on compte le nombre de true dans favList dans count (donc d'item déjà dans les favoris)
        int count = 0;
        foreach (bool val in favList)
        {
            if (val) count++;
        }

        if (favList[slot] || (count > 4))
        {
            return false;
        }
        else
        {
            favList[slot] = true;
            OnItemListChanged?.Invoke();
            return true;
        }
    }

    public bool DelFav(int slot) //supprime un item des favoris et retourne false si l'item n'était déjà pas présent
    {
        if (!favList[slot])
        {
            return false;
        }
        else
        {
            favList[slot] = false;
            OnItemListChanged?.Invoke();
            return true;
        }
    }

    private void DecaleFav(int slot)  //decale les favoris à partir d'un certain rang (pour les mettres à jour quand on supprime un item de l'inventaire)
    {
        favList[slot] = false;
        for (int i = slot + 1; i < favList.Count; i++)
        {
            if (favList[i])
            {
                favList[i - 1] = true;
                favList[i] = false;
            }
        }
    }
}
