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
        UpdateListeBatiments();
        navireConstruit = 0;
    }

    public void UpdateListeBatiments()
    {
        listeBatiment = new List<GameObject>();
        foreach (Transform child in GameObject.Find("Batiments").transform)
        {
            GameObject go = child.gameObject;
            if (go.activeSelf && go.name != "PrefabDesBâtiments") listeBatiment.Add(go);
        }
    }
}
