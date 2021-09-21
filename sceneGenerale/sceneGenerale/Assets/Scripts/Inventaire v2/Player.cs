using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    [SerializeField] public UI_Inventory uiInventory;

    void Start()
    {
        //inventory = Inventory.CreateInstance(new List<ItemAmount>());
        inventory = new Inventory(new List<ItemAmount>(), new List<ItemAmount>(),this);
        uiInventory = GameObject.Find("Inventaire2").transform.GetChild(0).gameObject.GetComponent<UI_Inventory>();
        uiInventory.SetInventory(inventory);
    }
}
