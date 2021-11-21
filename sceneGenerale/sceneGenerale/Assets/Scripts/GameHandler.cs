using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    // Arbres
    public GameObject pin;
    public GameObject connifere;
    public GameObject chene;
    public GameObject platane;

    // Rochers
    public GameObject Rocher1;
    public GameObject Rocher2;
    public GameObject Rocher3;
    public GameObject Rocher4;
    public GameObject Rocher5;
    public GameObject Rocher6;
    public GameObject Rocher7;

    // Batiments
    public GameObject boulangerie;
    public GameObject cabanon;
    public GameObject chaumiere;
    public GameObject ferme;
    public GameObject forge;
    public GameObject fosse;
    public GameObject gardeManger;
    public GameObject maisonPierre;
    public GameObject moulinEau;
    public GameObject moulinVent;
    public GameObject pecherie;
    public GameObject puit;

    // Batis batiments
    public GameObject batiBoulangerie;
    public GameObject batiCabanon;
    public GameObject batiChaumiere;
    public GameObject batiFerme;
    public GameObject batiForge;
    public GameObject batiFosse;
    public GameObject batiGardeManger;
    public GameObject batiMaisonPierre;
    public GameObject batiMoulinEau;
    public GameObject batiMoulinVent;
    public GameObject batiPecherie;
    public GameObject batiPuit;

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
        List<Vector3> listePositionsConniferes = new List<Vector3>();
        List<Vector3> listePositionsChenes = new List<Vector3>();
        List<Vector3> listePositionsPlatanes = new List<Vector3>();

        // On parcourt la liste des arbres (sous formes de GO) présent dans le jeu
        // Pour chacun d'entre eux, on créé une objet de la structure arbresJSON pouvant être transformé directement en JSON
        foreach (GameObject arbre in listeArbres)
        {
            string nomArbre = arbre.name;
            if (nomArbre.IndexOf("Pin", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsPins.Add(arbre.transform.position);

            else if (nomArbre.IndexOf("Chene", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsChenes.Add(arbre.transform.position);

            else if (nomArbre.IndexOf("Platane", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsPlatanes.Add(arbre.transform.position);

            else
                listePositionsConniferes.Add(arbre.transform.position);
        }

        /** ------------------ Sauvegarde des rochers ------------------ **/

        GameObject[] listeRoche1 = GameObject.FindGameObjectsWithTag("Roche1");
        GameObject[] listeRoche2 = GameObject.FindGameObjectsWithTag("Roche2");
        GameObject[] listeRoche3 = GameObject.FindGameObjectsWithTag("Roche3");

        // On créé chacune des listes de positions des arbres
        List<Vector3> listePositionsRochers1 = new List<Vector3>();
        List<Vector3> listePositionsRochers2 = new List<Vector3>();
        List<Vector3> listePositionsRochers3 = new List<Vector3>();
        List<Vector3> listePositionsRochers4 = new List<Vector3>();
        List<Vector3> listePositionsRochers5 = new List<Vector3>();
        List<Vector3> listePositionsRochers6 = new List<Vector3>();
        List<Vector3> listePositionsRochers7 = new List<Vector3>();

        foreach (GameObject arbre in listeRoche1)
        {
            string nomArbre = arbre.name;
            if (nomArbre.IndexOf("Rocher1", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers1.Add(arbre.transform.position);

            else if (nomArbre.IndexOf("Rocher4", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers4.Add(arbre.transform.position);

            else if (nomArbre.IndexOf("Rocher7", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers7.Add(arbre.transform.position);

            else
                print("Problème sauvegarde : un rocher de type autre que 1, 4 et 7 porte le tag \"Roche1\"");
        }

        foreach (GameObject arbre in listeRoche2)
        {
            string nomArbre = arbre.name;
            if (nomArbre.IndexOf("Rocher2", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers2.Add(arbre.transform.position);

            else if (nomArbre.IndexOf("Rocher5", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers5.Add(arbre.transform.position);

            else
                print("Problème sauvegarde : un rocher de type autre que 2 et 5 porte le tag \"Roche2\"");
        }

        foreach (GameObject arbre in listeRoche3)
        {
            string nomArbre = arbre.name;
            if (nomArbre.IndexOf("Rocher3", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers3.Add(arbre.transform.position);

            else if (nomArbre.IndexOf("Rocher6", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsRochers6.Add(arbre.transform.position);

            else
                print("Problème sauvegarde : un rocher de type autre que 3 et 6 porte le tag \"Roche3\"");
        }

        /** ------------------ Sauvegarde des batiments ------------------ **/

        // On recupere tous les elements dans le dossier des batiments                
        Dictionary<string, Vector3> listeBatiments = new Dictionary<string, Vector3>();

        for (int i = 0; i < dossierBatiments.childCount; i++)
        {
            listeBatiments.Add(dossierBatiments.GetChild(i).name, dossierBatiments.GetChild(i).position);
        }

        /** ------------------ Sauvegarde des données de l'inventaire ------------------ **/

        List<ItemAmount> listeItems = inventory.GetItemList();
        List<bool> listeFavoris = inventory.GetFavList();

        // Objet que l'on transformera en JSON
        GameData gameData = new GameData(listePositionsPins,
                                         listePositionsConniferes,
                                         listePositionsChenes,
                                         listePositionsPlatanes,
                                         listePositionsRochers1,
                                         listePositionsRochers2,
                                         listePositionsRochers3,
                                         listePositionsRochers4,
                                         listePositionsRochers5,
                                         listePositionsRochers6,
                                         listePositionsRochers7,
                                         listeBatiments,
                                         listeItems,
                                         listeFavoris);

        string jsonData = JsonUtility.ToJson(gameData, true);

        File.WriteAllText(Application.dataPath + "/sauvegarde.json", jsonData);
    }

    private void Load()
    {
        string donneesEnregistrees = File.ReadAllText(Application.dataPath + "/sauvegarde.json");

        // Si il n'y a aucun arbre à charger
        if (donneesEnregistrees == "{}" || donneesEnregistrees == "")
        {
            Debug.Log("Aucune donnée enregistrée");
            return;
        }

        GameData gameData = JsonUtility.FromJson<GameData>(donneesEnregistrees);

        foreach (Vector3 positionOfPin in gameData.listePositionsPins)
            Instantiate(pin, positionOfPin, pin.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfConnifere in gameData.listePositionsConniferes)
            Instantiate(connifere, positionOfConnifere, connifere.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfPlatane in gameData.listePositionsPlatanes)
            Instantiate(platane, positionOfPlatane, platane.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfChene in gameData.listePositionsChenes)
            Instantiate(chene, positionOfChene, chene.transform.rotation, dossierArbres);

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

        foreach (KeyValuePair<string, Vector3> kvp in gameData.dictionnaireBatiments)
        {
            string nomBat = kvp.Key;
            Vector3 positionBat = kvp.Value;

            if (nomBat.IndexOf("Boulangerie", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(boulangerie, positionBat, boulangerie.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("BatiBoulangerie", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiBoulangerie, positionBat, batiBoulangerie.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("Chaumière", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(chaumiere, positionBat, chaumiere.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("BatiChaumière", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiChaumiere, positionBat, batiChaumiere.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("Cabanon", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(cabanon, positionBat, cabanon.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("BatiCabanon", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiCabanon, positionBat, batiCabanon.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("Ferme", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(ferme, positionBat, ferme.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("BatiFerme", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiFerme, positionBat, batiFerme.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("Forge", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(forge, positionBat, forge.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("batiForge", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiForge, positionBat, batiForge.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("Fosse", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(fosse, positionBat, fosse.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("BatiFosse", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiFosse, positionBat, batiFosse.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("GardeManger", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(gardeManger, positionBat, gardeManger.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("BatiGardeManger", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiGardeManger, positionBat, batiGardeManger.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("MaisonPierre", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(maisonPierre, positionBat, maisonPierre.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("BatiMaisonPierre", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiMaisonPierre, positionBat, batiMaisonPierre.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("MoulinAEau", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(moulinEau, positionBat, moulinEau.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("BatiMoulinAEau", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiMoulinEau, positionBat, batiMoulinEau.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("MoulinAVent", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(moulinVent, positionBat, moulinVent.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("BatiMoulinAVent", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiMoulinVent, positionBat, batiMoulinVent.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("Pecherie", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(pecherie, positionBat, pecherie.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("BatiPecherie", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(batiPecherie, positionBat, batiPecherie.transform.rotation, dossierBatiments);

            else if (nomBat.IndexOf("Puit", StringComparison.OrdinalIgnoreCase) >= 0)
                Instantiate(puit, positionBat, puit.transform.rotation, dossierBatiments);

            else
            {
                print("Probleme chargement : le batiment " + nomBat + " n'est pas reconnu");
            }
        }

        // Demarrage de l'inventaire        
        inventory = new Inventory(gameData.listeItems, gameData.listeFavoris, player);
        uiInventory = GameObject.Find("Inventaire2").transform.GetChild(0).gameObject.GetComponent<UI_Inventory>();
        uiInventory.SetInventory(inventory);
    }
}

