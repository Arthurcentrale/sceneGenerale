﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptATHBatis : MonoBehaviour
{
    //IU
    private GameObject panel;
    public Sprite bulleNotready;
    public Sprite bulleReady;
    private GameObject infoBulle;

    public UI_Inventory ui_inventory;
    public Player player;
    public GameObject ouvrier;

    //le bati
    public GameObject bati;

    
    //Pour le Chauffage:
    public int nombreDeBâtimentsConstruits=0;

    //chaumière
    public GameObject chaumiere;
    public Item itemOneChaumiere;
    public Item itemTwoChaumiere;
    public int nombreItemOneChaumiere;
    public int nombreItemTwoChaumiere;

    //ferme
    public GameObject ferme;
    public Item itemOneFerme;
    public Item itemTwoFerme;
    public int nombreItemOneFerme;
    public int nombreItemTwoFerme;

    //Pêcherie
    public GameObject pecherie;
    public Item itemOnePecherie;
    public Item itemTwoPecherie;
    public int nombreItemOnePecherie;
    public int nombreItemTwoPecherie;

    //boulangerie
    public GameObject boulangerie;
    public Item itemOneBoulangerie;
    public Item itemTwoBoulangerie;
    public int nombreItemOneBoulangerie;
    public int nombreItemTwoBoulangerie;

    //Moulin à eau
    public GameObject moulinEau;
    public Item itemOneMoulinEau;
    public Item itemTwoMoulinEau;
    public int nombreItemOneMoulinEau;
    public int nombreItemTwoMoulinEau;

    //Moulin à vent
    public GameObject moulinVent;
    public Item itemOneMoulinVent;
    public Item itemTwoMoulinVent;
    public int nombreItemOneMoulinVent;
    public int nombreItemTwoMoulinVent;

    //Puit
    public GameObject puit;
    public Item itemOnePuit;
    public Item itemTwoPuit;
    public int nombreItemOnePuit;
    public int nombreItemTwoPuit;

    //Verrerie
    public GameObject verrerie;
    public Item itemOneVerrerie;
    public Item itemTwoVerrerie;
    public int nombreItemOneVerrerie;
    public int nombreItemTwoVerrerie;

    //Decharge
    public GameObject decharge;
    public Item itemOneDecharge;
    public Item itemTwoDecharge;
    public int nombreItemOneDecharge;
    public int nombreItemTwoDecharge;

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
    float time;
    float timeDepart;
    float ecartTime;
    public float TimerInterval = 5;
    float tick;
    private float tempsConstruChaumiere = 5;

    //donnees
    public float tempsConstruTotal = 1000; //doit être exprimé en heures
    public Item itemOne;
    public Item itemTwo;
    private int nombreItemOne;
    private int nombreItemTwo;
    private int nombreItemOneRestants;
    private int nombreItemTwoRestants;
    private int nombreItemOneNécessaire;
    private int nombreItemTwoNécessaire;

    //outils
    public bool RessourcesNécessairesDéposées = false;
    public bool permissionConstruction = false;
    public bool constructionTerminee = false;
    public bool depotEnCours = false;
    private MissionManager missionManager;

    private Text ouvrierOccupe;
    private BoutonMenu2 Emptyscriptconstru;
    [HideInInspector] public bool fermeConstruit = false;
    [HideInInspector] public bool pecherieConstruit = false;
    [HideInInspector] public bool moulinEauConstruit = false;
    [HideInInspector] public bool moulinVentConstruit = false;
    [HideInInspector] public bool boulangerieConstruit = false;
    [HideInInspector] public bool dechargeConstruit = false;
    [HideInInspector] public bool puitConstruit = false;
    [HideInInspector] public bool etabliConstruit = false;
    [HideInInspector] public bool verrerieConstruit = false;

    private Transform dossierbatiments;


    // Start is called before the first frame update

    private void Awake()
    {
        time = (int)Time.time;
        tick = TimerInterval;
    }
    void Start()
    {
        missionManager = GameObject.Find("menuMissionsPageGauche").GetComponent<MissionManager>();
        panel = GameObject.Find("PanelBatisConstruction");
        infoBulle = GameObject.Find("PanelBatisConstruction").transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        texteTemps = temps.GetComponent<Text>();
        nombreItemTwoRestants = nombreItemTwoNécessaire;
        nombreItemOneRestants = nombreItemOneNécessaire;
        ouvrierOccupe = GameObject.Find("TextOuvrierOccupe").GetComponent<Text>();
        Emptyscriptconstru = GameObject.Find("Empty script constru 2").GetComponent<BoutonMenu2>();
        dossierbatiments = GameObject.Find("Batiments").transform;

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

        if ((int) (tempsConstruChaumiere - ecartTime) == 0 && !constructionTerminee) finirConstruction();
    }

    public void affectation()
    {
        bati = GameObject.Find("Bati");
        
        if (bati.CompareTag("BatiChaumière"))
        {
            itemOne = itemOneChaumiere;
            itemTwo = itemTwoChaumiere;
            nombreItemOneNécessaire = nombreItemOneChaumiere;
            nombreItemTwoNécessaire = nombreItemTwoChaumiere;
        }

        else if (bati.CompareTag("BatiFerme"))
        {
            itemOne = itemOneFerme;
            itemTwo = itemTwoFerme;
            nombreItemOneNécessaire = nombreItemOneFerme;
            nombreItemTwoNécessaire = nombreItemTwoFerme;
        }

        else if (bati.CompareTag("BatiPêcherie"))
        {
            itemOne = itemOnePecherie;
            itemTwo = itemTwoPecherie;
            nombreItemOneNécessaire = nombreItemOnePecherie;
            nombreItemTwoNécessaire = nombreItemTwoPecherie;
        }

        else if (bati.CompareTag("BatiBoulangerie"))
        {
            itemOne = itemOneBoulangerie;
            itemTwo = itemTwoBoulangerie;
            nombreItemOneNécessaire = nombreItemOneBoulangerie;
            nombreItemTwoNécessaire = nombreItemTwoBoulangerie;
        }

        else if (bati.CompareTag("BatiMoulinAEau"))
        {
            itemOne = itemOneMoulinEau;
            itemTwo = itemTwoMoulinEau;
            nombreItemOneNécessaire = nombreItemOneMoulinEau;
            nombreItemTwoNécessaire = nombreItemTwoMoulinEau;
        }

        textItemOne.text = nombreItemOneNécessaire.ToString();
        textItemTwo.text = nombreItemTwoNécessaire.ToString();

    }

    public void resetConstruction()
    {
        infoBulle.GetComponent<Button>().image.sprite = bulleNotready;
        imageOne.SetActive(true);
        imageTwo.SetActive(true);
        bulleConstruire.interactable = false;
        bordereauConstruire.interactable = false;
        bulleDeposer.interactable = true;
        bordereauDeposer.interactable = true;
        temps.SetActive(false);
        constructionTerminee = false;
        

    }

    public void DepotRessource()
    {
        //cette ligne sert à trouver le bati pour plus tard
        depotEnCours = true;
        ui_inventory = player.uiInventory;
        nombreItemTwo = player.uiInventory.CountItem("Bois");
        nombreItemOne = player.uiInventory.CountItem("Pierre");
        
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

        if (nombreItemOneNécessaire < 0) nombreItemOneNécessaire = 0;
        if (nombreItemTwoNécessaire < 0) nombreItemTwoNécessaire = 0;

        nombreItemTwo = player.uiInventory.CountItem("Bois");
        nombreItemOne = player.uiInventory.CountItem("Pierre");
        textItemOne.text = nombreItemOneNécessaire.ToString();
        textItemTwo.text = nombreItemTwoNécessaire.ToString();

        if (nombreItemOneNécessaire == 0 && nombreItemTwoNécessaire == 0)
        {
            infoBulle.GetComponent<Button>().image.sprite = bulleReady;
            RessourcesNécessairesDéposées = !RessourcesNécessairesDéposées;
            imageOne.SetActive(false);
            imageTwo.SetActive(false);
            bulleConstruire.interactable = true;
            bordereauConstruire.interactable = true;


        }

    }

    public void DebuterConstruction()
    {
        permissionConstruction = true;
        temps.SetActive(true);
        timeDepart = Time.time;
        depotEnCours = false;
        ouvrier.transform.position = new Vector3 (bati.transform.position.x+6,ouvrier.transform.position.y,bati.transform.position.z-5);
        ouvrier.GetComponent<Animator>().SetFloat("Construction", 1);

        BuildingLayerMag.updateBatLayers();
    }

    void ConstructionInProgress()
    {
        int tempsConstruTotalEntier = (int)(3600 * tempsConstruTotal); //ceci est exprimé en secondes
        if (permissionConstruction)
        {
            time = Time.time;
            ecartTime = (int) (time - timeDepart);
            
        }
        texteTemps.text = string.Format("{0:0}:{1:00}", Mathf.Floor((tempsConstruChaumiere- ecartTime) / 60), (tempsConstruChaumiere - ecartTime) % 60);
       
    }

    void finirConstruction()
    {
        if (bati.CompareTag("BatiChaumière"))
        {
            Instantiate(chaumiere, new Vector3(bati.transform.position.x, 5.81f, bati.transform.position.z + 5), chaumiere.transform.rotation, dossierbatiments);
            missionManager.Build("chaumière");
            nombreDeBâtimentsConstruits+=1;
        }

        else if (bati.CompareTag("BatiFerme"))
        {
            ferme.transform.position = new Vector3(bati.transform.position.x, ferme.transform.position.y, bati.transform.position.z);
            missionManager.Build("ferme");
            fermeConstruit = true;
            nombreDeBâtimentsConstruits+=1;
        }

        else if (bati.CompareTag("BatiPêcherie"))
        {
            Instantiate(pecherie, new Vector3(bati.transform.position.x, 3.82f, bati.transform.position.z + 7), pecherie.transform.rotation, dossierbatiments);
            missionManager.Build("pêcherie");
            pecherieConstruit = true;
            nombreDeBâtimentsConstruits+=1;
        }

        else if (bati.CompareTag("BatiBoulangerie"))
        {
            boulangerie.transform.position = new Vector3(bati.transform.position.x, boulangerie.transform.position.y, bati.transform.position.z);
            missionManager.Build("boulangerie");
            boulangerieConstruit = true;
            nombreDeBâtimentsConstruits+=2;
        }
        else if (bati.CompareTag("BatiMoulinAEau")) 
        {
            moulinEau.transform.position = new Vector3(bati.transform.position.x, moulinEau.transform.position.y, bati.transform.position.z);
            missionManager.Build("moulin à eau");
            moulinEauConstruit = true;
            nombreDeBâtimentsConstruits+=1;
        }
        
        Destroy(bati.gameObject);
        ouvrier.GetComponent<Animator>().SetFloat("Construction", 0);
        ouvrier.transform.position = new Vector3(ouvrier.transform.position.x, ouvrier.transform.position.y, ouvrier.transform.position.z -6);
        permissionConstruction = false;
        constructionTerminee = true;
        RessourcesNécessairesDéposées = false;
        BuildingLayerMag.updateBatLayers();
        timeDepart = time;
        ecartTime = (int) (timeDepart - time);
        permettreConstru();
        Emptyscriptconstru.acBoutonsConstru();
        panel.SetActive(false);


    }

    public void abandonnerConstruction()
    {
        Destroy(bati.gameObject);
        ouvrier.GetComponent<Animator>().SetFloat("Construction", 0);
        ouvrier.transform.position = new Vector3(ouvrier.transform.position.x, ouvrier.transform.position.y, ouvrier.transform.position.z - 6);
        panel.SetActive(false);
        permissionConstruction = false;
        constructionTerminee = true;
        RessourcesNécessairesDéposées = false;
        BuildingLayerMag.updateBatLayers();
        timeDepart = time;
        ecartTime = (int)(timeDepart - time);
        permettreConstru();
        Emptyscriptconstru.acBoutonsConstru();
        
    }

    

    public void permettreConstru()
    {
        ouvrierOccupe.enabled = false;
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