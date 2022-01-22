using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironnementManager : MonoBehaviour
{
    public static EnvironnementManager instance;

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


    public float qualiteAir;
    public float qualiteEau;
    public float qualiteSol;

    public float maxQE = 100f;

    private void Start()
    {
        qualiteAir = 100f;
        qualiteEau = maxQE;
        qualiteSol = 100f;
    }

    public void MajEnviro()
    {

    }
}
