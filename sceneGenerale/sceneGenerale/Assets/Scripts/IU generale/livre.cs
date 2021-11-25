using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class livre : MonoBehaviour
{
    private Animator animatorLivreFerme;
    private Animator animatorLivreOuvert;
    private GameObject boutonConstruction;
    private GameObject menuConstruPageDroite;
    private GameObject fermetureBouton;

    // Start is called before the first frame update
    void Start()
    {
        animatorLivreFerme = GameObject.Find("LivreFerme").GetComponent<Animator>();
        animatorLivreOuvert = GameObject.Find("LivreOuvert").GetComponent<Animator>();
        boutonConstruction = GameObject.Find("ConstructionBouton");
        menuConstruPageDroite = GameObject.Find("menuConstructionPageDroite");
        fermetureBouton = GameObject.Find("FermetureBouton");
    }

    // Update is called once per frame
    public void ouvertureLivre()
    {
        animatorLivreFerme.SetTrigger("Selected");
        animatorLivreOuvert.SetTrigger("Selected");
        boutonConstruction.SetActive(true);
        menuConstruPageDroite.SetActive(false);
        fermetureBouton.SetActive(false);
    }

    public void ouvertureMenuConstru()
    {
        animatorLivreOuvert.SetTrigger("OuvertureComplete");
        boutonConstruction.SetActive(false);
        fermetureBouton.SetActive(true);
    }

    public void affichagePageDroiteMenuConstru()
    {
        menuConstruPageDroite.SetActive(true);
    }

    public void fermetureLivre()
    {
        animatorLivreOuvert.SetTrigger("FermetureComplete");
        fermetureBouton.SetActive(false);
        animatorLivreFerme.SetTrigger("Return");
    }
}
