using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class HabitantManager : ScriptableObject
{
    //liste de tous les habitants du jeu
    public List<GameObject> habitants;

    /*
    public HabitantManager(List<GameObject> _habitants)
    {
        this.habitants = _habitants;
    }
    */
    private void Start()
    {

        GameObject habitant = GameObject.Find("habitants") ;
        foreach(Transform child in habitant.transform)
        {
            habitants.Add(child.gameObject);
        }
        //Debug.Log((habitants.Count));
    }

    //fonction qu'on appelle à chaque fois qu'il y a un nouvel habitant qui rejoint la communauté (et dont la variable isVillager vaut true)

    void ActiverHabitant(string habitantname)
    {
        foreach(GameObject habitant in habitants)
        {
            if (habitant.CompareTag(habitantname))
            {
                habitant.SetActive(true);
            }
        }
    }

    void DesactiverHabitant(string habitantname)
    {
        foreach (GameObject habitant in habitants)
        {
            if (habitant.CompareTag(habitantname))
            {
                habitant.SetActive(false);
            }
        }
    }



    void addHabitant(GameObject newHabitant)
    {
        habitants.Add(newHabitant);
    }
}


