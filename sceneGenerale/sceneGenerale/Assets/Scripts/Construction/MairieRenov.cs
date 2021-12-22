using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MairieRenov : MonoBehaviour
{
    public GameObject panel;
    bool open;
    bool onPanel;
    public bool isOccupied;
    Vector2 mP;
    new public Camera camera;
    public Animator animator;




    public UI_Inventory ui_inventory;
    public Player player;
    public GameObject ouvrier;

    public Item bois;
    public Image boisImage;
    private int nombreBois;

    private Text ouvrierOccupe;


    public Button bulleInfo;
    public Button bulleConstruire;
    public Button bulleDeposer;
    public Button bordereauConstruire;
    public Button bordereauDeposer;

    public Text textBois;
    public GameObject texteBois;

    //temps
    public GameObject temps;
    private Text texteTemps;
    float time;
    float timeDepart;
    float ecartTime;
    public float TimerInterval = 5;
    float tick;

    private int nombreBoisNecessaire = 10;

    //outils
    public bool RessourcesNécessairesDéposées = false;
    public bool permissionConstruction = false;
    public bool constructionTerminee = false;
    public bool depotEnCours = false;

    private int tempsConstru = 30;

    public GameObject mairie;


    private void Awake()
    {
        time = (int)Time.time;
        tick = TimerInterval;
    }

    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("PanelMairieRenov");
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        animator = panel.transform.GetChild(0).GetComponent<Animator>();

        onPanel = false;
        open = false;
        isOccupied = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        texteTemps = temps.GetComponent<Text>();

        ouvrierOccupe = GameObject.Find("TextOuvrierOccupe").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
            mP = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (onPanel == false)
            {
                panel.SetActive(false);
                open = false;
            }

            if (open == false && (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) + ((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) < 1))
            {
                if ((Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("MairieRuine")))
                {
                    panel.transform.position = new Vector2(mP.x + panel.GetComponent<RectTransform>().rect.width, mP.y);
                    panel.gameObject.SetActive(true);
                    animator.SetTrigger("Ouverture");
                    open = true;
                    if (constructionTerminee) resetConstruction();

                    ui_inventory = player.uiInventory;
                    nombreBois = player.uiInventory.CountItem("Bois");
                    textBois.text = nombreBoisNecessaire.ToString();


                }

            }

        }

        ui_inventory = player.uiInventory;
        nombreBois = player.uiInventory.CountItem("Bois");

        if (RessourcesNécessairesDéposées && permissionConstruction)
        {
            ConstructionInProgress();
        }

        if ((int)(tempsConstru - ecartTime) == 0) finirConstruction();
    }


    public void resetConstruction()
    {

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
        nombreBois = player.uiInventory.CountItem("Bois");
        print("bois du joueur : " + nombreBois);

        //if ya du bois et nombre bois inventaire< nombre bois nécessaire à la constru du bâtiment
        if (nombreBois > 0)
        {
            if (nombreBois <= nombreBoisNecessaire)
            {
                Debug.Log("cas1");
                nombreBoisNecessaire -= nombreBois;
                RetirerInventaire(bois, nombreBois);
            }
            else
            {
                Debug.Log("cas2");
                RetirerInventaire(bois, nombreBoisNecessaire);
                nombreBoisNecessaire = 0;

            }


        }

        if (nombreBoisNecessaire < 0) nombreBoisNecessaire = 0;

        nombreBois = player.uiInventory.CountItem("Bois");
        textBois.text = nombreBoisNecessaire.ToString();

        if (nombreBoisNecessaire == 0)
        {
            RessourcesNécessairesDéposées = !RessourcesNécessairesDéposées;
            texteBois.SetActive(false);
            bulleConstruire.interactable = true;
            bordereauConstruire.interactable = true;
            boisImage.gameObject.SetActive(false);


        }

    }

    public void DebuterConstruction()
    {
        permissionConstruction = true;
        temps.SetActive(true);
        timeDepart = Time.time;
        depotEnCours = false;
        ouvrier.transform.position = new Vector3(transform.position.x, ouvrier.transform.position.y, transform.position.z - 14);
        ouvrier.GetComponent<Animator>().SetFloat("Construction", 1);
    }

    void ConstructionInProgress()
    {
        if (permissionConstruction)
        {
            time = Time.time;
            ecartTime = (int)(time - timeDepart);

        }
        texteTemps.text = string.Format("{0:0}:{1:00}", Mathf.Floor((tempsConstru - ecartTime) / 60), (tempsConstru - ecartTime) % 60);

    }

    void finirConstruction()
    {
        gameObject.SetActive(false);
        ouvrier.GetComponent<Animator>().SetFloat("Construction", 0);
        ouvrier.transform.position = new Vector3(ouvrier.transform.position.x, ouvrier.transform.position.y, ouvrier.transform.position.z - 6);
        permissionConstruction = false;
        constructionTerminee = true;
        RessourcesNécessairesDéposées = false;
        BuildingLayerMag.updateBatLayers();
        timeDepart = time;
        ecartTime = (int)(timeDepart - time);
        ouvrierOccupe.enabled = false;
        bulleInfo.gameObject.SetActive(false);
        mairie.SetActive(true);


    }

    //fonction pour retirer un item de l'inventaire
    void RetirerInventaire(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {

        player.inventory.DelItem(new ItemAmount(Item: item, Amount: Amount));
    }


    public void ClickOnPanel()
    {
        onPanel = true;
        Deplacement.enMenu = true;
    }

    public void ClickOutPanel()
    {
        onPanel = false;
        Deplacement.enMenu = false;
    }
}
