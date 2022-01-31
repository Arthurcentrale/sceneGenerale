﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonMenu2 : MonoBehaviour
{
    Vector3 position = new Vector3(7f, 1f, 7f);

    public static bool en_construction;
    /// Début des bools associés à chacun des bâtiments
    public static bool en_construction_Chaumière;
    public static bool en_construction_Pêcherie;
    public static bool en_construction_MoulinAEau;
    public static bool en_construction_MoulinAVent;
    public static bool en_construction_Boulangerie;
    public static bool en_construction_Cabanon;
    public static bool en_construction_Puits;
    public static bool en_construction_Fosse;
    public static bool en_construction_Forge;
    public static bool en_construction_MaisonPierre;
    public static bool en_construction_GardeManger;
    public static bool en_construction_Ferme;
    public static bool en_construction_Etabli;
    /// </summary>
    /// 
    /// Chaumière ///
    public GameObject boutonValiderConstructionChaumière;
    public GameObject ChaumièreDéplaçable;
    public GameObject BatiChaumière;
    public GameObject prefabBatiChaumière;
    public GameObject prefabChaumièreDéplaçable;
    public static GameObject nouvelleChaumière;


    /// Pêcherie ///
    public GameObject boutonValiderConstructionPêcherie;
    public GameObject PêcherieDéplaçable;
    public GameObject BatiPêcherie;
    public GameObject prefabBatiPêcherie;
    public GameObject prefabPêcherieDéplaçable;
    public static GameObject nouvellePêcherie;


    /// MoulinAEau ///
    public GameObject boutonValiderConstructionMoulinAEau;
    public GameObject MoulinAEauDéplaçable;
    public GameObject BatiMoulinAEau;
    public GameObject prefabBatiMoulinAEau;
    public GameObject prefabMoulinAEauDéplaçable;
    public static GameObject nouvelleMoulinAEau;


    /// MoulinAVent ///
    public GameObject boutonValiderConstructionMoulinAVent;
    public GameObject MoulinAVentDéplaçable;
    public GameObject BatiMoulinAVent;
    public GameObject prefabBatiMoulinAVent;
    public GameObject prefabMoulinAVentDéplaçable;
    public static GameObject nouvelleMoulinAVent;


    /// Boulangerie ///
    public GameObject boutonValiderConstructionBoulangerie;
    public GameObject BoulangerieDéplaçable;
    public GameObject BatiBoulangerie;
    public GameObject prefabBatiBoulangerie;
    public GameObject prefabBoulangerieDéplaçable;
    public static GameObject nouvelleBoulangerie;


    /// Cabanon ///
    public GameObject boutonValiderConstructionCabanon;
    public GameObject CabanonDéplaçable;
    public GameObject BatiCabanon;
    public GameObject prefabBatiCabanon;
    public GameObject prefabCabanonDéplaçable;
    public static GameObject nouvelleCabanon;


    /// Puits ///
    public GameObject boutonValiderConstructionPuits;
    public GameObject PuitsDéplaçable;
    public GameObject BatiPuits;
    public GameObject prefabBatiPuits;
    public GameObject prefabPuitsDéplaçable;
    public static GameObject nouvellePuits;

    /// Forge ///
    public GameObject boutonValiderConstructionForge;
    public GameObject ForgeDéplaçable;
    public GameObject BatiForge;
    public GameObject prefabBatiForge;
    public GameObject prefabForgeDéplaçable;
    public static GameObject nouvelleForge;


    /// Fosse ///
    public GameObject boutonValiderConstructionFosse;
    public GameObject FosseDéplaçable;
    public GameObject BatiFosse;
    public GameObject prefabBatiFosse;
    public GameObject prefabFosseDéplaçable;
    public static GameObject nouvelleFosse;


    /// MaisonPierre ///
    public GameObject boutonValiderConstructionMaisonPierre;
    public GameObject MaisonPierreDéplaçable;
    public GameObject BatiMaisonPierre;
    public GameObject prefabBatiMaisonPierre;
    public GameObject prefabMaisonPierreDéplaçable;
    public static GameObject nouvelleMaisonPierre;


    /// GardeManger ///
    public GameObject boutonValiderConstructionGardeManger;
    public GameObject GardeMangerDéplaçable;
    public GameObject BatiGardeManger;
    public GameObject prefabBatiGardeManger;
    public GameObject prefabGardeMangerDéplaçable;
    public static GameObject nouvelleGardeManger;


    /// Ferme ///
    public GameObject boutonValiderConstructionFerme;
    public GameObject FermeDéplaçable;
    public GameObject BatiFerme;
    public GameObject prefabBatiFerme;
    public GameObject prefabFermeDéplaçable;
    public static GameObject nouvelleFerme;

    /// Etabli ///
    public GameObject boutonValiderConstructionEtabli;
    public GameObject EtabliDéplaçable;
    public GameObject BatiEtabli;
    public GameObject prefabBatiEtabli;
    public GameObject prefabEtabliDéplaçable;
    public static GameObject nouvelleEtabli;


    public GameObject player;

    public GameObject boutonAnnulerConstruction;
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
    public GameObject Etabli;
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
    public GameObject PanelInformationPêcherie;
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
    public GameObject PanelInformationEtabli;

    private Text ouvrierOccupe;
    private GameObject batimentsConstruBoutons;

    public GameObject boutonInventaire;
    public GameObject favorisInventaire;

    // Dossier des batiments
    // Permet d'instantier chque batiment dedans
    private Transform dossierBatiments;
    [HideInInspector] public livre livre;

    void Start()
    {
        dossierBatiments = GameObject.Find("Batiments").transform;
        ouvrierOccupe = GameObject.Find("TextOuvrierOccupe").GetComponent<Text>();
        batimentsConstruBoutons = GameObject.Find("menuConstructionPageGauche");
        livre = GameObject.Find("Camera").GetComponent<livre>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuEstAffiche | MenuConstructionEstAffiche | MenuInformationTenteEstAffiche | PanelInformationTenteEstAffiche)
        {
            Deplacement.enMenu = true;
        }
    }

    public void empecherConstru()
    {
        ouvrierOccupe.enabled = true;
    }

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

    public void desacBoutonsConstru()
    {
        foreach (Transform child in batimentsConstruBoutons.transform.GetChild(2))
        {
            child.gameObject.GetComponent<Button>().interactable = false;
        }
        foreach (Transform child in batimentsConstruBoutons.transform.GetChild(3))
        {
            child.gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void acBoutonsConstru()
    {
        foreach (Transform child in batimentsConstruBoutons.transform.GetChild(2))
        {
            child.gameObject.GetComponent<Button>().interactable = true;
        }
        foreach (Transform child in batimentsConstruBoutons.transform.GetChild(3))
        {
            child.gameObject.GetComponent<Button>().interactable = true;
        }
    }


    /////////////////////////////////////////////////////DÉBUT CHAUMIERE/////////////////////////////////////////////////////////////////
    public void ConstruireChaumièreDepuisMenuInformation()
    {

        nouvelleChaumière = Instantiate(prefabChaumièreDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleChaumière.name = "nouvelleChaumière";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        //rajout du bool pour chaque bâtiment, pour que le script TerrainRayonYoupi fonctionne correctement
        en_construction_Chaumière = true;
        //
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationChaumière.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionChaumière.SetActive(true);
        




    }

    public void ConstruireChaumièreDepuisMenuConstruction()
    {


        nouvelleChaumière = Instantiate(prefabChaumièreDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleChaumière.name = "nouvelleChaumière";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Chaumière = true;
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
        BatiChaumière = Instantiate(prefabBatiChaumière, ChaumièreDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiChaumière.name = ("Bati");
        Destroy(nouvelleChaumière); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_Chaumière = false;
        boutonValiderConstructionChaumière.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        empecherConstru();
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }





    /////////////////////////////////////////////////////DÉBUT PECHERIE/////////////////////////////////////////////////////////////////
    public void ConstruirePêcherieDepuisMenuInformation()
    {

        nouvellePêcherie = Instantiate(prefabPêcherieDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvellePêcherie.name = "nouvellePêcherie";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Pêcherie = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationPêcherie.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionPêcherie.SetActive(true);




    }

    public void ConstruirePêcherieDepuisMenuConstruction()
    {


        nouvellePêcherie = Instantiate(prefabPêcherieDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvellePêcherie.name = "nouvellePêcherie";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Pêcherie = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionPêcherie.SetActive(true);




    }


    public void ValiderConstructionPêcherie()
    {
        PêcherieDéplaçable = GameObject.Find("nouvellePêcherie");

        bool autorisation1 = PêcherieDéplaçable.transform.GetChild(2).gameObject.GetComponent<CollisionRiviere>().autorisation;
        bool autorisation2 = PêcherieDéplaçable.transform.GetChild(1).gameObject.GetComponent<CollisionTerre>().autorisation;


        if (autorisation1 && autorisation2)
        {
            BatiPêcherie = Instantiate(prefabBatiPêcherie, PêcherieDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
            BatiPêcherie.name = ("Bati");
            Destroy(nouvellePêcherie); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
            en_construction = false;
            en_construction_Pêcherie = false;
            boutonValiderConstructionPêcherie.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
            Deplacement.enMenu = false;
            BuildingLayerMag.updateBatLayers();

            empecherConstru();
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            livre.animatorLivreFerme.SetTrigger("Return");

        }
        
    }







    /////////////////////////////////////////////////////DÉBUT MOUIN A EAU/////////////////////////////////////////////////////////////////
    public void ConstruireMoulinAEauDepuisMenuInformation()
    {

        nouvelleMoulinAEau = Instantiate(prefabMoulinAEauDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleMoulinAEau.name = "nouvelleMoulinAEau"; // oui nouvelle :) 
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_MoulinAEau = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationMoulinAEau.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionMoulinAEau.SetActive(true);




    }

    public void ConstruireMoulinAEauDepuisMenuConstruction()
    {


        nouvelleMoulinAEau = Instantiate(prefabMoulinAEauDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleMoulinAEau.name = "nouvelleMoulinAEau";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_MoulinAEau = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionMoulinAEau.SetActive(true);




    }


    public void ValiderConstructionMoulinAEau()
    {
        MoulinAEauDéplaçable = GameObject.Find("nouvelleMoulinAEau");

        bool autorisation1 = MoulinAEauDéplaçable.transform.GetChild(2).gameObject.GetComponent<CollisionRiviere>().autorisation;
        bool autorisation2 = MoulinAEauDéplaçable.transform.GetChild(1).gameObject.GetComponent<CollisionTerre>().autorisation;

        if (autorisation1 && autorisation2)
        {
            BatiMoulinAEau = Instantiate(prefabBatiMoulinAEau, MoulinAEauDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
            BatiMoulinAEau.name = ("Bati");
            Destroy(nouvelleMoulinAEau); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
            en_construction = false;
            en_construction_MoulinAEau = false;
            boutonValiderConstructionMoulinAEau.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
            Deplacement.enMenu = false;
            BuildingLayerMag.updateBatLayers();

            empecherConstru();
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            livre.animatorLivreFerme.SetTrigger("Return");
        }

    }













    /////////////////////////////////////////////////////DÉBUT MOULIN A VENT/////////////////////////////////////////////////////////////////
    public void ConstruireMoulinAVentDepuisMenuInformation()
    {

        nouvelleMoulinAVent = Instantiate(prefabMoulinAVentDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleMoulinAVent.name = "nouvelleMoulinAVent";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_MoulinAVent = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationMoulinAVent.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionMoulinAVent.SetActive(true);




    }

    public void ConstruireMoulinAVentDepuisMenuConstruction()
    {


        nouvelleMoulinAVent = Instantiate(prefabMoulinAVentDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleMoulinAVent.name = "nouvelleChaumière";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_MoulinAVent = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionMoulinAVent.SetActive(true);




    }


    public void ValiderConstructionMoulinAVent()
    {
        MoulinAVentDéplaçable = GameObject.Find("nouvelleMoulinAVent");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiMoulinAVent = Instantiate(prefabBatiMoulinAVent, MoulinAVentDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiMoulinAVent.name = ("Bati");
        Destroy(nouvelleMoulinAVent); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_MoulinAVent = false;
        boutonValiderConstructionMoulinAVent.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        empecherConstru();
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }











    /////////////////////////////////////////////////////DÉBUT BOULANGERIE/////////////////////////////////////////////////////////////////
    public void ConstruireBoulangerieDepuisMenuInformation()
    {

        nouvelleBoulangerie = Instantiate(prefabBoulangerieDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleBoulangerie.name = "nouvelleBoulangerie";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Boulangerie = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationBoulangerie.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionBoulangerie.SetActive(true);




    }

    public void ConstruireBoulangerieDepuisMenuConstruction()
    {


        nouvelleBoulangerie = Instantiate(prefabBoulangerieDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleBoulangerie.name = "nouvelleBoulangerie";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Boulangerie = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionBoulangerie.SetActive(true);




    }


    public void ValiderConstructionBoulangerie()
    {
        BoulangerieDéplaçable = GameObject.Find("nouvelleBoulangerie");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiBoulangerie = Instantiate(prefabBatiBoulangerie, BoulangerieDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiBoulangerie.name = ("Bati");
        Destroy(nouvelleBoulangerie); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_Boulangerie = false;
        boutonValiderConstructionBoulangerie.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        empecherConstru();
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }















    /////////////////////////////////////////////////////DÉBUT CABANON/////////////////////////////////////////////////////////////////
    public void ConstruireCabanonDepuisMenuInformation()
    {

        nouvelleCabanon = Instantiate(prefabCabanonDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleCabanon.name = "nouvelleCabanon";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Cabanon = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationCabanon.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionCabanon.SetActive(true);




    }

    public void ConstruireCabanonDepuisMenuConstruction()
    {


        nouvelleCabanon = Instantiate(prefabCabanonDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleCabanon.name = "nouvelleCabanon";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Cabanon = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionCabanon.SetActive(true);




    }


    public void ValiderConstructionCabanon()
    {
        CabanonDéplaçable = GameObject.Find("nouvelleCabanon");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiCabanon = Instantiate(prefabBatiCabanon, CabanonDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiCabanon.name = ("Bati");
        Destroy(nouvelleCabanon); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_Cabanon = false;
        boutonValiderConstructionCabanon.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        empecherConstru();
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }













    /////////////////////////////////////////////////////DÉBUT PUITS/////////////////////////////////////////////////////////////////
    public void ConstruirePuitsDepuisMenuInformation()
    {

        nouvellePuits = Instantiate(prefabPuitsDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvellePuits.name = "nouvellePuits";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Puits = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationPuits.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionPuits.SetActive(true);




    }

    public void ConstruirePuitsDepuisMenuConstruction()
    {


        nouvellePuits = Instantiate(prefabPuitsDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvellePuits.name = "nouvellePuits";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Puits = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionPuits.SetActive(true);




    }


    public void ValiderConstructionPuits()
    {
        PuitsDéplaçable = GameObject.Find("nouvellePuits");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiPuits = Instantiate(prefabBatiPuits, PuitsDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiPuits.name = ("Bati");
        Destroy(nouvellePuits); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_Puits = false;
        boutonValiderConstructionPuits.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        empecherConstru();
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }












    /////////////////////////////////////////////////////DÉBUT FORGE/////////////////////////////////////////////////////////////////
    public void ConstruireForgeDepuisMenuInformation()
    {

        nouvelleForge = Instantiate(prefabForgeDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleForge.name = "nouvelleForge";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Forge = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationForge.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionForge.SetActive(true);




    }

    public void ConstruireForgeDepuisMenuConstruction()
    {


        nouvelleForge = Instantiate(prefabForgeDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleForge.name = "nouvelleForge";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Forge = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionForge.SetActive(true);




    }


    public void ValiderConstructionForge()
    {
        ForgeDéplaçable = GameObject.Find("nouvelleForge");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiForge = Instantiate(prefabBatiForge, ForgeDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiForge.name = ("Bati");
        Destroy(nouvelleForge); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_Forge = false;
        boutonValiderConstructionForge.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        empecherConstru();
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }










    /////////////////////////////////////////////////////DÉBUT FOSSE/////////////////////////////////////////////////////////////////
    public void ConstruireFosseDepuisMenuInformation()
    {

        nouvelleFosse = Instantiate(prefabFosseDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleFosse.name = "nouvelleFosse";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Fosse = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationFosse.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionFosse.SetActive(true);




    }

    public void ConstruireFosseDepuisMenuConstruction()
    {


        nouvelleFosse = Instantiate(prefabFosseDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleFosse.name = "nouvelleFosse";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Fosse = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionFosse.SetActive(true);




    }


    public void ValiderConstructionFosse()
    {
        FosseDéplaçable = GameObject.Find("nouvelleFosse");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiFosse = Instantiate(prefabBatiFosse, FosseDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiFosse.name = ("Bati");
        Destroy(nouvelleFosse); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_Fosse = false;
        boutonValiderConstructionFosse.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        empecherConstru();
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }





    /////////////////////////////////////////////////////DÉBUT MAISON EN PIERRE/////////////////////////////////////////////////////////////////
    public void ConstruireMaisonPierreDepuisMenuInformation()
    {

        nouvelleMaisonPierre = Instantiate(prefabMaisonPierreDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleMaisonPierre.name = "nouvelleMaisonPierre";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_MaisonPierre = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationMaisonPierre.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionMaisonPierre.SetActive(true);




    }

    public void ConstruireMaisonPierreDepuisMenuConstruction()
    {


        nouvelleMaisonPierre = Instantiate(prefabMaisonPierreDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleMaisonPierre.name = "nouvelleMaisonPierre";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_MaisonPierre = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionMaisonPierre.SetActive(true);




    }


    public void ValiderConstructionMaisonPierre()
    {
        MaisonPierreDéplaçable = GameObject.Find("nouvelleMaisonPierre");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiMaisonPierre = Instantiate(prefabBatiMaisonPierre, MaisonPierreDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiMaisonPierre.name = ("Bati");
        Destroy(nouvelleMaisonPierre); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_MaisonPierre = false;
        boutonValiderConstructionMaisonPierre.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        empecherConstru();
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }






    /////////////////////////////////////////////////////DÉBUT GARDE MANGER/////////////////////////////////////////////////////////////////
    public void ConstruireGardeMangerDepuisMenuInformation()
    {

        nouvelleGardeManger = Instantiate(prefabGardeMangerDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleGardeManger.name = "nouvelleGardeManger";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_GardeManger = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationGardeManger.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionGardeManger.SetActive(true);




    }

    public void ConstruireGardeMangerDepuisMenuConstruction()
    {


        nouvelleGardeManger = Instantiate(prefabGardeMangerDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleGardeManger.name = "nouvelleGardeManger";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_GardeManger = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionGardeManger.SetActive(true);




    }


    public void ValiderConstructionGardeManger()
    {
        GardeMangerDéplaçable = GameObject.Find("nouvelleGardeManger");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiGardeManger = Instantiate(prefabBatiGardeManger, GardeMangerDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiGardeManger.name = ("Bati");
        Destroy(nouvelleGardeManger); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_GardeManger = false;
        boutonValiderConstructionGardeManger.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        empecherConstru();
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }





    /////////////////////////////////////////////////////DÉBUT FERME/////////////////////////////////////////////////////////////////
    public void ConstruireFermeDepuisMenuInformation()
    {

        nouvelleFerme = Instantiate(prefabFermeDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleFerme.name = "nouvelleFerme";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Ferme = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationFerme.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionFerme.SetActive(true);




    }

    public void ConstruireFermeDepuisMenuConstruction()
    {


        nouvelleFerme = Instantiate(prefabFermeDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleFerme.name = "nouvelleFerme";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Ferme = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionFerme.SetActive(true);




    }


    public void ValiderConstructionFerme()
    {
        FermeDéplaçable = GameObject.Find("nouvelleFerme");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiFerme = Instantiate(prefabBatiFerme, FermeDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiFerme.name = ("Bati");
        Destroy(nouvelleFerme); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_Ferme = false;
        boutonValiderConstructionFerme.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        empecherConstru();
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }


    /////////////////////////////////////////////////////DÉBUT ETABLI/////////////////////////////////////////////////////////////////
    public void ConstruireEtabliDepuisMenuInformation()
    {

        nouvelleEtabli = Instantiate(prefabEtabliDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleEtabli.name = "nouvelleEtabli";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Etabli = true;
        //MenuInformationChaumièrestAffiche = false;
        PanelInformationEtabli.SetActive(false);
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionEtabli.SetActive(true);




    }

    public void ConstruireEtabliDepuisMenuConstruction()
    {


        nouvelleEtabli = Instantiate(prefabEtabliDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)5, 0), Quaternion.Euler(-20, 0, 0), dossierBatiments); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
        nouvelleEtabli.name = "nouvelleEtabli";
        //print("console");
        //col = nouveauMoulin.GetComponent<BoxCollider>(); // c'était utile quand on travaillait avec des cubes, mais là c'est des planes il faudra adapter avec le nouveau système d'hitibox
        en_construction = true;
        en_construction_Etabli = true;
        //MenuInformationChaumièrestAffiche = false;
        EnleverMenuConstructionsPage1(); // cette fois on enlève le menu de construction, puisqu'on construit directement depuis le menu
        boutonMenu.SetActive(true);
        boutonMenuEstAffiche = true;
        boutonValiderConstructionEtabli.SetActive(true);
        



    }


    public void ValiderConstructionEtabli()
    {
        EtabliDéplaçable = GameObject.Find("nouvelleEtabli");
        //print(MoulinDéplaçable.transform.position.x);
        //print(MoulinDéplaçable.transform.position.y);
        //print(MoulinDéplaçable.transform.position.z);
        BatiEtabli = Instantiate(prefabBatiEtabli, EtabliDéplaçable.transform.position + new Vector3(0f, -2.7f, -5f), Quaternion.Euler(-20, 0, 0), dossierBatiments); //Le vrai bâti
        BatiEtabli.name = ("Bati");
        Destroy(nouvelleEtabli); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        en_construction_Etabli = false;
        boutonValiderConstructionEtabli.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )
        Deplacement.enMenu = false;
        BuildingLayerMag.updateBatLayers();

        ouvrierOccupe.enabled = true;
        desacBoutonsConstru();
        boutonInventaire.SetActive(true);
        favorisInventaire.SetActive(true);
        livre.animatorLivreFerme.SetTrigger("Return");
    }



    public void AnnulerConstruction(){ //j'en fais qu'un seul
        if (en_construction_Boulangerie){
            Destroy(nouvelleBoulangerie);
            en_construction=false;
            en_construction_Boulangerie=false;
            boutonValiderConstructionBoulangerie.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_Cabanon){
            Destroy(nouvelleCabanon);
            en_construction=false;
            en_construction_Cabanon=false;
            boutonValiderConstructionCabanon.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_Chaumière){
            Destroy(nouvelleChaumière);
            en_construction=false;
            en_construction_Chaumière=false;
            boutonValiderConstructionChaumière.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_Etabli){
            Destroy(nouvelleEtabli);
            en_construction=false;
            en_construction_Etabli=false;
            boutonValiderConstructionEtabli.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_Ferme){
            Destroy(nouvelleFerme);
            en_construction=false;
            en_construction_Ferme=false;
            boutonValiderConstructionFerme.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_Forge){
            Destroy(nouvelleForge);
            en_construction=false;
            en_construction_Forge=false;
            boutonValiderConstructionForge.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_Fosse){
            Destroy(nouvelleFosse);
            en_construction=false;
            en_construction_Fosse=false;
            boutonValiderConstructionFosse.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_GardeManger){
            Destroy(nouvelleGardeManger);
            en_construction=false;
            en_construction_GardeManger=false;
            boutonValiderConstructionGardeManger.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_MaisonPierre){
            Destroy(nouvelleMaisonPierre);
            en_construction=false;
            en_construction_MaisonPierre=false;
            boutonValiderConstructionMaisonPierre.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_MoulinAEau){
            Destroy(nouvelleMoulinAEau);
            en_construction=false;
            en_construction_MoulinAEau=false;
            boutonValiderConstructionMoulinAEau.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_MoulinAVent){
            Destroy(nouvelleMoulinAVent);
            en_construction=false;
            en_construction_MoulinAVent=false;
            boutonValiderConstructionMoulinAVent.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_Puits){
            Destroy(nouvellePuits);
            en_construction=false;
            en_construction_Puits=false;
            boutonValiderConstructionPuits.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

        if (en_construction_Pêcherie){
            Destroy(nouvellePêcherie);
            en_construction=false;
            en_construction_Pêcherie=false;
            boutonValiderConstructionPêcherie.SetActive(false);
            boutonAnnulerConstruction.SetActive(false);
            Deplacement.enMenu = false;
            desacBoutonsConstru();
            boutonInventaire.SetActive(true);
            favorisInventaire.SetActive(true);
            BuildingLayerMag.updateBatLayers();
            livre.animatorLivreFerme.SetTrigger("Return");
        }

    }






}
