using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    DateTime current;
    DateTime tomorrow;
    double seconds;

    Planter planter;
    Agri agri;
    scriptRepousse repousse;
    Boulangerie boulangerie;
    MoulinVent moulinVent;
    MoulinEau moulinEau;

    void Start()
    {
        planter = GameObject.Find("Ferme").GetComponent<Planter>();
        agri = GameObject.Find("Ferme").GetComponent<Agri>();

        repousse = GameObject.Find("Terrain").GetComponent<scriptRepousse>();
        boulangerie = GameObject.FindGameObjectWithTag("Boulangerie").GetComponent<Boulangerie>();
        moulinVent = GameObject.FindGameObjectWithTag("MoulinVent").GetComponent<MoulinVent>();
        moulinEau = GameObject.FindGameObjectWithTag("MoulinEau").GetComponent<MoulinEau>();
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
        //Debug.Log("TEST MAJ DE MINUIT");

        //Agri
        /*
        agri.MajNiveau();
        planter.enabled = true;
        planter.ToutesMAJ();
        planter.enabled = false;
        */

        /*
            boulangerie.FonctionMinuit();
            moulinVent.FonctionMinuit();
            moulinEau.FonctionMinuit();
         */
        // Repousse
        //repousse.majMinuit();
    }
}
