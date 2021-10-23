using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pecherie : MonoBehaviour
{
    //Ouvrir UI
    public GameObject panel;
    bool open;
    bool onPanel;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;
    public Button validation;

    //Gestion des paramètres globaux

    public HabitantBehaviour habitant;
    public GameManager gm;

    //Booleens pour savoir si la pecherie est habité / Si on a validé une nouvelle valeur à produire dans la journée

    public bool isOccupied,valider;

    // Valeur du poisson non validé ( Slider) , validé une fois qu'on a cliqué

    int QuantitePoissonNonValide,AnciennequantitePoisson;
    int QuantitePoisson;
    int i,j;
    bool limite;
    bool limite1, limite2, limite3;
    //Timer pour le délai a changer

    float timer = 0f;
    float delai = 5f; 

    //Affichage

    public Text textPoisson;
    public CompteurBouffe compteurbouffe;
    public Slider slider;

    void Start()
    {
        limite1 = false;
        limite2 = false;
        limite3 = false;
        i = 0;
        onPanel = false;
        open = false;
        isOccupied = false;
        QuantitePoisson = 0;
        AnciennequantitePoisson = 0;
        textPoisson.text = 0.ToString() + '/' + gm.socialManager.nombreAlimentsDifferents.ToString();
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
        compteurbouffe = compteurbouffe.GetComponent<CompteurBouffe>();
        validation = validation.GetComponent<Button>();
        validation.onClick.AddListener(ValiderValeur);
    }

    void Update()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
            mP = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (onPanel == false)
            {
                panel.SetActive(false);
                open = false;
                Deplacement.enMenu = false;
            }

            if (open == false && (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) + ((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) < 1))
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Pecherie"))

                { 
                    //Ajouter update valeur max du slider, etc..
                    panel.transform.position = new Vector2(mP.x + panel.GetComponent<RectTransform>().rect.width, mP.y);
                    panel.gameObject.SetActive(true);
                    animator.SetTrigger("ouverture1BulleCouper");
                    open = true;
                    Deplacement.enMenu = true;
                    slider.minValue = 0;
                    slider.maxValue = QuantiteMax(habitant);
                }

            }
            if(open == true)
            {
                QuantitePoissonNonValide = (int) slider.value;
            }


        }
        if (isOccupied) //Fonctionnement de la pecherie que si un pecheur est présent ++ Blocage jusqu'à minuit quand on valide
        {
            timer += Time.deltaTime;
            if (timer >= delai)
            {
                timer = 0f;
                QuantitePoisson++;
                textPoisson.text = QuantitePoisson.ToString();

            }
        }

    }
    public void ClickOnPanel()
    {
        onPanel = true;
    }

    public void ClickOutPanel()
    {
        onPanel = false;
    }

    //Fonction quand on clique sur le bouton du milieu
    public void RecupererPoisson()
    { 
        if (isOccupied && valider)
        {
            if(QuantitePoisson > 8)
            {
                i++; // On incrémente la valeur de jour où on peche plus de 8 poissons
                j = 0; // On remet a 0 le nombre de jour où on peche moins de 8 poissons
            }
            else if(isOccupied)
            {
                i = 0;
                j++;
            }
            // on doit enlever dans le compteur bouffe general l'ancienne valeur avant de rajouter la nouvelle
        }
        else // Si on a pas validé, on garde le meme nombre de poisson produit que la veille
        {
            CompteurBouffe.Data.NbrBouffe -= Mathf.Max(QuantitePoisson - MalusQualite(gm), 0); // On enleve la quantité de poisson choisie la veille
            UpdateQualiteEau(gm);
            CompteurBouffe.Data.NbrBouffe += Mathf.Max(QuantitePoisson - MalusQualite(gm), 0); // On regarde la malus sur la production après mise a jour de la qualité
            //On ne reinitialise aucune valeur car elle reste si le joueur décide de ne pas les modifier certains jours
            compteurbouffe.text.text = CompteurBouffe.Data.NbrBouffe.ToString();// On a pas changer la valeur de Quantité poisson par rapport à la veille, on doit juste vérifier la qualité de l'eau
        }
    }
    public void RendreOccupe() //A modifier quand le pecheur sera implémenté
    {
        // Pecherie.habitant = Gerard;
        isOccupied = !isOccupied;
    }
    int GetLevel(HabitantBehaviour habitant)
    {
        return habitant.ecoLevel;
    } //On recupère le level du pecheur

    void UpdateVariete(HabitantBehaviour habitants,GameManager gm) //A utiliser que quand on level up
    {
        int QE = gm.environnementManager.qualiteEau;
        if (habitant.ecoLevel == 1)
        {
            gm.socialManager.nombreAlimentsDifferents+=1;
        }
        if(habitant.ecoLevel == 3)
        {
            gm.socialManager.nombreAlimentsDifferents+=1;
        }
        if(habitant.ecoLevel == 5)
        {
            gm.socialManager.nombreAlimentsDifferents +=1;
        }
        if(!limite1 && QE < 60)
        {
            gm.socialManager.nombreAlimentsDifferents -= 1;
            limite1 = true;
            //Qualité max de l'eau -1 a voir dans le script gamemanager
        }
        if(limite1 && QE > 70)
        {
            gm.socialManager.nombreAlimentsDifferents += 1;
            limite1 = false;
        }
        if (!limite2 && QE < 40)
        {
            gm.socialManager.nombreAlimentsDifferents -= 1;
            limite2 = true;
            //Qualité max de l'eau -1 a voir dans le script gamemanager
        }
        if (limite2 && QE > 50)
        {
            gm.socialManager.nombreAlimentsDifferents += 1;
            limite2 = false;
        }
        if (!limite3 && QE < 20)
        {
            gm.socialManager.nombreAlimentsDifferents -= 1;
            limite3 = true;
            //Qualité max de l'eau -1 a voir dans le script gamemanager
        }
        if (limite3 && QE > 30)
        {
            gm.socialManager.nombreAlimentsDifferents += 1;
            limite3 = false;
        }
    }

    int QuantiteMax(HabitantBehaviour habitant)
    {
        limite = limitemax(limite);

        if (limite && GetLevel(habitant) >= 2)
        {
            return 8;
        }
        if(!limite && GetLevel(habitant) == 1)
        {
            return 5;
        }
        if (!limite && GetLevel(habitant) == 2)
        {
            return 10;
        }
        if (!limite && GetLevel(habitant) == 3)
        {
            return 15;
        }
        if (!limite && GetLevel(habitant) == 4)
        {
            return 20;
        }
        if (!limite && GetLevel(habitant) == 5)
        {
            return 25;
        }
        else
        {
            return 0;
        }

    } //Quantité max que le pecheur peut produire selon son level

    int MalusQualite(GameManager gm)
    {
        int QE = gm.environnementManager.qualiteEau;
        if(QE >= 80)
        {
            return 0;
        }
        if(QE < 80 && QE >= 65)
        {
            return -1;
        }
        if(QE<65 && QE >= 50)
        {
            return -2;
        }
        if(QE<50 && QE >= 35)
        {
            return -3;
        }
        if(QE<35 && QE >= 20)
        {
            return -4;
        }
        if(QE<20 && QE >= 5)
        {
            return -5;
        }
        else
        {
            return -6;
        }
    } //Malus sur la production selon la qualité de l'eau


    // Fonction pour modifier la qualité max de l'eau 


    void ValiderValeur()    // Fonction sur bouton quand on valide la quantité qu'on veut produire
    {
        QuantitePoisson = QuantitePoissonNonValide;
        valider = true;
        StartCoroutine(Coroutine());
        //Lancer le blocage de la valeur jusqu'à minuit
    }

    bool  limitemax(bool limite)
    {
        if (!limite && i >= 5)
        {
            return true;
        }
        else if (limite && j >= 5)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    void FonctionMinuit() //Fonction à lancer à minuit tous les jours qui permet de pouvoir modifier la valeur qu'on a validé le jour d'avant
    {
        if (valider)
        {
            valider = false;
            AnciennequantitePoisson = QuantitePoisson;
        }
        else
        {
            RecupererPoisson();
            AnciennequantitePoisson = QuantitePoisson;
        }
    }

    IEnumerator Coroutine() // On bloque jusqu'a minuit
    {
        validation.interactable = false;
        yield return new WaitForSeconds(10); //Pour test que tout marche
        /*DateTime current = DateTime.Now; //Vrai mécanique minuit
        DateTime tomorrow = current.AddDays(1).Date; 

        double seconds = (tomorrow - current).TotalSeconds;
        yield return new WaitForSeconds((float) seconds);*/
        FonctionMinuit();
        validation.interactable = true;
    }
}
