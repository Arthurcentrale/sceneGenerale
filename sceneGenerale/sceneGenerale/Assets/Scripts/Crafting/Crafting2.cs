using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting2 : MonoBehaviour
{
    public string nomItemCraft;
    private MissionManager missionManager;

    List<ItemAmount> itemList;
    public GameObject Fond;
    public GameObject FondaActiver;
    public Inventaire inventaire; //inventaire
    public Player player; //on veut recuperer le script inventaire sur le joueur
    public RecetteCraft recettecraft; // la recette que l'on veut craft
    public Button Inc, Dec;
    GameObject BoutonsCrafting;
    public Text text;
    public Button bFond;
    Button button; 
    public int Count;
   
    // Start is called before the first frame update
    void Start()
    {
        //missionManager = GameObject.Find("menuMissionsPageGauche").GetComponent<MissionManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        BoutonsCrafting = GameObject.Find("Menus/Crafting/MenuCrafting/MenuAtelierFabrication/BoutonsCrafting");
        itemList = player.inventory.GetItemList();
        bFond = bFond.GetComponent<Button>();
        bFond.onClick.AddListener(TaskOnClick);
        Count = 1;
        inventaire = inventaire.GetComponent<Inventaire>();
        button = GetComponent<Button>();
        button.onClick.AddListener(UpdateFond);
        Inc = Inc.GetComponent<Button>();
        Dec = Dec.GetComponent<Button>();
        Inc.onClick.AddListener(Increment);
        Dec.onClick.AddListener(Decrement);
        text.text = Count.ToString() + " / " + maxCount(recettecraft).ToString();
    }


    void TaskOnClick() //Lorsque l'on clic sur le bouton, fait ça
    {
        for (int i = 1; i <= Count; i++)
            Craft();
        text.text = Count.ToString() + " / " + maxCount(recettecraft).ToString();
        Count = 1;
    }

    int CountItem(string itemname) // On compte le nombre de d'item qui s'appellent itemname dans l'inventaire
    {
        int Amount = 0;
        for(int i = 0; i < UI_Inventory.xSizeMaxInv * UI_Inventory.ySizeMaxInv; i++)
        {
            if (itemList[i].Item.ItemName == itemname)
            {
                Amount += itemList[i].Amount * itemList[i].Item.Weight;
            }
            else if (itemList[i].Item.ItemName == "Vide")
            {
            }
        }
        return Amount;
    }


    bool CanCraft() // On vérifie si on peut craft
    {
        foreach (ItemAmount ItemAmount in recettecraft.Results) // Si on a assez de place
        {
            if (player.uiInventory.NbrPlace(ItemAmount.Item) == 0)
            {
                return false;
            }
        }
        foreach (ItemAmount ItemAmount in recettecraft.Materials) // Si on a les ressources necessaire
        {
            if (player.uiInventory.CountItem(ItemAmount.Item.ItemName) < ItemAmount.Amount)
            {
                return false;
            }
        }
        return true;
    }
    int NbrPlace(Item item) //On compte le nombre de place pour un item
    {
        int Count = 0;
        if (item.Weight == 5) //si l'item est un outil
        {
            for (int i = 0; i < UI_Inventory.xSizeMaxInv * UI_Inventory.ySizeMaxInv; i++)
            {
                if (itemList[i].Item.ItemName == "Vide") // le nombre de place correspond aux nombre de slot vide
                {
                    Count++;
                }
            }
            return Count;
        }
        else // si l'item n'est pas un outil
        {
            for (int i = 0; i < UI_Inventory.xSizeMaxInv * UI_Inventory.ySizeMaxInv; i++)
            {
                if (itemList[i].Item.ItemName == "Vide" || itemList[i].Item == item) // le nombre de place correspond aux nombre de slot vide et ceux ou il y a le meme item avec moins
                                                                                   // de 64 items
                {
                    Count += 5 - itemList[i].Amount * itemList[i].Item.Weight;
                }
            }
            return Count;
        }
    }


    void Craft() // On réalise le craft
    {
        if (CanCraft()) // si c'est possible
        {
            foreach (ItemAmount ItemAmount in recettecraft.Materials)
            {
                player.inventory.DelItem(ItemAmount); //on enleve les ressources necessaire pour craft
            }
            foreach (ItemAmount ItemAmount in recettecraft.Results)
            {
                player.inventory.AddItem(ItemAmount);
                ItemAmount.durability = 5;// on ajoute les résultats
            }
            text.text = Count.ToString() + " / " + maxCount(recettecraft).ToString();
            missionManager.Craft(nomItemCraft);
        }
        else
        // Si CanCraft est false
        {
            Debug.Log("Pas assez de matériel ou de place");
        }
    }

    void AjouterInventaire(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {
        if (NbrPlace(item) < Amount) // Pas assez de place
        {
            Debug.Log("Il n'y a pas de place dans l'inventaire");
        }
        else
        {
            int i = 0; // pour parcourir l'inventaire
            int x = Amount; // le total d'objet à placer
            while (x != 0) // tant que l'on a pas tout placé
            {
                if (inventaire.Slot[i].Item == item) // si on a le bon item dans l'inventaire
                {
                    if (x + inventaire.Slot[i].Amount * item.Weight > 5) // si on doit placer trop d'item par rapport a la place qu'il reste dans ce slot
                    {
                        x -= 5 / item.Weight - inventaire.Slot[i].Amount;
                        inventaire.Slot[i].Amount = 5 / item.Weight; // on place ce que l'on peut et on continue de parcourir la liste pour placer le reste
                        GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text=inventaire.Slot[i].Amount.ToString();
                    }
                    else // si on a assez de place , on place tout
                    {
                        inventaire.Slot[i].Amount += x;
                        GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text=inventaire.Slot[i].Amount.ToString();
                        x = 0;

                    }
                }
                if (inventaire.Slot[i].Item.ItemName == "Vide") // Si l'emplacement est vide, on met les items la
                {
                    inventaire.Slot[i].Item = item;
                    inventaire.Slot[i].Amount += x;
                    x = 0;
                    // Mise a jour des sprites et textes
                    /*
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().sprite=item.Icon;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text=item.ItemName;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text=inventaire.Slot[i].Amount.ToString();
                    */
                }
                i++;

            }
        }
    }

    void RetirerInventaire(Item item, int Amount) //On veut retirer Amount items
    {
        if (CountItem(item.ItemName) < Amount) // on vérifie qu'il y a bien plus que Amount items dans l'inventaire
        {
            Debug.Log("Pas assez pour enlever");
        }
        else
        {
            int x = Amount; // x représente le total d'items a enlevés dans l'inventaire
            int i = GameObject.Find("Inventory").transform.GetChild(0).childCount-2;
            int j =i;
            int k;
            while (x != 0) // tant qu'on a pas tout enlevé, on parcourt l'inventaire
            {
                if (inventaire.Slot[i].Item == item) // si c'est le bon item
                {
                    if (inventaire.Slot[i].Amount < x) // Si il y a pas assez d'item dans ce slot pour tout enlever,
                    {
                        x -= inventaire.Slot[i].Amount; // et il ne reste plus que x-amount a enlever
                        inventaire.Slot[i].Amount = 0;
                        // on enleve tout ce qu'il y a dans ce slot
                        for (k=i;k<j;k++)
                        {
                            inventaire.Slot[k]=inventaire.Slot[k+1];
                            /*
                            GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().sprite=GameObject.Find("Inventory").transform.GetChild(0).GetChild(i+1).GetChild(0).GetComponent<Image>().sprite;
                            GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text=GameObject.Find("Inventory").transform.GetChild(0).GetChild(i+1).GetChild(1).GetComponent<Text>().text;
                            GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text=GameObject.Find("Inventory").transform.GetChild(0).GetChild(i+1).GetChild(2).GetComponent<Text>().text;                    
                            */
                        }
                        i--;
                        
                    }
                    else // si il y a assez de place
                    {
                        inventaire.Slot[i].Amount -= x; // on enleve tout
                        x = 0;
                        GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text=inventaire.Slot[i].Amount.ToString();
                    }
                }
                i--;
            }
        }
    }

    public int maxCount(RecetteCraft recettecraft)
    {
        int i = 0;
        while ((player.uiInventory.NbrPlace(recettecraft.Results[0].Item) * recettecraft.Results[0].Amount >= i + 1) && (player.uiInventory.CountItem(recettecraft.Materials[0].Item.ItemName) >= (i + 1) * recettecraft.Materials[0].Amount) && (player.uiInventory.CountItem(recettecraft.Materials[1].Item.ItemName) >= (i + 1) * recettecraft.Materials[1].Amount))
            i++;
        return i;
    }

    public void Increment()
    {

        if (Count < maxCount(recettecraft))
        {
            Count++;
            text.text = Count.ToString() + " / " + maxCount(recettecraft).ToString();
        }
    }

    public void Decrement()
    {
        if (Count > 1)
        {
            Count--;
            text.text = Count.ToString() + " / " + maxCount(recettecraft).ToString();
        }
    }

    void UpdateFond()
    {
        for (int j = 0; j < Fond.transform.childCount; j++)
        {
            BoutonsCrafting.SetActive(false);
            Fond.transform.GetChild(j).gameObject.SetActive(false);
            FondaActiver.SetActive(true);
            text.text = Count.ToString() + " / " + maxCount(recettecraft).ToString();
        }
    }


}