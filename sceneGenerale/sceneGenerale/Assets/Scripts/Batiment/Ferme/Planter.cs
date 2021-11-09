﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planter : MonoBehaviour
{
    private Camera MainCamera;

    public static int capaciteTravail;
    private int capaciteTravailUtilisee;
    public static int culture;    //nombre de culture dispo pour le jouer (pas encore implémenté)

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

    //array qui contient les capacités de travail demandés par les cultures, puis les quantités de nourritures produites avec engrais
    public int[] arrayCT = new int[] { 10, 12, 15, 17, 20, 20, 0};   //on met un 0 à la fin pour que quand planteSelectionee == -1, ça update la CT de 0 en appellant arrayCT[-1]
    public int[] arrayQN_max = new int[] { 0, 2, 4, 5, 7, 7 };

    public int[,] cultureParcelles;    //array qui contient les numéros (correspondant à un enum de Culture et à la quantité de nourriture générée par cette culture) de chaque culture présente sur chaque parcdelle
                                       //de base on va le remplir de -1 quand il n'y a pas de culture
    public GameObject[,] arrayPrefabsPlantes;  //array qui contient les prefabs des plantes qui sont instanciées pour pouvoir les enlever
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

    public int engraisDispo;         //nombre d'engrais naturels
    private int planteSelectionnee;  //de base aucune, donc -1
    public Slider slider;

    public GameObject panelPlantage;

    void Start()
    {
        MainCamera = GameObject.Find("Camera").GetComponent<Camera>();

        capaciteTravail = 50;
        capaciteTravailUtilisee = 0;
        culture = 1;

        xNbrParcelles = Agri.xNbrParcelles;
        yNbrParcelles = Agri.yNbrParcelles;

        //initialisation de cultureParcelles
        cultureParcelles = new int[xNbrParcelles, yNbrParcelles];
        for (int i = 0; i < xNbrParcelles * yNbrParcelles; i++) cultureParcelles[i % xNbrParcelles, i / xNbrParcelles] = -1;
        arrayPrefabsPlantes = new GameObject[xNbrParcelles, yNbrParcelles];
        engraisParcelles = new int[xNbrParcelles, yNbrParcelles];

        engraisDispo = 5;
        planteSelectionnee = -1;

        sizeParcelle = zoneBleuePf.GetComponent<Renderer>().bounds.size;
        sizeParcelle.y = 0f;
        planteContainer.position = fermeTransform.position - (new Vector3(sizeParcelle.x * (xNbrParcelles - 1) / 2, 1.14f, sizeParcelle.z * (yNbrParcelles - 1) / 2 - 1.5f));
    }

    void Update()
    {
        slider.maxValue = capaciteTravail;
        slider.value = capaciteTravailUtilisee;

        //On cherche à savoir si le joueur clique sur une parcelle (on pourrait remplacer tout ça par des fonctions trigger lorsqu'on touche les colliders des parcelles ou cultures)
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

                if (objetTouche.name.Length == 10)  //c'est l'indication que c'est une parcelle labourée finale, donc on peut planter une culture dessus
                {
                    int i = ToInt(objetTouche.name[0]);
                    int j = ToInt(objetTouche.name[1]);
                    if (cultureParcelles[i,j] == -1)  //si il n'y a pas de culture sur la parcelle
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

    private int ToInt(char c)
    {
        return (int)(c - '0');
    }

    public void PlanterCulture(int x, int y) //Ajoute un prefab de la plante sélectionnée sur une certaine parcelle
    {
        GameObject plante;
        float taillePlante = 0.2f;
        if (planteSelectionnee == -1) Debug.Log("Aucune plante sélectionnée");
        else if (planteSelectionnee == 0)
        {
            if (capaciteTravailUtilisee + arrayCT[planteSelectionnee] <= capaciteTravail) {
                plante = Instantiate(blePf, planteContainer.position + new Vector3((x - taillePlante) * sizeParcelle.x, 0f, (y - taillePlante) * sizeParcelle.z), Quaternion.identity, planteContainer);
                plante.name = Enum.GetName(typeof(Culture), planteSelectionnee);
                GameManager.environnementManager.qualiteSol -= 1;
                arrayPrefabsPlantes[x, y] = plante;
            }
        }
        else if (planteSelectionnee == 1)
        {
            if (capaciteTravailUtilisee + arrayCT[planteSelectionnee] <= capaciteTravail)
            {
                plante = Instantiate(maisPf, planteContainer.position + new Vector3((x - taillePlante) * sizeParcelle.x, 0f, (y - taillePlante) * sizeParcelle.z), Quaternion.identity, planteContainer);
                plante.name = Enum.GetName(typeof(Culture), planteSelectionnee);
                GameManager.environnementManager.qualiteSol -= 1;
                arrayPrefabsPlantes[x, y] = plante;
            }
        }
        else if (planteSelectionnee == 2)
        {
            if (capaciteTravailUtilisee + arrayCT[planteSelectionnee] <= capaciteTravail)
            {
                plante = Instantiate(saladePf, planteContainer.position + new Vector3((x - taillePlante) * sizeParcelle.x, 0f, (y - taillePlante) * sizeParcelle.z), Quaternion.identity, planteContainer);
                plante.name = Enum.GetName(typeof(Culture), planteSelectionnee);
                GameManager.environnementManager.qualiteSol -= 1;
                arrayPrefabsPlantes[x, y] = plante;
            }
        }
        else if (planteSelectionnee == 3)
        {
            if (capaciteTravailUtilisee + arrayCT[planteSelectionnee] <= capaciteTravail)
            {
                plante = Instantiate(tomatePf, planteContainer.position + new Vector3((x - taillePlante) * sizeParcelle.x, 0f, (y - taillePlante) * sizeParcelle.z), Quaternion.identity, planteContainer);
                plante.name = Enum.GetName(typeof(Culture), planteSelectionnee);
                GameManager.environnementManager.qualiteSol -= 1;
                arrayPrefabsPlantes[x, y] = plante;
            }
        }
        else if (planteSelectionnee == 5)
        {
            if (capaciteTravailUtilisee + arrayCT[planteSelectionnee] <= capaciteTravail)
            {
                plante = Instantiate(raisinPf, planteContainer.position + new Vector3((x - taillePlante) * sizeParcelle.x, 0f, (y - taillePlante) * sizeParcelle.z), Quaternion.identity, planteContainer);
                plante.name = Enum.GetName(typeof(Culture), planteSelectionnee);
                GameManager.environnementManager.qualiteSol -= 1;
                arrayPrefabsPlantes[x, y] = plante;
            }
        }
        MajCT();
        cultureParcelles[x, y] = planteSelectionnee;
    }

    public void EnleverCulture(int x, int y)    //On enlève la plante sélectionnée d'une certaine parcelle
    {
        //On enleve la plante de l'array cultureParcelles
        cultureParcelles[x, y] = -1;
        //On détruit ensuite le prefab de la plante dans planteContainer
        Destroy(arrayPrefabsPlantes[x, y]);
        capaciteTravailUtilisee -= arrayCT[planteSelectionnee];  //on met à jour la capacité de travail
        planteSelectionnee = -1;
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

    public void MajQuantiteNourriture()  //Fonction qui met à jour la quantité de nourriture tous les jours et qui met paille et blé produite dans le coffre,  on met pas encore à jour la variété
    {
        int q; //quantité nourriture sur les parcelle
        for (int i = 0; i < xNbrParcelles; i++)
        {
            for (int j = 0; j < yNbrParcelles; j++)
            {
                q = cultureParcelles[i, j];

                if (q == (int)Culture.Ble)
                {
                    //coder un truc pour mettre du blé et de la paille dans le coffre
                }

                // On regarde ensuite si il y a de l'engrais 
                else if (engraisParcelles[i,j] > 0)
                {
                    GameManager.socialManager.quantiteNourriture += arrayQN_max[q];
                }

                else // Sinon on met les autres valeurs en prenant compte la pluriculture
                {
                    if (q == (int)Culture.Ble)   //Blé sur parcelle
                    {
                        GameManager.socialManager.quantiteNourriture += q;
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
                            GameManager.socialManager.quantiteNourriture += q + 1;
                        }
                        else GameManager.socialManager.quantiteNourriture += q;
                    }
                    else if (((int)Culture.Tomate <= q) && (q <= (int)Culture.Raisin))  //Tomate ou Raisin
                    {
                        int n = 0; //on compte le nombre de cultures qu'il y a sur les parcelles autour
                        if ((i > 0) && (cultureParcelles[i - 1, j] > -1)) n++;
                        else if ((i < xNbrParcelles - 1) && (cultureParcelles[i + 1, j] > -1)) n++;
                        else if ((j > 0) && (cultureParcelles[i, j - 1] > -1)) n++;
                        else if ((j < yNbrParcelles - 1) && (cultureParcelles[i, j + 1] > -1)) n++;

                        if (n > 1) GameManager.socialManager.quantiteNourriture = q + 1;
                        else GameManager.socialManager.quantiteNourriture = q;
                    }
                }
            }
        }
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

    public void MajQS()   //La qualité du sol augmente tous les jours de 0.1 à minuit
    {
        GameManager.environnementManager.qualiteSol += 0.1f;
    }

    public void DepotEngraisChimique(int x, int y)    //On met de l'engrais chimique au coordonnées d'une certains parcelle
    {
        engraisParcelles[x, y] += 8;  //De l'engrais pendant huit jours
        GameManager.environnementManager.qualiteSol -= 0.5f;
        GameManager.environnementManager.qualiteEau -= 0.5f;
    }

    public bool DepotEngraisNaturel(int x, int y)   //On met de l'engrais naturel aux coordonnées d'une parcelle, retourne false si on en avait plus
    {
        if (engraisDispo > 0)
        {
            engraisParcelles[x, y] += 4;
            GameManager.environnementManager.qualiteSol += 0.2f;
            engraisDispo -= 1;
            return true;
        }
        return false;
    }

    public void SortiePlantage()  //bouton vert
    {
        panelPlantage.SetActive(false);
        this.GetComponent<Planter>().enabled = false;
    }
}
