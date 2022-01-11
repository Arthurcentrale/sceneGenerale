using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // pour le bug avec les bouttons 
//oyu
public class TerrainRayonYoupi : MonoBehaviour
{
    public GameObject ChaumièreDéplaçable;
    public GameObject PêcherieDéplaçable;
    public GameObject MoulinAEauDéplaçable;
    public GameObject MoulinAVentDéplaçable;
    public GameObject BoulangerieDéplaçable;
    public GameObject CabanonDéplaçable;
    public GameObject PuitsDéplaçable;
    public GameObject ForgeDéplaçable;
    public GameObject FosseDéplaçable;
    public GameObject MaisonPierreDéplaçable;
    public GameObject GardeMangerDéplaçable;
    public GameObject FermeDéplaçable;

    private bool jeToucheUnBouton = false; //pour éviter de déplacer le bâti quand je veux valider sa construction
    public Camera cam;
    //public BoutonsMenuConstruction boutonsMenuConstruction;
    private void OnMouseDown()  // même principe que le point and click
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);

        RaycastHit hit;
        //print("je touche le terrain, super!! hihihihi");

        if (BoutonsMenuConstruction.en_construction)
        {   // ici je prends le bool en_construction qui se situe dans le script construction
            //print("oui");
        }

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 dir = hit.point;
            // cette partie ne devrait plus être nécessaire si on utilise les rect.contains
            //if (hit.collider.CompareTag("Bouton"))
            //{
            //    jeToucheUnBouton = true;
            //    //print("Je touche un bouton, eww");
            //}

            ///////////////////////////////////////  CHAUMIERE //////////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_Chaumière) // + Un bool pour chacun des bâtiments? ( ;_________; ) Oui :) et du coup en_construction est obsolète :) 
            {   // ici je prends le bool en_construction qui se situe dans le script construction
                //print("oui");
                //print("ohayo");
                //print(dir.y);
                ChaumièreDéplaçable = GameObject.Find("nouvelleChaumière");
                if (!EventSystem.current.IsPointerOverGameObject())    // On ne déplace pas la chaumière si on clique sur un bouton ou autre objet gui
                {
                    ChaumièreDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }// Et là je déplace nouveauBatiment qui provient aussi du script construction
                //3.1f est un facteur correctif propre à la hauteur du bati
            }

            //////////////////////////////// PECHERIE //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_Pêcherie)
            {
                PêcherieDéplaçable = GameObject.Find("nouvellePêcherie");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    PêcherieDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                    
                    
                }
            }



            //////////////////////////////// MOULIN A EAU //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_MoulinAEau)
            {
                MoulinAEauDéplaçable = GameObject.Find("nouvelleMoulinAEau");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    MoulinAEauDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }
            }





            //////////////////////////////// MOULIN A VENT //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_MoulinAVent)
            {
                MoulinAVentDéplaçable = GameObject.Find("nouvelleMoulinAVent");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    MoulinAVentDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }
            }






            //////////////////////////////// BOULANGERIE //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_Boulangerie)
            {
                BoulangerieDéplaçable = GameObject.Find("nouvelleBoulangerie");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    BoulangerieDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }
            }








            //////////////////////////////// CABANON //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_Cabanon)
            {
                CabanonDéplaçable = GameObject.Find("nouvelleCabanon");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    CabanonDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }
            }








            //////////////////////////////// PUITS //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_Puits)
            {
                PuitsDéplaçable = GameObject.Find("nouvellePuits");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    PuitsDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }
            }










            //////////////////////////////// FORGE //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_Forge)
            {
                ForgeDéplaçable = GameObject.Find("nouvelleForge");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    ForgeDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }
            }










            //////////////////////////////// FOSSE //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_Fosse)
            {
                FosseDéplaçable = GameObject.Find("nouvelleFosse");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    FosseDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }
            }








            //////////////////////////////// MAISON EN PIERRE //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_MaisonPierre)
            {
                MaisonPierreDéplaçable = GameObject.Find("nouvelleMaisonPierre");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    MaisonPierreDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }
            }





            //////////////////////////////// GARDE MANGER //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_GardeManger)
            {
                GardeMangerDéplaçable = GameObject.Find("nouvelleGardeManger");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    GardeMangerDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }
            }







            //////////////////////////////// FERME //////////////////////////////////////
            if (BoutonMenu2.en_construction && BoutonMenu2.en_construction_Ferme)
            {
                FermeDéplaçable = GameObject.Find("nouvelleFerme");
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    FermeDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y + 5f, dir.z);
                }
            }




        }
        //moving = true;
    }

    
}
