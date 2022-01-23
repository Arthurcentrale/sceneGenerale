﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public List<Item> ListeNourriture;

    // Start is called before the first frame update
    void Start()
    {
        var list = Resources.LoadAll("items", typeof(Item)).Cast<Item>();

        foreach (Item item in list)
        {
            if (item.isFood == true)
            {
                ListeNourriture.Add(item);
            }
        }
    }
    public Item Findinlist(string texte)
    {
        Item item = new Item();
        for(int i = 0; i < ListeNourriture.Count; i++)
        {
            if (texte == ListeNourriture[i].ItemName) item = ListeNourriture[i];
        }
        return item;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
