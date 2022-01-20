using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    /*
    //variables globales de gestion
    public float globalFoodQuantity;
    public float globalFoodVariety;
    public float globalWaste;
    */

    public float score;
    private bool victoire;

    private void Start()
    {
        score = 0f;
        victoire = false;
    }

    private void MajScore()
    {
        //On met a jour les autres managers
        SocialManager.instance.MajSocial();
        EnvironnementManager.instance.MajEnviro();
        score = EnvironnementManager.instance.qualiteAir + EnvironnementManager.instance.qualiteEau + EnvironnementManager.instance.qualiteSol + SocialManager.instance.ecoSensibilisation + SocialManager.instance.qualiteDeVie;
    }
}