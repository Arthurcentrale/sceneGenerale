using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Labourage : MonoBehaviour
{
    public Camera MainCamera;

    public GameObject zoneBleuePf;   //prefab de la zone bleue de prévisualisation d'un champ
    public GameObject zoneVertePf;   //prefab de la zone verte de prévisualisation d'un champ labouré
    public GameObject parcelleLaboureePf;   //prefab d'une parcelle placée et confirmée
    private Vector3 sizeParcelle;     //taille de ces prefabs
    public float bordure;             //taile de la bordure qui sépare les champs

    public Transform fermeTransform;       //Transform de la ferme
    private Vector2 sizeFerme;             //Sa taille
    private Animator animatorLivreActivite;

    //on récupère ces variables dans Agri
    private int xNbrParcelles;
    private int yNbrParcelles;

    public bool[,] parcellesLabourees;    //arrays qui indiquent les positions des parcelles labourées et celle adjacentes disponibles autour de la ferme
    public bool[,] parcellesAdjacentes;

    public static int nbreParcellesPlacables;    
    public static int nbreParcellesPlacees;

    public Transform parcelleContainer;   //Gameobject qui contient les instances des parcelles

    public GameObject panelLabourage;
    private Text nbreParcelles;

    public void Start()
    {
        animatorLivreActivite = GameObject.Find("LivreFerme").GetComponent<Animator>();

        MainCamera = GameObject.Find("Camera").GetComponent<Camera>();

        xNbrParcelles = Agri.xNbrParcelles;
        yNbrParcelles = Agri.yNbrParcelles;


        nbreParcelles = panelLabourage.transform.GetChild(0).gameObject.GetComponent<Text>();

        InitialiseParcelles();

        //On place parcelleContainer à l'origine de l'endroit à partir duquel seront placées les parcelles
        sizeParcelle = zoneBleuePf.GetComponent<Renderer>().bounds.size;
        sizeParcelle.y = 0f;
        bordure = 2f;
        sizeParcelle += new Vector3(bordure, 0f, bordure);
        sizeFerme = fermeTransform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().size;
        //parcelleContainer.position = fermeTransform.position - (new Vector3((sizeParcelle.x * xNbrParcelles / 2) - sizeFerme.x, 4.40f, (sizeParcelle.z * yNbrParcelles / 2) - sizeFerme.y));
        parcelleContainer.position = fermeTransform.position - (new Vector3(sizeParcelle.x * (xNbrParcelles - 1) / 2, 4.40f, sizeParcelle.z * (yNbrParcelles - 1) / 2));

        //parcellesAdjacentes[(int)(xNbrParcelles - sizeFerme.x / sizeParcelle.x) / 2 - 1, (int)(yNbrParcelles - sizeFerme.y / sizeParcelle.z) / 2] = true;
        //parcellesAdjacentes[(xNbrParcelles - 1) / 2 - 1, (yNbrParcelles - 1) / 2] = true;

        MajPrefabsLabourage();
    }

    void Update()
    {
        //On cherche à savoir si le joueur clique sur une parcelle non-labourée
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

                if (objetTouche.tag == "ParcelleBleue")  //c'est l'indication que c'est une parcelle bleue, donc on la laboure
                {
                    int i = ToInt(objetTouche.name[0]);
                    int j = ToInt(objetTouche.name[1]);
                    Labourer(i, j);
                    MajPrefabsLabourage();
                }
                else if (objetTouche.tag == "ParcelleVerte") //c'est une parcelle verte donc on la déselectionne
                {
                    int i = ToInt(objetTouche.name[0]);
                    int j = ToInt(objetTouche.name[1]);
                    Delabourer(i, j);
                    MajPrefabsLabourage();
                }
            }
        }
    }

    private void InitialiseParcelles() //permet d'initialiser les tableaux de parcelles, dans Start() ou quand on ferme le menu sans valider
    {
        parcellesLabourees = new bool[xNbrParcelles, yNbrParcelles];
        parcellesAdjacentes = new bool[xNbrParcelles, yNbrParcelles];

        //on a la ferme à la place de la parcelle du milieu et on la considere comme une parcelle labourée
        Labourer((xNbrParcelles - 1) / 2, (yNbrParcelles - 1) / 2);
    }

    private int ToInt(char c)
    {
        return (int)(c - '0');
    }

    private void Labourer(int x, int y) //on laboure une des parcelles
    {
        if (nbreParcellesPlacees < nbreParcellesPlacables)
        {
            nbreParcellesPlacees += 1;

            parcellesLabourees[x, y] = true;
            parcellesAdjacentes[x, y] = false;

            //on met à jour les parcelles dispo autour (4 cas) (elle ne deviennent pas dispo si elles sont déjà labourées
            if ((x > 0) && !parcellesLabourees[x - 1, y]) parcellesAdjacentes[x - 1, y] = true;
            if ((x < xNbrParcelles - 1) && !parcellesLabourees[x + 1, y]) parcellesAdjacentes[x + 1, y] = true;
            if ((y > 0) && !parcellesLabourees[x, y - 1]) parcellesAdjacentes[x, y - 1] = true;
            if ((y < yNbrParcelles - 1) && !parcellesLabourees[x, y + 1]) parcellesAdjacentes[x, y + 1] = true;
            

            EnvironnementManager.instance.qualiteSol -= 0.2f;

            nbreParcelles.text = nbreParcellesPlacees.ToString() + "/" + nbreParcellesPlacables.ToString();
        }
        else Debug.Log("Le nombre max de parcelles a été atteint pour le moment");
    }

    private void Delabourer(int x, int y)
    {
        nbreParcellesPlacees -= 1;

        parcellesLabourees[x, y] = false;
        parcellesAdjacentes[x, y] = true;

        //Pour toutes les parcelles adjacentes autour, on va regarder si elle sont adjacentes avec une autre parcelle labourée pour voir si on les garde
        if ((x > 0) && !CheckParcelleLaboureeAdjacente(x - 1, y)) parcellesAdjacentes[x - 1, y] = false;
        if ((x < xNbrParcelles - 1) && !CheckParcelleLaboureeAdjacente(x + 1, y)) parcellesAdjacentes[x + 1, y] = false;
        if ((y > 0) && !CheckParcelleLaboureeAdjacente(x, y - 1)) parcellesAdjacentes[x, y - 1] = false;
        if ((y < yNbrParcelles - 1) && !CheckParcelleLaboureeAdjacente(x, y + 1)) parcellesAdjacentes[x, y + 1] = false;

        nbreParcelles.text = nbreParcellesPlacees.ToString() + "/" + nbreParcellesPlacables.ToString();
    }

    private bool CheckParcelleLaboureeAdjacente(int x, int y)  //Pour une parcelle adjacente, on regarde si on a bien une parcelle labouree autour pour voir si on la garde
    {
        if ((x > 0) && parcellesLabourees[x - 1, y]) return true;
        else if ((x < xNbrParcelles - 1) && parcellesLabourees[x + 1, y]) return true;
        else if ((y > 0) && parcellesLabourees[x, y - 1]) return true;
        else if ((y < yNbrParcelles - 1) && parcellesLabourees[x, y + 1]) return true;
        else return false;
    }

    public void MajPrefabsLabourage()
    {
        //On détruit d'abord les anciennes parcelles dns parcelleContainer
        foreach (Transform child in parcelleContainer)
        {
            Destroy(child.gameObject);
        }
        //Puis on affiche les nouvelles
        GameObject parc;

        for (int i=0; i<xNbrParcelles; i++)
        {
            for (int j = 0; j< yNbrParcelles; j++)
            {
                if (parcellesAdjacentes[i, j])
                {
                    parc = (GameObject) Instantiate(zoneBleuePf, parcelleContainer.position + new Vector3(i * sizeParcelle.x, 0f, j * sizeParcelle.z), Quaternion.identity, parcelleContainer);
                    parc.name = i.ToString() + j.ToString() + "bleue";
                }
                if (parcellesLabourees[i, j])
                {
                    parc = Instantiate(zoneVertePf, parcelleContainer.position + new Vector3(i * sizeParcelle.x, 0f, j * sizeParcelle.z), Quaternion.identity, parcelleContainer);
                    parc.name = i.ToString() + j.ToString() + "verte";
                }
            }
        }

        /*
        float X, Y;
        for (int i = 0; i < xNbrParcelles; i++)
        {
            for (int j = 0; j < yNbrParcelles; j++)
            {
                if ((i <= (xNbrParcelles - sizeFerme.x / sizeParcelle.x) / 2) || (j <= (yNbrParcelles - sizeFerme.y / sizeParcelle.z) / 2))
                {
                    (X, Y) = (i * sizeParcelle.x, j * sizeParcelle.z);
                }
                else if ((i > (xNbrParcelles - sizeFerme.x / sizeParcelle.x) / 2) && (j > (yNbrParcelles - sizeFerme.y / sizeParcelle.z) / 2))  
                {
                    (X, Y) = (i * sizeParcelle.x + sizeFerme.x, j * sizeParcelle.z + sizeFerme.y);
                }
                else if ((i > (xNbrParcelles - sizeFerme.x / sizeParcelle.x) / 2) && (j > (yNbrParcelles - sizeFerme.y / sizeParcelle.z) / 2))
                {
                    (X, Y) = (i * sizeParcelle.x + sizeFerme.x, j * sizeParcelle.z);
                }
                else
                {
                    (X, Y) = (i * sizeParcelle.x, j * sizeParcelle.z + sizeFerme.y);
                }

                if (parcellesAdjacentes[i, j])
                {
                    parc = (GameObject)Instantiate(zoneBleuePf, parcelleContainer.position + new Vector3(X, 0f, Y), Quaternion.identity, parcelleContainer);
                    parc.name = i.ToString() + j.ToString();
                }
                if (parcellesLabourees[i, j])
                {
                    parc = Instantiate(zoneVertePf, parcelleContainer.position + new Vector3(X, 0f, Y), Quaternion.identity, parcelleContainer);
                    parc.name = i.ToString() + j.ToString() + "verte";
                }
            }
        }
        */
    }

    public void SortieLabourageAvecValidation()    //bouton vert
    {

        animatorLivreActivite.SetTrigger("Return");
        //On détruit d'abord les anciennes parcelles dans parcelleContainer
        foreach (Transform child in parcelleContainer)
        {
            Destroy(child.gameObject);
        }
        //Puis on affiche les nouvelles
        GameObject parc;
        for (int i = 0; i < xNbrParcelles; i++)
        {
            for (int j = 0; j < yNbrParcelles; j++)
            {
                if (parcellesLabourees[i, j])
                {
                    parc = Instantiate(parcelleLaboureePf, parcelleContainer.position + new Vector3(i * sizeParcelle.x, 0f, j * sizeParcelle.z), Quaternion.identity, parcelleContainer);
                    parc.name = i.ToString() + j.ToString() + "labouree";
                }
            }
        }
        //On cache l'UI et on re-désactive le script
        panelLabourage.SetActive(false);
        this.GetComponent<Labourage>().enabled = false;
        this.GetComponent<Agri>().menuOuvert = false;

    }

    public void SortieLabourageSansValidation()     //bouton rouge
    {
        animatorLivreActivite.SetTrigger("Return");

        //On détruit toutes les prefabs de parcelles
        foreach (Transform child in parcelleContainer)
        {
            Destroy(child.gameObject);
        }
        //Reset des tableaux parcelles
        InitialiseParcelles();
        //Puis on cache l'UI et on re-désactive le script
        panelLabourage.SetActive(false);
        this.GetComponent<Labourage>().enabled = false;
        this.GetComponent<Agri>().menuOuvert = false;
        this.GetComponent<Ferme>().open = false;
    }
}
