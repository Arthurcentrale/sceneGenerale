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
    Maladie maladie;
    Boulangerie boulangerie;
    MoulinVent moulinVent;
    MoulinEau moulinEau;

    void Start()
    {
        planter = GameObject.Find("Ferme").GetComponent<Planter>();
        agri = GameObject.Find("Ferme").GetComponent<Agri>();
        maladie=GameObject.Find("Game Manager").GetComponent<Maladie>();

        repousse = GameObject.Find("Terrain").GetComponent<scriptRepousse>();
        boulangerie = GameObject.FindGameObjectWithTag("Boulangerie").GetComponent<Boulangerie>();
        moulinVent = GameObject.FindGameObjectWithTag("MoulinVent").GetComponent<MoulinVent>();
        moulinEau = GameObject.FindGameObjectWithTag("MoulinEau").GetComponent<MoulinEau>();
        StartCoroutine(Coroutine()); 
        StartCoroutine(CoroutineTroisHeures());
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



    IEnumerator AttenteTroisHeures() // On bloque jusqu'au prochain créneau de 3 heures
    {
        // current = DateTime.Now;  //Donne le jour et l'heure
        // //print(current);
        // tomorrow = current.AddDays(0.125).Date;   //AddDays marche prend en paramètre un double, donc normalement ça correspond bien à une attente de 3 heures
        // //print(tomorrow);
        // seconds = (tomorrow - current).TotalSeconds; //3600*3

        
        //yield return new WaitForSeconds((float) seconds);
        yield return new WaitForSeconds(5f); //L'actualisation de la maladie (on test à 5s)

        Fonctions3Heures();
    }




    IEnumerator Coroutine() //Coroutine principale qui boucle à chaque fois que l'on a attendu jusqu'à minuit indéfiniment
    {
        while(true)
        {
            yield return StartCoroutine(AttenteMinuit());
            //yield return StartCoroutine(AttenteTroisHeures());
        }
    }


    IEnumerator CoroutineTroisHeures(){//Coroutine qui active les fonctions toutes les 3 heures miam on se régale
        while(true){
            yield return StartCoroutine(AttenteTroisHeures());
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


        //Maladie
        var rand= UnityEngine.Random.Range(0f,1f);
        //print(rand);
        //print(maladie.maladieEnCours);
        if (rand<=0.03 && !(maladie.maladieEnCours)){ //3% de chance de déclencher une maladie si ya pas déjà une maladie en cours
            maladie.FonctionQuiSeDéclencheÀMinuit();
        }
    }

    void Fonctions3Heures(){
        // Toutes les fonctions à éxécuter toutes les 3 heures

        //Maladie
        //on vérifie d'abord que ya encore la maladie
        maladie.VérifierMaladie();
        if (maladie.maladieEnCours){
            maladie.ActualisationMaladie(maladie.essenceMalade);
        }
    }


    
}
