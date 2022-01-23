using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantBehaviour : MonoBehaviour
{
    //variables générales :
    public Vector3 originalPosition;
    public Vector3 mairiePosition;//donne l'endroit où les habitants se rendent quand ils sont recrutables et qu'ils ne sont pas encore logés
    public Vector3 housePosition;

    public string nom;

    //image
    public Sprite image;

    //besoins et déchets
    public int foodQuantity;
    public List<Item> foodVariety;
    public float waste;

    //niveau d'écosensibilisation
    public float ecoPoints;
    public int ecoLevel;
    private int ecoPointsMax = 101;

    //statut dans l'aventure
    public bool isVillager = true;
    public bool isHoused = false; // Boolen pour savoir si l'habitant possède sa chaumière
    public bool hasWorkplace = false;
    public bool isWorking = false; // Booleen pour savoir si l'habitant possède son batiment de travail

    //conditions = bâtiments nécessaires pour qu'il puisse travailler et autres


    // Start is called before the first frame update
    void Start()
    {
    }
    void UpdateSituation()
    {
        if (isHoused && hasWorkplace) isWorking = true; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //fonction dépalçant un personnage disponible mais pas prêt à travailler près de la mairie
    void goToMairie()
    {       
            transform.position = mairiePosition;
    }

    void goToOriginalPosition()
    {
        transform.position = originalPosition;
    }

    void goToHouse()
    {
        transform.position = housePosition;
    }

    void AffecterMaison() // Lorsqu'on affecte l'habitant à la maison
    {
        // mettre la position de la maison comme coordonnées pour les tps le soir (z +/- 5 pour etre juste devant)
        // Mettre l'habitant comme résident de la maison pour qu'elle ne soit plus compté comme vide
        // Ajouter la consommation de l'habitant a la conso globale
    }

    void Fonction6h()
    {
        if (System.DateTime.Now.Hour == 6) goToOriginalPosition();
        else if (System.DateTime.Now.Hour == 20) goToHouse();
    }

    void levelUp()
    {
        if (ecoPoints <30 && ecoPoints >=10)
        {
            ecoLevel = 2;
        } else if (ecoPoints < 60 && ecoPoints >= 30)
        {
            ecoLevel = 3;
        } else if (ecoPoints < 100 && ecoPoints >= 60)
        {
            ecoLevel = 4;
        } else if (ecoPoints == 101)
        {
            ecoLevel = 5;
        }
    }

    public void Requierements()
    {
        //Afficher la bonne animation et panel selon ce qu'il manque pour que l'habitant se mette à travailler
        Debug.Log(isHoused & hasWorkplace);
    }
}

public class Bucheron : HabitantBehaviour
{
    public int chauffageNeed;
}
