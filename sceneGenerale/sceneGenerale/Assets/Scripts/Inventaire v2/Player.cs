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

        // L'inventaire est maintenant initialisé de base au cas ou le gameHandler n'a pas de sauvegarde de base sur laquelle s'appuyer
        // Il devrait dans ce cas la, Save un inventaire vide puis le Load et donc avoir des favoris vides et l'inventaire de base aussi si on ajoute pas d'items

        // ------------------------------------------------------------------------- //

        inventory = new Inventory(new List<ItemAmount>(), new List<bool>(new bool[24]), this);
        uiInventory = GameObject.Find("Inventaire2").transform.GetChild(0).gameObject.GetComponent<UI_Inventory>();
        uiInventory.SetInventory(inventory);
    }

    public void createInventory(List<ItemAmount> listeItems, List<bool> listeFavoris)        
    {
        inventory = new Inventory(listeItems, listeFavoris, this);
        uiInventory = GameObject.Find("Inventaire2").transform.GetChild(0).gameObject.GetComponent<UI_Inventory>();
        uiInventory.SetInventory(inventory);
    }
}
