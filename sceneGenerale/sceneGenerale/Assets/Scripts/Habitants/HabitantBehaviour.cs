using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantBehaviour : MonoBehaviour
{
    //variables générales :
    public Vector3 mairiePosition = new Vector3(0, 0, 0); //donne l'endroit où les habitants se rendent quand ils sont recrutables et qu'ils ne sont pas encore logés

    public string nom;

    //image
    public Sprite image;

    //besoins et déchets
    public float food_qty;
    public float food_vrty;
    public float waste;

    //niveau d'écosensibilisation
    public float ecopoints;

    //statut dans l'aventure
    public bool isVillager = false;
    public bool readyToWork = false;

    //conditions = bâtiments nécessaires pour qu'il puisse travailler et autres


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goToMairie();
        
    }

    //fonction dépalçant un personnage disponible mais pas prêt à travailler près de la mairie
    void goToMairie()
    {
        if (isVillager && !readyToWork)
        {
            transform.position = mairiePosition;
        }
    }
    
}
