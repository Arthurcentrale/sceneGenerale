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
    public int nbParcelleBle;
    public int nbParcelleMais;

    public bool isOccupied, valider;

    float timer = 0f;
    float delai = 5f;

    public Slider slider;

    bool isBle;
    int QuantiteFarineNonValide;
    int QuantiteFarine;

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
        slider.minValue = 0;
        //slider.maxValue = QuantiteMax(habitant);
        slider.maxValue = 15;
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
    /*int QuantiteMax(HabitantBehaviour habitant)
    {
        if (isBle)
        {
            if (habitant.ecoLevel == 1)
            {
                return Math.Max(3, nbParcelleBle);
            }
            if (habitant.ecoLevel == 2)
            {
                return Math.Max(6, nbParcelleBle);
            }
            if (habitant.ecoLevel == 3)
            {
                return Math.Max(9, nbParcelleBle);
            }
            if (habitant.ecoLevel == 4)
            {
                return Math.Max(12, nbParcelleBle);
            }
            else
            {
                return Math.Max(15, nbParcelleBle);
            }
        }
        else
        {
            if (habitant.ecoLevel == 1)
            {
                return Math.Max(2, nbParcelleBle);
            }
            if (habitant.ecoLevel == 2)
            {
                return Math.Max(4, nbParcelleBle);
            }
            if (habitant.ecoLevel == 3)
            {
                return Math.Max(6, nbParcelleBle);
            }
            if (habitant.ecoLevel == 4)
            {
                return Math.Max(8, nbParcelleBle);
            }
            else
            {
                return Math.Max(10, nbParcelleBle);
            }
        }
    }*/
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

}