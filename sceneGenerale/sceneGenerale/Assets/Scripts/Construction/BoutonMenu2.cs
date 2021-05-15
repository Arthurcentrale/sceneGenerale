using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonMenu2 : MonoBehaviour
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



    

    // Update is called once per frame
    void Update()
    {
        
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


    /////////////////////////////////////////////////////DÉBUT CHAUMIERE/////////////////////////////////////////////////////////////////
    public void ConstruireChaumièreDepuisMenuInformation()
    {

        nouvelleChaumière = Instantiate(prefabChaumièreDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)3.1, 0), Quaternion.Euler(-20, 0, 0)); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
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


        nouvelleChaumière = Instantiate(prefabChaumièreDéplaçable, player.transform.position + 3 * Vector3.forward + new Vector3(0, (float)3.1, 0), Quaternion.Euler(-20, 0, 0)); // Quaternion Euler c'est pour les angles, pour qu'on garde bien la bonne vue
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
        BatiChaumière = Instantiate(prefabBatiChaumière, ChaumièreDéplaçable.transform.position, Quaternion.Euler(-20, 0, 0)); //Le vrai bâti
        BatiChaumière.name = ("BatiChaumière");
        Destroy(nouvelleChaumière); // On détruit le plane qui permet de valider la position du bâtiment (Si on passe pas par un plane intermédiaire, quand on cliquera sur le plane un menu s'affichera du coup on pourra pas placer précisément le bâtiment
        en_construction = false;
        boutonValiderConstructionChaumière.SetActive(false); // on enlève le menu valider (oui yen a un pour chaque bâtiment oui :) :) :) :) )

    }
}
