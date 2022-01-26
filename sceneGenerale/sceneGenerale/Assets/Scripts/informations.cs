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
    public bool Rien;
    bool eau, air, sol, variete, quantité, chaleur = false;
    bool eau2, air2, sol2, variete2, quantité2, chaleur2 = false;
    public namedBoolean boolenrien,booleau,boolair,boolsol,boolvariete,boolquantité;
    public namedBoolean booleau2, boolair2, boolsol2, boolvariete2, boolquantité2;
    bool inutile;

    public class namedBoolean
    {
        public string name;
        public bool boolen;
        public namedBoolean(string v,bool p)
        {
            this.name= v;
            this.boolen = p;
        }
    }
    void Start()
    {
        Rien = true;
        boolenrien = new namedBoolean(name = "Rien",Rien);
        booleau = new namedBoolean(name = "eau", eau);
        booleau2 = new namedBoolean(name = "eau", eau);
        boolair = new namedBoolean(name = "air", air);
        boolair2 = new namedBoolean(name = "air2", air2);
        boolsol = new namedBoolean(name = "sol", sol);
        boolsol2 = new namedBoolean(name = "sol2", sol2);
        boolvariete = new namedBoolean(name = "variete", variete);
        boolvariete2 = new namedBoolean(name = "variete2", variete2);
        boolquantité = new namedBoolean(name = "quantité", quantité);
        boolquantité2 = new namedBoolean(name = "quantité2", quantité2);
        namedBoolean boolquisertarien = new namedBoolean(name = "menuInformations", inutile);
        qeau = EnvironnementManager.instance.qualiteEau;
        qair = EnvironnementManager.instance.qualiteAir;
        qsol = EnvironnementManager.instance.qualiteSol;
        quantiténourriture = SocialManager.instance.quantiteNourriture;
        varietenourriture = SocialManager.instance.nombreAlimentsDifferents;
        listeinfos = Ajouterliste(boolenrien);
        // ???? quantitéchauffage = GameManager.socialManager.chauffage
    }
    public void ClickInfo()
    {
        Stack<string> ancienneliste = new Stack<string>(listeinfos);
        UpdateValeur();
        if(eau == true)
        {
            if (!eau2){
                listeinfos = Ajouterliste(booleau);
            }
            else
            {
                listeinfos = Ajouterliste(booleau2);
            }
        }
        if (air == true)
        {
            if (!air2)
            {
                listeinfos = Ajouterliste(boolair);
            }
            else
            {
                listeinfos = Ajouterliste(boolair2);
            }
        }
        if (sol == true)
        {
            if (!sol2)
            {
                listeinfos = Ajouterliste(boolsol);
            }
            else
            {
                listeinfos = Ajouterliste(boolsol2);
            }
        }
        if (quantité == true)
        {
            if (!quantité2)
            {
                listeinfos = Ajouterliste(boolquantité);
            }
            else
            {
                listeinfos = Ajouterliste(boolquantité2);
            }
        }
        if (variete == true)
        {
            if (!variete2)
            {
                listeinfos = Ajouterliste(boolvariete);
            }
            else
            {
                listeinfos = Ajouterliste(boolvariete2);
            }
        }

        //Partie Affichage 
        int i = listeinfos.Count;
        int j = 0;
        listeinfos = Inverser(listeinfos);
        Stack<string> burner = new Stack<string>(listeinfos);// On crée une copie pour ne pas supprimer la pile lors de l'affichage
        foreach (Transform child in panelinfo.transform)
        {
            if(j < i)
            {
                child.gameObject.SetActive(true);
                if (trouverinfo(ancienneliste,burner.Peek()) == 10) child.GetChild(0).gameObject.SetActive(true); // Si l'info n'était pas dans les infos la derniere fois qu'on a regardé, alors c'est une nouvelle, on active le NEW
                else child.GetChild(0).gameObject.SetActive(false);
                child.gameObject.GetComponent<Text>().text = burner.Pop();
                j++;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
            
        }
        listeinfos = Inverser(listeinfos);
    }
    void UpdateValeur()
    {
        float qe = EnvironnementManager.instance.qualiteEau;
        float qa = EnvironnementManager.instance.qualiteAir;
        float qs = EnvironnementManager.instance.qualiteSol;
        int qn = SocialManager.instance.quantiteNourriture;
        int vn = SocialManager.instance.nombreAlimentsDifferents;
        if (qeau > 50f && qe < 50f) eau = true;
        if (qeau > 20f && qe < 20f) eau2 = true;
        if (qair > 50f && qa < 50f) air = true;
        if (qair > 20f && qa < 20f) air2 = true;
        if (qsol > 50f && qs < 50f) sol = true;
        if (qsol > 20f && qs < 20f) sol2 = true;
        if (quantiténourriture > 10 && qn < 10) quantité = true;
        if (quantiténourriture > 0 && qn < 0) quantité2 = true;
        if (varietenourriture > 50f && vn < 50) variete = true;
        if (varietenourriture > 20f && vn < 20) variete2 = true;
        qeau = qe;
        qair = qa;
        qsol = qs;
        quantiténourriture = qn;
        varietenourriture = vn;
        if ((eau || air || sol || quantité || variete) && Rien)
        {
            Rien = false;
            listeinfos =Deleteinfo(boolenrien);
        }
    }

    Stack<string> Deleteinfo(namedBoolean boolen)
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
        }
        return liste;
    }
    Stack<string> Remplacerinfo(namedBoolean boolen,namedBoolean nouveaubool) // On veut remplacer boolen par nouveaubool dans la pile
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

    Stack<string> Remplacerinfostring(string boolen,namedBoolean nouveaubool) // On veut remplacer boolen par nouveaubool dans la pile
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

        while(liste.Count > 0)
        {
            l.Push(liste.Pop());
        }

        return l;
    }
    Stack<string> Ajouterinfo(namedBoolean boolen) // Cas normal, pas de remplacement spécial donc soit on ajoute, soit on remplace par l'information la plus ancienne
    {
        Stack<string> liste = listeinfos;
        if(liste.Count == longueurmax)
        {
            liste = Inverser(liste);
            liste.Pop();
            liste = Inverser(liste);
            liste.Push(Associer(boolen));  
        }
        else
        {
            liste.Push(Associer(boolen));
        }
        return liste;

    }

    int trouverinfo(Stack<string> liste,string texte)
    {
        Stack<string> copie = new Stack<string>(liste);
        int i = 10;
        for(int j=0;j < liste.Count; j++)
        {
            if (copie.Pop() == texte)
            {
                i = j;
            }
        }
        return i;
    }

    
    string Associer(namedBoolean nb)
    { 
        if (nb.name == "eau") return "La qualité de l'eau a diminué ces derniers temps";
        else if (nb.name == "eau2") return "La qualité de l'eau est déplorable, il faut agir";
        else if (nb.name == "air") return "La qualité de l'air a diminué ces derniers temps";
        else if (nb.name == "air2") return "La qualité de l'air est déplorable, il faut agir";
        else if (nb.name == "sol") return "La qualité du sol a diminué ces derniers temps";
        else if (nb.name == "sol2") return "La qualité du sol est déplorable, il faut agir";
        else if (nb.name == "eau") return "La qualité de l'eau a diminué ces derniers temps";
        else if (nb.name == "eau2") return "La qualité de l'eau est déplorable, il faut agir";
        else if (nb.name == "quantité") return "La quantité de nourriture disponible se fait maigre";
        else if (nb.name == "quantité2") return "Vous ne produisez pas assez de nourriture pour nourrir tous vos habitants";
        else if (nb.name == "variete") return "Certains habitants se plaignent du manque de variété des aliemnts qu'ils consomment";
        else if (nb.name == "variete2") return "Les habitants ne sont pas satisfait du manque de variété de leur nourriture";
        else if (nb.name == "Rien")
        {
            return "Rien a signaler dans votre communauté";
        }
        else
        {
            return null;
        }
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

    Stack<string> Ajouterliste(namedBoolean boolen)
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
                    listeinfos = Remplacerinfostring(Associerstring(boolen.ToString().Substring(0, boolen.ToString().Length - 1)),boolen);
                }
                else // si le rang 1 n'est pas présent, on ajoute simplement dans la liste
                {
                    listeinfos = Ajouterinfo(boolen);
                }
            }
        }

        return listeinfos;
    }
}
