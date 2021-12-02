using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptATHBatis : MonoBehaviour
{

    public UI_Inventory ui_inventory;
    public Player player;

    //les boutons
    public Button bulleInfo;
    public Button bulleConstruire;
    public Button bulleDeposer;
    public Button bulleSupprimer;
    public Button bordereauConstruire;
    public Button bordereauDeposer;
    public Button bordereauSupprimer;

    public Text textItemOne;
    public Text textItemTwo;
    public GameObject imageOne;
    public GameObject imageTwo;

    //temps
    public GameObject temps;
    private Text texteTemps;
    private int heures;
    private int minutes;
    private int secondes;

    //donnees
    public float tempsConstruTotal = 0.001f; //doit être exprimé en heures
    public Item itemOne;
    public Item itemTwo;
    private int nombreItemOne;
    private int nombreItemTwo;
    private int nombreItemOneRestants;
    private int nombreItemTwoRestants;
    private int nombreItemOneNécessaire = 1;
    private int nombreItemTwoNécessaire = 1;

    //outils
    public bool RessourcesNécessairesDéposées = false;
    private bool permissionConstruction = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        texteTemps = temps.GetComponent<Text>();
        nombreItemTwoRestants = nombreItemTwoNécessaire;
        nombreItemOneRestants = nombreItemOneNécessaire;


    }

    // Update is called once per frame
    void Update()
    {

        ui_inventory = player.uiInventory;
        nombreItemTwo = player.uiInventory.CountItem("Bois");
        nombreItemOne = player.uiInventory.CountItem("Pierre");

        if (RessourcesNécessairesDéposées && permissionConstruction)
        {
            ConstructionInProgress();
        }
    }

    public void DepotRessource()
    {
        ui_inventory = player.uiInventory;
        nombreItemTwo = player.uiInventory.CountItem("Bois");
        nombreItemOne = player.uiInventory.CountItem("Pierre");
        print(nombreItemOne);
        //if ya du bois et nombre bois inventaire< nombre bois nécessaire à la constru du bâtiment
        if (nombreItemOne > 0)
        {
            if (nombreItemOne <= nombreItemOneNécessaire)
            {
                nombreItemOneNécessaire -= nombreItemOne;
                RetirerInventaire(itemOne, nombreItemOne);
            }
            else
            {
                RetirerInventaire(itemOne, nombreItemOneNécessaire);
                nombreItemOneNécessaire = 0;

            }
            
            
        }
        if (nombreItemTwo > 0)
        {
            if (nombreItemTwo <= nombreItemTwoNécessaire)
            {
                nombreItemTwoNécessaire -= nombreItemTwo;
                RetirerInventaire(itemTwo, nombreItemTwo);
            }
            else
            {
                RetirerInventaire(itemTwo, nombreItemTwoNécessaire);
                nombreItemTwoNécessaire = 0;

            }
            
            
        }

        textItemOne.text = nombreItemOne.ToString();
        textItemTwo.text = nombreItemTwo.ToString();

        if (nombreItemOneNécessaire == 0 && nombreItemTwoNécessaire == 0)
        {
            RessourcesNécessairesDéposées = !RessourcesNécessairesDéposées;
            imageOne.SetActive(false);
            imageTwo.SetActive(false);
            bulleConstruire.interactable = true;
            bordereauConstruire.interactable = true;


        }

    }

    void DebuterConstruction()
    {
        permissionConstruction = true;
        temps.SetActive(true);
    }

    void ConstructionInProgress()
    {
        int tempsConstruTotalEntier = (int)(3600 * tempsConstruTotal); //ceci est exprimé en secondes

        heures = tempsConstruTotalEntier / 3600;
        minutes = (tempsConstruTotalEntier - heures * 3600) / 60;
        secondes = (tempsConstruTotalEntier - heures * 3600 - minutes * 60);
        tempsConstruTotal -= Time.deltaTime;
        texteTemps.text = heures.ToString() + " : " + minutes.ToString() + " : " + secondes.ToString();
    }

    //fonction pour ajouter un item à l'inventaire
    void AjouterInventaire(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {

        player.inventory.AddItem(new ItemAmount(Item: item, Amount: Amount));
    }

    //fonction pour retirer un item de l'inventaire
    void RetirerInventaire(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {

        player.inventory.DelItem(new ItemAmount(Item: item, Amount: Amount));
    }

}
