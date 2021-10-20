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

    //Gestion des paramètres globaux

    public HabitantBehaviour habitant;
    public GameManager gm;

    //Booleens pour savoir si la pecherie est habité / Si on a validé une nouvelle valeur à produire dans la journée

    public bool isOccupied,valider;

    // Valeur du poisson non validé ( Slider) , validé une fois qu'on a cliqué

    int QuantitePoissonNonValide;
    int QuantitePoisson;

    //Timer pour le délai a changer

    float timer = 0f;
    float delai = 5f; 

    //Affichage

    public Text textPoisson;
    public CompteurBouffe compteurbouffe;

    void Start()
    {
        onPanel = false;
        open = false;
        isOccupied = false;
        QuantitePoisson = 0;
        textPoisson.text = 0.ToString();
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
        compteurbouffe = compteurbouffe.GetComponent<CompteurBouffe>();
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
                }

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
        if (valider)
        {
            // on doit enlever dans le compteur bouffe general l'ancienne valeur avant de rajouter la nouvelle
        }
        else
        {
            UpdateQualiteEau(gm);
            CompteurBouffe.Data.NbrBouffe += Mathf.Max(QuantitePoisson - MalusQualite(gm), 0);
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
        if(habitant.ecoLevel == 1)
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
    }

    void UpdateQualiteEau(GameManager gm) //Ajouter les bonnes fonctionnalités
    {
        int QE = gm.environnementManager.qualiteEau;
        


    }
    int QuantiteMax(HabitantBehaviour habitant)
    {
        if(GetLevel(habitant) == 1)
        {
            return 5;
        }
        if (GetLevel(habitant) == 2)
        {
            return 10;
        }
        if (GetLevel(habitant) == 3)
        {
            return 15;
        }
        if (GetLevel(habitant) == 4)
        {
            return 20;
        }
        if (GetLevel(habitant) == 5)
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

    //Slider pour l'incrémentation

    // Fonction pour modifier la qualité max de l'eau 

    //Fonction pour check le truc des 5 jours

    void ValiderValeur()    // Fonction sur bouton quand on valide la quantité qu'on veut produire
    {
        QuantitePoisson = QuantitePoissonNonValide;
        valider = true;
        //Lancer le blocage de la valeur jusqu'à minuit
    }
    void FonctionMinuit() //Fonction à lancer à minuit tous les jours qui permet de pouvoir modifier la valeur qu'on a validé le jour d'avant
    {
        // Le bouton est de nouveau activable
        valider = false;
    }
}
