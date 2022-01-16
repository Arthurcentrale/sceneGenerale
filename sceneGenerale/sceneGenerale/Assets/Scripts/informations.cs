using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class informations : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelinfo;
    int longueurmax = 6;
    Stack<string> listeinfos = new Stack<string>();
    float qeau, qair, qsol;
    int quantiténourriture, quantitéchauffage, varietenourriture;
    bool Rien;
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
        Rien = true;
        listeinfos.Push(Associer(Rien));
        // ???? quantitéchauffage = GameManager.socialManager.chauffage
    }

    void ClickInfo()
    {
        UpdateValeur();
        if(eau == true)
        {
            if (!eau2){
                Ajouterliste(eau);
            }
            else
            {
                Ajouterliste(eau2);
            }
        }
        if (air == true)
        {
            if (!air2)
            {
                Ajouterliste(air);
            }
            else
            {
                Ajouterliste(air2);
            }
        }
        if (sol == true)
        {
            if (!sol2)
            {
                Ajouterliste(sol);
            }
            else
            {
                Ajouterliste(sol2);
            }
        }
        if (quantité == true)
        {
            if (!quantité2)
            {
                Ajouterliste(quantité);
            }
            else
            {
                Ajouterliste(quantité2);
            }
        }
        if (variete == true)
        {
            if (!variete2)
            {
                Ajouterliste(variete);
            }
            else
            {
                Ajouterliste(variete2);
            }
        }

        //Partie Affichage 
        int i = listeinfos.Count;
        int j = 0;
        Inverser(listeinfos);
        Stack<string> burner = listeinfos; // On crée une copie pour ne pas supprimer la pile lors de l'affichage
        foreach (Transform child in panelinfo.transform)
        {
            if(j < i)
            {
                child.gameObject.SetActive(true);
                if (trouverinfo(burner,burner.Pop()) == 10) child.GetChild(0).gameObject.SetActive(true); // Si l'info n'était pas dans les infos la derniere fois qu'on a regardé, alors c'est une nouvelle, on active le NEW
                else child.GetChild(0).gameObject.SetActive(false);
                child.gameObject.GetComponent<Text>().text = burner.Pop();
                j++;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
            
        }
        Inverser(listeinfos);
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
        if (eau || air || sol || quantité || variete) Rien = false;
        listeinfos = Deleteinfo(Rien);
    }

    Stack<string> Deleteinfo(bool boolen)
    {
        Stack<string>  liste = listeinfos;
        Stack<string> vide = new Stack<string>();
        string aenlever = Associer(boolen);
        int h = trouverinfo(liste, aenlever);
        if(h == 10)
        {
            return liste;
        }
        else
        {
            int i = 0;
            while (i < h)
            {
                vide.Push(liste.Pop());
                i++;
            }

            liste.Pop();
            while (i > 0)
            {
                liste.Push(vide.Pop());
                i--;
            }
            return liste;
        }
    }
    Stack<string> Remplacerinfo(bool boolen,bool nouveaubool) // On veut remplacer boolen par nouveaubool dans la pile
    {
        Stack<string> liste = listeinfos;
        Stack<string> vide = new Stack<string>();
        string aenlever = Associer(boolen);
        int h = trouverinfo(liste, aenlever);
        if (h == 10)
        {
            return liste;
        }
        else
        {
            int i = 0;
            while (i < h)
            {
                vide.Push(liste.Pop());
                i++;
            }

            liste.Pop();
            liste.Push(Associer(nouveaubool));
            while (i > 0)
            {
                liste.Push(vide.Pop());
                i--;
            }
            return liste;
        }
    }

    Stack<string> Remplacerinfostring(string boolen, bool nouveaubool) // On veut remplacer boolen par nouveaubool dans la pile
    {
        Stack<string> liste = listeinfos;
        Stack<string> vide = new Stack<string>();
        string aenlever = boolen;
        int h = trouverinfo(liste, aenlever);
        if (h == 10)
        {
            return liste;
        }
        else
        {
            int i = 0;
            while (i < h)
            {
                vide.Push(liste.Pop());
                i++;
            }

            liste.Pop();
            liste.Push(Associer(nouveaubool));
            while (i > 0)
            {
                liste.Push(vide.Pop());
                i--;
            }
            return liste;
        }
    }

    Stack<string> Inverser(Stack<string> liste)
    {
        Stack<string> l = new Stack<string>();
        while(liste.Count >= 0)
        {
            l.Push(liste.Pop());
        }
        return l;
    }
    Stack<string> Ajouterinfo(bool boolen) // Cas normal, pas de remplacement spécial donc soit on ajoute, soit on remplace par l'information la plus ancienne
    {
        Stack<string> liste = listeinfos;
        if(liste.Count == longueurmax)
        {
            Inverser(liste);
            liste.Pop();
            Inverser(liste);
            liste.Push(Associer(boolen));  
        }
        else
        {
            liste.Push(Associer(boolen));
        }
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
        else if (boolen.ToString() == "Rien") return "Rien a signaler dans votre communauté";
        else return null;
    }
    string Associerstring(string boolen)
    {
        if (boolen == "eau") return "La qualité de l'eau a diminué ces derniers temps";
        else if (boolen == "eau2") return "La qualité de l'eau est déplorable, il faut agir";
        else if (boolen == "air") return "La qualité de l'air a diminué ces derniers temps";
        else if (boolen == "air2") return "La qualité de l'air est déplorable, il faut agir";
        else if (boolen == "sol") return "La qualité du sol a diminué ces derniers temps";
        else if (boolen == "sol2") return "La qualité du sol est déplorable, il faut agir";
        else if (boolen == "eau") return "La qualité de l'eau a diminué ces derniers temps";
        else if (boolen == "eau2") return "La qualité de l'eau est déplorable, il faut agir";
        else if (boolen == "quantité") return "La quantité de nourriture disponible se fait maigre";
        else if (boolen == "quantité2") return "Vous ne produisez pas assez de nourriture pour nourrir tous vos habitants";
        else if (boolen == "variete") return "Certains habitants se plaignent du manque de variété des aliemnts qu'ils consomment";
        else if (boolen == "variete2") return "Les habitants ne sont pas satisfait du manque de variété de leur nourriture";
        else if (boolen == "Rien") return "Rien a signaler dans votre communauté";
        else return null;
    }

    void Ajouterliste(bool boolen)
    {
        //Verifier si il y est pas deja, dans ce cas on fait rien
        if (trouverinfo(listeinfos,Associer(boolen)) != 10)
        {
            //on ne fait rien
        }
        else // il n'est pas déjà présent
        {
            //si c'est un rang 1
            if(boolen.ToString().IndexOf("2",StringComparison.OrdinalIgnoreCase) <0) Ajouterinfo(boolen); // Si c'est un rang 1, on ajoute dans la liste -> la fonction ajouteliste s'occupe de voir si il y'a la place ou non
                                                                                                               //si c'est un rang 2
            if (boolen.ToString().IndexOf("2", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (trouverinfo(listeinfos,boolen.ToString().Substring(0,boolen.ToString().Length -1)) !=10) // si le rang 1 est présent dans la liste
                {
                    Remplacerinfostring(Associerstring(boolen.ToString().Substring(0, boolen.ToString().Length - 1)),boolen);
                }
                else // si le rang 1 n'est pas présent, on ajoute simplement dans la liste
                {
                    Ajouterinfo(boolen);
                }
            }
        }

        //Si c'et un rang 2, on degage le rang 1 et on met le rang 2 en premier si il est dans la liste
        // Si il c'est un rang 2 mais qu'il y a pas de rang 1 qui correspond, on degage le plus ancien( le dernier de la pile)
    }
}
