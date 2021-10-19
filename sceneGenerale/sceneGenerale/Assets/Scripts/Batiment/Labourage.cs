using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Labourage : MonoBehaviour
{
    public Camera MainCamera;

    public GameObject zoneBleuePf;   //prefab de la zone bleue de prévisualisation d'un champ
    public GameObject zoneVertePf;   //prefab de la zone verte d'un champ labouré
    private Vector3 sizeParcelle;     //taille de ces prefabs

    private int xNbrParcelles;
    private int yNbrParcelles;
    public bool[,] parcellesLabourees;    //arrays qui indiquent les positions des parcelles labourées et celle adjacentes disponibles autour de la ferme
    public bool[,] parcellesAdjacentes;

    public Transform fermeTransform;       //Transform de la ferme
    public Transform parcelleContainer;   //Gameobject qui contient les instances des parcelles

    public void Start()
    {
        MainCamera = GameObject.Find("Camera").GetComponent<Camera>();

        xNbrParcelles = 7;
        yNbrParcelles = 5;

        parcellesLabourees = new bool[xNbrParcelles, yNbrParcelles];
        parcellesAdjacentes = new bool[xNbrParcelles, yNbrParcelles];

        //on a la ferme à la place de la parcelle du milieu et on la considere comme une parcelle labourée
        Labourer((xNbrParcelles - 1) / 2, (yNbrParcelles - 1) / 2);

        //On place parcelleContainer à l'origine de l'endroit à partir duquel seront placées les parcelles
        sizeParcelle = zoneBleuePf.GetComponent<Renderer>().bounds.size;
        sizeParcelle.y = 0f;
        parcelleContainer.position = fermeTransform.position - (new Vector3(sizeParcelle.x * (xNbrParcelles - 1) / 2, 1.14f, sizeParcelle.z * (yNbrParcelles - 1) / 2 - 1.5f));
    }

    public void UpdateZonePreview()    //déplace le prefab de la zone de prévisualisation à la position du pointeur
    {
        zoneVertePf.transform.position = TouchToPos();
    }

    public Vector3 TouchToPos()  //Retourne la coordonnée du premier objet rencontré par un rayon qui part de l'endroit où on touche l'écran
    {
        //on récupère la position du toucher de l'utilisateur sur l'écran
        Vector2 touchPosition = Input.mousePosition;

        //on transforme cette position en rayon perpendiculaire au plan de la camera
        Ray ray = MainCamera.ScreenPointToRay(new Vector3(touchPosition.x, touchPosition.y, 0f));
        RaycastHit hit;
        Vector3 newTargetPos = new Vector3();
        //si ce rayon rencontre un obstable on récupère la position de l'impact
        if (Physics.Raycast(ray, out hit))
        {
            newTargetPos = hit.point;
        }
        return newTargetPos;
    }

    public void Labourer(int x, int y) //on laboure une des parcelles
    {
        parcellesLabourees[x, y] = true;
        parcellesAdjacentes[x, y] = false;

        //on met à jour les parcelles dispo autour (4 cas)
        if ((x > 0) && !parcellesLabourees[x - 1, y]) parcellesAdjacentes[x - 1, y] = true;
        if ((x < 6) && !parcellesLabourees[x + 1, y]) parcellesAdjacentes[x + 1, y] = true;
        if ((y > 0) && !parcellesLabourees[x, y - 1]) parcellesAdjacentes[x, y - 1] = true;
        if ((y < 4) && !parcellesLabourees[x, y + 1]) parcellesAdjacentes[x, y + 1] = true;
    }

    public void MajPrefabsLabourage()
    {
        //On détruit d'abord les anciennes parcelles dns parcelleContainer
        foreach (Transform child in parcelleContainer)
        {
            Destroy(child.gameObject);
        }
        //Puis on affiche les nouvelles
        for (int i=0; i<xNbrParcelles; i++)
        {
            for (int j = 0; j< yNbrParcelles; j++)
            {
                if (parcellesAdjacentes[i, j])
                {
                    Instantiate(zoneBleuePf, parcelleContainer.position + new Vector3(i * sizeParcelle.x, 0f, j * sizeParcelle.z), Quaternion.identity, parcelleContainer);
                }
                if (parcellesLabourees[i, j])
                {
                    Instantiate(zoneVertePf, parcelleContainer.position + new Vector3(i * sizeParcelle.x, 0f, j * sizeParcelle.z), Quaternion.identity, parcelleContainer);
                }
            }
        }
    }
}
