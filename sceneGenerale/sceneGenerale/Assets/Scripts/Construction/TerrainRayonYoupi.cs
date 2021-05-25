using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRayonYoupi : MonoBehaviour
{
    public GameObject ChaumièreDéplaçable;
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
            if (BoutonMenu2.en_construction) // + Un bool pour chacun des bâtiments? ( ;_________; )
            {   // ici je prends le bool en_construction qui se situe dans le script construction
                //print("oui");
                print("ohayo");
                print(dir.y);
                ChaumièreDéplaçable = GameObject.Find("nouvelleChaumière");
                ChaumièreDéplaçable.gameObject.transform.localPosition = new Vector3(dir.x, dir.y +3.1f, dir.z); // Et là je déplace nouveauBatiment qui provient aussi du script construction
                //3.1f est un facteur correctif propre à la hauteur du bati
            }
        }
        //moving = true;
    }
}
