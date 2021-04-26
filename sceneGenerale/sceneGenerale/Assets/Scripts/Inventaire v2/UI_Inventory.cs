using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private GameObject itemSlotTemplate;

    public Item Bois;
    public Item Marteau;

    public void Awake()
    {
        itemSlotContainer = gameObject.transform.GetChild(0).GetChild(0);
        itemSlotTemplate = itemSlotContainer.GetChild(0).gameObject;

        //WoodIcon = Resources.Load("Wood") as Sprite;
        //BerryIcon = Resources.Load("Berry") as Sprite;
    }

    public void SetInventory(Inventory inventory)
    {
        inventory.AddItem(new ItemAmount(Item: Bois, Amount: 2));
        inventory.AddItem(new ItemAmount(Item: Marteau, Amount: 1));
        //Item.Create_Instance("Berry")

        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender,System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        //d'abord on supprimer l'inventaire de la frame d'avant
        foreach (Transform child in itemSlotContainer)
        {
            if (child.name == "ItemSlotTemplate") continue;
            Destroy(child.gameObject);
        }

        //puis on réaffiche l'inventaire actualisé
        int x = 0;
        int y = 0;
        float itemSlotSize = 50f;
        foreach(ItemAmount item in inventory.GetItemList())
        {
            Debug.Log(item.Item.ItemName);
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate,itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(40 + x * itemSlotSize,y * itemSlotSize - 40);
            
            Image image = itemSlotRectTransform.transform.GetChild(0).gameObject.GetComponent<Image>();
            image.sprite = item.Item.Icon;
            x++;
            if (x>5)
            {
                x = 0;
                y++;
            }
        }
    }
}
