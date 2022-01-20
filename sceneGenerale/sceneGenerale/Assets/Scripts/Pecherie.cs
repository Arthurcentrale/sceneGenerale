using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pecherie : MonoBehaviour
{
    //Ouvrir UI
    public GameObject panel;
    public GameObject menuinfo;
    public GameObject choixhabitant;
    bool open;
    bool onPanel;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;
    public Button validation;
    public Text textslider;

    //Gestion des paramètres globaux

    public HabitantBehaviour habitant;


    //Booleens pour savoir si la pecherie est habité / Si on a validé une nouvelle valeur à produire dans la journée

    public bool isOccupied,valider;

    // Valeur du poisson non validé ( Slider) , validé une fois qu'on a cliqué

    int QuantitePoissonNonValide,AnciennequantitePoisson;
    int QuantitePoisson;
    int i,j;
    int nbrdiminutionQE; // On regarde combien de fois la qualité max de l'eau à diminué
    int levelactuel;
    bool limite;
    bool limite1, limite2, limite3;
    //Timer pour le délai a changer

    float timer = 0f;
    float delai = 5f; 

    //Affichage

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
        nbrdiminutionQE = 0;
        AnciennequantitePoisson = 0;
        textslider.text = 0.ToString();
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
        compteurbouffe = compteurbouffe.GetComponent<CompteurBouffe>();
        validation = validation.GetComponent<Button>();
        //validation.onClick.AddListener(ValiderValeur);
    }

    void Update()
    {
        slider.minValue = 0;
        slider.maxValue = QuantiteMax(habitant);
        AffichageSlider();

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
            mP = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (onPanel == false)
            {
                panel.SetActive(false);
                open = false;
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
                    

                }

            }
            if(open == true)
            {
                QuantitePoissonNonValide = (int) slider.value;
            }


        }
        
        //¨Partie si on ne valide pas dans la journée

        if(isOccupied && habitant != null && habitant.isHoused && valider == false)
        {
            timer += Time.deltaTime;
            if (timer >= delai)
            {
                FonctionMinuit();
                timer = 0;
            }

        }
        /*if (isOccupied) //Fonctionnement de la pecherie que si un pecheur est présent ++ Blocage jusqu'à minuit quand on valide
        {
            timer += Time.deltaTime;
            if (timer >= delai)
            {
                timer = 0f;
                QuantitePoisson++;
                textPoisson.text = QuantitePoisson.ToString();

            }
        }*/

    }
    public void ClickOnPanel()
    {
        onPanel = true;
        Deplacement.enMenu = true;
    }

    public void ClickOutPanel()
    {
        onPanel = false;
        Deplacement.enMenu = false;
    }

    public void AffichageSlider()
    {
        textslider.text = slider.value.ToString();
    }

    //Fonction quand on clique sur le bouton du milieu
    public void RecupererPoisson()
    {
        if (isOccupied)
        {
            if (QuantitePoisson  > 20)
            {
                i++; // On incrémente la valeur de jour où on peche plus de 8 poissons
                j = 0; // On remet a 0 le nombre de jour où on peche moins de 8 poissons
            }
            else if(QuantitePoisson - MalusQualite() < 4)
            {
                i = 0;
                j++;
            }
            // on doit enlever dans le compteur bouffe general l'ancienne valeur avant de rajouter la nouvelle
            //QuantitéPoisson vaut la valeur de la veille si on ne valide pas de nouvelle valeur donc c'est bon
            CompteurBouffe.Data.NbrBouffe -= AnciennequantitePoisson - MalusQualite();
            UpdateQE();
            UpdateVariete();
            CompteurBouffe.Data.NbrBouffe += QuantitePoisson - MalusQualite();
            //On ne reinitialise aucune valeur car elle reste si le joueur décide de ne pas les modifier certains jours
            compteurbouffe.CBouffe.text = CompteurBouffe.Data.NbrBouffe.ToString();// On a pas changer la valeur de Quantité poisson par rapport à la veille, on doit juste vérifier la qualité de l'eau
            compteurbouffe.CompteurVariete.text = SocialManager.instance.nombreAlimentsDifferents.ToString();
            compteurbouffe.CompteurQualiteEau.text = EnvironnementManager.instance.qualiteEau.ToString("F1");
        }
        Debug.Log("qe" + EnvironnementManager.instance.qualiteEau);
        Debug.Log("qn" + SocialManager.instance.quantiteNourriture);
        Debug.Log("v" + SocialManager.instance.nombreAlimentsDifferents);

    }
    public void RendreOccupe() //A modifier quand le pecheur sera implémenté
    {
        GameObject gameObject1 = GameObject.Find("pêcheur");
        habitant = gameObject1.GetComponent<HabitantBehaviour>();
        habitant.hasWorkplace = true;
        isOccupied = !isOccupied;
        InitVariete(habitant);

        // Si non, on affiche que personne est disponible
    }
    int GetLevel(HabitantBehaviour habitant)
    {
        if (habitant != null) return habitant.ecoLevel;
        else return 0;
    } //On recupère le level du pecheur

    void UpdateVariete() //A utiliser que quand on level up
    {
        float QE = EnvironnementManager.instance.qualiteEau;
        if (habitant.ecoLevel == 3 && levelactuel < 3 )
        {
            SocialManager.instance.nombreAlimentsDifferents += 1;
        }
        if (habitant.ecoLevel == 5 && levelactuel < 5)
        {
            SocialManager.instance.nombreAlimentsDifferents += 1;
        }
        if (!limite1 && QE < 60 && SocialManager.instance.nombreAlimentsDifferents >1 )
        {
            SocialManager.instance.nombreAlimentsDifferents -= 1;
            limite1 = true;
            if(nbrdiminutionQE == 0)
            {
                EnvironnementManager.instance.maxQE -= 5;
                nbrdiminutionQE++;
            }
            //Qualité max de l'eau -5 a voir dans le script gamemanager
        }
        if(limite1 && QE > 70)
        {
            SocialManager.instance.nombreAlimentsDifferents += 1;
            limite1 = false;
        }
        if (!limite2 && QE < 40 && SocialManager.instance.nombreAlimentsDifferents > 1)
        {
            SocialManager.instance.nombreAlimentsDifferents -= 1;
            limite2 = true;
            if(nbrdiminutionQE == 1)
            {
                EnvironnementManager.instance.maxQE -= 5;
                nbrdiminutionQE++;
            }
            //Qualité max de l'eau -1 a voir dans le script gamemanager
        }
        if (limite2 && QE > 50)
        {
            SocialManager.instance.nombreAlimentsDifferents += 1;
            limite2 = false;
        }
        if (!limite3 && QE < 20 && SocialManager.instance.nombreAlimentsDifferents > 1)
        {
            SocialManager.instance.nombreAlimentsDifferents -= 1;
            limite3 = true;
            if(nbrdiminutionQE == 2)
            {
                EnvironnementManager.instance.maxQE -= 5;
                nbrdiminutionQE++;
            }
            //Qualité max de l'eau -1 a voir dans le script gamemanager
        }
        if (limite3 && QE > 30)
        {
            SocialManager.instance.nombreAlimentsDifferents += 1;
            limite3 = false;
        }
    }

    void InitVariete(HabitantBehaviour habitant)
    {
        if (habitant.ecoLevel == 1)
        {
            SocialManager.instance.nombreAlimentsDifferents =1;
        }
        if(habitant.ecoLevel == 3)
        {
            SocialManager.instance.nombreAlimentsDifferents =2;
        }
        if(habitant.ecoLevel == 5)
        {
            SocialManager.instance.nombreAlimentsDifferents =3;
        }
        levelactuel = habitant.ecoLevel;
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

    int MalusQualite()
    {
        float QE = EnvironnementManager.instance.qualiteEau;
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

    void UpdateQE()
    {
        if(QuantitePoisson > 8)
        {
            EnvironnementManager.instance.qualiteEau = Mathf.Max( EnvironnementManager.instance.qualiteEau -(float) 0.1 * (QuantitePoisson - 8),0);
        }
        else
        {
            EnvironnementManager.instance.qualiteEau = Mathf.Min( EnvironnementManager.instance.qualiteEau - (float)0.1 * (QuantitePoisson - 8), EnvironnementManager.instance.maxQE);
        }
    } 

    public void ValiderValeur()    // Fonction sur bouton quand on valide la quantité qu'on veut produire
    {
        QuantitePoisson = QuantitePoissonNonValide;
        RecupererPoisson();
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
            return limite;
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
        yield return new WaitForSeconds(5); //Pour test que tout marche
        /*DateTime current = DateTime.Now; //Vrai mécanique minuit
        DateTime tomorrow = current.AddDays(1).Date; 

        double seconds = (tomorrow - current).TotalSeconds;
        yield return new WaitForSeconds((float) seconds);*/
        FonctionMinuit();
        validation.interactable = true;
        timer = 0;
    }

    // Partie IU et Boutons : 

    public void FctInfo()
    {
        if (habitant == null)
        {
            menuinfo.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = null;
            menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Vacant";
            menuinfo.transform.GetChild(4).gameObject.SetActive(false);
            menuinfo.transform.GetChild(5).gameObject.SetActive(false);
            menuinfo.transform.GetChild(6).gameObject.SetActive(false);
            menuinfo.transform.GetChild(7).gameObject.SetActive(false);
            menuinfo.transform.GetChild(8).gameObject.SetActive(false);
            menuinfo.transform.GetChild(9).gameObject.SetActive(true);
            panel.SetActive(false);
            menuinfo.SetActive(true);
        }
        else if (habitant != null && habitant.isHoused == false)
        {
            menuinfo.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = habitant.image;
            menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = habitant.nom;
            menuinfo.transform.GetChild(4).gameObject.SetActive(true);
            menuinfo.transform.GetChild(5).gameObject.SetActive(false);
            menuinfo.transform.GetChild(6).gameObject.SetActive(false);
            menuinfo.transform.GetChild(7).gameObject.SetActive(false);
            menuinfo.transform.GetChild(8).gameObject.SetActive(false);
            menuinfo.transform.GetChild(9).gameObject.SetActive(true);
            panel.SetActive(false);
            menuinfo.SetActive(true);
        }
        else
        {
            menuinfo.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = habitant.image;
            menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = habitant.nom;
            menuinfo.transform.GetChild(4).gameObject.SetActive(false);
            menuinfo.transform.GetChild(5).gameObject.SetActive(true);
            menuinfo.transform.GetChild(6).gameObject.SetActive(true);
            menuinfo.transform.GetChild(7).gameObject.SetActive(true);
            menuinfo.transform.GetChild(8).gameObject.SetActive(true);
            menuinfo.transform.GetChild(9).gameObject.SetActive(true);
            panel.SetActive(false);
            menuinfo.SetActive(true);
        }
    }

    public void FctChoix()
    {

        GameObject PecheurDispo = TrouverPecheur();
        if(PecheurDispo == null)
        {
            choixhabitant.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            choixhabitant.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = PecheurDispo.GetComponent<HabitantBehaviour>().image;
            choixhabitant.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = PecheurDispo.GetComponent<HabitantBehaviour>().name;
        }
        panel.SetActive(false);
        choixhabitant.SetActive(true);
    }
    GameObject TrouverPecheur()
    {
        GameObject ha = new GameObject();
        GameObject habitant = GameObject.Find("habitants");
        foreach (Transform child in habitant.transform)
        {
            HabitantBehaviour Behaviour = child.GetComponent<HabitantBehaviour>();
            if(Behaviour.transform.name == "pêcheur")
            {
                if (Behaviour.hasWorkplace == false)
                {
                    ha = child.gameObject;
                }
            }
        }
        return ha;
    }

    public void quitter()
    {
        choixhabitant.SetActive(false);
        panel.SetActive(false);
        open = false;
        animator.SetTrigger("ouverture1BulleCouper");
        Deplacement.enMenu = false;
    }
    public void selectionpecheur()
    {
        GameObject pecheurdispo = TrouverPecheur();
        habitant = pecheurdispo.GetComponent<HabitantBehaviour>();
        habitant.hasWorkplace = true;
        isOccupied = true;
        choixhabitant.SetActive(false);
        panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        panel.SetActive(true);
    }
    public void quitter2()
    {
        menuinfo.SetActive(false);
        panel.SetActive(false);
        open = false;
        animator.SetTrigger("ouverture1BulleCouper");
        Deplacement.enMenu = false;
    }

}
