using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boulangerie : MonoBehaviour
{
    //Ouvrir UI
    public GameObject panel;
    bool open;
    bool onPanel;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;
    public Button validation;
    public Text textslider1;
    public Text textslider2;
    public bool isOccupied, valider;

    float timer = 0f;
    float delai = 5f;

    public Slider slider1;
    public Slider slider2;

    //Gestion des paramètres globaux

    public HabitantBehaviour habitant;
    public GameObject go;

    int QuantitePainMais;
    int QuantitePainMaisNonValide;
    int QuantitePainBle;
    int QuantitePainBleNonValide;
    public CompteurBouffe compteurbouffe;

    void Start()
    {
        onPanel = false;
        open = false;
        isOccupied = false;
        QuantitePainMais = 0;
        QuantitePainMaisNonValide = 0;
        QuantitePainBle = 0;
        QuantitePainBleNonValide = 0;
        textslider1.text = 0.ToString();
        textslider2.text = 0.ToString();
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
        validation = validation.GetComponent<Button>();
        compteurbouffe = compteurbouffe.GetComponent<CompteurBouffe>();

    }

    // Update is called once per frame
    void Update()
    {
        slider1.minValue = 0;
        slider1.maxValue = QuantiteMax(habitant, true);
        slider2.minValue = 0;
        slider2.maxValue = QuantiteMax(habitant, false);
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
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Boulangerie"))

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
                QuantitePainBleNonValide = (int)slider1.value;
                QuantitePainMaisNonValide = (int)slider2.value;
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

    int QuantiteMax(HabitantBehaviour habitant, bool isBle)
    {
        if (isBle)
        {
            if (habitant.ecoLevel == 1)
            {
                return Math.Max(3, MoulinVent.StockFarineBle);
            }
            if (habitant.ecoLevel == 2)
            {
                return Math.Max(6, MoulinVent.StockFarineBle);
            }
            if (habitant.ecoLevel == 3)
            {
                return Math.Max(9, MoulinVent.StockFarineBle);
            }
            if (habitant.ecoLevel == 4)
            {
                return Math.Max(12, MoulinVent.StockFarineBle);
            }
            else
            {
                return Math.Max(15, MoulinVent.StockFarineBle);
            }
        }
        else
        {
            if (habitant.ecoLevel == 1)
            {
                return Math.Max(2, MoulinVent.StockFarineMais);
            }
            if (habitant.ecoLevel == 2)
            {
                return Math.Max(4, MoulinVent.StockFarineMais);
            }
            if (habitant.ecoLevel == 3)
            {
                return Math.Max(6, MoulinVent.StockFarineMais);
            }
            if (habitant.ecoLevel == 4)
            {
                return Math.Max(8, MoulinVent.StockFarineMais);
            }
            else
            {
                return Math.Max(10, MoulinVent.StockFarineMais);
            }
        }
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
        textslider1.text = slider1.value.ToString();
        textslider2.text = slider2.value.ToString();
    }

    public void ValiderValeur()    // Fonction sur bouton quand on valide la quantité qu'on veut produire
    {
        QuantitePainBle = QuantitePainBleNonValide;
        QuantitePainMais = QuantitePainMaisNonValide;
        CompteurBouffe.Data.NbrBouffe += QuantitePainMais;
        CompteurBouffe.Data.NbrBouffe += QuantitePainBle;
        valider = true;
        StartCoroutine(Coroutine());
        //Lancer le blocage de la valeur jusqu'à minuit
    }

    void FonctionMinuit() //Fonction à lancer à minuit tous les jours qui permet de pouvoir modifier la valeur qu'on a validé le jour d'avant
    {
        if (valider)
        {
            valider = false;
        }
        else
        {
            CompteurBouffe.Data.NbrBouffe += QuantitePainBle;
            CompteurBouffe.Data.NbrBouffe += QuantitePainMais * 3;
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
