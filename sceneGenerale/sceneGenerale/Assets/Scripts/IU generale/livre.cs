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
    private GameObject fermetureBouton;

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

    public Sprite descriptionChaumiere;
    public Sprite descriptionEtabli;
    public Sprite descriptionFerme;
    public Sprite descriptionPecherie;
    public Sprite descriptionMoulinEau;
    public Sprite descriptionBoulangerie;

    //éléments autres
    private string constructeur;
    public GameObject emptydeArthur;
    private BoutonMenu2 fonctionsConstru;
    private bool constru;

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
        fermetureBouton = GameObject.Find("FermetureBouton");
        nom = GameObject.Find("nom").GetComponent<Image>();
        description = GameObject.Find("description").GetComponent<Image>();
        ressource1 = GameObject.Find("itemOne").GetComponent<Image>();
        ressource2 = GameObject.Find("itemTwo").GetComponent<Image>();
        qteR1 = GameObject.Find("numberOne").GetComponent<Image>();
        qteR2 = GameObject.Find("numberTwo").GetComponent<Image>();
        fonctionsConstru = emptydeArthur.GetComponent<BoutonMenu2>();
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
        fermetureBouton.SetActive(true);
    }

    public void ouvertureMenuConstru()
    {
        animatorLivreOuvert.SetTrigger("OuvertureComplete");
        boutonConstruction.SetActive(false);
        boutonMissions.SetActive(false);
        boutonProgression.SetActive(false);
        boutonInformations.SetActive(false);
        fermetureBouton.SetActive(true);
        constru = true;
    }

    public void affichagePageDroiteMenuConstru()
    {
        menuConstruPageDroite.SetActive(true);
    }

    public void fermetureLivre()
    {
        animatorLivreFerme.SetTrigger("Return");
        if (constru) 
        {
            animatorLivreOuvert.SetTrigger("FermetureComplete");
        }
        else animatorLivreOuvert.SetTrigger("Fermeture");


        fermetureBouton.SetActive(false);
        constru = false;
    }


    public void construire()
    {
        if (constructeur == "chaumiere") fonctionsConstru.ConstruireChaumièreDepuisMenuConstruction();
        else if (constructeur == "etabli") print("pas encore craftable");
        else if (constructeur == "ferme") fonctionsConstru.ConstruireFermeDepuisMenuConstruction();
        else if (constructeur == "pecherie") fonctionsConstru.ConstruirePêcherieDepuisMenuConstruction();
        else if (constructeur == "moulinEau") fonctionsConstru.ConstruireMoulinAEauDepuisMenuConstruction();
        else if (constructeur == "boulangerie") fonctionsConstru.ConstruireBoulangerieDepuisMenuConstruction();

    }

    public void chaumiere()
    {
        print("chaumiere");
        nom.sprite = nomChaumiere;
        description.sprite = descriptionChaumiere;
        constructeur = "chaumiere";
    }

    public void etabli()
    {
        print("etabli");
        nom.sprite = nomEtabli;
        description.sprite = descriptionEtabli;
        constructeur = "etabli";
    }

    public void ferme()
    {
        nom.sprite = nomFerme;
        description.sprite = descriptionFerme;
        constructeur = "ferme";
    }

    public void pecherie()
    {
        nom.sprite = nomPecherie;
        description.sprite = descriptionPecherie;
        constructeur = "pecherie";
    }

    public void moulinEau()
    {
        nom.sprite = nomMoulinEau;
        description.sprite = descriptionMoulinEau;
        constructeur = "moulinEau";
    }

    public void boulangerie()
    {
        nom.sprite = nomBoulangerie;
        description.sprite = descriptionBoulangerie;
        constructeur = "boulangerie";
    }
}
