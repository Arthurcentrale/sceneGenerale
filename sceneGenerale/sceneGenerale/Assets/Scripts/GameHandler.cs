using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    public GameObject pin;
    public GameObject douglas;
    public GameObject chene;
    public GameObject hetre;

    private Transform dossierArbres;

    public float intervalleSauvegarde = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        dossierArbres = GameObject.Find("Arbres").transform;
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
        GameObject[] listeArbres = GameObject.FindGameObjectsWithTag("Arbre");

        // On créé chacune des listes de positions des arbres
        List<Vector3> listePositionsPins = new List<Vector3>();
        List<Vector3> listePositionsDouglas = new List<Vector3>();
        List<Vector3> listePositionsChenes = new List<Vector3>();
        List<Vector3> listePositionsHetres = new List<Vector3>();

        // On parcourt la liste des arbres (sous formes de GO) présent dans le jeu
        // Pour chacun d'entre eux, on créé une objet de la structure arbresJSON pouvant être transformé directement en JSON
        foreach (GameObject arbre in listeArbres)
        {
            string nomArbre = arbre.name;
            if (nomArbre.IndexOf("Pin", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsPins.Add(arbre.transform.position);

            else if (nomArbre.IndexOf("Chene", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsChenes.Add(arbre.transform.position);

            else if (nomArbre.IndexOf("Hetre", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsHetres.Add(arbre.transform.position);

            else if (nomArbre.IndexOf("Douglas", StringComparison.OrdinalIgnoreCase) >= 0)
                listePositionsDouglas.Add(arbre.transform.position);
        }

        // Objet que l'on transformera en JSON
        GameData gameData = new GameData(listePositionsPins, listePositionsDouglas, listePositionsChenes, listePositionsHetres);

        string jsonData = JsonUtility.ToJson(gameData, true);

        File.WriteAllText(Application.dataPath + "/sauvegarde.json", jsonData);
    }

    private void Load()
    {
        string donneesEnregistrees = File.ReadAllText(Application.dataPath + "/sauvegarde.json");

        // Si il n'y a aucun arbre à charger
        if (donneesEnregistrees == "{}" || donneesEnregistrees == "")
        {
            Debug.Log("Aucun arbre");
            return;
        }

        GameData gameData = JsonUtility.FromJson<GameData>(donneesEnregistrees);

        foreach (Vector3 positionOfPin in gameData.listePositionsPins)
            Instantiate(pin, positionOfPin, pin.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfDouglas in gameData.listePositionsDouglas)
            Instantiate(douglas, positionOfDouglas, douglas.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfHetre in gameData.listePositionsHetres)
            Instantiate(hetre, positionOfHetre, hetre.transform.rotation, dossierArbres);

        foreach (Vector3 positionOfChene in gameData.listePositionsChenes)
            Instantiate(chene, positionOfChene, chene.transform.rotation, dossierArbres);
    }
}

