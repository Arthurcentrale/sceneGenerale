using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    DateTime current;
    DateTime tomorrow;
    double seconds;

    void Start()
    {
        StartCoroutine(Coroutine());
    }

    IEnumerator AttenteMinuit() // On bloque jusqu'a minuit
    {
        current = DateTime.Now;  //Donne le jour et l'heure
        tomorrow = current.AddDays(1).Date;   
        seconds = (tomorrow - current).TotalSeconds;

        //yield return new WaitForSeconds((float) seconds);
        yield return new WaitForSeconds(5f); //Pour test que tout marche

        FonctionsMinuit();
    }

    IEnumerator Coroutine() //Coroutine principale qui boucle à chaque fois que l'on a attendu jusqu'à minuit indéfiniment
    {
        while(true)
        {
            yield return StartCoroutine(AttenteMinuit());
        }
    }

    void FonctionsMinuit()
    {
        //Toutes les fonctions à exécuter à minuit;
    }
}
