using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoulinEau : MonoBehaviour
{
    //Ouvrir UI
    public GameObject panel;
    bool open;
    bool onPanel;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;
    public Button validation;
    public Text textslider;
    public Text textType;

    //Gestion des paramètres globaux

    public HabitantBehaviour habitant;
    public GameObject go;

    //nombreDeParcelle
    int nbParcelleBle;
    int nbParcelleMais;

    public bool isOccupied, valider;

    float timer = 0f;
    float delai = 5f;

    public Slider slider;

    bool isBle = true;
    int QuantiteFarineNonValide;
    int QuantiteFarine;
    bool isEmpty;

    public GameObject menuinfo;
    public GameObject choixhabitant;

    void Start()
    {
        onPanel = false;
        open = false;
        isOccupied = false;
        QuantiteFarine = 0;
        QuantiteFarineNonValide = 0;
        textslider.text = 0.ToString();
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
        validation = validation.GetComponent<Button>();
        textType.text = "Blé";
    }

    void Update()
    {
        nbParcelleBle = Planter.nbrePlantes[0];
        nbParcelleMais = Planter.nbrePlantes[1];
        slider.minValue = 0;
        slider.maxValue = QuantiteMax(habitant);
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
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("MoulinEau"))

                {
                    
                    //Ajouter update valeur max du slider, etc..
                    panel.transform.position = new Vector2(mP.x + panel.GetComponent<RectTransform>().rect.width, mP.y);
                    panel.gameObject.SetActive(true);
                    animator.SetTrigger("ouverture1BulleCouper");
                    open = true;


                }

            }
            if (open == true)
            {
                QuantiteFarineNonValide = (int)slider.value;
            }

            if (isOccupied && valider == false)
            {
                timer += Time.deltaTime;
                if (timer >= delai)
                {
                    FonctionMinuit();
                    timer = 0;
                }

            }


        }
    }
    public void BoutonChangemntType()
    {
        isBle = !isBle;
        if (isBle)
        {
            textType.text = "Blé";
        }
        else
        {
            textType.text = "Maïs";
        }
    }
    int QuantiteMax(HabitantBehaviour habitant)
    {
        if (habitant == null)
        {
            return 0;
        }

        if (isBle)
        {
            if (habitant.ecoLevel == 1)
            {
                return Math.Min(3, nbParcelleBle);
            }
            if (habitant.ecoLevel == 2)
            {
                return Math.Min(6, nbParcelleBle);
            }
            if (habitant.ecoLevel == 3)
            {
                return Math.Min(9, nbParcelleBle);
            }
            if (habitant.ecoLevel == 4)
            {
                return Math.Min(12, nbParcelleBle);
            }
            else
            {
                return Math.Min(15, nbParcelleBle);
            }
        }
        else
        {
            if (habitant.ecoLevel == 1)
            {
                return Math.Min(2, nbParcelleMais);
            }
            if (habitant.ecoLevel == 2)
            {
                return Math.Min(4, nbParcelleMais);
            }
            if (habitant.ecoLevel == 3)
            {
                return Math.Min(6, nbParcelleMais);
            }
            if (habitant.ecoLevel == 4)
            {
                return Math.Min(8, nbParcelleMais);
            }
            else
            {
                return Math.Min(10, nbParcelleMais);
            }
        }
        
    }
    public void ValiderValeur()    // Fonction sur bouton quand on valide la quantité qu'on veut produire
    {
        QuantiteFarine = QuantiteFarineNonValide;
        if (isBle)
        {
            MoulinVent.StockFarineBle += QuantiteFarine;
        }
        else
        {
            MoulinVent.StockFarineMais += QuantiteFarine;
        }
        valider = true;
        StartCoroutine(Coroutine());
        //Lancer le blocage de la valeur jusqu'à minuit
    }

    void UpdateQE()
    {
        float malus = 0f;
        if (QuantiteFarine >= 7)
        {
            malus = 0.1f;
        }
        if (QuantiteFarine >= 13)
        {
            malus = 0.2f;
        }
        GameManager.environnementManager.qualiteEau -= malus;
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

    void FonctionMinuit() //Fonction à lancer à minuit tous les jours qui permet de pouvoir modifier la valeur qu'on a validé le jour d'avant
    {
        if (valider)
        {
            valider = false;
        }
        else
        {
            if (isBle)
            {
                MoulinVent.StockFarineBle += QuantiteFarine;
            }
            else
            {
                MoulinVent.StockFarineMais += QuantiteFarine;
            }
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
            menuinfo.transform.GetChild(10).gameObject.SetActive(false);
            menuinfo.transform.GetChild(11).gameObject.SetActive(false);
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
            menuinfo.transform.GetChild(10).gameObject.SetActive(false);
            menuinfo.transform.GetChild(11).gameObject.SetActive(false);
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
            menuinfo.transform.GetChild(10).gameObject.SetActive(true);
            menuinfo.transform.GetChild(11).gameObject.SetActive(true);
            panel.SetActive(false);
            menuinfo.SetActive(true);
        }
    }
    GameObject TrouverMeunier()
    {
        GameObject ha = new GameObject();
        GameObject habitant = GameObject.Find("habitants");
        foreach (Transform child in habitant.transform)
        {
            HabitantBehaviour Behaviour = child.GetComponent<HabitantBehaviour>();
            if (Behaviour.transform.name == "Meunier")
            {
                if (Behaviour.hasWorkplace == false)
                {
                    ha = child.gameObject;
                }
            }
        }
        return ha;
    }

    public void FctChoix()
    {

        GameObject PecheurDispo = TrouverMeunier();
        if (PecheurDispo == null)
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

    public void selectionartisan()
    {
        GameObject artisandispo = TrouverMeunier();
        habitant = artisandispo.GetComponent<HabitantBehaviour>();
        habitant.hasWorkplace = true;
        isEmpty = false;
        choixhabitant.SetActive(false);
        if (habitant.isHoused == false)
        {
            panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        panel.SetActive(true);
    }

    public void quitter()
    {
        choixhabitant.SetActive(false);
        panel.SetActive(false);
        open = false;
        animator.SetTrigger("ouverture1BulleCouper");
        Deplacement.enMenu = false;
    }
    public void quitter2()
    {
        menuinfo.SetActive(false);
        panel.SetActive(false);
        open = false;
        Deplacement.enMenu = false;
        animator.SetTrigger("ouverture1BulleCouper");
    }

}