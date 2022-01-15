using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class informations : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelinfo;
    int longueurmax = 6;
    Stack<string> listeinfos = new Stack<string>();
    float qeau, qair, qsol;
    int quantiténourriture, quantitéchauffage, varietenourriture;
    bool eau, air, sol, variete, quantité, chaleur = false;
    bool eau2, air2, sol2, variete2, quantité2, chaleur2 = false;
    void Start()
    {
        panelinfo = GameObject.Find("menuinformations");
        qeau = GameManager.environnementManager.qualiteEau;
        qair = GameManager.environnementManager.qualiteAir;
        qsol = GameManager.environnementManager.qualiteSol;
        quantiténourriture = GameManager.socialManager.quantiteNourriture;
        varietenourriture = GameManager.socialManager.nombreAlimentsDifferents;
        // ???? quantitéchauffage = GameManager.socialManager.chauffage
    }

    void ClickInfo()
    {
        UpdateValeur();
        if(eau == true)
        {
            if (!eau2 && trouverinfo(listeinfos, Associer(eau)) == 10){
                Ajouterliste(eau);
            }
            if (eau2)
            {
                Ajouterliste(eau2);
            }
        }


        //Partie Affichage 
        int i = listeinfos.Count;
        int j = 0;
        foreach (Transform child in panelinfo.transform)
        {
            if(j < i)
            {
                child.gameObject.SetActive(true);
                if (trouverinfo(listeinfos,listeinfos.Pop()) == 10) child.GetChild(1).gameObject.SetActive(true); // Si l'info n'était pas dans les infos la derniere fois qu'on a regardé, alors c'est une nouvelle, on active le NEW
                else child.GetChild(1).gameObject.SetActive(false);
                child.GetChild(0).gameObject.GetComponent<Text>().text = listeinfos.Pop();
                j++;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
            
        }
    }
    void UpdateValeur()
    {
        float qe = GameManager.environnementManager.qualiteEau;
        float qa = GameManager.environnementManager.qualiteAir;
        float qs = GameManager.environnementManager.qualiteSol;
        int qn = GameManager.socialManager.quantiteNourriture;
        int vn = GameManager.socialManager.nombreAlimentsDifferents;
        if (qeau > 50f && qe < 50) eau = true;
        if (qeau > 20f && qe < 20) eau2 = true;
        if (qair > 50f && qa < 50) air = true;
        if (qair > 20f && qa < 20) air2 = true;
        if (qsol > 50f && qs < 50) sol = true;
        if (qsol > 20f && qs < 20) sol2 = true;
        if (quantiténourriture > 10 && qn < 10) quantité = true;
        if (quantiténourriture > 0 && qn < 0) quantité2 = true;
        if (varietenourriture > 50f && vn < 50) variete = true;
        if (varietenourriture > 20f && vn < 20) variete2 = true;
        qeau = qe;
        qair = qa;
        qsol = qs;
        quantiténourriture = qn;
        varietenourriture = vn;

    }

    Stack<string> Deleteinfo(bool boolen)
    {
        Stack<string>  liste = listeinfos;
        string aenlever = Associer(boolen);
        int h = trouverinfo(liste, aenlever);


        return liste;
    }

    string[] Decalerinfos(string[] liste,int i)
    {
        string[] newliste = liste;

        return newliste;
    }

    int trouverinfo(Stack<string> liste,string texte)
    {
        Stack<string> copie = liste;
        int i;
        for(int j=0;j < liste.Count; j++)
        {
            if (copie.Pop() == texte)
            {
                i = j;
            }
        }
        return 10;
    }
    
    string Associer(bool boolen)
    {
        if (boolen.ToString() == "eau") return "La qualité de l'eau a diminué ces derniers temps";
        else if (boolen.ToString() == "eau2") return "La qualité de l'eau est déplorable, il faut agir";
        else if (boolen.ToString() == "air") return "La qualité de l'air a diminué ces derniers temps";
        else if (boolen.ToString() == "air2") return "La qualité de l'air est déplorable, il faut agir";
        else if (boolen.ToString() == "sol") return "La qualité du sol a diminué ces derniers temps";
        else if (boolen.ToString() == "sol2") return "La qualité du sol est déplorable, il faut agir";
        else if (boolen.ToString() == "eau") return "La qualité de l'eau a diminué ces derniers temps";
        else if (boolen.ToString() == "eau2") return "La qualité de l'eau est déplorable, il faut agir";
        else if (boolen.ToString() == "quantité") return "La quantité de nourriture disponible se fait maigre";
        else if (boolen.ToString() == "quantité2") return "Vous ne produisez pas assez de nourriture pour nourrir tous vos habitants";
        else if (boolen.ToString() == "variete") return "Certains habitants se plaignent du manque de variété des aliemnts qu'ils consomment";
        else if (boolen.ToString() == "variete2") return "Les habitants ne sont pas satisfait du manque de variété de leur nourriture";
        else return null;
    }

    void Ajouterliste(bool boolen)
    {
        //Verifier si il y est pas deja, dans ce cas on fait rien
        //Si c'et un rang 2, on degage le rang 1 et on met le rang 2 en premier
        
        // Si il c'est un rang 2 mais qu'il y a pas de rang 1 qui correspond, on degage le plus ancien( le dernier de la pile)
   


        
    }
}
