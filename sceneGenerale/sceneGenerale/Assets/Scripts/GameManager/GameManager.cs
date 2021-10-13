using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
    //variables globales de gestion
    public float globalFoodQuantity;
    public float globalFoodVariety;
    public float globalWaste;
    */

    public int score { get; set; }
    private bool victoire;

    //On initialise toute les instances uniques (singletons) des différents managers
    SocialManager socialManager = new SocialManager(0,0,0);
    public EnvironnementManager environnementManager = new EnvironnementManager(0,0,0);
    public DeveloppementManager developpementManager = new DeveloppementManager(0);
    //public HabitantManager habitantManager = new HabitantManager(new List<GameObject>());

    private void MajScore()
    {
        //On met a jour les autres managers
        socialManager.MajSocial();
        environnementManager.MajEnviro();
        this.score = environnementManager.qualiteAir + environnementManager.qualiteEau + environnementManager.qualiteSol + socialManager.ecoSensibilisation + socialManager.qualiteDeVie;
    }
}
