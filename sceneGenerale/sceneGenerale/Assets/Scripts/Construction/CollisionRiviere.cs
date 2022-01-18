using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRiviere : MonoBehaviour
{

    private GameObject pecherie;
    public GameObject detecteurTerre;
    public bool autorisation = false;
    // Start is called before the first frame update

    /*
    void Start()
    {
        if (name == "DetecteurRiviere")
        {
            pecherie = GameObject.Find("nouvellePêcherie").transform.GetChild(0).gameObject;
            detecteurTerre = GameObject.Find("nouvellePêcherie").transform.GetChild(1).gameObject;
        }
        else 
        {
            pecherie = GameObject.Find("nouvelleMoulinAEau").transform.GetChild(0).gameObject;
            detecteurTerre = GameObject.Find("nouvelleMoulinAEau").transform.GetChild(1).gameObject;
        }
        
        pecherie.GetComponent<SpriteRenderer>().color = Color.red;
        
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.IndexOf("RiverTrigger", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            autorisation = true;
            StartCoroutine(attendrelautre());
            gereCouleur(autorisation, detecteurTerre.GetComponent<CollisionTerre>().autorisation);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.IndexOf("RiverTrigger", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            autorisation = false;
            StartCoroutine(attendrelautre());
            gereCouleur(autorisation, detecteurTerre.GetComponent<CollisionTerre>().autorisation);
        }

    }

    private void gereCouleur(bool auto1, bool auto2)
    {
        if (auto1 && auto2)
        {
            pecherie.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.5f);
        }

        else
        {
            pecherie.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    IEnumerator attendrelautre()
    {
        yield return new WaitForSeconds(0.1f);
    }*/

    
}

