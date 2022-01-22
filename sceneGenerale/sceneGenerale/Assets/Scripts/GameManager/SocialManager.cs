using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialManager : MonoBehaviour
{
    public static SocialManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(instance);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }


    public int quantiteNourriture;
    public int ecoSensibilisation;
    public float qualiteDeVie;

    public int nombreAlimentsDifferents;

    public List<GameObject> listeBatiment;  //copie de la liste des batiments de DeveloppementManager qu'on va update avec la fonction MajSocial()
    public List<GameObject> habitants;      //copie de la liste des habitants de HabitantManager qu'on va update avec la fonction MajSocial()


    private void Start()
    {
        quantiteNourriture = 0;
        ecoSensibilisation = 70;
        qualiteDeVie = 0f;

        nombreAlimentsDifferents = 0;
    }

    public void MajSocial()
    {
        //On met à jour quantiteNourriture
        listeBatiment = DeveloppementManager.instance.listeBatiment;
        foreach(GameObject batimentGO in listeBatiment)
        {
            quantiteNourriture += batimentGO.GetComponent<Batiment>().productionFood(); //on ajoute la production de nourriture de chaque batiment de la liste lors d'une maj
        }
        habitants = GameObject.Find("Habitant Manager").GetComponent<HabitantManager>().habitants;
        float conso = 0f;
        foreach (GameObject habitantGO in habitants)
        {
            conso += habitantGO.GetComponent<HabitantBehaviour>().foodQuantity; //on somme la consommation de nourriture de chaque habitant
        }
        quantiteNourriture -= (int) conso; //on la retire de la quantite totale restante

        //On met à jour l'écosensibilisation
        int sommmeEcoLevel = 0;
        foreach (GameObject habitantGO in habitants)
        {
            sommmeEcoLevel += habitantGO.GetComponent<HabitantBehaviour>().ecoLevel; //on fait la somme des ecoLevels
        }
        ecoSensibilisation = sommmeEcoLevel / (habitants.Count * 5) * 100;

        //On met à jour la qualité de vie
        int sommeSatisfactionNourriture = nombreAlimentsDifferents * habitants.Count;
        foreach (GameObject habitantGO in habitants)
        {
            sommeSatisfactionNourriture -= (int) habitantGO.GetComponent<HabitantBehaviour>().foodVariety.Count; //on fait la somme des satisfaction pour chaque habitant
        }
        int sommeChauffage = 0;
        foreach (GameObject batimentGO in listeBatiment)
        {
            Batiment bati = batimentGO.GetComponent<Batiment>();
            sommeChauffage += bati.chauffageActual - bati.chauffageNeed;
        }
        //EnvironnementManager enviro = GameObject.Find("GameManager").GetComponent<GameManager>().environnementManager;
        //qualiteDeVie = (sommeChauffage + (enviro.qualiteAir + enviro.qualiteEau + enviro.qualiteSol) / 300) * 100 / 3 + sommeSatisfactionNourriture;
    }
}
