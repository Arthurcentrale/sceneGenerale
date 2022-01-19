using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbreManager : MonoBehaviour
{

    public enum etatEnum
    {
        adulteRobuste,
        adulteFrele,
        adulteMalade,
        arbusteMalade,
        arbuste,
        souche,

    }

    public enum essenceEnum
    {
        chene,
        hetre,
        pin,
        douglas,
        bouleau,
    }

    //les arbres robustes
    public GameObject cheneRobuste;
    public GameObject hetreRobuste;
    public GameObject douglasRobuste;
    public GameObject pinRobuste;
    public GameObject bouleauRobuste;

    //les arbres frêles
    public GameObject cheneFrele;
    public GameObject hetreFrele;
    public GameObject douglasFrele;
    public GameObject pinFrele;
    public GameObject bouleauFrele;

    //les arbres malades
    public GameObject cheneMalade;
    public GameObject hetreMalade;
    public GameObject douglasMalade;
    public GameObject pinMalade;
    public GameObject bouleauMalade;

    //les arbustes
    public GameObject arbusteChene;
    public GameObject arbusteHetre;
    public GameObject arbusteDouglas;
    public GameObject arbustePin;
    public GameObject arbusteBouleau;


    //les arbustes malades
    public GameObject arbusteCheneMalade;
    public GameObject arbusteHetreMalade;
    public GameObject arbusteDouglasMalade;
    public GameObject arbustePinMalade;
    public GameObject arbusteBouleauMalade;

    //Temps de croissance
    public int tempsCroissanceChene = 6;
    public int tempsCroissanceDouglas = 4;
    public int tempsCroissancePin = 5;
    public int tempsCroissanceHetre = 4;
    public int tempsCroissanceBouleau = 3;

    //absorption de Co2
    public float absorptionChene = 0.2f;
    public float absorptionHetre = 0.2f;
    public float absorptionPin = 0.1f;
    public float absorptionDouglas = 0.1f;
    public float absorptionBouleau = 0.2f;



    //liste des arbres dans le jeu
    public GameObject[] arbres;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void declencherEpidemie()
    {
        int declencheur = UnityEngine.Random.Range(1, 100);

        if (declencheur < 4)
        {
            int randomNumber = UnityEngine.Random.Range(1, 100);
            string essenceMalade;
            if (randomNumber < 31) essenceMalade = "Chene";
            else if (randomNumber >= 31 && randomNumber < 41) essenceMalade = "Hetre";
            else if (randomNumber >= 41 && randomNumber < 71) essenceMalade = "Pin";
            else if (randomNumber >= 71 && randomNumber < 86) essenceMalade = "Douglas";
            else essenceMalade = "Bouleau";

        }
        int nombreArbres = 0;
        GameObject.Find("");
    }

    public void ageArbresPlus() //augmente l'âge des arbres chaque jour
    {
        arbres = GameObject.FindGameObjectsWithTag("Arbre");

        foreach(var arbre in arbres)
        {
            if (arbre.name.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                arbre.GetComponent<SoucheComportement>().age += 1;
            }
            else arbre.GetComponent<ArbreComportement>().age += 1;
        }
    }

    public void contaminationArbresPlus() //fonction exécutée toutes les 3 heures, qui accentue la maladie des arbres malades pour les rapprocher de la mort
    {
        arbres = GameObject.FindGameObjectsWithTag("Arbre");
        Debug.Log("fonction contamination");
        foreach(var arbre in arbres)
        {
            if (name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                arbre.GetComponent<ArbreComportement>().contamination += 3;

                arbre.GetComponent<ArbreComportement>().tuerArbreMalade();
            }
        }
    }

    
        
}
