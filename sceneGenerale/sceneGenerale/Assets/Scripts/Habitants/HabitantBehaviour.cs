using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantBehaviour : MonoBehaviour
{
    //variables générales :
    public Vector3 originalPosition;
    public Vector3 mairiePosition; //donne l'endroit où les habitants se rendent quand ils sont recrutables et qu'ils ne sont pas encore logés

    public string nom;

    //image
    public Sprite image;

    //besoins et déchets
    public float foodQuantity;
    public float foodVariety;
    public float waste;

    //niveau d'écosensibilisation
    public float ecoPoints;
    public int ecoLevel;
    private int ecoPointsMax = 101;

    //statut dans l'aventure
    public bool isVillager = false;

    //conditions = bâtiments nécessaires pour qu'il puisse travailler et autres


    // Start is called before the first frame update
    void Start()
    {
        
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
}

public class Bucheron : HabitantBehaviour
{
    public int chauffageNeed;
}
