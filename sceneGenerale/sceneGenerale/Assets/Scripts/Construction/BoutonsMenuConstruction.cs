using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonsMenuConstruction : MonoBehaviour
{
    Vector3 position = new Vector3(7f, 1f, 7f);
    public static GameObject nouvelleChaumière;
    public static bool en_construction;
    public GameObject boutonValiderConstructionChaumière;
    public GameObject ChaumièreDéplaçable;
    public GameObject BatiChaumière;
    public GameObject prefabBatiChaumière;
    public GameObject prefabChaumièreDéplaçable;
    public GameObject player;
    public bool boutonMenuEstAffiche = true;
    public bool MenuEstAffiche = false;
    public bool MenuConstructionEstAffiche = false;
    public bool MenuInformationTenteEstAffiche = false;
    public bool PanelInformationTenteEstAffiche = false;
    public bool OnEstEnPage1 = false;
    public bool OnEstEnPage2 = false;
    public GameObject boutonMenu;
    public GameObject boutonConstruction;
    public GameObject boutonOptions;
    public GameObject boutonAgriculture;
    public GameObject boutonCloseMenu;
    public GameObject boutonPageSuivanteDepuis1;
    public GameObject boutonPagePrécédenteDepuis2;
    public GameObject Chaumière;  // correspond au pannel chaumière
    public GameObject Pècherie;
    public GameObject MoulinAEau;
    public GameObject MoulinAVent;
    public GameObject Boulangerie;
    public GameObject Cabanon;
    public GameObject Puits;
    public GameObject Forge;
    public GameObject Fosse;
    public GameObject MaisonPierre;
    public GameObject GardeManger;
    public GameObject Ferme;
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
    public GameObject PanelInformationMaisonEnPierre;
    public GameObject PanelInformationGardeManger;
    public GameObject PanelInformationFerme;

    // Dossier des batiments
    // Sert a instantier les nouveaux batiments dedans
    private Transform dossierBatiments;

    ////////////////////// PARTIE CONSTRUCTION ////////////////////////////////////
    //public GameObject prefabChaumièreDéplaçable;



    // Les bâtiments: Chaumière, Pècherie, MoulinAEau, MoulinAVent, Boulangerie, Cabanon de grand forestier, Puits, Forge, Fosse commune, Maison en pierre, Garde-manger, Ferme


    /////////////////////////////////////////////////////DEBUT PARTIE MENU/////////////////////////////////////////////////////////////////////////


    private void Start()
    {
        dossierBatiments = GameObject.Find("Batiments").transform;
    }

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
        Deplacement.enMenu = true;
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
        Deplacement.enMenu = false;
        if (MenuEstAffiche == true)
        {
            EnleverMenuPrincipal();

        }
    }


    // les 2 prochaines fonctions sont là pour me faire gagner du temps, comme affichermenu plus haut


    void AfficherMenuConstructionsPage1()
    {

        MenuConstructionEstAffiche = true;
        Chaumière.SetActive(true);
        Pècherie.SetActive(true);
        MoulinAEau.SetActive(true);
        MoulinAVent.SetActive(true);
        Boulangerie.SetActive(true);
        Cabanon.SetActive(true);
        boutonCloseMenuConstruction.SetActive(true);
        boutonMenu.SetActive(false);
        boutonMenuEstAffiche = false;
        OnEstEnPage1 = true;
        boutonPageSuivanteDepuis1.SetActive(true);

    }

    void AfficherMenuConstructionPage2()
    {
        Chaumière.SetActive(false);
        Pècherie.SetActive(false);
        MoulinAEau.SetActive(false);
        MoulinAVent.SetActive(false);
        Boulangerie.SetActive(false);
        Cabanon.SetActive(false);
        boutonPageSuivanteDepuis1.SetActive(false);
        MenuConstructionEstAffiche = true;
        Puits.SetActive(true);
        Forge.SetActive(true);
        Fosse.SetActive(true);
        MaisonPierre.SetActive(true);
        GardeManger.SetActive(true);
        Ferme.SetActive(true);
        boutonPagePrécédenteDepuis2.SetActive(true);
        boutonCloseMenuConstruction.SetActive(true);
        OnEstEnPage1 = false;
        OnEstEnPage2 = true;
    }

    void EnleverMenuConstructionsPage1()
    {

        MenuConstructionEstAffiche = false;
        Pècherie.SetActive(false);
        Chaumière.SetActive(false);
        MoulinAEau.SetActive(false);
        MoulinAVent.SetActive(false);
        Boulangerie.SetActive(false);
        Cabanon.SetActive(false);
        boutonCloseMenuConstruction.SetActive(false);
        boutonPageSuivanteDepuis1.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        OnEstEnPage1 = false;
    }

    void EnleverMenuConstructionsPage2()
    {

        MenuConstructionEstAffiche = false;
        Puits.SetActive(false);
        Forge.SetActive(false);
        Fosse.SetActive(false);
        MaisonPierre.SetActive(false);
        GardeManger.SetActive(false);
        Ferme.SetActive(false);
        boutonCloseMenuConstruction.SetActive(false);
        boutonPagePrécédenteDepuis2.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        OnEstEnPage2 = false;
    }

    public void MenuConstructions()
    {
        if (MenuConstructionEstAffiche == false)
        {
            EnleverMenuPrincipal();
            AfficherMenuConstructionsPage1();
        }


    }


    public void CloseMenuConstructions()
    {
        Deplacement.enMenu = false;
        if (MenuConstructionEstAffiche == true && OnEstEnPage1)
        {
            EnleverMenuConstructionsPage1();         // je pourrais faire un bouton retour où on retourne sur le menu principal mais en vrai ça change pas grand chose

        }
        if (MenuConstructionEstAffiche == true && OnEstEnPage2)
        {
            EnleverMenuConstructionsPage2();
        }
    }


    public void boutonPageSuivante1()
    {
        if (OnEstEnPage1)
        {
            EnleverMenuConstructionsPage1();
            AfficherMenuConstructionPage2();
            OnEstEnPage1 = false;
            OnEstEnPage2 = true;
        }
    }

    public void boutonPagePrécédente2()
    {
        if (OnEstEnPage2)
        {
            EnleverMenuConstructionsPage2();
            AfficherMenuConstructionsPage1();
            OnEstEnPage1 = true;
            OnEstEnPage2 = false;
        }
    }


    ////////////////////////////////////////////////////DEBUT CHAUMIERE//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationChaumiere()
    {
        if (MenuConstructionEstAffiche == true)
        {
            EnleverMenuConstructionsPage1();
            boutonMenu.SetActive(false); //sauf qu'en fait il va pas être activé
            boutonMenuEstAffiche = false;
            PanelInformationChaumière.SetActive(true);
            //PanelInformationTenteEstAffiche = true;  // On va pas le garder après, il sert pas à grand chose
        }
    }


    public void RetourAuMenuConstructionDepuisChaumiere() // Oui on va en faire un pour chaque bâtiment :) :) 
    {

        PanelInformationChaumière.SetActive(false);

        MenuConstructions();


    }
    ////////////////////////////////////////////////////FIN CHAUMIERE//////////////////////////////////////////////////////////////






    ////////////////////////////////////////////////////DEBUT PECHERIE//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationPecherie()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructionsPage1();
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
            EnleverMenuConstructionsPage1();
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
            EnleverMenuConstructionsPage1();
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
            EnleverMenuConstructionsPage1();
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
            EnleverMenuConstructionsPage1();
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
            EnleverMenuConstructionsPage2();
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
            EnleverMenuConstructionsPage2();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationForge.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisForge()
    {

        PanelInformationForge.SetActive(false);

        MenuConstructions();


    }
    ////////////////////////////////////////////////////FIN FORGE//////////////////////////////////////////////////////////////






    ////////////////////////////////////////////////////DEBUT FOSSE COMMUNE//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationFosse()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructionsPage2();
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
            EnleverMenuConstructionsPage2();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationMaisonEnPierre.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisMaisonEnPierre()
    {

        PanelInformationMaisonEnPierre.SetActive(false);

        MenuConstructions();


    }
    ////////////////////////////////////////////////////FIN MAISON EN PIERRE//////////////////////////////////////////////////////////////







    ////////////////////////////////////////////////////DEBUT GARDE-MANGER//////////////////////////////////////////////////////////////
    public void AfficherMenuInformationGardeManger()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructionsPage2();
            boutonMenu.SetActive(false);
            boutonMenuEstAffiche = false;
            PanelInformationGardeManger.SetActive(true);

        }
    }

    public void RetourAuMenuConstructionDepuisGardeManger()
    {

        PanelInformationGardeManger.SetActive(false);

        MenuConstructions();


    }
    ////////////////////////////////////////////////////FIN GARDE-MANGER//////////////////////////////////////////////////////////////








    ////////////////////////////////////////////////////DEBUT FERME//////////////////////////////////////////////////////////////    
    public void AfficherMenuInformationFerme()
    {
        if (MenuConstructionEstAffiche)
        {
            EnleverMenuConstructionsPage2();
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

    /////////////////////////////////////////////////////FIN PARTIE MENU/////////////////////////////////////////////////////////////////////////














    ////////////////////////////////////////////////////DEBUT PARTIE CONSTRUCTION/////////////////////////////////////////////////////////////////





    /////////////////////////////////////////////////////DÉBUT CHAUMIERE/////////////////////////////////////////////////////////////////
    public void ConstruireChaumièreDepuisMenuInformation()
    {

        nouvelleChaumière = Instantiate(prefabChaumièreDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)3.1, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleChaumière.name = "nouvelleChaumière";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationChaumière.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionChaumière.SetActive(true);




    }

    public void ConstruireChaumièreDepuisMenuConstruction()
    {


        nouvelleChaumière = Instantiate(prefabChaumièreDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)3.1, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleChaumière.name = "nouvelleChaumière";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionChaumière.SetActive(true);




    }


    public void ValiderConstructionChaumière()
    {
        ChaumièreDéplaçable = GameObject.Find("nouvelleChaumière");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiChaumière = Instantiate(prefabBatiChaumière, ChaumièreDéplaçable.transform.position, Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiChaumière.name = ("BatiChaumière");
        Destroy(nouvelleChaumière); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        boutonValiderConstructionChaumière.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )

    }
}


