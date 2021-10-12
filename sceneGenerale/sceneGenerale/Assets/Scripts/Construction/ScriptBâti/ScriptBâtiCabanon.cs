using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBâtiCabanon : MonoBehaviour
{
    public bool menuBâtiPasDéjàAffiché = true;
    public bool menuConstructionPasDéjàAffiché = false;
    private Vector3 mP;
    private Vector3 position;
    private Vector3 positionBis;
    public Camera camera;
    public Inventaire inventaire;
    private Rect rect;
    private bool positionDéfinie = false;
    private int NbrBoisNécessaire = 0; //je mettrai les vraies valeurs plus tard
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
    private float tempsConstructionChaumière = 10;  // je devrais le rename boulangerie mais c'est une variable private ça va rien changer bref flemme :) 
    private int tempsConstructionChaumièreEntier = 10; //10 sec pour l'instant, on changera plus tard
    private bool débuterConstruction = false;
    public GameObject prefabCabanon;
    public GameObject Cabanon;
    public GameObject BatiCabanon;
    //public GameObject BatiMoulin;
    public bool onAPasEncoreDétruitLeBâti = true;
    //public static BoutonsMenuConstruction BatiMoulin;

    //public Camera camera;
    //public GameObject prefabMenuBâti;

    private Rect menuBâti;
    // Start is called before the first frame update
    void Start()
    {

        //inventaire = inventaire.GetComponent<Inventaire>();

    }

    // Update is called once per frame
    void Update() // Pour savoir quand je clique dehors oui
    {
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
                if (hit.collider.CompareTag("BatiCabanon"))
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
    }

    void OnMouseDown()
    {

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
            //print(boutonsMenuConstruction.boutonMenuEstAffiche);
            //if (boutonsMenuConstruction.boutonMenuEstAffiche)
            //{
            //   print("oui");
            //}


            //if (Physics.Raycast(ray, out hit))
            //{
            //    if (hit.collider.CompareTag("Untagged"))
            //    {
            //        OnCliqueDehors = true;
            //    }
            //    if (hit.collider.CompareTag("Moulin"))
            //    {
            //        OnCliqueDehors = false;
            //    }

            //}

        }

    }


    private void OnGUI() //affichage des menus
    {

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
                GUI.Box(new Rect(mP.x, Screen.height - mP.y, 150, 250), "Cabanon");   // la fonction Screen.height permet 

                //GUI.enabled = (CountItem("Hache") > 0); // si on a une hache, on peut cliquer sur le bouton (Pour la construction jsp s'il faut des items ou des pnj dispo bref)
                if (!RessourcesNécessairesDéposées)//tant qu'on a pas encore déposer toutes les ressources, on peut continuer à en déposer
                {
                    if (GUI.Button(new Rect(mP.x, Screen.height - mP.y + 40, 150, 50), "Déposer ressources")) //le bouton pour déposer les ressources
                    {
                        //script du bouton déposer ressources
                        //if ya du bois et nombre bois inventaire< nombre bois nécessaire à la constru du bâtiment
                        //if (NbrBois(Inventaire) > 0){
                        //    if (NbrBois(Inventaire) <= NbrBoisNécessaire){
                        //        NbrBoisNécessaire -= NbrBois(Inventaire);
                        //        AjouterInventaire("Bois", -NbrBois(Inventaire));
                        //    }
                        //    else
                        //    {
                        //        AjouterInventaire("Bois", -NbrBoisNécessaire);
                        //        NbrBoisNécessaire=0;

                        //    }
                        //    if NbrBoisNécessaire==0 && NbrTissusNécessaire==0 &&...{
                        //       RessourcesNécessairesDéposées=true;
                        //    }
                        //}
                        // Et on fait pareil pour les autres ressources nécessaires
                        RessourcesNécessairesDéposées = !RessourcesNécessairesDéposées;
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

                        BatiCabanon = GameObject.Find("BatiCabanon");

                        Cabanon = Instantiate(prefabCabanon, BatiCabanon.transform.position, Quaternion.Euler(-20, 0, 0)); //Le moulin final
                        Destroy(BatiCabanon);
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
}
