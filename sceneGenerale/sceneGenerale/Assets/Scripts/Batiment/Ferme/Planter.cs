using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planter : MonoBehaviour
{
    private Camera MainCamera;
    public static int[] nbrePlantes = new int[6] { 0, 0, 0, 0, 0, 0 };

    private Animator animatorLivreActivite;

    //Variables récupérées dans Agri.cs
    private int xNbrParcelles;
    private int yNbrParcelles;

    //------QUANTITES NOURRITURE------//

    //types de cultures que l'on fait correspondre aux quantitées de nourriture qu'ils génèrent
    enum Culture
    {
        Ble = 0,
        Mais = 1,
        Salade = 2,
        Tomate = 3,
        Raisin = 5
    }

    public int[] arrayQN_max = new int[] { 0, 2, 4, 5, 7, 7 };

    //------CAPACITE DE TRAVAIL------//

    public static int capaciteTravail;
    private int capaciteTravailUtilisee;

    //capacités de travail demandées par types de cultures
    public const int bleCT = 10;
    public const int maisCT = 12;
    public const int saladeCT = 15;
    public const int tomateCT = 17;
    public const int raisinCT = 20;

    //array qui contient les capacités de travail demandés par les cultures, puis les quantités de nourritures produites avec engrais
    public int[] arrayCT;

    //------------CULTURES------------//

    public static int culture;         //nombre de culture dispo pour le jouer (pas encore implémenté)
    public int[,] cultureParcelles;    //array qui contient les numéros (correspondant à un enum de Culture et à la quantité de nourriture générée par cette culture) de chaque culture présente sur chaque parcdelle
                                       //de base on va le remplir de -1 quand il n'y a pas de culture
    private int planteSelectionnee;  //de base aucune, donc 6
    public GameObject[,] arrayPrefabsPlantes;  //array qui contient les prefabs des plantes qui sont instanciées pour pouvoir les enlever

    private Player player;
    public Item Paille;

    //prefab des images que l'on va afficher sur les parcelles
    public GameObject blePf;
    public GameObject maisPf;
    public GameObject saladePf;
    public GameObject tomatePf;
    public GameObject raisinPf;

    public Transform planteContainer; //gameObject vide dans lequel on instantie les prefabs des plantes
    public GameObject zoneBleuePf;    //on recupère aussi le prefab d'une parcelle juste pour avoir sa taille
    public Transform fermeTransform;   //Transform de la ferme
    private Vector3 sizeParcelle;      //taille des prefabs des parcelles

    public GameObject panelPlantage;
    public Slider slider;

    //------ENGRAIS------//

    public int[,] engraisParcelles;     //array qui contient le nombre de jours restants durant lesquels un engrais va continuer à agir, la valeur est donc à 0 par defaut
    public static bool modeEngrais;    //bool qui indique si on est dans le menu engrais ou dans le menu plantage
    public int engraisSelectionne;  //0 pour le chimique et 1 pour le naturel
    public int engraisDispo;         //nombre d'engrais naturels

    public GameObject panelEngrais;
    public Text textNombreEngraisNaturel;

    public static bool isOccupied;

    public int nombreDeMais;
    private int compteurPaille;
    public Text textNombrePaille;
    private Batiment batiment;

    //------SUREXPLOITATION/QUALITE_SOL------//

    public int[,] anciennesCultureParcelles;  //Tableau qui garde en mémoire les cultures qu'il y avait sur les parcelles le jour précédent pour voir si ça a changé
    public int[,] nbrJoursMemeCulture;        //Tableau qui contient le nombre de jours depuis lesquels on a la même culture sur une parcelle
    public int[,] nbrJoursCultureParcelles;   //Tableau qui commpte juste le nombre de jour depuis lesquels on a une culture tout court sur la parcelle

    void Awake()
    {
        nombreDeMais = 0;
        compteurPaille = 0;
        batiment = this.GetComponent<Batiment>();
    }

    void Start()
    {
        arrayCT = new int[7] { 10, 12, 15, 17, 20, 20, 0 };   //on met un 0 à la fin pour que quand planteSelectionee == 6, ça update la CT de 0 en appellant arrayCT[6]

        animatorLivreActivite = GameObject.Find("LivreFerme").GetComponent<Animator>();
        MainCamera = GameObject.Find("Camera").GetComponent<Camera>();
        xNbrParcelles = Agri.xNbrParcelles;
        yNbrParcelles = Agri.yNbrParcelles;

        capaciteTravail = 50;
        capaciteTravailUtilisee = 0;

        culture = 1;
        //initialisation de cultureParcelles
        cultureParcelles = new int[xNbrParcelles, yNbrParcelles];
        for (int i = 0; i < xNbrParcelles * yNbrParcelles; i++) cultureParcelles[i % xNbrParcelles, i / xNbrParcelles] = -1;
        SelectionAucunePlante();
        arrayPrefabsPlantes = new GameObject[xNbrParcelles, yNbrParcelles];

        player = GameObject.Find("Principal_OK").GetComponent<Player>();

        sizeParcelle = zoneBleuePf.GetComponent<Renderer>().bounds.size;
        sizeParcelle.y = 0f;
        planteContainer.position = fermeTransform.position - (new Vector3(sizeParcelle.x * (xNbrParcelles - 1) / 2, 1.14f, sizeParcelle.z * (yNbrParcelles - 1) / 2 - 1.5f));

        engraisParcelles = new int[xNbrParcelles, yNbrParcelles];
        modeEngrais = false;
        engraisDispo = 5;

        anciennesCultureParcelles = new int[xNbrParcelles, yNbrParcelles];
        for (int i = 0; i < xNbrParcelles * yNbrParcelles; i++) anciennesCultureParcelles[i % xNbrParcelles, i / xNbrParcelles] = -1;
        nbrJoursMemeCulture = new int[xNbrParcelles, yNbrParcelles];
        nbrJoursCultureParcelles = new int[xNbrParcelles, yNbrParcelles];

        //MajNiveau();
    }

    void Update()
    {
        slider.maxValue = capaciteTravail;
        slider.value = capaciteTravailUtilisee;

        //On cherche à savoir si le joueur clique sur une parcelle
        // (faudra bien si on a plus de 10x10 parcelles)
        if (Input.GetMouseButtonDown(0))
        {
            //on récupère la position du toucher de l'utilisateur sur l'écran
            Vector2 touchPosition = Input.mousePosition;

            //on transforme cette position en rayon perpendiculaire au plan de la camera
            Ray ray = MainCamera.ScreenPointToRay(new Vector3(touchPosition.x, touchPosition.y, 0f));
            RaycastHit hit;

            //si ce rayon rencontre un obstable on récupère l'objet
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objetTouche = hit.transform.gameObject;

                if ((objetTouche.tag == "Parcelle") || (objetTouche.tag == "Plante"))  //c'est l'indication que c'est une parcelle labourée finale ou une culture, donc on peut planter une culture dessus ou mettre de l'engrais
                {
                    int i = ToInt(objetTouche.name[0]);
                    int j = ToInt(objetTouche.name[1]);

                    if (modeEngrais)
                    {
                        if (DepotEngrais(i, j))
                        {
                            Debug.Log("Depot engrais en (" + i.ToString() + "," + j.ToString() + ")");
                        }
                        else
                        {
                            Debug.Log("Plus d'engrais naturel en réserve");
                        }
                        textNombreEngraisNaturel.text = engraisDispo.ToString();
                    }
                    else  //mode plantation
                    {
                        if (cultureParcelles[i, j] == -1)  //si il n'y a pas de culture sur la parcelle
                        {
                            PlanterCulture(i, j);
                        }
                        else
                        {
                            EnleverCulture(i, j);
                        }
                    }
                }
            }
        }
    }

    private int ToInt(char c)
    {
        return (int)(c - '0');
    }

    public void PlanterCulture(int x, int y) //Ajoute un prefab de la plante sélectionnée sur une certaine parcelle
    {
        GameObject plante;
        float taillePlante = 0.2f;
        if (planteSelectionnee == 6)
        {
            Debug.Log("Aucune plante sélectionnée"); 
            cultureParcelles[x, y] = -1; 
            return;
        }
        else if (planteSelectionnee == 0)
        {
            if (capaciteTravailUtilisee + arrayCT[planteSelectionnee] <= capaciteTravail) {
                plante = Instantiate(blePf, planteContainer.position + new Vector3((x - taillePlante) * sizeParcelle.x, 0f, (y - taillePlante) * sizeParcelle.z), Quaternion.identity, planteContainer);
                plante.name = x.ToString() + y.ToString() + Enum.GetName(typeof(Culture), planteSelectionnee);
                EnvironnementManager.instance.qualiteSol += 0.1f;
                arrayPrefabsPlantes[x, y] = plante;
            }
        }
        else if (planteSelectionnee == 1)
        {
            if (capaciteTravailUtilisee + arrayCT[planteSelectionnee] <= capaciteTravail)
            {
                plante = Instantiate(maisPf, planteContainer.position + new Vector3((x - taillePlante) * sizeParcelle.x, 0f, (y - taillePlante) * sizeParcelle.z), Quaternion.identity, planteContainer);
                plante.name = x.ToString() + y.ToString() + Enum.GetName(typeof(Culture), planteSelectionnee);
                EnvironnementManager.instance.qualiteSol += 0.1f;
                arrayPrefabsPlantes[x, y] = plante;
            }
        }
        else if (planteSelectionnee == 2)
        {
            if (capaciteTravailUtilisee + arrayCT[planteSelectionnee] <= capaciteTravail)
            {
                plante = Instantiate(saladePf, planteContainer.position + new Vector3((x - taillePlante) * sizeParcelle.x, 0f, (y - taillePlante) * sizeParcelle.z), Quaternion.identity, planteContainer);
                plante.name = x.ToString() + y.ToString() + Enum.GetName(typeof(Culture), planteSelectionnee);
                EnvironnementManager.instance.qualiteSol += 0.1f;
                arrayPrefabsPlantes[x, y] = plante;
            }
        }
        else if (planteSelectionnee == 3)
        {
            if (capaciteTravailUtilisee + arrayCT[planteSelectionnee] <= capaciteTravail)
            {
                plante = Instantiate(tomatePf, planteContainer.position + new Vector3((x - taillePlante) * sizeParcelle.x, 0f, (y - taillePlante) * sizeParcelle.z), Quaternion.identity, planteContainer);
                plante.name = x.ToString() + y.ToString() + Enum.GetName(typeof(Culture), planteSelectionnee);
                EnvironnementManager.instance.qualiteSol += 0.1f;
                arrayPrefabsPlantes[x, y] = plante;
            }
        }
        else if (planteSelectionnee == 5)
        {
            if (capaciteTravailUtilisee + arrayCT[planteSelectionnee] <= capaciteTravail)
            {
                plante = Instantiate(raisinPf, planteContainer.position + new Vector3((x - taillePlante) * sizeParcelle.x, 0f, (y - taillePlante) * sizeParcelle.z), Quaternion.identity, planteContainer);
                plante.name = x.ToString() + y.ToString() + Enum.GetName(typeof(Culture), planteSelectionnee);
                EnvironnementManager.instance.qualiteSol += 0.1f;
                arrayPrefabsPlantes[x, y] = plante;
            }
        }
        MajCT();
        cultureParcelles[x, y] = planteSelectionnee;
        MajQuantiteNourriture();
        UpdateVariete();
    }

    public void EnleverCulture(int x, int y)    //On enlève la plante sélectionnée d'une certaine parcelle
    {
        capaciteTravailUtilisee -= arrayCT[cultureParcelles[x, y]];  //on met à jour la capacité de travail
        //On enleve la plante de l'array cultureParcelles
        cultureParcelles[x, y] = -1;
        //On détruit ensuite le prefab de la plante dans planteContainer
        Destroy(arrayPrefabsPlantes[x, y]);
        
        SelectionAucunePlante();
        MajQuantiteNourriture();
    }

    private void MajCT()     //mise à jour de la quantité de travail, en fonction de la plante sélectionnée
    {
        capaciteTravailUtilisee += arrayCT[planteSelectionnee];
    }

    // Fonctions appelées lorsqu'on clique sur les boutons des différentes plantes que l'on veut planter
    public void SelectionBle()
    {
        planteSelectionnee = 0;
    }

    public void SelectionMais()
    {
        planteSelectionnee = 1;
    }

    public void SelectionSalade()
    {
        planteSelectionnee = 2;
    }

    public void SelectionTomate()
    {
        planteSelectionnee = 3;
    }

    public void SelectionRaisin()
    {
        planteSelectionnee = 5;
    }

    public void SelectionAucunePlante()
    {
        planteSelectionnee = 6;
    }

    public void SelectionChimique()
    {
        engraisSelectionne = 0;
    }

    public void SelectionNaturel()
    {
        engraisSelectionne = 1;
    }

    public void MajQuantiteNourriture()  //Fonction qui met à jour la quantité de nourriture tous les jours et qui met paille et blé produite dans le coffre,  on met pas encore à jour la variété
    {
        //D'abord on reset la quantité de nourriture produite dans le manager du batiement ferme
        batiment.quantiteNourriture = 0;

        //D'abord on augmente la quantité de nourriture du manager avec la quantité de maîs non consommée pour faire de la farine 
        batiment.quantiteNourriture += nombreDeMais;
        //puis on reset cette variable et on va l'augmenter par la suite
        nombreDeMais = 0;

        int q; //quantité nourriture sur les parcelle
        for (int i = 0; i < xNbrParcelles; i++)
        {
            for (int j = 0; j < yNbrParcelles; j++)
            {
                q = cultureParcelles[i, j];

                if (q == (int)Culture.Ble)
                {
                    //coder un truc pour mettre de la paille dans le coffre
                    //Debug.Log("ajout paille");
                    compteurPaille += 1;
                    MajCompteurPaille();
                }

                // On regarde ensuite si il y a de l'engrais 
                else if (engraisParcelles[i,j] > 0)
                {
                    batiment.quantiteNourriture += arrayQN_max[q];
                }

                else // Sinon on met les autres valeurs en prenant compte la pluriculture
                {
                    if (q == (int)Culture.Ble)   //Blé sur parcelle
                    {
                        batiment.quantiteNourriture += q;

                    }
                    else if (q == (int)Culture.Mais)   //Mais
                    {
                        //on check les 4 parcelles autour si il y a au moins une culture
                        if (
                           ((i > 0) && (cultureParcelles[i - 1, j] > -1))
                        || ((i < xNbrParcelles - 1) && (cultureParcelles[i + 1, j] > -1))
                        || ((j > 0) && (cultureParcelles[i, j - 1] > -1))
                        || ((j < yNbrParcelles - 1) && (cultureParcelles[i, j + 1] > -1))
                           )
                        {
                            nombreDeMais += q + 1;
                        }
                        else nombreDeMais += q;
                    }
                    else if (q == (int)Culture.Salade)  //Salade..
                    {
                        //on check les 4 parcelles autour si il y a au moins une culture
                        if (
                           ((i > 0) && (cultureParcelles[i - 1, j] > -1))
                        || ((i < xNbrParcelles - 1) && (cultureParcelles[i + 1, j] > -1))
                        || ((j > 0) && (cultureParcelles[i, j - 1] > -1))
                        || ((j < yNbrParcelles - 1) && (cultureParcelles[i, j + 1] > -1))
                           )
                        {
                            batiment.quantiteNourriture += q + 1;
                        }
                        else batiment.quantiteNourriture += q;
                    }
                    else if (((int)Culture.Tomate <= q) && (q <= (int)Culture.Raisin))  //Tomate ou Raisin
                    {
                        int n = 0; //on compte le nombre de cultures qu'il y a sur les parcelles autour
                        if ((i > 0) && (cultureParcelles[i - 1, j] > -1)) n++;
                        else if ((i < xNbrParcelles - 1) && (cultureParcelles[i + 1, j] > -1)) n++;
                        else if ((j > 0) && (cultureParcelles[i, j - 1] > -1)) n++;
                        else if ((j < yNbrParcelles - 1) && (cultureParcelles[i, j + 1] > -1)) n++;

                        if (n > 1) batiment.quantiteNourriture = q + 1;
                        else batiment.quantiteNourriture = q;
                    }
                }
            }
        }
        SocialManager.instance.MajNourriture(); //on met a jour la quantité dans le manager global
    }

    void MajCompteurPaille()
    {
        if (compteurPaille <= 25) textNombrePaille.text = compteurPaille.ToString();
        else textNombrePaille.text = "25";
    }

    public void RecuperePaille()  //quand on clique sur le bouton pour récupérer la paille
    {
        player.inventory.AddItem(new ItemAmount(Item: Paille, Amount: Math.Min(compteurPaille,25)));   //on ajoute dans l'inventaire le nombre de paille stocké limité à 25
        compteurPaille = 0;  //on reset le compteur
        MajCompteurPaille();
    }

    int[] CalculeNbrePlantes()  //fonction qui retourne un array contenant le nombre de chaque plante dans l'array cultureParcelles
    {
        int[] nbPlantes = new int[6] {0, 0, 0, 0, 0, 0};
        int q;
        for (int i = 0; i < xNbrParcelles; i++)
        {
            for (int j = 0; j < yNbrParcelles; j++)
            {
                q = cultureParcelles[i, j];
                if (q >= 0) nbPlantes[q] += 1;
            }
        }
        return nbPlantes;
    }

    void UpdateVariete()
    {
        List<Item> plantesProduites = new List<Item>();   //liste de variete des cultures
        int[] plantes = new int[6] { 0, 0, 0, 0, 0, 0 };  //tableau où on note qu'on a déjà rencontré une culture
        int q;

        for (int i = 0; i < xNbrParcelles; i++)
        {
            for (int j = 0; j < yNbrParcelles; j++)
            {
                q = cultureParcelles[i, j];
                if (q >= 0) //si il y a une plante sur cette parcelles
                {
                    if (plantes[q] == 0) //si on l'a pas déja ajoutée
                    {
                        plantes[q] += 1;  //on notifie dans le tableau qu'on la ajoutée
                        plantesProduites.Add(Item.Create_Instance(Enum.GetName(typeof(Culture), q), true));  //et on l'ajoute dans la liste de variété
                    }
                }
            }
        }
        batiment.ressourcesProduction = plantesProduites;
    }

    public void MajEngrais()   //fonction qui enlève un jour d'engrais dans toutes les parcelles ou il y en a
    {
        for (int i = 0; i < xNbrParcelles; i++)
        {
            for (int j = 0; j < yNbrParcelles; j++)
            {
                if (engraisParcelles[i, j] > 0) engraisParcelles[i, j] += -1;
            }
        }
    }

    public void MajQS()   //Mise à jour de la qualité du sol tout les jours à minuit
    {
        //Elle augmente d'abord naturellement de 0.1
        EnvironnementManager.instance.qualiteSol += 0.1f;

        int q; //stocke la culture en train d'être traitée
        for (int i = 0; i < xNbrParcelles; i++)
        {
            for (int j = 0; j < yNbrParcelles; j++)
            {
                q = cultureParcelles[i, j];
                if (q >= 0) //si la parcelle est cultivée
                {
                    nbrJoursCultureParcelles[i, j] += 1;
                    if (q == anciennesCultureParcelles[i, j]) //si c'est la même que le jour précédent
                    {
                        nbrJoursMemeCulture[i, j] += 1;
                    }
                    //Conséquences sur la qualité du sol :
                    int n = nbrJoursCultureParcelles[i, j];
                    if ((n >= 20) && ((n-20) % 10 == 0)) EnvironnementManager.instance.qualiteSol -= 1f; //on enleve 1 de QS si ça fait plus de 20 jours (et un mutliple de 10 depuis (0 inclu)) qu'on a une culture
                    int m = nbrJoursMemeCulture[i, j];
                    if ((m >= 10) && ((m - 10) % 6 == 0)) EnvironnementManager.instance.qualiteSol -= 1f; //on enleve 1 de QS si ça fait plus de 10 jours (et un mutliple de 6 depuis (0 inclu)) qu'on a la meme culture
                }
                else //si elle n'est pas cultivée
                {
                    nbrJoursCultureParcelles[i, j] = 0; //on reset
                    if (q == anciennesCultureParcelles[i, j]) //si c'est la même que le jour précédent
                    {
                        nbrJoursMemeCulture[i, j] += 1;
                    }
                    //Conséquence sur la qualité du sol :
                    if (nbrJoursMemeCulture[i,j] == 3) EnvironnementManager.instance.qualiteSol += 0.5f;  //Quand ça fait 3 jours qu'on a pas cultivé
                }
            }
        }
       // Array.Copy(cultureParcelles, anciennesCultureParcelles); //on reset les anciennes cultures avec les nouvelles
    }

    public void ToutesMAJ()
    {
        MajQS();
        MajEngrais();
        MajQuantiteNourriture();
    }

    public bool DepotEngrais(int x, int y) //On met de l'engrais aux coordonnées d'une parcelle, retourne false si on en avait plus (si naturel)
    {
        if (engraisSelectionne == 0) //chimique
        {
            engraisParcelles[x, y] += 8;  //De l'engrais pendant huit jours
            EnvironnementManager.instance.qualiteSol -= 1f;
            EnvironnementManager.instance.qualiteEau -= 0.5f;
            return true;
        }
        else if (engraisDispo > 0)  //naturel
        {
            engraisParcelles[x, y] += 4;
            EnvironnementManager.instance.qualiteSol += 0.2f;
            engraisDispo -= 1;
            return true;
        }
        return false;
    }

    public void SortiePlantage()  //bouton vert
    {
        nbrePlantes = CalculeNbrePlantes();
        this.GetComponent<Recap>().MajMenuRecap(Labourage.nbreParcellesPlacees, Labourage.nbreParcellesPlacables, capaciteTravailUtilisee, capaciteTravail, nbrePlantes[0], nbrePlantes[1], nbrePlantes[2], nbrePlantes[3], nbrePlantes[5]);
        panelPlantage.SetActive(false);
        this.GetComponent<Planter>().enabled = false;
        animatorLivreActivite.SetTrigger("Return");
    }

    public void SortieEngrais()  //bouton vert
    {
        panelEngrais.SetActive(false);
        modeEngrais = false;
        this.GetComponent<Planter>().enabled = false;
        animatorLivreActivite.SetTrigger("Return");
    }
}