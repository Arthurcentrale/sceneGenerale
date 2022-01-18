using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{
    public EventSystem eventSystem;
    private ArbreManager arbreManager;

    private Animator animPlayer;

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

    private GameObject boutonPlanterGraine;

    private GameObject boutonJeterItem;
    private GameObject menuJeterItem;
    private GameObject boutonEquiper;

    GameObject Background;
    GameObject BouttonOuvertureGO;

    public Item Bois;
    public Item Marteau;
    public Item Hache;
    public Item Pioche;
    public Item Pierre;
    public Item GraineChene;

    public static int xSizeMaxInv;
    public static int ySizeMaxInv;
    public static int nbrFavoris;

    public int stadeAffichage;
    public int slotEquipé;

    public Sprite empty;

    //partie son
    public AudioClip premierClicBulle;
    public AudioClip secondClicBulle;
    public AudioClip fermeture;
    public AudioClip equipOutil;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        arbreManager = GameObject.Find("Game Manager").GetComponent<ArbreManager>();
        animPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

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

        //boutonPlanterGraine = gameObject.transform.GetChild(0).GetChild(4).gameObject;

        boutonJeterItem = gameObject.transform.GetChild(0).GetChild(5).gameObject;
        menuJeterItem = gameObject.transform.GetChild(0).GetChild(6).gameObject;
        boutonEquiper = gameObject.transform.GetChild(0).GetChild(7).gameObject;

        Background = transform.GetChild(0).gameObject;
        Background.SetActive(false);
        BouttonOuvertureGO = transform.GetChild(2).gameObject;

        xSizeMaxInv = 8;
        ySizeMaxInv = 3;
        nbrFavoris = 4;

        stadeAffichage = 0;
        slotEquipé = 0;
    }

    void Update ()     // permet de fermer le bouton 'ajouter au favoris' et 'planter graine' si on clique ailleurs
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
            boutonPlanterGraine.SetActive(false);
            boutonJeterItem.SetActive(false);
            boutonEquiper.SetActive(false);
        }

        /*
        if (IsPointerOverUIObject()) Debug.Log("OUI");
        else Debug.Log("NON");
        */

        // Regarde si on est au dessus d'un element de l'UI pour voir si on a cliqué en dehors des boutons de l'inventaire
        if (!IsPointerOverUIObject() && (stadeAffichage == 1) && Input.GetMouseButton(0))
        {
            animator.SetTrigger("fermerInvFavs");
            stadeAffichage -= 1;
            Debug.Log("On a cliqué en dehors et on ferme le panneau des favoris");
        }
    }

    private bool IsPointerOverUIObject() //fonction qui permet de savoir si on a le curseur au dessus d'un objet de l'UI
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        bool val = false;
        foreach (RaycastResult hit in results)
        {
            if (hit.gameObject.layer == 5) val = true;  //Layer 5 = UI
        }
        return val;
    }

    public void BouttonOuverture()   //on clique sur le bouton inventaire
    {
        if (stadeAffichage == 0)   //inventaire fermé
        {
            animator.SetTrigger("ouvrirInvFavs");
            stadeAffichage += 1;
            audioSource.PlayOneShot(premierClicBulle);
        }
        else if (stadeAffichage == 1)   //favoris dépliés
        {
            animator.SetTrigger("fermerInvFavs");
            StartCoroutine(DelayOuvertureInv(0.5f));
            audioSource.PlayOneShot(secondClicBulle);
            Deplacement.enMenu = true;
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

        BouttonOuvertureGO.SetActive(false);
        //animator.SetTrigger("fermerBouton");
        favSlotContainerDepliement.gameObject.SetActive(false);
        Background.SetActive(true);
        stadeAffichage += 1;
        
    }


    public void BouttonFermeture()   //clique sur croix avec inventaire complet ouvert
    {
        audioSource.PlayOneShot(fermeture,0.2f);
        stadeAffichage = 0;
        Background.SetActive(false);
        BouttonOuvertureGO.SetActive(true);
        favSlotContainerDepliement.gameObject.SetActive(true);
        //animator.SetTrigger("ouvrirBouton");
        Deplacement.enMenu = false;
    }

    public void SetInventory(Inventory _inventory)   //initialisation de l'inventaire
    {
        inventory = _inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        //inventory.AddItem(new ItemAmount(Item: Hache, Amount: 1));
        //inventory.AddItem(new ItemAmount(Item: Bois, Amount: 5));    
        
        //inventory.AddItem(new ItemAmount(Item: Pioche, Amount: 1));
        
        //inventory.AddItem(new ItemAmount(Item: Pierre, Amount: 5));
        //inventory.AddItem(new ItemAmount(Item: GraineChene, Amount: 1));
        


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

            //on met le tag objet ou graine pour savoir si on affiche le bouton "mettre aux favoris" ou "planter graine" si on clique dessus
            if ((14 <= int.Parse(item.Item.id)) && (int.Parse(item.Item.id) <= 18))
            {
                itemSlotRectTransform.tag = "Graine";
            }
            else if ((3 <= int.Parse(item.Item.id)) && (int.Parse(item.Item.id) <= 6))
            {
                itemSlotRectTransform.tag = "Outil";
            }
            else
            {
                itemSlotRectTransform.tag = "Objet";
            }

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


    public void AfficheBoutonsItem(Transform slotInv) //apelle toutes les fonctions qui permettent d'afficher les dífférents boutons spécifiques au tag de l'item
    {
        if (slotInv.tag == "Objet")
        {
            AfficheBoutonObjet(slotInv);
        }
        else if (slotInv.tag == "Graine")
        {
            AfficheBoutonPlanterGraine(slotInv);
        }
        else if (slotInv.tag == "Outil")
        {
            AfficheBoutonFav(slotInv);
        }
    }

    //Fonctions pour gérer les items avec le tag "Objet"

    private void AfficheBoutonObjet(Transform slotInv)   
    {
        string name = slotInv.gameObject.name;
        Vector3 pos = slotInv.position;

        boutonJeterItem.transform.position = pos;
        slotSelected = int.Parse(name);  //position de l'item à supprimer dans l'inventaire principal

        boutonFavAffiche = true;
        boutonJeterItem.SetActive(true);
    }

    public void AfficheMenuJeterItem()
    {
        menuJeterItem.SetActive(true);
    }

    public void OuiJeterItem()
    {
        menuJeterItem.SetActive(false);
        inventory.DelItemAtPos(slotSelected);
    }

    public void NonJeterItem()
    {
        menuJeterItem.SetActive(false);
    }


    //Fonctions pour gérer les items avec le tag "Outil"

    private void AfficheBoutonFav(Transform slotInv)   //on affiche le bouton qui permet d'ajouter un objet aux favoris
    {
        string name = slotInv.gameObject.name;
        Vector3 pos = slotInv.position;
        slotSelected = int.Parse(name);

        //bouton ajouter aux favoris
        
        moveToFav.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Mettre en favoris";
        Button button = moveToFav.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(CopyToFav);

        moveToFav.transform.position = pos + new Vector3(0f, 50f, 0f);
        moveToFav.SetActive(true);

        //bouton equiper

        boutonEquiper.transform.position = pos;
        boutonEquiper.SetActive(true);

        //bouton jeter

        boutonJeterItem.transform.position = pos - new Vector3(0f, 50f, 0f);
        boutonJeterItem.SetActive(true);

        boutonFavAffiche = true;
    }

    public void EquiperItem()
    {
        CopyToFav();

        int slot = slotSelected + 1;

        if (slot == slotEquipé) //l'item est déja équipé
        {
            slotEquipé = 0;
            Debug.Log("On déséquipe l'item n" + slot.ToString());
            animPlayer.SetBool("Pioche", false);
            animPlayer.SetBool("Sac", false);
            animPlayer.SetBool("Hache", false);
        }
        else  //on équipe l'objet
        {
            slotEquipé = slot;
            Debug.Log("L'item du slot n" + slotEquipé.ToString() + " est équipé");
            audioSource.PlayOneShot(equipOutil);
            if (slot == 1)
            {
                animPlayer.SetBool("Pioche", false);
                animPlayer.SetBool("Sac", false);
                animPlayer.SetBool("Hache", true);
            }
            else if (slot == 2)
            {
                animPlayer.SetBool("Hache", false);
                animPlayer.SetBool("Sac", false);
                animPlayer.SetBool("Pioche", true);
            }
        }

        Debug.Log("C'est l'item : " + NomItemEquip());
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
            animPlayer.SetBool("Pioche", false);
            animPlayer.SetBool("Sac", false);
            animPlayer.SetBool("Hache", false);
        }
        else
        {
            //on équipe l'objet
            slotEquipé = slot;
            Debug.Log("L'item du slot n" + slotEquipé.ToString() + " est équipé");
            audioSource.PlayOneShot(equipOutil);
            if (slot == 1)
            {
                animPlayer.SetBool("Pioche", false);
                animPlayer.SetBool("Sac", false);
                animPlayer.SetBool("Hache", true);
            }
            else if (slot == 2)
            {
                animPlayer.SetBool("Hache", false);
                animPlayer.SetBool("Sac", false);
                animPlayer.SetBool("Pioche", true);
            }
        }

        //partie animation
        

        //on repasse au stade 0 de l'affichage
        animator.SetTrigger("fermerInvFavs");
        stadeAffichage -= 1;

        Debug.Log("C'est l'item : " + NomItemEquip());
    }


    //Fonctions pour gérer les items avec le tag "Graine"

    public void AfficheBoutonPlanterGraine(Transform slotInv)
    {
        string name = slotInv.gameObject.name;
        Vector3 pos = slotInv.position;
        slotSelected = int.Parse(name);

        //bouton planter graine

        Item item = inventory.GetItemList()[slotSelected].Item;

        Button button = boutonPlanterGraine.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { PlanterGraine(item); });

        boutonPlanterGraine.transform.position = pos + new Vector3(0f, 30f, 0f);
        boutonPlanterGraine.SetActive(true);

        //bouton jeter

        boutonJeterItem.transform.position = pos - new Vector3(0f,30f,0f);
        boutonJeterItem.SetActive(true);

        boutonFavAffiche = true;
    }

    public void PlanterGraine(Item item)   //plante la graine
    {
        GameObject arbuste;
        Vector3 position = GameObject.Find("Principal_OK").transform.position + new Vector3(0f, 1.4f, 0f);

        switch (item.name)
        {
            case "GraineChene": 
                arbuste = arbreManager.arbusteChene;
                Instantiate(arbuste, position, arbuste.transform.rotation);
                break;
            case "GraineHetre":
                arbuste = arbreManager.arbusteHetre;
                Instantiate(arbuste, position, arbuste.transform.rotation);
                break;
            case "GraineDouglas":
                arbuste = arbreManager.arbusteDouglas;
                Instantiate(arbuste, position, arbuste.transform.rotation);
                break;
            case "GrainePin":
                arbuste = arbreManager.arbustePin;
                Instantiate(arbuste, position, arbuste.transform.rotation);
                break;
            case "GraineBouleau":
                arbuste = arbreManager.arbusteBouleau;
                Instantiate(arbuste, position, arbuste.transform.rotation);
                break;
        }

        inventory.DelItem(new ItemAmount(Item: item, Amount: 1));
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
    public ItemAmount ItemEquip() //Retourne le nom de l'item equipé et le string vide si il n'y en a pas
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
            return inventory.GetItemList()[slot - 1];
    }

    /*public void ReduitDuraEquip() //Retourne le nom de l'item equipé et le string vide si il n'y en a pas
    {
        if (slotEquipé == 0);
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
            inventory.GetItemList()[slot - 1].Item.ReduireDurabilite();
            if (inventory.GetItemList()[slot - 1].Item.durability == 0)
            {
                inventory.DelItem(inventory.GetItemList()[slot - 1]);
            }
        }
    }*/
}
