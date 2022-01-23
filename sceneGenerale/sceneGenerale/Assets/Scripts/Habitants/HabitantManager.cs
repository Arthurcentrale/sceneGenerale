using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class HabitantManager : MonoBehaviour
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
            if (child.gameObject.activeInHierarchy == true)
            {
                child.gameObject.GetComponent<HabitantBehaviour>().isVillager = true;
                habitants.Add(child.gameObject);
            }
            else child.gameObject.GetComponent<HabitantBehaviour>().isVillager = false;
        }

        //Debug.Log((habitants.Count));
    }

    //fonction qu'on appelle à chaque fois qu'il y a un nouvel habitant qui rejoint la communauté (et dont la variable isVillager vaut true)

    void ActiverHabitantsurIle(string habitantname)
    {
        foreach(GameObject habitant in habitants)
        {
            if (habitant.CompareTag(habitantname))
            {
                habitant.SetActive(true);
                habitant.GetComponent<HabitantBehaviour>().isVillager = true;
                habitant.transform.position = habitant.GetComponent<HabitantBehaviour>().mairiePosition;
            }
        }
    }
    
    void AllerTravail(string habitantname)
    {
        foreach (GameObject habitant in habitants)
        {
            if (habitant.CompareTag(habitantname))
            {
                habitant.transform.position = habitant.GetComponent<HabitantBehaviour>().originalPosition;
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


