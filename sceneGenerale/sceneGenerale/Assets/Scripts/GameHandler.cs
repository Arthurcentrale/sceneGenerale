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

    // Mairies
    public GameObject mairieRuine;
    public GameObject mairie;

    // Dossiers
    private Transform dossierArbres;
    private Transform dossierRochers;
    private Transform dossierBatiments;

    public float intervalleSauvegarde = 5.0f;

    string donneesEnregistrees;

    // Chemin vers le fichier bdd
    string path;

    // Start is called before the first frame update
    void Start()
    {
        dossierArbres = GameObject.Find("Arbres").transform;
        dossierRochers = GameObject.Find("Rocher").transform;
        dossierBatiments = GameObject.Find("Batiments").transform;

        InitSauvegarde.initSauvegarde();

        path = Application.persistentDataPath + "/Assets/sauvegarde";

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
        //Debug.Log("Sauvegarde");
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

        /** ------------------ Sauvegarde de l'état de la mairie ------------------ **/

        bool isMairieRenovee = MairieRenov.isMairieRenovee; 

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
                                         listeFavoris,
                                         isMairieRenovee);

        string jsonData = JsonUtility.ToJson(gameData, true);

        File.WriteAllText(path + "/sauvegarde.json", jsonData);
    }






    private void Load()
    {
        donneesEnregistrees = File.ReadAllText(path + "/sauvegarde.json");

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
                //print("Problème load : le nom du batiment (\"" + nomBat + "\") enregistré ne correspond à aucun batiment");
                continue;
            }

            else
            {
                Instantiate(batiment, positionBat, batiment.transform.rotation, dossierBatiments);
            }
        }

        // Si la mairie a déjà été rénovée
        if (gameData.isMairieRenovee)
        {
            mairieRuine.SetActive(false);
            mairie.SetActive(true);
            MairieRenov.isMairieRenovee = true;
        }

        List<ItemAmount> listeItems = new List<ItemAmount>();
        List<string> listeNoms = gameData.listeNomsItems;
        List<int> listeQuantites = gameData.listeAmountItems;

        /*
        for (int i = 0; i < gameData.listeAmountItems.Count; i++)
        {
            listeItems.Add(new ItemAmount(Item: Resources.Load("items/" + listeNoms[i], typeof(Item)) as Item, Amount: listeQuantites[i]));
        }
        */

        // Demarrage de l'inventaire
        //player.createInventory(listeItems, gameData.listeFavoris);
        player.createInventory(listeItems, new List<bool>());
        inventory = player.inventory;
        BuildingLayerMag.updateBatLayers();
    }

    public static void resetGame()
    {
        File.WriteAllText(Application.persistentDataPath + "/Assets/sauvegarde/sauvegarde.json", "{\"listePositionsPins\":[{\"x\":511.29998779296877,\"y\":5.650816917419434,\"z\":246.39999389648438},{\"x\":489.260009765625,\"y\":5.650816917419434,\"z\":246.39999389648438},{\"x\":542.2999877929688,\"y\":5.650816917419434,\"z\":261.3999938964844},{\"x\":398.29998779296877,\"y\":5.650816917419434,\"z\":261.3999938964844},{\"x\":447.5,\"y\":5.650816917419434,\"z\":245.80999755859376},{\"x\":395.1000061035156,\"y\":5.650816917419434,\"z\":402.70001220703127}],\"listePositionsDouglas\":[{\"x\":466.97808837890627,\"y\":0.0,\"z\":257.1081237792969},{\"x\":488.70001220703127,\"y\":0.0,\"z\":257.1081237792969},{\"x\":464.989990234375,\"y\":0.0,\"z\":240.0},{\"x\":464.989990234375,\"y\":0.0,\"z\":240.0},{\"x\":355.6000061035156,\"y\":0.0,\"z\":276.20001220703127},{\"x\":371.1000061035156,\"y\":0.0,\"z\":276.20001220703127},{\"x\":390.70001220703127,\"y\":0.0,\"z\":306.1000061035156},{\"x\":434.79998779296877,\"y\":0.0,\"z\":395.0},{\"x\":434.79998779296877,\"y\":0.0,\"z\":383.3999938964844},{\"x\":446.8999938964844,\"y\":0.0,\"z\":389.79998779296877},{\"x\":452.79998779296877,\"y\":0.0,\"z\":389.79998779296877},{\"x\":452.79998779296877,\"y\":0.0,\"z\":372.1000061035156},{\"x\":477.79998779296877,\"y\":0.0,\"z\":372.1000061035156},{\"x\":495.0,\"y\":0.0,\"z\":352.6000061035156},{\"x\":514.0,\"y\":0.0,\"z\":352.6000061035156},{\"x\":525.7999877929688,\"y\":0.0,\"z\":335.1000061035156}],\"listePositionsChenes\":[{\"x\":385.4255676269531,\"y\":5.650818824768066,\"z\":355.9426574707031},{\"x\":316.5,\"y\":5.650818824768066,\"z\":435.1000061035156},{\"x\":334.79998779296877,\"y\":5.650818824768066,\"z\":441.79998779296877},{\"x\":441.1000061035156,\"y\":5.650818824768066,\"z\":324.29998779296877},{\"x\":502.20001220703127,\"y\":5.650818824768066,\"z\":339.8999938964844},{\"x\":489.5,\"y\":5.650818824768066,\"z\":301.20001220703127},{\"x\":517.7999877929688,\"y\":5.650818824768066,\"z\":292.3999938964844},{\"x\":531.2999877929688,\"y\":5.650818824768066,\"z\":298.20001220703127},{\"x\":418.29998779296877,\"y\":5.650818824768066,\"z\":257.20001220703127}],\"listePositionsHetres\":[{\"x\":526.8411865234375,\"y\":4.0,\"z\":309.40313720703127},{\"x\":491.6000061035156,\"y\":4.0,\"z\":309.40313720703127},{\"x\":461.6000061035156,\"y\":4.0,\"z\":353.8999938964844},{\"x\":414.79998779296877,\"y\":4.0,\"z\":292.6000061035156},{\"x\":414.79998779296877,\"y\":4.0,\"z\":324.0},{\"x\":464.29998779296877,\"y\":4.0,\"z\":324.0},{\"x\":517.2999877929688,\"y\":4.0,\"z\":324.0}],\"listePositionsBouleaux\":[{\"x\":473.7300109863281,\"y\":4.639999866485596,\"z\":309.7200012207031},{\"x\":485.0,\"y\":4.639999866485596,\"z\":325.0},{\"x\":400.5,\"y\":4.639999866485596,\"z\":337.6000061035156},{\"x\":403.1000061035156,\"y\":4.639999866485596,\"z\":328.20001220703127},{\"x\":333.8999938964844,\"y\":4.639999866485596,\"z\":427.20001220703127}],\"listePositionsPinsFrele\":[{\"x\":507.29998779296877,\"y\":5.099999904632568,\"z\":266.30999755859377},{\"x\":529.730224609375,\"y\":5.099999904632568,\"z\":245.50999450683595}],\"listePositionsDouglasFrele\":[],\"listePositionsChenesFrele\":[{\"x\":381.20001220703127,\"y\":6.929999828338623,\"z\":338.1499938964844}],\"listePositionsHetresFrele\":[{\"x\":472.5,\"y\":4.400000095367432,\"z\":292.6000061035156}],\"listePositionsBouleauxFrele\":[],\"listePositionsPinsMalade\":[],\"listePositionsDouglasMalade\":[],\"listePositionsChenesMalade\":[],\"listePositionsHetresMalade\":[],\"listePositionsBouleauxMalade\":[],\"listePositionsPinsArbuste\":[],\"listePositionsDouglasArbuste\":[],\"listePositionsChenesArbuste\":[],\"listePositionsHetresArbuste\":[],\"listePositionsBouleauxArbuste\":[],\"listePositionsPinsArbusteMalade\":[],\"listePositionsDouglasArbusteMalade\":[],\"listePositionsChenesArbusteMalade\":[],\"listePositionsHetresArbusteMalade\":[],\"listePositionsBouleauxArbusteMalade\":[],\"listePositionsPinsSouche\":[],\"listePositionsDouglasSouche\":[],\"listePositionsChenesSouche\":[],\"listePositionsHetresSouche\":[],\"listePositionsBouleauxSouche\":[],\"listePositionsPinsSoucheMalade\":[],\"listePositionsDouglasSoucheMalade\":[],\"listePositionsChenesSoucheMalade\":[],\"listePositionsHetresSoucheMalade\":[],\"listePositionsBouleauxSoucheMalade\":[],\"listePositionsRochers1\":[{\"x\":504.3999938964844,\"y\":2.5899999141693117,\"z\":389.8999938964844},{\"x\":486.3999938964844,\"y\":2.5899999141693117,\"z\":379.4599914550781},{\"x\":419.1000061035156,\"y\":2.5899999141693117,\"z\":379.4599914550781},{\"x\":530.5999755859375,\"y\":2.5899999141693117,\"z\":349.1000061035156},{\"x\":519.4000244140625,\"y\":2.5899999141693117,\"z\":368.8999938964844},{\"x\":528.5,\"y\":2.5899999141693117,\"z\":364.3999938964844},{\"x\":554.7999877929688,\"y\":2.5899999141693117,\"z\":330.0},{\"x\":563.9000244140625,\"y\":2.5899999141693117,\"z\":298.8999938964844},{\"x\":563.9000244140625,\"y\":2.5899999141693117,\"z\":266.79998779296877},{\"x\":529.2000122070313,\"y\":2.5899999141693117,\"z\":266.79998779296877},{\"x\":461.8999938964844,\"y\":2.5899999141693117,\"z\":266.79998779296877},{\"x\":461.8999938964844,\"y\":2.5899999141693117,\"z\":294.5},{\"x\":375.70001220703127,\"y\":2.5899999141693117,\"z\":274.6000061035156},{\"x\":375.70001220703127,\"y\":2.5899999141693117,\"z\":241.60000610351563},{\"x\":407.70001220703127,\"y\":2.5899999141693117,\"z\":241.60000610351563},{\"x\":407.70001220703127,\"y\":2.5899999141693117,\"z\":224.3000030517578}],\"listePositionsRochers2\":[{\"x\":592.27099609375,\"y\":2.9000000953674318,\"z\":252.5},{\"x\":569.0999755859375,\"y\":2.9000000953674318,\"z\":309.29998779296877},{\"x\":546.5,\"y\":2.9000000953674318,\"z\":346.5},{\"x\":512.7999877929688,\"y\":2.9000000953674318,\"z\":359.79998779296877},{\"x\":473.6000061035156,\"y\":2.9000000953674318,\"z\":382.70001220703127},{\"x\":414.3999938964844,\"y\":2.9000000953674318,\"z\":382.70001220703127},{\"x\":377.0,\"y\":2.9000000953674318,\"z\":382.70001220703127},{\"x\":377.0,\"y\":2.9000000953674318,\"z\":344.20001220703127},{\"x\":433.1000061035156,\"y\":2.9000000953674318,\"z\":338.20001220703127},{\"x\":433.1000061035156,\"y\":2.9000000953674318,\"z\":282.20001220703127},{\"x\":433.1000061035156,\"y\":2.9000000953674318,\"z\":227.10000610351563},{\"x\":511.3999938964844,\"y\":2.9000000953674318,\"z\":227.10000610351563},{\"x\":527.5,\"y\":2.9000000953674318,\"z\":255.60000610351563}],\"listePositionsRochers3\":[{\"x\":429.86822509765627,\"y\":2.5999999046325685,\"z\":306.561279296875},{\"x\":513.7999877929688,\"y\":2.5999999046325685,\"z\":306.561279296875},{\"x\":514.7999877929688,\"y\":2.5999999046325685,\"z\":225.60000610351563},{\"x\":565.7000122070313,\"y\":2.5999999046325685,\"z\":276.79998779296877},{\"x\":586.5999755859375,\"y\":2.5999999046325685,\"z\":276.79998779296877},{\"x\":563.7999877929688,\"y\":2.5999999046325685,\"z\":333.20001220703127},{\"x\":542.2999877929688,\"y\":2.5999999046325685,\"z\":356.6000061035156},{\"x\":517.5999755859375,\"y\":2.5999999046325685,\"z\":356.6000061035156}],\"listePositionsRochers4\":[],\"listePositionsRochers5\":[{\"x\":417.2364501953125,\"y\":0.6700000166893005,\"z\":295.35845947265627},{\"x\":490.2300109863281,\"y\":0.6700000166893005,\"z\":281.57000732421877},{\"x\":490.2300109863281,\"y\":0.6700000166893005,\"z\":233.10000610351563},{\"x\":572.7999877929688,\"y\":0.6700000166893005,\"z\":271.79998779296877},{\"x\":562.5999755859375,\"y\":0.6700000166893005,\"z\":321.5},{\"x\":523.5999755859375,\"y\":0.6700000166893005,\"z\":367.20001220703127},{\"x\":377.8999938964844,\"y\":0.6700000166893005,\"z\":367.20001220703127}],\"listePositionsRochers6\":[{\"x\":407.9100036621094,\"y\":1.5,\"z\":300.0},{\"x\":544.2000122070313,\"y\":1.5,\"z\":338.6000061035156},{\"x\":501.6000061035156,\"y\":1.5,\"z\":375.29998779296877},{\"x\":575.0,\"y\":1.5,\"z\":258.5}],\"listePositionsRochers7\":[],\"listeNomsItems\":[\"Hache\",\"BoisRobuste\",\"Pioche\",\"Pierre\",\"GraineChene\",\"BoisRobuste\",\"BoisFrele\",\"GraineHetre\",\"BoisRobuste\",\"BoisFrele\",\"BoisFrele\",\"GraineBouleau\",\"BoisFrele\",\"BoisRobuste\",\"GrainePinM\",\"BoisRobuste\",\"BoisFrele\",\"GraineDouglas\",\"BoisRobuste\",\"BoisFrele\"],\"listeAmountItems\":[1,5,1,5,3,5,5,2,5,5,5,1,5,5,2,5,5,1,3,1],\"listeFavoris\":[true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"listeBatiments\":[\"MairieRuine\",\"Mairie\",\"Ferme\",\"MoulinAEau\",\"MoulinAVent\",\"Boulangerie\"],\"listePosBatiments\":[{\"x\":330.79998779296877,\"y\":12.75,\"z\":617.7000122070313},{\"x\":376.48724365234377,\"y\":12.744438171386719,\"z\":300.4572448730469},{\"x\":456.6700134277344,\"y\":4.440000057220459,\"z\":552.9000244140625},{\"x\":352.0,\"y\":5.809999942779541,\"z\":494.6499938964844},{\"x\":300.79998779296877,\"y\":7.849999904632568,\"z\":498.17999267578127},{\"x\":446.79998779296877,\"y\":5.829999923706055,\"z\":495.3999938964844}]}");
    }
}

