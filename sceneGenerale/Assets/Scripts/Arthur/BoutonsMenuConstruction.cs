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
    public GameObject PanelInformationChaumière;
    public GameObject PanelInformationPecherie;
    public GameObject PanelInformationMoulinAEau;
    public GameObject PanelInformationMoulinAVent;
    public GameObject PanelInformationBoulangerie;
    public GameObject PanelInformationCabanon;
    public GameObject PanelInformationPuits;
    public GameObject PanelInformationForge;
    public GameObject PanelInformationFosse;
    public GameObject PanelInformationMaisonPierre;
    public GameObject PanelInformationGardeManger;
    public GameObject PanelInformationFerme;

    // Les bâtiments: Chaumière, Pècherie, MoulinAEau, MoulinAVent, Boulangerie, Cabanon de grand forestier, Puit, Forge, Fosse commune, Maison en pierre, Garde-manger, Ferme




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

 
    ////////////////////////////////////////////////////DEBUT CHAUMIERE//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationChaumière()
    {
        if (MenuConstructionEstAffiche == true)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false); //sauf qu'ne fait il va pas être activé
            boutonMenuEstAffiche = false;
            PanelInformationChaumière.SetActive(true);
            //PanelInformationTenteEstAffiche = true;  // On va pas le garder après, il sert pas à grand chose
        }
    }


    public void RetourAuMenuConstructionDepuisChaumiere() // Oui on va en faire un pour chaque bâtiment :) :) 
    {
        
            PanelInformationChaumiere.SetActive(false);
            
            MenuConstructions();
        
        
    }
    ////////////////////////////////////////////////////FIN CHAUMIERE//////////////////////////////////////////////////////////////






    ////////////////////////////////////////////////////DEBUT PECHERIE//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationPecherie()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationPecherie.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisPecherie() 
    {
        
            PanelInformationPecherie.SetActive(false);
            
            MenuConstructions();
        

    }
    ////////////////////////////////////////////////////FIN PECHERIE//////////////////////////////////////////////////////////////
    // Et on fait de même pour tous les autres bâtiments :) :) 






    ////////////////////////////////////////////////////DEBUT MOULIN A EAU//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationMoulinAEau()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationMoulinAEau.SetActive(true);
            
        }
    }

    public void RetourAuMenuConstructionDepuisMoulinAEau() 
    {
        
            PanelInformationMoulinAEau.SetActive(false);
            
            MenuConstructions();
        

    }
    ////////////////////////////////////////////////////FIN MOULIN A EAU//////////////////////////////////////////////////////////////







    ////////////////////////////////////////////////////DEBUT MOULIN A VENT//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationMoulinAVent()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationMoulinAVent.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisMoulinAVent()
    {
        
            PanelInformationMoulinAVent.SetActive(false);

            MenuConstructions();
        

    }
    ////////////////////////////////////////////////////FIN MOULIN A VENT//////////////////////////////////////////////////////////////






    ////////////////////////////////////////////////////DEBUT BOULANGERIE//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationBoulangerie()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationBoulangerie.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisBoulangerie()
    {
        
            PanelInformationBoulangerie.SetActive(false);

            MenuConstructions();
        

    }
    ////////////////////////////////////////////////////FIN BOULANGERIE//////////////////////////////////////////////////////////////






    ////////////////////////////////////////////////////DEBUT CABANON//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationCabanon()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationCabanon.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisCabanon()
    {
        
            PanelInformationCabanon.SetActive(false);

            MenuConstructions();
        

    }
    ////////////////////////////////////////////////////FIN CABANON//////////////////////////////////////////////////////////////







    ////////////////////////////////////////////////////DEBUT PUITS//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationPuits()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationPuits.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisPuits()
    {
        
            PanelInformationPuits.SetActive(false);

            MenuConstructions();
        

    }
    ////////////////////////////////////////////////////FIN PUITS//////////////////////////////////////////////////////////////







    ////////////////////////////////////////////////////DEBUT FORGE//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationForge()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationForge.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisForge()
    {
        if (PanelInformationTenteEstAffiche == true)
        {
            PanelInformationMoulinAVent.SetActive(false);

            MenuConstructions();
        }

    }
    ////////////////////////////////////////////////////FIN FORGE//////////////////////////////////////////////////////////////






    ////////////////////////////////////////////////////DEBUT FOSSE COMMUNE//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationFosse()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationFosse.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisFosse()
    {
        
            PanelInformationFosse.SetActive(false);

            MenuConstructions();
        

    }
    ////////////////////////////////////////////////////FIN FOSSE COMMUNE//////////////////////////////////////////////////////////////







    ////////////////////////////////////////////////////DEBUT MAISON EN PIERRE//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationMaisonEnPierre()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationMaisonEnPierre.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisMaisonEnPierre()
    {
        
            PanelInformationMoulinAVent.SetActive(false);

            MenuConstructions();
        

    }
    ////////////////////////////////////////////////////FIN MAISON EN PIERRE//////////////////////////////////////////////////////////////







    ////////////////////////////////////////////////////DEBUT GARDE-MANGER//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationGardeManger()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationGardeManger.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisGardeManger()
    {
        
            PanelInformationMoulinAVent.SetActive(false);

            MenuConstructions();
        

    }
    ////////////////////////////////////////////////////FIN GARDE-MANGER//////////////////////////////////////////////////////////////








    ////////////////////////////////////////////////////DEBUT FERME//////////////////////////////////////////////////////////////    
    public void AfficherMenuInformationFerme()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructions();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationFerme.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisFerme()
    {
        
            PanelInformationFerme.SetActive(false);

            MenuConstructions();
        

    }
    ////////////////////////////////////////////////////FIN FERME//////////////////////////////////////////////////////////////
}
