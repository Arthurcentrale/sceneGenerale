using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    [SerializeField] public UI_Inventory uiInventory;

    void Start()
    {
        // ------------------------------------------------------------------------- //

        // L'inventaire est maintenant initialisé sur le GameHandler pour avoir      //
        // à la sauvegarde

        // ------------------------------------------------------------------------- //
        
        //inventory = Inventory.CreateInstance(new List<ItemAmount>());              
    }

    public void createInventory(List<ItemAmount> listeItems, List<bool> listeFavoris)        
    {
        inventory = new Inventory(listeItems, listeFavoris, this);
        uiInventory = GameObject.Find("Inventaire2").transform.GetChild(0).gameObject.GetComponent<UI_Inventory>();
        uiInventory.SetInventory(inventory);
    }
}
