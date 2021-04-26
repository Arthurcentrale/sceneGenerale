using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Agriculture : MonoBehaviour
{
    public static Camera MainCamera;
    /*
    public GameObject dirtPrefab;
    public GameObject grainePrefab;

    List<Vector3> positionsChamps;
    Vector3 posChamp;
    Vector3 posGraine;

    public static bool playerAbleToMove;

    bool corner1Selected;
    public static bool beginToSelect;
    public static bool commencerChamp;
    public static bool placerChamp;
    public static bool planterGraine;

    public static Touch farmCorner1;
    static Touch farmCorner2;

    public float longTouchTime = 1f;
    float touchBeganTime;
    float timeTouchLasted;
    bool touchFinished;
    */

    void Start()
    {
        MainCamera = GameObject.Find("Camera").GetComponent<Camera>();

        /*
        positionsChamps = new List<Vector3>();
        posChamp = Vector3.zero;
        posGraine = Vector3.zero;

        playerAbleToMove = true;

        corner1Selected = false;
        beginToSelect = false;
        commencerChamp = false;
        placerChamp = false;
        planterGraine = false;

        touchBeganTime = Time.time;
        */
    }

    /*
    void Update()
    {
        if (planterGraine)
        {
            PlanterGraine(posGraine);
            planterGraine = false;
        }
        else if (placerChamp)
        {
            commencerChamp = false;
            farmCorner2 = SelectionBox.endPos;

            Debug.Log("ON CREE LA FERME");
            CreeChamp(TouchToPos(farmCorner1), TouchToPos(farmCorner2), positionsChamps);
            placerChamp = false;
        }
        else if (commencerChamp)
        {
            corner1Selected = false;

            beginToSelect = true;
            if (SelectionBox.finishedSelecting) 
            {
                DialogUI.ShowFarmPanel(2);
            }
        }
        else if (corner1Selected)
        {
            DialogUI.ShowFarmPanel(1);
        }
        else
        {
            if (Player_script.VerifyTouch())
            {
                Touch touch = Player_script.ImportTouch();

                if (touch.phase == TouchPhase.Began)
                {
                    touchBeganTime = Time.time;
                }

                (touchFinished, timeTouchLasted, touchBeganTime) = DetectTouchDuration(touch,touchBeganTime);

                if (touchFinished)
                {
                    if (timeTouchLasted >= longTouchTime)
                    {
                        farmCorner1 = touch;
                        corner1Selected = true;
                    }
                    else
                    {
                        posChamp = ToucheUnChamp(TouchToPos(touch));
                        if (posChamp != Vector3.zero)
                        {
                            DialogUI.ShowFarmPanel(3);
                            posGraine = posChamp;
                        }
                        else if (!DialogUI.panelTouched)
                        {
                            playerAbleToMove = true;
                        }
                    }
                }
            }
        }
    }

    (bool,float,float) DetectTouchDuration(Touch touch,float timeTouchBegan) //retourne un bool qui dit si le toucher est finit ou pas, la durée depuis laquelle on appuie et l'heure à laquelle on a commencé
    {
        //Si le joueur appuye sans bouger
        if (touch.phase == TouchPhase.Stationary)
        {
            float timeSinceTouch = Time.time - touchBeganTime;
            //Debug.Log("vous appuyez depuis : " + timeSinceTouch.ToString()); //On affiche le temps depuis lequel on appuye sur l'écran
            return (false,timeSinceTouch, touchBeganTime);
        }
        // si on a appuyé pendant un temps court, alors on considère qu'on voulait seulement déplacer le joueur
        else if (touch.phase == TouchPhase.Ended)
        {
            float timeSinceTouch = Time.time - touchBeganTime;
            //Debug.Log("vous appuyez depuis : " + timeSinceTouch.ToString()); //On affiche le temps depuis lequel on appuye sur l'écran
            return (true, timeSinceTouch, timeTouchBegan);
        }
        else
        {
            float timeSinceTouch = Time.time - touchBeganTime;
            return (false, timeSinceTouch, timeTouchBegan);
        }
    }

    */

    public static Vector3 TouchToPos(Touch touch) //Retourne la position sur le plan en 3D à partir du toucher de l'écran
    {
        //on récupère la position en pixel du toucher de l'utilisateur sur l'écran
        Vector2 touchPixelPosition = touch.position;

        //on transforme cette position en rayon perpendiculaire au plan de la camera
        
        Ray ray = MainCamera.ScreenPointToRay(new Vector3(touchPixelPosition.x, touchPixelPosition.y, 0f));
        RaycastHit hit;
        Vector3 newTargetPos = new Vector3();
        //si ce rayon rencontre un obstable on récupère la position de l'impact et le jouer ce déplace vers cette position
        if (Physics.Raycast(ray, out hit))
        {
            newTargetPos = hit.point;
            //Debug.Log("Hit " + hit.collider.gameObject.name);
        }
        return newTargetPos;
    }

    /*
    
    void CreeChamp(Vector3 Coin1,Vector3 Coin2, List<Vector3> liste) //creer un champ entre les 2 coordonnées en ajoutant les postitions à la liste
    {
        float x1 = Coin1.x;
        float y1 = Coin1.z;

        float x2 = Coin2.x;
        float y2 = Coin2.z;

        float i, j;

        //Debug.Log("x1 : " + x1.ToString() + " x2 : " + x2.ToString() + " y1 : " + y1.ToString() + " y2 : " + y2.ToString());

        //Renderer rend = dirtPrefab.GetComponent<Renderer>();
        //Vector3 size = rend.bounds.size;


        //on echange les coordonnés si elles ne correspondent pas au parcours du champ dans l'ordre croissant
        if (x1 > x2)
        {
            float aux = x1;
            x1 = x2;
            x2 = aux;
        }

        if (y1 > y2)
        {
            float aux = y1;
            y1 = y2;
            y2 = aux;
        }

        //puis on parcourt le champ
        for (i = x1; i <= x2; i += 1f)
        {
            for (j = y1; j <= y2; j += 1f)
            {
                
                Debug.Log("Bloc posé");
                Vector3 pos = new Vector3(i, 0.01f, j);
                Instantiate(dirtPrefab, pos, Quaternion.identity);
                liste.Add(pos);
            }
        }
        //myTilemap.RefreshAllTiles();
    }

    void PlanterGraine(Vector3 pos)
    {
        Debug.Log("Graine posée");
        Instantiate(grainePrefab, pos, Quaternion.identity);
    }

    Vector3 ToucheUnChamp(Vector3 pos) //si un champ se trouve à la position donnée en argument on retourne la coordonnée de son centre, sinon on retourne le vecteur nul (un champ est toujours légèrement en hauteur donc ne se trouve jamais en (0,0,0))
    {
        Vector3 posChamp = Vector3.zero;
        float taille = (dirtPrefab.GetComponent<Renderer>().bounds.size.x) / 2;
        float x = pos.x;
        float z = pos.z;
        foreach(Vector3 champ in positionsChamps)
        {
            float x0 = champ.x;
            float z0 = champ.z;
            if (((x0 - taille) <= x) && (x <= (x0 + taille)) && ((z0 - taille) <= z) && (z <= (z0 + taille)))
            {
                posChamp = champ;
            }
        }
        return posChamp;
    }

*/
}