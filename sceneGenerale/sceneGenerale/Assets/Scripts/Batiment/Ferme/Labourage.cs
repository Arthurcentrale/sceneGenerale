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

    //on récupère ces variables dans Agri
    private int xNbrParcelles;
    private int yNbrParcelles;

    public bool[,] parcellesLabourees;    //arrays qui indiquent les positions des parcelles labourées et celle adjacentes disponibles autour de la ferme
    public bool[,] parcellesAdjacentes;

    public static int nbreParcellesPlacables;

    public Transform fermeTransform;       //Transform de la ferme
    public Transform parcelleContainer;   //Gameobject qui contient les instances des parcelles

    public GameObject panelLabourage;

    public void Start()
    {
        MainCamera = GameObject.Find("Camera").GetComponent<Camera>();

        xNbrParcelles = Agri.xNbrParcelles;
        yNbrParcelles = Agri.yNbrParcelles;

        nbreParcellesPlacables = 5;

        parcellesLabourees = new bool[xNbrParcelles, yNbrParcelles];
        parcellesAdjacentes = new bool[xNbrParcelles, yNbrParcelles];

        //on a la ferme à la place de la parcelle du milieu et on la considere comme une parcelle labourée
        Labourer((xNbrParcelles - 1) / 2, (yNbrParcelles - 1) / 2);

        //On place parcelleContainer à l'origine de l'endroit à partir duquel seront placées les parcelles
        sizeParcelle = zoneBleuePf.GetComponent<Renderer>().bounds.size;
        sizeParcelle.y = 0f;
        parcelleContainer.position = fermeTransform.position - (new Vector3(sizeParcelle.x * (xNbrParcelles - 1) / 2, 4.40f, sizeParcelle.z * (yNbrParcelles - 1) / 2 - 1.5f));
    }

    public void Update()
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

                if (objetTouche.name.Length == 2)  //c'est l'indication que c'est une parcelle bleue, donc on la laboure
                {
                    int i = ToInt(objetTouche.name[0]);
                    int j = ToInt(objetTouche.name[1]);
                    Labourer(i, j);
                }
            }
        }
    }

    private int ToInt(char c)
    {
        return (int)(c - '0');
    }

    public void Labourer(int x, int y) //on laboure une des parcelles
    {
        parcellesLabourees[x, y] = true;
        parcellesAdjacentes[x, y] = false;

        //on met à jour les parcelles dispo autour (4 cas) (elle ne deviennent pas dispo si elles sont déjà labourées
        if ((x > 0) && !parcellesLabourees[x - 1, y])                  parcellesAdjacentes[x - 1, y] = true;
        if ((x < xNbrParcelles - 1) && !parcellesLabourees[x + 1, y])  parcellesAdjacentes[x + 1, y] = true;
        if ((y > 0) && !parcellesLabourees[x, y - 1])                  parcellesAdjacentes[x, y - 1] = true;
        if ((y < yNbrParcelles - 1) && !parcellesLabourees[x, y + 1])  parcellesAdjacentes[x, y + 1] = true;
        MajPrefabsLabourage();
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
                    parc.name = i.ToString() + j.ToString();
                }
                if (parcellesLabourees[i, j])
                {
                    parc = Instantiate(zoneVertePf, parcelleContainer.position + new Vector3(i * sizeParcelle.x, 0f, j * sizeParcelle.z), Quaternion.identity, parcelleContainer);
                    parc.name = i.ToString() + j.ToString() + "verte";
                }
            }
        }
    }

    public void SortieLabourageAvecValidation()    //bouton vert
    {
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
    }

    public void SortieLabourageSansValidation()     //bouton rouge
    {
        //On détruit toutes les parcelles
        foreach (Transform child in parcelleContainer)
        {
            Destroy(child.gameObject);
        }
        //Puis on cache l'UI et on re-désactive le script
        panelLabourage.SetActive(false);
        this.GetComponent<Labourage>().enabled = false;
    }
}
