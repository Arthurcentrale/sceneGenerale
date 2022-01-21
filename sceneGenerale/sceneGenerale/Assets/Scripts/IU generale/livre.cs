using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class livre : MonoBehaviour
{

    //animators du livre fermé et ouvert
    private Animator animatorLivreFerme;
    private Animator animatorLivreOuvert;

    //éléments principaux de l'IU
    private GameObject boutonConstruction;
    private GameObject boutonInformations;
    private GameObject boutonMissions;
    private GameObject boutonProgression;
    private GameObject menuConstruPageDroite;
    private GameObject menuConstruPageGauche;
    private GameObject menuMissionsPageGauche;
    private GameObject menuMissionsPageDroite;
    private GameObject menuInfosPageGauche;
    private GameObject fermetureBouton;
    private GameObject menuConstruction;

    //éléments plus spécifiques
    private Image nom;
    private Image description;
    private Image ressource1;
    private Image ressource2;
    private Image qteR1;
    private Image qteR2;

    // base de données des images
    public Sprite nomChaumiere;
    public Sprite nomEtabli;
    public Sprite nomFerme;
    public Sprite nomPecherie;
    public Sprite nomMoulinEau;
    public Sprite nomBoulangerie;
    public Sprite nomMoulinVent;
    public Sprite nomPuit;
    public Sprite nomVerrerie;
    public Sprite nomDecharge;

    public Sprite descriptionChaumiere;
    public Sprite descriptionEtabli;
    public Sprite descriptionFerme;
    public Sprite descriptionPecherie;
    public Sprite descriptionMoulinEau;
    public Sprite descriptionBoulangerie;
    public Sprite descriptionMoulinVent;
    public Sprite descriptionPuit;
    public Sprite descriptionVerrerie;
    public Sprite descriptionDecharge;

    //nombres
    public Sprite un;
    public Sprite deux;
    public Sprite trois;
    public Sprite quatre;
    public Sprite cinq;
    public Sprite six;
    public Sprite sept;
    public Sprite huit;
    public Sprite neuf;
    public Sprite dix;
    public Sprite onze;
    public Sprite douze;
    public Sprite treize;
    public Sprite quatorze;
    public Sprite quinze;


    //éléments autres
    private string constructeur;
    public GameObject emptydeArthur;
    private BoutonMenu2 fonctionsConstru;
    private bool constru;
    private bool mission;
    private bool info;
    private ScriptATHBatis athBati;

    public GameObject boutonInventaire;
    public GameObject favorisInventaire;

    // Start is called before the first frame update
    void Start()
    {
        animatorLivreFerme = GameObject.Find("LivreFerme").GetComponent<Animator>();
        animatorLivreOuvert = GameObject.Find("LivreOuvert").GetComponent<Animator>();
        boutonConstruction = GameObject.Find("ConstructionBouton");
        boutonInformations = GameObject.Find("InformationBouton");
        boutonMissions = GameObject.Find("MissionBouton");
        boutonProgression = GameObject.Find("ProgressionBouton");

        menuConstruPageDroite = GameObject.Find("menuConstructionPageDroite");
        menuConstruPageGauche = GameObject.Find("menuConstructionPageGauche");
        menuMissionsPageGauche = GameObject.Find("menuMissionsPageGauche");
        menuMissionsPageDroite = GameObject.Find("menuMissionsPageDroite");
        menuInfosPageGauche = GameObject.Find("menuInfosPageGauche");
        menuConstruction = GameObject.Find("menuConstruction");

        fermetureBouton = GameObject.Find("FermetureBouton");
        nom = GameObject.Find("nom").GetComponent<Image>();
        description = GameObject.Find("description").GetComponent<Image>();
        ressource1 = GameObject.Find("itemOne").GetComponent<Image>();
        ressource2 = GameObject.Find("itemTwo").GetComponent<Image>();
        qteR1 = GameObject.Find("numberOne").GetComponent<Image>();
        qteR2 = GameObject.Find("numberTwo").GetComponent<Image>();
        fonctionsConstru = emptydeArthur.GetComponent<BoutonMenu2>();
        athBati = GameObject.Find("Camera").GetComponent<ScriptATHBatis>();
    }

    // Update is called once per frame
    public void ouvertureLivre()
    {
        animatorLivreFerme.SetTrigger("Selected");
        animatorLivreOuvert.SetTrigger("Selected");
        boutonConstruction.SetActive(true);
        boutonMissions.SetActive(true);
        boutonProgression.SetActive(true);
        boutonInformations.SetActive(true);
        menuConstruPageDroite.SetActive(false);
        menuConstruction.SetActive(false);
        menuConstruPageGauche.SetActive(false);
        menuMissionsPageGauche.SetActive(false);
        menuMissionsPageDroite.SetActive(false);
        menuInfosPageGauche.SetActive(false);
        fermetureBouton.SetActive(true);
    }

    public void ouvertureMenuConstru()
    {
        menuConstruPageGauche.SetActive(true);
        menuConstruction.SetActive(true);
        ouvreNimporteQuelMenu();
        constru = true;
    }

    public void ouvertureMenuMissions()
    {
        ouvreNimporteQuelMenu();
        menuMissionsPageGauche.SetActive(true);
        mission = true;
    }

    public void ouvertureMenuInfos()
    {
        ouvreNimporteQuelMenu();
        menuInfosPageGauche.SetActive(true);
        info = true;
    }

    public void affichagePageDroiteMenuConstru()
    {
        menuConstruPageDroite.SetActive(true);
    }

    public void fermetureLivre()
    {
        animatorLivreFerme.SetTrigger("Return");
        if (constru || mission || info) 
        {
            animatorLivreOuvert.SetTrigger("FermetureComplete");
        }
        else animatorLivreOuvert.SetTrigger("Fermeture");

        menuConstruPageGauche.SetActive(false);
        menuConstruction.SetActive(false);
        menuMissionsPageGauche.SetActive(false);
        menuMissionsPageDroite.SetActive(false);
        fermetureBouton.SetActive(false);
        constru = false;
        mission = false;
        info = false;
    }

 


    public void construire()
    {
        boutonInventaire.SetActive(false);
        favorisInventaire.SetActive(false);
        if (constructeur == "chaumiere") fonctionsConstru.ConstruireChaumièreDepuisMenuConstruction();
        else if (constructeur == "etabli") print("pas encore craftable");
        else if (constructeur == "ferme") fonctionsConstru.ConstruireFermeDepuisMenuConstruction();
        else if (constructeur == "pecherie") fonctionsConstru.ConstruirePêcherieDepuisMenuConstruction();
        else if (constructeur == "moulinEau") fonctionsConstru.ConstruireMoulinAEauDepuisMenuConstruction();
        else if (constructeur == "boulangerie") fonctionsConstru.ConstruireBoulangerieDepuisMenuConstruction();

    }

    public void chaumiere()
    {
        affichagePageDroiteMenuConstru();
        print("chaumiere");
        nom.sprite = nomChaumiere;
        description.sprite = descriptionChaumiere;
        constructeur = "chaumiere";
        selectivite(athBati.nombreItemOneChaumiere, athBati.nombreItemTwoChaumiere);

    }

    public void etabli()
    {
        if (!athBati.etabliConstruit)
        {
            affichagePageDroiteMenuConstru();
            print("etabli");
            nom.sprite = nomEtabli;
            description.sprite = descriptionEtabli;
            constructeur = "etabli";
        }
        
    }

    public void ferme()
    {
        
        if (!athBati.fermeConstruit)
        {
            affichagePageDroiteMenuConstru();
            nom.sprite = nomFerme;
            description.sprite = descriptionFerme;
            constructeur = "ferme";
            selectivite(athBati.nombreItemOneFerme, athBati.nombreItemTwoFerme);
        }
        
    }

    public void pecherie()
    {
        if (!athBati.pecherieConstruit)
        {
            affichagePageDroiteMenuConstru();
            nom.sprite = nomPecherie;
            description.sprite = descriptionPecherie;
            constructeur = "pecherie";
            selectivite(athBati.nombreItemOnePecherie, athBati.nombreItemTwoPecherie);
        }
        
    }

    public void moulinEau()
    {
        if (!athBati.moulinEauConstruit)
        {
            affichagePageDroiteMenuConstru();
            nom.sprite = nomMoulinEau;
            description.sprite = descriptionMoulinEau;
            constructeur = "moulinEau";
            selectivite(athBati.nombreItemOneMoulinEau, athBati.nombreItemTwoMoulinEau);
        }
        
    }

    public void boulangerie()
    {
        if (!athBati.boulangerieConstruit)
        {
            affichagePageDroiteMenuConstru();
            nom.sprite = nomBoulangerie;
            description.sprite = descriptionBoulangerie;
            constructeur = "boulangerie";
            selectivite(athBati.nombreItemOneBoulangerie, athBati.nombreItemTwoBoulangerie);
        }
        
    }

    public void moulinVent()
    {
        if (!athBati.moulinVentConstruit)
        {
            affichagePageDroiteMenuConstru();
            nom.sprite = nomMoulinVent;
            description.sprite = descriptionMoulinVent;
            constructeur = "moulinVent";
            selectivite(athBati.nombreItemOneMoulinVent, athBati.nombreItemTwoMoulinVent);
        }
        
    }

    public void puit()
    {
        if (athBati.puitConstruit)
        {
            affichagePageDroiteMenuConstru();
            nom.sprite = nomPuit;
            description.sprite = descriptionPuit;
            constructeur = "puit";
            selectivite(athBati.nombreItemOnePuit, athBati.nombreItemTwoPuit);
        }
        
    }

    public void verrerie()
    {
        if (athBati.verrerieConstruit)
        {
            affichagePageDroiteMenuConstru();
            nom.sprite = nomVerrerie;
            description.sprite = descriptionVerrerie;
            constructeur = "verrerie";
            selectivite(athBati.nombreItemOneVerrerie, athBati.nombreItemTwoVerrerie);
        }
        
    }

    public void decharge()
    {
        if (athBati.dechargeConstruit)
        {
            affichagePageDroiteMenuConstru();
            nom.sprite = nomDecharge;
            description.sprite = descriptionDecharge;
            constructeur = "decharge";
            selectivite(athBati.nombreItemOneDecharge, athBati.nombreItemTwoDecharge);
        }
       
    }


    public void selectivite(int premier, int second)
    {
        if (premier == 1) qteR1.sprite = un;
        else if (premier == 2) qteR1.sprite = deux;
        else if (premier == 3) qteR1.sprite = trois;
        else if (premier == 4) qteR1.sprite = quatre;
        else if (premier == 5) qteR1.sprite = cinq;
        else if (premier == 6) qteR1.sprite = six;
        else if (premier == 7) qteR1.sprite = sept;
        else if (premier == 8) qteR1.sprite = huit;
        else if (premier == 9) qteR1.sprite = neuf;
        else if (premier == 10) qteR1.sprite = dix;
        else if (premier == 11) qteR1.sprite = onze;
        else if (premier == 12) qteR1.sprite = douze;
        else if (premier == 13) qteR1.sprite = treize;
        else if (premier == 14) qteR1.sprite = quatorze;
        else if (premier == 15) qteR1.sprite = quinze;

        if (second == 1) qteR2.sprite = un;
        else if (second == 2) qteR2.sprite = deux;
        else if (second == 3) qteR2.sprite = trois;
        else if (second == 4) qteR2.sprite = quatre;
        else if (second == 5) qteR2.sprite = cinq;
        else if (second == 6) qteR2.sprite = six;
        else if (second == 7) qteR2.sprite = sept;
        else if (second == 8) qteR2.sprite = huit;
        else if (second == 9) qteR2.sprite = neuf;
        else if (second == 10) qteR2.sprite = dix;
        else if (second == 11) qteR2.sprite = onze;
        else if (second == 12) qteR2.sprite = douze;
        else if (second == 13) qteR2.sprite = treize;
        else if (second == 14) qteR2.sprite = quatorze;
        else if (second == 15) qteR2.sprite = quinze;
    }

    private void ouvreNimporteQuelMenu()
    {
        animatorLivreOuvert.SetTrigger("OuvertureComplete");
        boutonConstruction.SetActive(false);
        boutonMissions.SetActive(false);
        boutonProgression.SetActive(false);
        boutonInformations.SetActive(false);
        fermetureBouton.SetActive(true);
    }

    
}
