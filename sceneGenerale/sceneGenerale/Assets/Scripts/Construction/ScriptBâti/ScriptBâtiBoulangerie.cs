using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBâtiBoulangerie : MonoBehaviour
{
    public bool menuBâtiPasDéjàAffiché = true;
    public bool menuConstructionPasDéjàAffiché = false;
    private Vector3 mP;
    private Vector3 position;
    private Vector3 positionBis;
    private Camera camera;
    public Inventaire inventaire;
    private Rect rect;
    private bool positionDéfinie = false;
    private int NbrBoisNécessaire = 1; //je mettrai les vrais valeurs plus tard
    public int NbrBois;
    public int NbrPierre;
    public bool RessourcesNécessairesDéposées;
    public BoutonsMenuConstruction boutonsMenuConstruction;
    public bool OnCliqueDehors = false;
    public bool moving = false;
    public bool OnAfficheLeMenuDuBâti = true;
    private string str1;
    private string str2;
    private int heures;
    private int minutes;
    private int secondes;
    private float tempsConstructionChaumière = 3; //oui ça s'appelle chaumière :) 
    private int tempsConstructionChaumièreEntier = 3; //3 sec pour l'instant, on changera plus tard
    private bool débuterConstruction = false;
    public GameObject prefabBoulangerie;
    public GameObject Boulangerie;
    public GameObject BatiBoulangerie;
    //public GameObject BatiMoulin;
    public bool onAPasEncoreDétruitLeBâti = true;
    public UI_Inventory ui_inventory;
    public Item Bois, Pierre;


    //partie ath
    // je peux pas glisser les boutons, je passe par le padre
    private GameObject boutonTravailOuvrier;
    private GameObject boutonDéposerRessources;
    private GameObject boutonBanderoleDéposerRessources;
    private GameObject boutonFabriquerAvant;
    private GameObject boutonBanderoleFabriquerAvant; // dans le update if ressourcesNécessaires on met le setActive
    private GameObject boutonFabriquerAprès;
    private GameObject boutonBanderoleFabriquerAprès;
    private GameObject boutonAnnuler;
    private GameObject boutonBanderoleAnnuler;
    private GameObject FondPrefab;
    private GameObject Fond;
    private GameObject Menus;
    private GameObject MenuConstruction;
    private Animator animator;
    


    // animators, 
    /// <summary>
    /// 
    /// </summary>


    public Player player;
    private GameObject[] playerLeVrai;
    public GameObject lePapaDePlayer; //pour pouvoir récupérer player depuis la fonction start via un getchild. Je peux pas directement le link dans mon script prefab
    //public static BoutonsMenuConstruction BatiMoulin;

    //public Camera camera;
    //public GameObject prefabMenuBâti;

    private Rect menuBâti;
    
    //j'instantiate le fond au start avec un set active false? Puis transform à chaque clic?

    
    void Start()
    {
        //Partie ATH
        Menus = GameObject.Find("Menus");
        MenuConstruction = Menus.transform.Find("MenuConstruction").gameObject;
        Fond = MenuConstruction.transform.Find("AnimsATH/animATHBoulangerie/PanelMenuBoulangerie").gameObject;
        if (Fond != null)
        {
            print("çamarche");
        }
        boutonTravailOuvrier = Fond.transform.Find("InfoBulle").gameObject;
        boutonDéposerRessources = Fond.transform.Find("BulleDeposRessource").gameObject;
        boutonBanderoleDéposerRessources = boutonDéposerRessources.transform.Find("borderoDéposer").gameObject;
        boutonFabriquerAprès = Fond.transform.Find("BulleConstruireAprès").gameObject;
        boutonBanderoleFabriquerAprès = boutonFabriquerAprès.transform.Find("borderoConstruireAprès").gameObject;
        boutonAnnuler = Fond.transform.Find("BulleAnnuler").gameObject;
        boutonBanderoleAnnuler = boutonAnnuler.transform.Find("borderoAnnuler").gameObject;
        animator = Fond.transform.GetChild(0).GetComponent<Animator>();



        //Fond= Instantiate(FondPrefab, new Vector3(0f, 2f, 0f), Quaternion.Euler(0, 0, 0));
        //boutonTravailOuvrier = Fond.transform.GetChild(0);
        //boutonDéposerRessources = boutonTravailOuvrier.transform.GetChild(2) ;
        ////boutonDéposerRessources = boutonDéposerRessources.GetComponent<Button>();
        //boutonBanderoleDéposerRessources = boutonDéposerRessources.transform.GetChild(0);
        ////boutonBanderoleDéposerRessources = boutonBanderoleDéposerRessources.GetComponent<Button>();
        //boutonFabriquer = boutonTravailOuvrier.transform.GetChild(3);
        ////boutonFabriquer = boutonFabriquer.GetComponent<Button>();
        //boutonBanderoleFabriquer = boutonFabriquer.transform.GetChild(0);
        ////boutonBanderoleFabriquer = boutonBanderoleFabriquer.GetComponent<Button>();
        //boutonAnnuler = boutonTravailOuvrier.transform.GetChild(0);
        ////boutonAnnuler = boutonAnnuler.GetComponent<Button>();
        //boutonBanderoleAnnuler = boutonAnnuler.transform.GetChild(0);
        ////boutonBanderoleAnnuler=boutonBanderoleAnnuler.transform.GetChild(0);
        //animator = Fond.transform.GetChild(0).GetComponent<Animator>();





        //Partie Bâti
        playerLeVrai = GameObject.FindGameObjectsWithTag("Player");
        print(playerLeVrai[0]);
        player = playerLeVrai[0].GetComponent<Player>(); //yavait même pas besoin de passer par lePapaDePlayer en fait :) 
        ui_inventory = player.uiInventory;
        //inventaire = inventaire.GetComponent<Inventaire>();

    }

    // Update is called once per frame
    void Update() // Pour savoir quand je clique dehors oui
    {
        camera = Camera.main;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //print("Du clic du clic");

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Untagged"))
                {
                    OnCliqueDehors = true;
                }
                if (hit.collider.CompareTag("BatiBoulangerie")) //Problème: Quand on construit plusieurs chaumières en même temps (seul bâtiment avec lequel on peut faire ça :)))) )
                                                              //, et qu'on clique sur le bâti, le menu de tous les bâtis de chaumière s'affichent, puisqu'on fonctionne avec des tags, donc il va falloir trouver une astuce
                                                              //pour éviter ce petit problème. --> Après mûre réflexion, on s'en branle :) , de toute façon on va plus utiliser les onGUI pour les menus donc nsm

                {

                    OnCliqueDehors = false;
                    OnAfficheLeMenuDuBâti = true;
                }

            }
        }
        if (débuterConstruction)
        {
            tempsConstructionChaumièreEntier = (int)tempsConstructionChaumière; // On en a besoin pour faire les divisions entières

            heures = tempsConstructionChaumièreEntier / 3600;
            minutes = (tempsConstructionChaumièreEntier - heures * 3600) / 60;
            secondes = (tempsConstructionChaumièreEntier - heures * 3600 - minutes * 60);
            tempsConstructionChaumière -= Time.deltaTime;
        }

        if (OnCliqueDehors)
        {
            animator.SetTrigger("fermetture_3_bulle"); //tu sais pas écrire dodo
        }
    }

    void OnMouseDown()
    {
        camera = Camera.main;

        if (menuBâtiPasDéjàAffiché)// pour éviter d'avoir plusieurs menus en même temps
        {

            RaycastHit hit;  //même principe que pour la récolte
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            mP = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0); //là j'ai une position en pixels il faut que je la transforme en une position sur l'écran
            mP.z = camera.transform.position.y;
            position = camera.ScreenToWorldPoint(mP);
            //print(position.x);
            //print(mP.y);
            //print(position.z);
            positionDéfinie = true;
            rect = GUIUtility.ScreenToGUIRect(new Rect(position.x, position.y, 150, 250));
            //ici on va s'occuper de la partie affichage de l'animation pour l'ath
            Fond.SetActive(true);
            animator.SetTrigger("ouverture_3_bulle");



        }

    }

    public void DéposerRessources()
    {

    }


    private void OnGUI() //affichage des menus
    {
        camera = Camera.main;
        if (!OnCliqueDehors && OnAfficheLeMenuDuBâti)
        {
            //if (Input.GetKeyDown(KeyCode.Mouse0))
            //{
            //    print("Du clic du clic");
            //    RaycastHit hit;
            //    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        if (hit.collider.CompareTag("Untagged"))
            //        {
            //            OnCliqueDehors = true;
            //        }
            //        if (hit.collider.CompareTag("Moulin"))
            //        {
            //            OnCliqueDehors = false;
            //        }

            //    }
            //}
            if (menuBâtiPasDéjàAffiché && positionDéfinie)// && boutonsMenuConstruction.boutonMenuEstAffiche) // si ya pas déjà de menu pour éviter les doublons et si on a la position(sinon le menu s'affiche sans qu'on clique c'est chiant)
            {
                //print("oui");
                GUI.Box(new Rect(mP.x, Screen.height - mP.y, 150, 250), "Boulangerie");   // la fonction Screen.height permet 

                //GUI.enabled = (CountItem("Hache") > 0); // si on a une hache, on peut cliquer sur le bouton (Pour la construction jsp s'il faut des items ou des pnj dispo bref)
                if (!RessourcesNécessairesDéposées)//tant qu'on a pas encore déposer toutes les ressources, on peut continuer à en déposer
                {
                    if (GUI.Button(new Rect(mP.x, Screen.height - mP.y + 40, 150, 50), "Déposer ressources")) //le bouton pour déposer les ressources
                    {
                        //script du bouton déposer ressources. On a la fonction CountItem(string itemname) pour compter un item
                        NbrBois = player.uiInventory.CountItem("Bois");
                        NbrPierre = player.uiInventory.CountItem("Pierre");
                        print(NbrBois);
                        //if ya du bois et nombre bois inventaire< nombre bois nécessaire à la constru du bâtiment
                        if (NbrBois > 0)
                        {
                            if (NbrBois <= NbrBoisNécessaire)
                            {
                                NbrBoisNécessaire -= NbrBois;
                                RetirerInventaire(Bois, NbrBois);
                            }
                            else
                            {
                                RetirerInventaire(Bois, NbrBoisNécessaire);
                                NbrBoisNécessaire = 0;

                            }
                            //if NbrBoisNécessaire == 0 && NbrTissusNécessaire == 0 && ...{
                            //    RessourcesNécessairesDéposées = true;
                            //}
                            //}
                            // Et on fait pareil pour les autres ressources nécessaires
                            if (NbrBoisNécessaire == 0)
                            {
                                RessourcesNécessairesDéposées = !RessourcesNécessairesDéposées;
                            }
                        }
                    }
                }


                if (GUI.Button(new Rect(mP.x, Screen.height - mP.y + 100, 150, 50), "Choisir ouvrier")) // chiant de disable un seul bouton avec la commande GUI, donc je le laisse dispo mais quand on clique dessus ça fait rien
                {
                    if (RessourcesNécessairesDéposées) // et les autres ressources nécessaires =0
                    {
                        print("Tadaima!");
                        débuterConstruction = true;
                        // Alors on peut choisir le pnj qui va construire tout ce bazar
                    }
                }
                //if (RessourcesNécessairesDéposées) //si on a déposé toutes les ressources, on peut choisir un ouvrier pour débuter la construction
                //{
                //    if (GUI.Button(new Rect(mP.x, Screen.height - mP.y + 160, 150, 50), "Choisir ouvrier")) // d'ouvrier pour l'instant, on se contente de démarrer la construction (si on a toutes les ressources nécessaire ofc)
                //    {
                //        // pour l'instant on va dire qu'il faut 10 sec pour construire la bête
                //        ;
                //    }
                //}
                if (débuterConstruction)
                {
                    //print("La construction débute");
                    //tempsConstructionChaumièreEntier = (int)tempsConstructionChaumière; // On en a besoin pour faire les divisions entières

                    //heures = tempsConstructionChaumièreEntier / 3600;
                    //minutes = (tempsConstructionChaumièreEntier - heures * 3600) / 60;
                    //secondes = (tempsConstructionChaumièreEntier - heures * 3600 - minutes * 60);
                    //tempsConstructionChaumière -= Time.deltaTime * 0.5f;
                    if (secondes >= 0)
                    {
                        if (GUI.Button(new Rect(mP.x, Screen.height - mP.y + 160, 150, 50), heures.ToString("0") + ":" + minutes.ToString("0") + ":" + secondes.ToString("0")))
                        {
                            //if (heures == 0 && minutes == 0 && secondes == 0)
                            //{
                            //    BatiMoulin = GameObject.Find("BatiMoulin");
                            //    Moulin = Instantiate(prefabMoulin, BatiMoulin.transform.position, Quaternion.Euler(-20, 0, 0)); //Le moulin final
                            //    Destroy(BatiMoulin);
                            //}
                        }
                    }
                    if (secondes < 0 && onAPasEncoreDétruitLeBâti)
                    { //onGUI est une fonction qui s'appelle à chaque frame, donc il faut faire attention à ne construire le moulin qu'une seule fois

                        BatiBoulangerie = GameObject.Find("BatiBoulangerie");

                        Boulangerie = Instantiate(prefabBoulangerie, BatiBoulangerie.transform.position + new Vector3(0f, 2f, 0f), Quaternion.Euler(-20, 0, 0)); //Le moulin final
                        Destroy(BatiBoulangerie);
                        onAPasEncoreDétruitLeBâti = false;
                        débuterConstruction = false;
                    }
                }

                //menuBâtiPasDéjàAffiché = !menuBâtiPasDéjàAffiché;
                //print(OnCliqueDehors.ToString());
                if (!OnCliqueDehors)
                {
                    GUI.enabled = true;
                }

                if (GUI.Button(new Rect(mP.x, Screen.height - mP.y + 220, 150, 50), "Fermer"))
                {

                    OnAfficheLeMenuDuBâti = false;
                }
            }


            //GUI.enabled = true;
        }
    }


    void AjouterInventaire(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {

        player.inventory.AddItem(new ItemAmount(Item: item, Amount: Amount));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="Amount"></param>
    void RetirerInventaire(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {

        player.inventory.DelItem(new ItemAmount(Item: item, Amount: Amount));
    }

    /////////////////////////////////////////// Partie ATH construction de la Boulangerie ////////////////////////////////////////////

    //Il s'agit là de la partie script du bouton pour fabriquer la boulangerie. Il ne doit être activable que lorsque toutes les ressources nécessaires à la fabrication ont été déposées
    // Je fais le script ici car ya déjà les bools dont j'ai besoin (RessourcesNécessairesDéposées notamment)

    // void start où on récupère boutons, animators, texts --> pas besoin en fait c'est des préfabs youpii juste à glisser
    //puis le update où on rend le bouton interactable si les ressources nécessaires ont été déposées





}



















