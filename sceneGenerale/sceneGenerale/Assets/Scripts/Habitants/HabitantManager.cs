﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantManager : MonoBehaviour
{
    //liste de tous les habitants du jeu
    public List<GameObject> habitants;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //fonction qu'on appelle à chaque fois qu'il y a un nouvel habitant qui rejoint la communauté (et dont la variable isVillager vaut true)
    void addHabitant(GameObject newHabitant)
    {
        habitants.Add(newHabitant);
    }
}

