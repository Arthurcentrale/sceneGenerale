using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recap : MonoBehaviour
{
    public Transform panelRecap;

    private Text nbreParcelles;

    private Slider slider;

    private Text bleTxt;
    private Text maisTxt;
    private Text saladeTxt;
    private Text tomateTxt;
    private Text raisinTxt;

    private Image imageHabitant;
    private Text textHabitant;


    void Start()
    {
        nbreParcelles = panelRecap.GetChild(1).gameObject.GetComponent<Text>();

        slider = panelRecap.GetChild(2).gameObject.GetComponent<Slider>();

        bleTxt = panelRecap.GetChild(3).GetChild(1).gameObject.GetComponent<Text>();
        maisTxt = panelRecap.GetChild(4).GetChild(1).gameObject.GetComponent<Text>();
        saladeTxt = panelRecap.GetChild(5).GetChild(1).gameObject.GetComponent<Text>();
        tomateTxt = panelRecap.GetChild(6).GetChild(1).gameObject.GetComponent<Text>();
        raisinTxt = panelRecap.GetChild(7).GetChild(1).gameObject.GetComponent<Text>();

        imageHabitant = panelRecap.GetChild(10).gameObject.GetComponent<Image>();
        textHabitant = panelRecap.GetChild(11).gameObject.GetComponent<Text>();
    }

    public void MajMenuRecap(int parcellesUtilisees, int parcellesDispo, int CT, int CT_max, int ble, int mais, int salade, int tomate, int raisin)
    {
        nbreParcelles.text = parcellesUtilisees.ToString() + "/" + parcellesDispo.ToString();

        slider.maxValue = CT_max;
        slider.value = CT;

        bleTxt.text = ble.ToString();
        maisTxt.text = mais.ToString();
        saladeTxt.text = salade.ToString();
        tomateTxt.text = tomate.ToString();
        raisinTxt.text = raisin.ToString();
    }

    public void MajHabitant(Sprite img, string txt)
    {
        imageHabitant.sprite = img;
        textHabitant.text = txt;
    }

    public void OuvertureMenuRecap()
    {
        panelRecap.gameObject.SetActive(true);
    }

    public void FermetureMenuRecap()
    {
        panelRecap.gameObject.SetActive(false);
        this.gameObject.GetComponent<Ferme>().open = false;
        Deplacement.enMenu = false;
        this.GetComponent<Agri>().menuOuvert = false;
        this.GetComponent<Ferme>().open = false;
    }
}