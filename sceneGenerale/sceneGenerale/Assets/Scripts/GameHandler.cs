using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    // Arbres
    public GameObject pin;
    public GameObject douglas;
    public GameObject chene;
    public GameObject hetre;
    public GameObject bouleau;

    public GameObject pinFrele;
    public GameObject douglasFrele;
    public GameObject cheneFrele;
    public GameObject hetreFrele;
    public GameObject bouleauFrele;

    public GameObject pinMalade;
    public GameObject douglasMalade;
    public GameObject cheneMalade;
    public GameObject hetreMalade;
    public GameObject bouleauMalade;

    public GameObject pinArbuste;
    public GameObject douglasArbuste;
    public GameObject cheneArbuste;
    public GameObject hetreArbuste;
    public GameObject bouleauArbuste;

    public GameObject pinArbusteMalade;
    public GameObject douglasArbusteMalade;
    public GameObject cheneArbusteMalade;
    public GameObject hetreArbusteMalade;
    public GameObject bouleauArbusteMalade;

    public GameObject pinSouche;
    public GameObject douglasSouche;
    public GameObject cheneSouche;
    public GameObject hetreSouche;
    public GameObject bouleauSouche;

    public GameObject pinSoucheMalade;
    public GameObject douglasSoucheMalade;
    public GameObject cheneSoucheMalade;
    public GameObject hetreSoucheMalade;
    public GameObject bouleauSoucheMalade;


    // Rochers
    public GameObject Rocher1;
    public GameObject Rocher2;
    public GameObject Rocher3;
    public GameObject Rocher4;
    public GameObject Rocher5;
    public GameObject Rocher6;
    public GameObject Rocher7;

    public List<GameObject> listeCompleteBatiments = new List<GameObject>(24);

    public Inventory inventory;
    public Player player;
    [SerializeField] public UI_Inventory uiInventory;

    // Dossiers
    private Transform dossierArbres;
    private Transform dossierRochers;
    private Transform dossierBatiments;

    public float intervalleSauvegarde = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        dossierArbres = GameObject.Find("Arbres").transform;
        dossierRochers = GameObject.Find("Rocher").transform;
        dossierBatiments = GameObject.Find("Batiments").transform;

        //fillListeCompleteBatiments();

        Load();
        treeLayersMag.updateTreeLayers();
        StartCoroutine(SaveGame());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SaveGame()
    {
        Debug.Log("Sauvegarde");
        yield return new WaitForSeconds(intervalleSauvegarde);
        Save();
        StartCoroutine(SaveGame());
    }

    // Enregistre les données sur le fichier sauvegarde.txt
    private void Save()
    {
        /** ------------------ Sauvegarde des arbres ------------------ **/

        GameObject[] listeArbres = GameObject.FindGameObjectsWithTag("Arbre");

        // On créé chacune des listes de positions des arbres
        List<Vector3> listePositionsPins = new List<Vector3>();
        List<Vector3> listePositionsDouglas = new List<Vector3>();
        List<Vector3> listePositionsChenes = new List<Vector3>();
        List<Vector3> listePositionsHetres = new List<Vector3>();
        List<Vector3> listePositionsBouleaux = new List<Vector3>();

        List<Vector3> listePositionsPinsFrele = new List<Vector3>();
        List<Vector3> listePositionsDouglasFrele = new List<Vector3>();
        List<Vector3> listePositionsChenesFrele = new List<Vector3>();
        List<Vector3> listePositionsHetresFrele = new List<Vector3>();
        List<Vector3> listePositionsBouleauxFrele = new List<Vector3>();

        List<Vector3> listePositionsPinsMalade = new List<Vector3>();
        List<Vector3> listePositionsDouglasMalade = new List<Vector3>();
        List<Vector3> listePositionsChenesMalade = new List<Vector3>();
        List<Vector3> listePositionsHetresMalade = new List<Vector3>();
        List<Vector3> listePositionsBouleauxMalade = new List<Vector3>();

        List<Vector3> listePositionsPinsArbuste = new List<Vector3>();
        List<Vector3> listePositionsDouglasArbuste = new List<Vector3>();
        List<Vector3> listePositionsChenesArbuste = new List<Vector3>();
        List<Vector3> listePositionsHetresArbuste = new List<Vector3>();
        List<Vector3> listePositionsBouleauxArbuste = new List<Vector3>();

        List<Vector3> listePositionsPinsArbusteMalade = new List<Vector3>();
        List<Vector3> listePositionsDouglasArbusteMalade = new List<Vector3>();
        List<Vector3> listePositionsChenesArbusteMalade = new List<Vector3>();
        List<Vector3> listePositionsHetresArbusteMalade = new List<Vector3>();
        List<Vector3> listePositionsBouleauxArbusteMalade = new List<Vector3>();

        List<Vector3> listePositionsPinsSouche = new List<Vector3>();
        List<Vector3> listePositionsDouglasSouche = new List<Vector3>();
        List<Vector3> listePositionsChenesSouche = new List<Vector3>();
        List<Vector3> listePositionsHetresSouche = new List<Vector3>();
        List<Vector3> listePositionsBouleauxSouche = new List<Vector3>();

        List<Vector3> listePositionsPinsSoucheMalade = new List<Vector3>();
        List<Vector3> listePositionsDouglasSoucheMalade = new List<Vector3>();
        List<Vector3> listePositionsChenesSoucheMalade = new List<Vector3>();
        List<Vector3> listePositionsHetresSoucheMalade = new List<Vector3>();
        List<Vector3> listePositionsBouleauxSoucheMalade = new List<Vector3>();

        // On parcourt la liste des arbres (sous formes de GO) présent dans le jeu
        // Pour chacun d'entre eux, on créé une objet de la structure arbresJSON pouvant être transformé directement en JSON
        foreach (GameObject arbre in listeArbres)
        {
            string nomArbre = arbre.name;
            if (nomArbre.IndexOf("Pin", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (nomArbre.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
                    listePositionsPinsFrele.Add(arbre.transform.position);

                else if (nomArbre.IndexOf("ArbusteMalade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsPinsArbusteMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Arbuste", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsPinsArbuste.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsPinsMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("SoucheMalade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsPinsSoucheMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsPinsSouche.Add(arbre.transform.position);
                    continue;
                }

                else
                {
                    listePositionsPins.Add(arbre.transform.position);
                    continue;
                }
            }

            else if (nomArbre.IndexOf("Chene", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (nomArbre.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
                    listePositionsChenesFrele.Add(arbre.transform.position);

                else if (nomArbre.IndexOf("ArbusteMalade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsChenesArbusteMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Arbuste", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsChenesArbuste.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsChenesMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsChenesSouche.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("SoucheMalade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsChenesSoucheMalade.Add(arbre.transform.position);
                    continue;
                }

                else
                {
                    listePositionsChenes.Add(arbre.transform.position);
                    continue;
                }
            }


            else if (nomArbre.IndexOf("Hetre", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (nomArbre.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
                    listePositionsHetresFrele.Add(arbre.transform.position);

                else if (nomArbre.IndexOf("ArbusteMalade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsHetresArbusteMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Arbuste", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsHetresArbuste.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsHetresMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsHetresSouche.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("SoucheMalade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsHetresSoucheMalade.Add(arbre.transform.position);
                    continue;
                }

                else
                {
                    listePositionsHetres.Add(arbre.transform.position);
                    continue;
                }
            }

            else if (nomArbre.IndexOf("Bouleau", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (nomArbre.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
                    listePositionsBouleauxFrele.Add(arbre.transform.position);

                else if (nomArbre.IndexOf("ArbusteMalade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsBouleauxArbusteMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Arbuste", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsBouleauxArbuste.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsBouleauxMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsBouleauxSouche.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("SoucheMalade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsBouleauxSoucheMalade.Add(arbre.transform.position);
                    continue;
                }

                else
                {
                    listePositionsBouleaux.Add(arbre.transform.position);
                    continue;
                }
            }

            else if (nomArbre.IndexOf("Douglas", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (nomArbre.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
                    listePositionsDouglasFrele.Add(arbre.transform.position);

                else if (nomArbre.IndexOf("ArbusteMalade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsDouglasArbusteMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Arbuste", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsDouglasArbuste.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsDouglasMalade.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsDouglasSouche.Add(arbre.transform.position);
                    continue;
                }

                else if (nomArbre.IndexOf("SoucheMalade", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    listePositionsDouglasSoucheMalade.Add(arbre.transform.position);
                    continue;
                }

                else
                {
                    listePositionsDouglas.Add(arbre.transform.position);
                    continue;
                }
            }

            else
            {
                print("Problème save : le gameobjet a le tag \"Arbre\" mais son type n'est pas identifiable");
            }
        }

        /** ------------------ Sauvegarde des rochers ------------------ **/

        GameObject[] listeRochers = GameObject.FindGameObjectsWithTag("Rocher");

        // On créé chacune des listes de positions des arbres
        List<Vector3> listePositionsRochers1 = new List<Vector3>();
        List<Vector3> listePositionsRochers2 = new List<Vector3>();
        List<Vector3> listePositionsRochers3 = new List<Vector3>();
        List<Vector3> listePositionsRochers4 = new List<Vector3>();
        List<Vector3> listePositionsRochers5 = new List<Vector3>();
        List<Vector3> listePositionsRochers6 = new List<Vector3>();
        List<Vector3> listePositionsRochers7 = new List<Vector3>();

        foreach (GameObject rocher in listeRochers)
        {
            string nomRocher = rocher.name;
            if (nomRocher.IndexOf("Rocher1", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers1.Add(rocher.transform.position);

            else if (nomRocher.IndexOf("Rocher4", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers4.Add(rocher.transform.position);

            else if (nomRocher.IndexOf("Rocher7", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers7.Add(rocher.transform.position);

            else if (nomRocher.IndexOf("Rocher2", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers2.Add(rocher.transform.position);

            else if (nomRocher.IndexOf("Rocher5", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers5.Add(rocher.transform.position);

            else if (nomRocher.IndexOf("Rocher3", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers3.Add(rocher.transform.position);

            else if (nomRocher.IndexOf("Rocher6", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers6.Add(rocher.transform.position);

            else
                print("Problème sauvegarde : un objet portant le tag \"Rocher\" n'a pas de type de roche correspondant");
        }

        /** ------------------ Sauvegarde des batiments ------------------ **/

        // On recupere tous les elements dans le dossier des batiments                
        List<string> listeBatiments = new List<string>();
        List<Vector3> listePosBatiments = new List<Vector3>();

        for (int i = 0; i < dossierBatiments.childCount; i++)
        {
            Transform currentBat = dossierBatiments.GetChild(i);

            if (currentBat.name.IndexOf("Déplaçable", StringComparison.OrdinalIgnoreCase) >= 0)
                continue;

            listeBatiments.Add(currentBat.name);
            listePosBatiments.Add(currentBat.position);
        }

        /** ------------------ Sauvegarde des données de l'inventaire ------------------ **/

        List<ItemAmount> listeItems = inventory.GetItemList();
        List<bool> listeFavoris = inventory.GetFavList();

        // Objet que l'on transformera en JSON
        GameData gameData = new GameData(listePositionsPins,
                                         listePositionsDouglas,
                                         listePositionsChenes,
                                         listePositionsHetres,
                                         listePositionsBouleaux,
                                         listePositionsPinsFrele,
                                         listePositionsDouglasFrele,
                                         listePositionsChenesFrele,
                                         listePositionsHetresFrele,
                                         listePositionsBouleauxFrele,
                                         listePositionsPinsMalade,
                                         listePositionsDouglasMalade,
                                         listePositionsChenesMalade,
                                         listePositionsHetresMalade,
                                         listePositionsBouleauxMalade,
                                         listePositionsPinsArbuste,
                                         listePositionsDouglasArbuste,
                                         listePositionsChenesArbuste,
                                         listePositionsHetresArbuste,
                                         listePositionsBouleauxArbuste,
                                         listePositionsPinsArbusteMalade,
                                         listePositionsDouglasArbusteMalade,
                                         listePositionsChenesArbusteMalade,
                                         listePositionsHetresArbusteMalade,
                                         listePositionsBouleauxArbusteMalade,
                                         listePositionsPinsSouche,
                                         listePositionsDouglasSouche,
                                         listePositionsChenesSouche,
                                         listePositionsHetresSouche,
                                         listePositionsBouleauxSouche,
                                         listePositionsPinsSoucheMalade,
                                         listePositionsDouglasSoucheMalade,
                                         listePositionsChenesSoucheMalade,
                                         listePositionsHetresSoucheMalade,
                                         listePositionsBouleauxSoucheMalade,
                                         listePositionsRochers1,
                                         listePositionsRochers2,
                                         listePositionsRochers3,
                                         listePositionsRochers4,
                                         listePositionsRochers5,
                                         listePositionsRochers6,
                                         listePositionsRochers7,
                                         listeBatiments,
                                         listePosBatiments,
                                         listeItems,
                                         listeFavoris);

        string jsonData = JsonUtility.ToJson(gameData, true);

        File.WriteAllText(Application.dataPath + "/sauvegarde.json", jsonData);
    }

    private void Load()
    {
        string donneesEnregistrees = File.ReadAllText(Application.dataPath + "/sauvegarde.json");

        // Si il n'y a rien à charger
        if (donneesEnregistrees == "{}" || donneesEnregistrees == "")
        {
            Debug.Log("Aucune donnée enregistrée");
            return;
        }

        GameData gameData = JsonUtility.FromJson<GameData>(donneesEnregistrees);

        /** -------------------- ARBRES EN BONNE SANTE -------------------- **/

        foreach (Vector3 positionOfPin in gameData.listePositionsPins)
            Instantiate(pin, positionOfPin, pin.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfdouglas in gameData.listePositionsDouglas)
            Instantiate(douglas, positionOfdouglas, douglas.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfhetre in gameData.listePositionsHetres)
            Instantiate(hetre, positionOfhetre, hetre.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfChene in gameData.listePositionsChenes)
            Instantiate(chene, positionOfChene, chene.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfBouleau in gameData.listePositionsBouleaux)
            Instantiate(bouleau, positionOfBouleau, bouleau.transform.rotation, dossierArbres);


        /** -------------------- ARBRES MALADES -------------------- **/
        foreach (Vector3 positionOfPin in gameData.listePositionsPinsMalade)
            Instantiate(pinMalade, positionOfPin, pin.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfdouglas in gameData.listePositionsDouglasMalade)
            Instantiate(douglasMalade, positionOfdouglas, douglas.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfhetre in gameData.listePositionsHetresMalade)
            Instantiate(hetreMalade, positionOfhetre, hetre.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfChene in gameData.listePositionsChenesMalade)
            Instantiate(cheneMalade, positionOfChene, chene.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfBouleau in gameData.listePositionsBouleauxMalade)
            Instantiate(bouleauMalade, positionOfBouleau, bouleau.transform.rotation, dossierArbres);


        /** -------------------- ARBRES FRELES -------------------- **/
        foreach (Vector3 positionOfPin in gameData.listePositionsPinsFrele)
            Instantiate(pinFrele, positionOfPin, pin.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfdouglas in gameData.listePositionsDouglasFrele)
            Instantiate(douglasFrele, positionOfdouglas, douglas.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfhetre in gameData.listePositionsHetresFrele)
            Instantiate(hetreFrele, positionOfhetre, hetre.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfChene in gameData.listePositionsChenesFrele)
            Instantiate(cheneFrele, positionOfChene, chene.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfBouleau in gameData.listePositionsBouleauxFrele)
            Instantiate(bouleauFrele, positionOfBouleau, bouleau.transform.rotation, dossierArbres);


        /** -------------------- ARBRES ARBUSTES -------------------- **/
        foreach (Vector3 positionOfPin in gameData.listePositionsPinsArbuste)
            Instantiate(pinArbuste, positionOfPin, pin.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfdouglas in gameData.listePositionsDouglasArbuste)
            Instantiate(douglasArbuste, positionOfdouglas, douglas.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfhetre in gameData.listePositionsHetresArbuste)
            Instantiate(hetreArbuste, positionOfhetre, hetre.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfChene in gameData.listePositionsChenesArbuste)
            Instantiate(cheneArbuste, positionOfChene, chene.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfBouleau in gameData.listePositionsBouleauxArbuste)
            Instantiate(bouleauArbuste, positionOfBouleau, bouleau.transform.rotation, dossierArbres);


        /** -------------------- ARBRES ARBUSTES MALADES -------------------- **/
        foreach (Vector3 positionOfPin in gameData.listePositionsPinsArbusteMalade)
            Instantiate(pinArbusteMalade, positionOfPin, pin.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfdouglas in gameData.listePositionsDouglasArbusteMalade)
            Instantiate(douglasArbusteMalade, positionOfdouglas, douglas.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfhetre in gameData.listePositionsHetresArbusteMalade)
            Instantiate(hetreArbusteMalade, positionOfhetre, hetre.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfChene in gameData.listePositionsChenesArbusteMalade)
            Instantiate(cheneArbusteMalade, positionOfChene, chene.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfBouleau in gameData.listePositionsBouleauxArbusteMalade)
            Instantiate(bouleauArbusteMalade, positionOfBouleau, bouleau.transform.rotation, dossierArbres);


        /** -------------------- SOUCHES -------------------- **/

        foreach (Vector3 positionOfPin in gameData.listePositionsPinsSouche)
            Instantiate(pinSouche, positionOfPin, pin.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfdouglas in gameData.listePositionsDouglasSouche)
            Instantiate(douglasSouche, positionOfdouglas, douglas.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfhetre in gameData.listePositionsHetresSouche)
            Instantiate(hetreSouche, positionOfhetre, hetre.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfChene in gameData.listePositionsChenesSouche)
            Instantiate(cheneSouche, positionOfChene, chene.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfBouleau in gameData.listePositionsBouleauxSouche)
            Instantiate(bouleauSouche, positionOfBouleau, bouleau.transform.rotation, dossierArbres);


        /** -------------------- SOUCHES MALADES -------------------- **/

        foreach (Vector3 positionOfPin in gameData.listePositionsPinsSoucheMalade)
            Instantiate(pinSoucheMalade, positionOfPin, pin.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfdouglas in gameData.listePositionsDouglasSoucheMalade)
            Instantiate(douglasSoucheMalade, positionOfdouglas, douglas.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfhetre in gameData.listePositionsHetresSoucheMalade)
            Instantiate(hetreSoucheMalade, positionOfhetre, hetre.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfChene in gameData.listePositionsChenesSoucheMalade)
            Instantiate(cheneSoucheMalade, positionOfChene, chene.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfBouleau in gameData.listePositionsBouleauxSoucheMalade)
            Instantiate(bouleauSoucheMalade, positionOfBouleau, bouleau.transform.rotation, dossierArbres);


        // On fait pas apparaitre les rochers parce qu'ils sont sur la scene pour l'instant
        // Le script creerait donc un double de chaque a tous les appels      
        foreach (Vector3 positionOfRocher1 in gameData.listePositionsRochers1)
            Instantiate(Rocher1, positionOfRocher1, Rocher1.transform.rotation, dossierRochers);

        foreach (Vector3 positionOfRocher2 in gameData.listePositionsRochers2)
            Instantiate(Rocher2, positionOfRocher2, Rocher2.transform.rotation, dossierRochers);

        foreach (Vector3 positionOfRocher3 in gameData.listePositionsRochers3)
            Instantiate(Rocher3, positionOfRocher3, Rocher3.transform.rotation, dossierRochers);

        foreach (Vector3 positionOfRocher4 in gameData.listePositionsRochers4)
            Instantiate(Rocher4, positionOfRocher4, Rocher4.transform.rotation, dossierRochers);

        foreach (Vector3 positionOfRocher5 in gameData.listePositionsRochers5)
            Instantiate(Rocher5, positionOfRocher5, Rocher5.transform.rotation, dossierRochers);

        foreach (Vector3 positionOfRocher6 in gameData.listePositionsRochers6)
            Instantiate(Rocher6, positionOfRocher6, Rocher6.transform.rotation, dossierRochers);

        foreach (Vector3 positionOfRocher7 in gameData.listePositionsRochers7)
            Instantiate(Rocher7, positionOfRocher7, Rocher7.transform.rotation, dossierRochers);


        for (int i = 0; i < gameData.listePosBatiments.Count; i++)
        {
            string nomBat = gameData.listeBatiments[i];

            if (nomBat.IndexOf("Ferme", StringComparison.OrdinalIgnoreCase) >= 0 || nomBat.IndexOf("MoulinAEau", StringComparison.OrdinalIgnoreCase) >= 0 || nomBat.IndexOf("Boulangerie", StringComparison.OrdinalIgnoreCase) >= 0 || nomBat.IndexOf("MoulinAVent", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                continue;
            }

            Vector3 positionBat = gameData.listePosBatiments[i];

            GameObject batiment = null;

            foreach (GameObject go in listeCompleteBatiments)
            {
                if (nomBat.IndexOf(go.name, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    batiment = go;
                    break;
                }
            }

            if (batiment == null)
            {
                print("Problème load : le nom du batiment (\"" + nomBat + "\") enregistré ne correspond à aucun batiment");
                continue;
            }

            else
            {
                Instantiate(batiment, positionBat, batiment.transform.rotation, dossierBatiments);
            }
        }

        List<ItemAmount> listeItems = new List<ItemAmount>();
        List<string> listeNoms = gameData.listeNomsItems;
        List<int> listeQuantites = gameData.listeAmountItems;

        for (int i = 0; i<gameData.listeAmountItems.Count; i++)
        {
            listeItems.Add(new ItemAmount(Item: Resources.Load("items/" + listeNoms[i], typeof(Item)) as Item, Amount: listeQuantites[i]));
        }


        // Demarrage de l'inventaire
        player.createInventory(listeItems, gameData.listeFavoris);
        inventory = player.inventory;

        BuildingLayerMag.updateBatLayers();
    }
}

