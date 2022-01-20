using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloppementManager : MonoBehaviour
{
    public static DeveloppementManager instance;

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


    public int progression;
    public List<GameObject> listeBatiment;
    public int navireConstruit;

    private void Start()
    {
        progression = 0;
        listeBatiment = new List<GameObject>();
        navireConstruit = 0;
    }
}
