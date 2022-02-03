using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agri : MonoBehaviour
{
    //dimensions maxi du champ de parcelles autour de la ferme
    public static int xNbrParcelles;
    public static int yNbrParcelles;

    public GameObject panelLabourage;
    public GameObject panelPlantage;
    public GameObject panelEngrais;

    public GameObject boutonBle;
    public GameObject boutonMais;
    public GameObject boutonSalade;
    public GameObject boutonTomate;
    public GameObject boutonRaisin;

    private Animator animatorLivreActivite;

    public int niveauAgriculteur;

    public Player player;
    public bool menuOuvert;

    void Start()
    {
        xNbrParcelles = 7;
        yNbrParcelles = 5;

        boutonBle = panelPlantage.transform.GetChild(1).gameObject;
        boutonMais = panelPlantage.transform.GetChild(2).gameObject;
        boutonSalade = panelPlantage.transform.GetChild(3).gameObject;
        boutonTomate = panelPlantage.transform.GetChild(4).gameObject;
        boutonRaisin = panelPlantage.transform.GetChild(5).gameObject;

        animatorLivreActivite = GameObject.Find("LivreFerme").GetComponent<Animator>();

        //niveauAgriculteur = 2;
        player = GameObject.Find("Principal_OK").GetComponent<Player>();
        menuOuvert = false;

        MajNiveau();
    }

    void Update()
    {
        if (menuOuvert)
        {
            player.uiInventory.FermeBoutonInventaire();
        }
        else
        {
            player.uiInventory.AfficheBoutonInventaire();
        }
    }

    public void MajNiveau()
    {
        Planter.culture = niveauAgriculteur;

        switch (niveauAgriculteur)
        {
            case 1:
                Labourage.nbreParcellesPlacables = 5;
                Planter.capaciteTravail = 50;
                boutonBle.SetActive(true);
                break;
            case 2:
                Labourage.nbreParcellesPlacables = 10;
                Planter.capaciteTravail = 100;
                boutonBle.SetActive(true);
                boutonMais.SetActive(true);
                break;
            case 3:
                Labourage.nbreParcellesPlacables = 15;
                Planter.capaciteTravail = 200;
                boutonBle.SetActive(true);
                boutonMais.SetActive(true);
                boutonSalade.SetActive(true);
                break;
            case 4:
                Labourage.nbreParcellesPlacables = 20;
                Planter.capaciteTravail = 350;
                boutonBle.SetActive(true);
                boutonMais.SetActive(true);
                boutonSalade.SetActive(true);
                boutonTomate.SetActive(true);
                break;
            default:
                Labourage.nbreParcellesPlacables = 25;
                Planter.capaciteTravail = 500;
                boutonBle.SetActive(true);
                boutonMais.SetActive(true);
                boutonSalade.SetActive(true);
                boutonTomate.SetActive(true);
                boutonRaisin.SetActive(true);
                break;
        }
    }

    public void EntreeLabourage()
    {
        Labourage lab = GameObject.Find("Ferme").GetComponent<Labourage>();
        panelLabourage.SetActive(true);
        lab.enabled = true;
        lab.MajPrefabsLabourage();

        animatorLivreActivite.SetTrigger("Selected");
        menuOuvert = true;
        this.GetComponent<Ferme>().open = true;
        this.GetComponent<Ferme>().panel.SetActive(false);
        Camera.main.fieldOfView = 70;
    }

    public void EntreePlantage()
    {
        Planter plan = GameObject.Find("Ferme").GetComponent<Planter>();
        panelPlantage.SetActive(true);
        plan.enabled = true;

        animatorLivreActivite.SetTrigger("Selected");
        menuOuvert = true;
        this.GetComponent<Ferme>().open = true;
        this.GetComponent<Ferme>().panel.SetActive(false);
        Camera.main.fieldOfView = 70;
    }

    public void EntreeEngrais()
    {
        Planter plan = GameObject.Find("Ferme").GetComponent<Planter>();
        panelEngrais.SetActive(true);
        plan.enabled = true;
        Planter.modeEngrais = true;

        animatorLivreActivite.SetTrigger("Selected");
        menuOuvert = true;
        this.GetComponent<Ferme>().open = true;
        this.GetComponent<Ferme>().panel.SetActive(false);
        Camera.main.fieldOfView = 70;
    }
}
