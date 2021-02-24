using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonsMenuConstruction : MonoBehaviour
{
    public bool boutonMenuEstAffiche = true;
    public bool MenuEstAffiche = false;
    public bool MenuConstructionEstAffiche = false;
    public bool MenuInformationTenteEstAffiche = false;
    public bool PanelInformationTenteEstAffiche = false;
    public GameObject boutonMenu;
    public GameObject boutonConstruction;
    public GameObject boutonOptions;
    public GameObject boutonAgriculture;
    public GameObject boutonCloseMenu;
    public GameObject Tente;  // correspond au pannel tente
    public GameObject Chaumière;
    public GameObject Moulin;
    public GameObject boutonCloseMenuConstruction;
    public GameObject PanelInformationTente;






    void EnleverMenuPrincipal()             // Des fonctions intermédiaires pour coder les boutons un peu plus vite
        {
            boutonConstruction.SetActive(false);
            boutonOptions.SetActive(false);
            boutonAgriculture.SetActive(false);
            boutonCloseMenu.SetActive(false);
            boutonMenu.SetActive(true);
            boutonMenuEstAffiche = true;
            MenuEstAffiche = false;
        }


    void AfficherMenuPrincipal()   // Pareil
        {
            boutonConstruction.SetActive(true);
            boutonOptions.SetActive(true);
            boutonAgriculture.SetActive(true);
            boutonCloseMenu.SetActive(true);
            boutonMenu.SetActive(false);
            MenuEstAffiche = true;
            boutonMenuEstAffiche = false;
        }




   public void menuPrincipal()                 // Code le bouton menu principal (on affiche le menu, on désaffiche le bouton menu)
     {
            if (MenuEstAffiche == false)
        {
            AfficherMenuPrincipal();
            
        }
    }




    public void CloseMenuPrincipal()   // le bouton close
    {
        if (MenuEstAffiche == true)
        {
            EnleverMenuPrincipal();
            
        }
    }


    // les 2 prochaines fonctions sont là pour me faire gagner du temps, comme affichermenu plus haut


    void AfficherMenuConstructions()
    { 
        
        MenuConstructionEstAffiche = true;
        Tente.SetActive(true);
        Chaumière.SetActive(true);
        Moulin.SetActive(true);
        boutonCloseMenuConstruction.SetActive(true);
        boutonMenu.SetActive(false);
        boutonMenuEstAffiche = false;


    }

    void EnleverMenuConstructions()
    {
       
        MenuConstructionEstAffiche = false;
        Tente.SetActive(false);
        Chaumière.SetActive(false);
        Moulin.SetActive(false);
        boutonCloseMenuConstruction.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
    }

    public void MenuConstructions()
    {
        if (MenuConstructionEstAffiche == false)
        {
            EnleverMenuPrincipal();
            AfficherMenuConstructions();
        }
        

    }


    public void CloseMenuConstructions()
    {
        if (MenuConstructionEstAffiche == true)
        {
            EnleverMenuConstructions();         // je pourrais faire un bouton retour où on retourne sur le menu principal mais en vrai ça change pas grand chose

        }
    }


    public void AfficherMenuInformationTente()
    {
        if (MenuConstructionEstAffiche == true)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationTente.SetActive(true);
            PanelInformationTenteEstAffiche = true;
        }
    }


    public void RetourAuMenuConstruction()
    {
        if (PanelInformationTenteEstAffiche == true)
        {
            PanelInformationTente.SetActive(false);
            PanelInformationTenteEstAffiche = false;
            MenuConstructions();
        }
        
    }
    







}
