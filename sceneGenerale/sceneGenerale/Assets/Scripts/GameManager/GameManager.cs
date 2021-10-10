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

    SocialManager socialManager = new SocialManager(0,0,0);
    EnvironnementManager environnementManager = new EnvironnementManager(0,0,0);
    DeveloppementManager developpementManager = new DeveloppementManager(0);

    private void MajScore()
    {
        this.score = environnementManager.qualiteAir + environnementManager.qualiteEau + environnementManager.qualiteSol + socialManager.ecoSensibilisation + socialManager.qualiteDeVie;
    }
}
