﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planter : MonoBehaviour
{
    private Camera MainCamera;

    public static int capaciteTravail;
    public static int culture;

    //types de cultures que l'on fait correspondre aux quantitées de nourriture qu'ils génèrent
    enum Culture
    {
        Ble = 0,
        Mais = 1,
        Salade = 2,
        Tomate = 3,
        Raisin = 5
    }

    //capacités de travail demandées par types de cultures
    public const int bleCT = 10;
    public const int maisCT = 12;
    public const int saladeCT = 15;
    public const int tomateCT = 17;
    public const int raisinCT = 20;

    public int quantiteNourriture;
    public int[,] cultureParcelles;    //array qui contient les numéros (correspondant à un enum de Culture et à la quantité de nourriture générée par cette culture) de chaque culture présente sur chaque parcdelle
                                       //de base on va le remplir de -1 quand il n'y a pas de culture
    public int[,] engraisParcelles;     //array qui contient le nombre de jours restants durant lesquels un engrais va continuer à agir, la valeur est donc à 0 par defaut

    //on récupère ces variables dans Agri
    private int xNbrParcelles;
    private int yNbrParcelles;

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

    public int engraisDispo;
    private int planteSelectionnee; //de base aucune, donc -1

    void Start()
    {
        MainCamera = GameObject.Find("Camera").GetComponent<Camera>();

        capaciteTravail = 50;
        culture = 1;

        quantiteNourriture = 0;

        xNbrParcelles = Agri.xNbrParcelles;
        yNbrParcelles = Agri.yNbrParcelles;

        //initialisation de cultureParcelles
        cultureParcelles = new int[xNbrParcelles, yNbrParcelles];
        for (int i = 0; i < xNbrParcelles * yNbrParcelles; i++) cultureParcelles[i % xNbrParcelles, i / xNbrParcelles] = -1;
        engraisParcelles = new int[xNbrParcelles, yNbrParcelles];

        engraisDispo = 5;
        planteSelectionnee = -1;

        sizeParcelle = zoneBleuePf.GetComponent<Renderer>().bounds.size;
        sizeParcelle.y = 0f;
        planteContainer.position = fermeTransform.position - (new Vector3(sizeParcelle.x * (xNbrParcelles - 1) / 2, 1.14f, sizeParcelle.z * (yNbrParcelles - 1) / 2 - 1.5f));
    }

    void Update()
    {
        //On cherche à savoir si le joueur clique sur une parcelle
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

                if (objetTouche.name.Length == 10)  //c'est l'indication que c'est une parcelle labourée finale, donc on peut planter une culture dessus
                {
                    int i = ToInt(objetTouche.name[0]);
                    int j = ToInt(objetTouche.name[1]);
                    PlanterCulture(i, j);
                }
            }
        }
    }

    private int ToInt(char c)
    {
        return (int)(c - '0');
    }

    public void PlanterCulture(int x, int y) //plante la plante sélectionnée sur une certaine parcelle
    {
        GameObject plante;
        if (planteSelectionnee == -1) Debug.Log("Aucune plante sélectionnée");
        else if (planteSelectionnee == 0)
        {
            plante = Instantiate(blePf, planteContainer.position + new Vector3((x + 0.4f) * sizeParcelle.x, 0f, (y + 0.4f) * sizeParcelle.z), Quaternion.identity, planteContainer);
            plante.name = Enum.GetName(typeof(Culture), planteSelectionnee);
        }
        else if (planteSelectionnee == 1)
        {
            plante = Instantiate(maisPf, planteContainer.position + new Vector3((x + 0.4f) * sizeParcelle.x, 0f, (y + 0.4f) * sizeParcelle.z), Quaternion.identity, planteContainer);
            plante.name = Enum.GetName(typeof(Culture), planteSelectionnee);
        }
        else if (planteSelectionnee == 2)
        {
            plante = Instantiate(saladePf, planteContainer.position + new Vector3((x + 0.4f) * sizeParcelle.x, 0f, (y + 0.4f) * sizeParcelle.z), Quaternion.identity, planteContainer);
            plante.name = Enum.GetName(typeof(Culture), planteSelectionnee);
        }
        else if (planteSelectionnee == 3)
        {
            plante = Instantiate(tomatePf, planteContainer.position + new Vector3((x + 0.4f) * sizeParcelle.x, 0f, (y + 0.4f) * sizeParcelle.z), Quaternion.identity, planteContainer);
            plante.name = Enum.GetName(typeof(Culture), planteSelectionnee);
        }
        else if (planteSelectionnee == 5)
        {
            plante = Instantiate(raisinPf, planteContainer.position + new Vector3((x + 0.4f) * sizeParcelle.x, 0f, (y + 0.4f) * sizeParcelle.z), Quaternion.identity, planteContainer);
            plante.name = Enum.GetName(typeof(Culture), planteSelectionnee);
        }
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

    public void MajQuantiteNourriture()  //avec prise en compte pluriculture, pas encore engrais ni qualité de sols
    {
        int q; //quantité nourriture sur les parcelle
        for (int i = 0; i < xNbrParcelles; i++)
        {
            for (int j = 0; j < yNbrParcelles; j++)
            {
                q = cultureParcelles[i, j];
                if (q == (int)Culture.Ble)   //Blé sur parcelle
                {              
                    quantiteNourriture += q;
                }
                else if (((int)Culture.Mais <= q) && (q <= (int)Culture.Salade))  //Mais ou Salade..
                {
                    //on check les 4 parcelles autour si il y a au moins une culture
                    if (
                       ((i > 0) && (cultureParcelles[i - 1, j] > -1))
                    || ((i < xNbrParcelles - 1) && (cultureParcelles[i + 1, j] > -1))
                    || ((j > 0) && (cultureParcelles[i, j - 1] > -1))
                    || ((j < yNbrParcelles - 1) && (cultureParcelles[i, j + 1] > -1))
                       )
                    {
                        quantiteNourriture += q + 1;
                    }
                    else quantiteNourriture += q;
                }
                else if (((int)Culture.Tomate <= q) && (q <= (int)Culture.Raisin))  //Tomate ou Raisin
                {
                    int n = 0; //on compte le nombre de cultures qu'il y a sur les parcelles autour
                    if ((i > 0) && (cultureParcelles[i - 1, j] > -1)) n++;
                    else if ((i < xNbrParcelles - 1) && (cultureParcelles[i + 1, j] > -1)) n++;
                    else if ((j > 0) && (cultureParcelles[i, j - 1] > -1)) n++;
                    else if ((j < yNbrParcelles - 1) && (cultureParcelles[i, j + 1] > -1)) n++;

                    if (n > 1) quantiteNourriture = q + 1;
                    else quantiteNourriture = q;
                }
            }
        }
    }
}
