﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{
    public EventSystem eventSystem;

    private Inventory inventory;
    private Transform itemSlotContainer;
    private GameObject itemSlotTemplate;

    private Transform favSlotContainer;
    private GameObject favSlotTemplate;

    private Transform favSlotContainerDepliement;
    private Animator animator;

    private GameObject moveToFav;
    private bool boutonFavAffiche;
    private int slotSelected;
    private bool prevoirAffichage;

    GameObject Background;
    GameObject BouttonOuvertureGO;

    public Item Bois;
    public Item Marteau;
    public Item Hache;

    public static int xSizeMaxInv;
    public static int ySizeMaxInv;
    public static int nbrFavoris;

    public int stadeAffichage;
    public int slotEquipé;

    public Sprite empty;

    public void Awake()
    {
        itemSlotContainer = gameObject.transform.GetChild(0).GetChild(0);
        itemSlotTemplate = itemSlotContainer.GetChild(0).gameObject;

        favSlotContainer = gameObject.transform.GetChild(0).GetChild(1);
        favSlotTemplate = favSlotContainer.GetChild(0).gameObject;

        favSlotContainerDepliement = gameObject.transform.GetChild(1);
        animator = gameObject.GetComponent<Animator>();

        moveToFav = gameObject.transform.GetChild(0).GetChild(3).gameObject;
        boutonFavAffiche = false;
        moveToFav.SetActive(false);
        slotSelected = 0;
        prevoirAffichage = false;

        Background = transform.GetChild(0).gameObject;
        Background.SetActive(false);
        BouttonOuvertureGO = transform.GetChild(2).gameObject;

        xSizeMaxInv = 8;
        ySizeMaxInv = 3;
        nbrFavoris = 4;

        stadeAffichage = 0;
        slotEquipé = 0;
    }

    void LateUpdate ()     // permet de fermer le bouton 'ajouter au favoris' si on clique ailleurs
    {
        if (boutonFavAffiche && Input.GetMouseButton(0))
        {
            prevoirAffichage = true;
        }
        else if (prevoirAffichage)
        {
            prevoirAffichage = false;
            boutonFavAffiche = false;
            moveToFav.SetActive(false);
        }

        // Regarde si on est au dessus d'un element de l'UI pour voir si on a cliqué en dehors des boutons de l'inventaire
        if (!eventSystem.IsPointerOverGameObject() && (stadeAffichage == 1) && Input.GetMouseButton(0))
        {
            animator.SetTrigger("fermerInvFavs");
            stadeAffichage -= 1;
            Debug.Log("On a cliqué en dehors et on ferme le panneau des favoris");
        }
    }

    public void BouttonOuverture()   //on clique sur le bouton inventaire
    {
        if (stadeAffichage == 0)   //inventaire fermé
        {
            animator.SetTrigger("ouvrirInvFavs");
            stadeAffichage += 1;
        }
        else if (stadeAffichage == 1)   //favoris dépliés
        {
            animator.SetTrigger("fermerInvFavs");
            StartCoroutine(DelayOuvertureInv(0.5f));
        }   
        else                      //inventaire complet ouvert
        {
            animator.SetTrigger("ouverture1BulleCouper");
            Background.SetActive(false);
            stadeAffichage -= 1;
        }
    }

    IEnumerator DelayOuvertureInv(float delayTime) //ouverture inventaire complet avec un delai
    {
        yield return new WaitForSeconds(delayTime);

        //BouttonOuvertureGO.SetActive(false);
        animator.SetTrigger("fermerBouton");
        //favSlotContainerDepliement.gameObject.SetActive(false);
        Background.SetActive(true);
        stadeAffichage += 1;
    }


    public void BouttonFermeture()   //clique sur croix avec inventaire complet ouvert
    {
        stadeAffichage = 0;
        Background.SetActive(false);
        //BouttonOuvertureGO.SetActive(true);
        //favSlotContainerDepliement.gameObject.SetActive(true);
        animator.SetTrigger("ouvrirBouton");
    }

    public void SetInventory(Inventory _inventory)   //initialisation de l'inventaire
    {
        inventory = _inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        inventory.AddItem(new ItemAmount(Item: Bois, Amount: 2));
        inventory.AddItem(new ItemAmount(Item: Hache, Amount: 1));

        RefreshInventoryItems();
        RefreshInventoryFavoris();
    }

    public void Inventory_OnItemListChanged()   //event qui actionne le refresh de l'inventaire
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
        float itemSlotSize = 66.84f;
        foreach(ItemAmount item in inventory.GetItemList())
        {
            //Debug.Log(item.Item.ItemName);
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate,itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.gameObject.name = (x + y*x).ToString();
            itemSlotRectTransform.anchoredPosition = new Vector2(43.5f + x * itemSlotSize,-47f -y * itemSlotSize);
            
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
            itemSlotRectTransform.anchoredPosition = new Vector2(43.5f + x * itemSlotSize,-47f -y * itemSlotSize);

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
        //d'abord on supprime l'inventaire de la frame d'avant
        foreach (Transform child in favSlotContainer)
        {
            if (child.name == "FavSlotTemplate") continue;
            Destroy(child.gameObject);
        }

        //Puis on réaffiche l'inventaire actualisé
        int x = 0;     //numéro du favoris pour l'affichage
        int slot = 0;  //position du favoris dans l'inventaire

        float itemSlotSize = 66.84f;
        List<bool> favList = inventory.GetFavList();
        foreach (bool val in favList)
        {
            if (val)  //si il y a un favoris à cet emplacement
            {
                //On récupère l'item qui correspond à ce numéro de favoris
                ItemAmount item = (inventory.GetItemList())[slot];

                // On refresh d'abord les favoris dans l'inventaire principal

                RectTransform favSlotRectTransform = Instantiate(favSlotTemplate, favSlotContainer).GetComponent<RectTransform>();
                favSlotRectTransform.gameObject.SetActive(true);
                favSlotRectTransform.gameObject.name = slot.ToString();  //on renomme le gameObject par la position du favoris dans l'inventaire
                favSlotRectTransform.anchoredPosition = new Vector2(46.49f + x * itemSlotSize, -41.09f);

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

                // Ensuite on refresh les favoris dans le petit menu depliant

                Transform favSlotTemplate1 = favSlotContainerDepliement.GetChild(x);
                image = favSlotTemplate1.GetChild(0).gameObject.GetComponent<Image>();
                image.sprite = item.Item.Icon;
                amountText = favSlotTemplate1.GetChild(1).gameObject.GetComponent<Text>();
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
            slot++;
        }
        while (x < nbrFavoris)  //on affiche des slots vides ensuite si l'inventaire n'est pas rempli
        {
            // On refresh d'abord les favoris dans l'inventaire principal

            RectTransform favSlotRectTransform = Instantiate(favSlotTemplate, favSlotContainer).GetComponent<RectTransform>();
            favSlotRectTransform.gameObject.SetActive(true);
            favSlotRectTransform.anchoredPosition = new Vector2(46.49f + x * itemSlotSize, -41.09f);

            // Ensuite on refresh les favoris dans le petit menu depliant

            Transform favSlotTemplate1 = favSlotContainerDepliement.GetChild(x);
            Image image = favSlotTemplate1.GetChild(0).gameObject.GetComponent<Image>();
            image.sprite = favSlotTemplate.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
            Text amountText = favSlotTemplate1.GetChild(1).gameObject.GetComponent<Text>();
            amountText.text = "";

            x++;
        }
    }

    public void AfficheBoutonFav(Transform slotInv)   //on affiche le bouton qui permet d'ajouter un objet aux favoris
    {
        string name = slotInv.gameObject.name;
        if (name.Length < 3) //on vérifie qu'il y avait bien un objet dans ce slot (si il est renommé par un nombre à 1 ou 2 chiffres)
        {
            Vector3 pos = slotInv.position;
            //pos.x += 10f;
            //pos.y += 25f;
            moveToFav.transform.position = pos;
            slotSelected = int.Parse(name);  //position de l'item à ajouter dans l'inventaire principal
            moveToFav.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Mettre en favoris";

            Button button = moveToFav.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(CopyToFav);

            boutonFavAffiche = true;
            moveToFav.SetActive(true);
        }
    }

    public void AfficheBoutonEnleverFav(Transform slotInv)   //on affiche le bouton qui permet d'enlever un objet des favoris
    {
        string name = slotInv.gameObject.name;
        if (name.Length < 3)
        {
            Vector3 pos = slotInv.position;
            //pos.x += 10f;
            //pos.y += 25f;
            moveToFav.transform.position = pos;
            slotSelected = int.Parse(name);  //position du favoris dans la liste itemList (inventaire principal) et donc position du true dans favList
            moveToFav.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Enlever des favoris";

            Button button = moveToFav.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(DelFromFav);

            boutonFavAffiche = true;
            moveToFav.SetActive(true);
        }
    }

    public void EquiperFav(Transform slotInv)   //on equipe le favoris sur lequel on a cliqué dans le menu dépliant
    {
        string name = slotInv.gameObject.name;
        int slot = name[name.Length - 1] - '0';

        if (slot == slotEquipé)
        {
            slotEquipé = 0;
            Debug.Log("On déséquipe l'item n" + slot.ToString());
        }
        else
        {
            //on équipe l'objet
            slotEquipé = slot;
            Debug.Log("L'item du slot n" + slotEquipé.ToString() + " est équipé");
        }

        //on repasse au stade 0 de l'affichage
        animator.SetTrigger("fermerInvFavs");
        stadeAffichage -= 1;

        Debug.Log("C'est l'item : " + NomItemEquip());
    }



    public void CopyToFav()   //ajoute un item au favoris si on peut
    {
        if (!inventory.AddToFav(slotSelected))
        {
            Debug.Log("Item deja ajouté aux favoris");
        }
        //boutonFavAffiche = false;
        //moveToFav.SetActive(false);
    }

    public void DelFromFav()     //enleve un item des favoris
    {
        inventory.DelFav(slotSelected);
        //boutonFavAffiche = false;
        //moveToFav.SetActive(false);
    }

    public int CountItem(string itemname) // On compte le nombre de d'item qui s'appellent itemname dans l'inventaire
    {
        List<ItemAmount> itemList = inventory.GetItemList();
        int Amount = 0;
        foreach (ItemAmount inventoryItem in itemList)
        {
            if (inventoryItem.Item.ItemName == itemname)
            {
                Amount += inventoryItem.Amount * inventoryItem.Item.Weight;
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
                Count += (inventory.sizeMaxStack / item.Weight) - inventoryItem.Amount;
            }
        }
        Count += ((xSizeMaxInv * ySizeMaxInv) - itemList.Count) * inventory.sizeMaxStack / item.Weight;
        return Count;
    }

    public string NomItemEquip() //Retourne le nom de l'item equipé et le string vide si il n'y en a pas
    {
        if (slotEquipé == 0) return "";
        else
        {
            // 1<=slotEquipé<=4 ; on cherche a retrouver la place dans favList a laquel correspond ce slot equipé
            List<bool> favList = inventory.GetFavList();
            int count = 0;  //on compte le nombre de true qu'on rencontre
            int slot = 0;   //vraie position de l'item dans favList
            while (count < slotEquipé)
            {
                if (favList[slot]) count++;
                slot++;
            }
            return inventory.GetItemList()[slot-1].Item.name;
        }
    }
}
