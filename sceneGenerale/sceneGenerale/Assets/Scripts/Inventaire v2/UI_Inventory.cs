using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private GameObject itemSlotTemplate;

    private Transform favSlotContainer;
    private GameObject favSlotTemplate;

    bool activation = false;
    Canvas C;

    public Item Bois;
    public Item Marteau;
    public Item Hache;

    public static int xSizeMaxInv;
    public static int ySizeMaxInv;

    public void Awake()
    {
        C = GetComponent<Canvas>();
        C.enabled = false;

        itemSlotContainer = gameObject.transform.GetChild(0).GetChild(0);
        itemSlotTemplate = itemSlotContainer.GetChild(0).gameObject;

        favSlotContainer = gameObject.transform.GetChild(0).GetChild(1);
        favSlotTemplate = favSlotContainer.GetChild(0).gameObject;

        //WoodIcon = Resources.Load("Wood") as Sprite;
        //BerryIcon = Resources.Load("Berry") as Sprite;

        xSizeMaxInv = 6;
        ySizeMaxInv = 5;
    }

    void Update()
    {   
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            activation = !activation;
            C.enabled = activation;
            Deplacement.enMenu = activation;
        }

        RefreshInventoryFavoris();
    }
    public void SetInventory(Inventory inventory)
    {
        inventory.AddItem(new ItemAmount(Item: Bois, Amount: 3));
        inventory.AddItem(new ItemAmount(Item: Marteau, Amount: 2));
        inventory.AddItem(new ItemAmount(Item: Hache, Amount: 1));

        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
        RefreshInventoryFavoris();
    }

    private void Inventory_OnItemListChanged(object sender,System.EventArgs e)
    {
        RefreshInventoryItems();
        RefreshInventoryFavoris();
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
            itemSlotRectTransform.anchoredPosition = new Vector2(60 + x * itemSlotSize,-y * itemSlotSize - 40);
            
            Image image = itemSlotRectTransform.transform.GetChild(0).gameObject.GetComponent<Image>();
            image.sprite = item.Item.Icon;
            Text amountText = itemSlotRectTransform.transform.GetChild(1).gameObject.GetComponent<Text>();
            if (item.Amount > 1)
            {
                amountText.text = item.Amount.ToString();
            }
            else
            {
                amountText.text = "";
            }

            x++;
            if (x>= xSizeMaxInv)
            {
                x = 0;
                y++;
            }
        }
        while (y < ySizeMaxInv)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(60 + x * itemSlotSize, -y * itemSlotSize - 40);

            x++;
            if (x >= xSizeMaxInv)
            {
                x = 0;
                y++;
            }
        }
    }

    private void RefreshInventoryFavoris()
    {
        //d'abord on supprimer l'inventaire de la frame d'avant
        foreach (Transform child in favSlotContainer)
        {
            if (child.name == "FavSlotTemplate") continue;
            Destroy(child.gameObject);
        }

        //puis on réaffiche l'inventaire actualisé
        int x = 0;
        float itemSlotSize = 50f;
        foreach (ItemAmount item in inventory.GetFavList())
        {
            RectTransform favSlotRectTransform = Instantiate(favSlotTemplate, favSlotContainer).GetComponent<RectTransform>();
            favSlotRectTransform.gameObject.SetActive(true);
            favSlotRectTransform.anchoredPosition = new Vector2(60 + x * itemSlotSize,- 30);

            Image image = favSlotRectTransform.transform.GetChild(0).gameObject.GetComponent<Image>();
            image.sprite = item.Item.Icon;
            Text amountText = favSlotRectTransform.transform.GetChild(1).gameObject.GetComponent<Text>();
            if (item.Amount > 1)
            {
                amountText.text = item.Amount.ToString();
            }
            else
            {
                amountText.text = "";
            }

            x++;
        }
        while (x < xSizeMaxInv)
        {
            RectTransform favSlotRectTransform = Instantiate(favSlotTemplate, favSlotContainer).GetComponent<RectTransform>();
            favSlotRectTransform.gameObject.SetActive(true);
            favSlotRectTransform.anchoredPosition = new Vector2(60 + x * itemSlotSize, -30);

            x++;
        }
    }

    public int CountItem(string itemname) // On compte le nombre de d'item qui s'appellent itemname dans l'inventaire
    {
        List<ItemAmount> itemList = inventory.GetItemList();
        int Amount = 0;
        foreach (ItemAmount inventoryItem in itemList)
        {
            if (inventoryItem.Item.ItemName == itemname)
            {
                Amount += inventoryItem.Amount; //* inventaire.Slot[i].Item.Weight;
            }
        }
        return Amount;
    }

    public int NbrPlace(Item item) //On compte le nombre de place pour un item
    {
        List<ItemAmount> itemList = inventory.GetItemList();
        int Count = 0;
        foreach (ItemAmount inventoryItem in itemList)
        {
            if (inventoryItem.Item.ItemName == item.ItemName)
            {
                Count += inventory.sizeMaxStack - inventoryItem.Amount; //* inventaire.Slot[i].Item.Weight;
            }
        }
        Count += ((xSizeMaxInv * ySizeMaxInv) - itemList.Count) * inventory.sizeMaxStack;
        return Count;
    }
}
