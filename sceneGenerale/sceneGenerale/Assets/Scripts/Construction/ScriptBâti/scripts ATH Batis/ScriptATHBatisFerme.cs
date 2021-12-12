using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptATHBatisFerme : MonoBehaviour
{

    public UI_Inventory ui_inventory;
    public Player player;
    public GameObject ouvrier;

    //les batis
    public GameObject bati;

    //les batiments
    public GameObject ferme;


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
    public float tempsConstruChaumiere = 10;

    //donnees
    private float tempsConstruTotal; //doit être exprimé en heures
    public Item itemOne;
    public Item itemTwo;
    private int nombreItemOne;
    private int nombreItemTwo;
    private int nombreItemOneRestants;
    private int nombreItemTwoRestants;
    public int nombreItemOneNécessaire = 1;
    public int nombreItemTwoNécessaire = 1;

    //outils
    public bool RessourcesNécessairesDéposées = false;
    private bool permissionConstruction = false;


    // Start is called before the first frame update

    private void Awake()
    {
        time = (int)Time.time;
        tick = TimerInterval;
    }
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

        if ((int)(tempsConstruChaumiere - ecartTime) == 0) finirConstruction();
    }

    public void DepotRessource()
    {
        //cette ligne sert à trouver le bati pour plus tard
        bati = GameObject.Find("BatiFerme");

        ui_inventory = player.uiInventory;
        nombreItemTwo = player.uiInventory.CountItem("Bois");
        nombreItemOne = player.uiInventory.CountItem("Pierre");
        print("pierre du joueur : " + nombreItemOne);
        print("bois du joueur : " + nombreItemTwo);
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

    public void DebuterConstruction()
    {
        permissionConstruction = true;
        temps.SetActive(true);
        timeDepart = Time.time;

        ouvrier.transform.position = new Vector3(bati.transform.position.x + 6, ouvrier.transform.position.y, bati.transform.position.z - 5);
        ouvrier.GetComponent<Animator>().SetFloat("Construction", 1);
    }

    void ConstructionInProgress()
    {
        int tempsConstruTotalEntier = (int)(3600 * tempsConstruTotal); //ceci est exprimé en secondes
        if (permissionConstruction)
        {
            time = Time.time;
            ecartTime = (int)(time - timeDepart);

        }
        texteTemps.text = string.Format("{0:0}:{1:00}", Mathf.Floor((tempsConstruChaumiere - ecartTime) / 60), (tempsConstruChaumiere - ecartTime) % 60);

    }

    void finirConstruction()
    {
        Instantiate(ferme, new Vector3(bati.transform.position.x, 5.81f, bati.transform.position.z), ferme.transform.rotation);
        Destroy(bati.gameObject);
        ouvrier.GetComponent<Animator>().SetFloat("Construction", 0);
        ouvrier.transform.position = new Vector3(ouvrier.transform.position.x, ouvrier.transform.position.y, ouvrier.transform.position.z - 5);
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

    void relierBatiABatiment()
    {

    }

}
