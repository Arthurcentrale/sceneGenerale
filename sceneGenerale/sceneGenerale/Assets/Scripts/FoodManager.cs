using System.Collections;
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
        Debug.Log(ListeNourriture.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
