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
        uiInventory = GameObject.Find("Inventaire2").transform.GetChild(3).gameObject.GetComponent<UI_Inventory>();
        uiInventory.SetInventory(inventory);

        //ItemWorld.SpawnItemWorld(new Vector3(70, 1.3f, 13), new ItemAmount(Item: Item.Create_Instance("Wood"), Amount: 1));
        //ItemWorld.SpawnItemWorld(new Vector3(75, 1.3f, 13), new ItemAmount(Item: Item.Create_Instance("Berry"), Amount: 1));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("test");
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
}
