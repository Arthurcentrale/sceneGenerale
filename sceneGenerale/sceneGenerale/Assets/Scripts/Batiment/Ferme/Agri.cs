using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agri : MonoBehaviour
{
    public int niveauAgriculteur;

    //dimensions maxi du champ de parcelles autour de la ferme
    public static int xNbrParcelles;
    public static int yNbrParcelles;

    public GameObject panelLabourage;
    public GameObject panelPlantage;
    public GameObject panelEngrais;

    void Start()
    {
        xNbrParcelles = 7;
        yNbrParcelles = 5;
    }

    public void MajNiveau()
    {
        Planter.culture = niveauAgriculteur;

        if (niveauAgriculteur == 2)
        {
            Labourage.nbreParcellesPlacables = 10;
            Planter.capaciteTravail = 100;
        }
        else if (niveauAgriculteur == 3)
        {
            Labourage.nbreParcellesPlacables = 15;
            Planter.capaciteTravail = 200;
        }
        else if (niveauAgriculteur == 4)
        {
            Labourage.nbreParcellesPlacables = 20;
            Planter.capaciteTravail = 350;
        }
        else if (niveauAgriculteur > 4)
        {
            Labourage.nbreParcellesPlacables = 25;
            Planter.capaciteTravail = 500;
        }
    }

    public void EntreeLabourage()
    {
        Labourage lab = GameObject.Find("Ferme").GetComponent<Labourage>();
        panelLabourage.SetActive(true);
        lab.enabled = true;
        lab.MajPrefabsLabourage();
    }

    public void EntreePlantage()
    {
        Planter plan = GameObject.Find("Ferme").GetComponent<Planter>();
        panelPlantage.SetActive(true);
        plan.enabled = true;
    }

    public void EntreeEngrais()
    {
        Planter plan = GameObject.Find("Ferme").GetComponent<Planter>();
        panelEngrais.SetActive(true);
        plan.enabled = true;
        Planter.modeEngrais = true;
    }
}
