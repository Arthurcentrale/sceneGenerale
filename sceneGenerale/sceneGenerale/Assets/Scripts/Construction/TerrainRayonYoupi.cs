using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRayonYoupi : MonoBehaviour
{
    public GameObject ChaumièreDéplaçablePrefab;
    private bool jeToucheUnBouton = false; //pour éviter de déplacer le bâti quand je veux valider sa construction
    //public BoutonsMenuConstruction boutonsMenuConstruction;
    private void OnMouseDown()  // même principe que le point and click
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        RaycastHit hit;
        //print("je touche le terrain, super!! hihihihi");

        if (BoutonsMenuConstruction.en_construction)
        {   // ici je prends le bool en_construction qui se situe dans le script construction
            //print("oui");
        }

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 dir = hit.point;

            if (hit.collider.CompareTag("Bouton"))
            {
                jeToucheUnBouton = true;
                //print("Je touche un bouton, eww");
            }
            if (BoutonsMenuConstruction.en_construction && !jeToucheUnBouton) // + Un bool pour chacun des bâtiments? ( ;_________; )
            {   // ici je prends le bool en_construction qui se situe dans le script construction
                //print("oui");
                ChaumièreDéplaçablePrefab = GameObject.Find("nouvelleChaumière");
                ChaumièreDéplaçablePrefab.gameObject.transform.localPosition = new Vector3(dir.x, 3.1f, dir.z); // Et là je déplace nouveauBatiment qui provient aussi du script construction
            }
        }
        //moving = true;
    }
}
