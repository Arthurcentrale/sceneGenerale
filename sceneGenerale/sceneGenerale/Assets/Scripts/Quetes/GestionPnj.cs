using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPnj : MonoBehaviour
{
    public static Camera MainCamera;

    void Start()
    {
        MainCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Player_script.VerifyTouch())
        {
            Touch touch = Player_script.ImportTouch();
            string name = ObjectTouched(touch);
            if (name == "Pnj_test")
            {
                TriggerDialog();
                bool activation = GameObject.Find("Inventory").GetComponent<Canvas>().enabled;
                activation = !activation;
                GameObject.Find("Inventory").GetComponent<Canvas>().enabled = activation;
            }
        }
    }

    public static string ObjectTouched(Touch touch) //Retourne le nom de l'object touché par le joueur 
    {
        //on récupère la position en pixel du toucher de l'utilisateur sur l'écran
        Vector2 touchPixelPosition = touch.position;

        //on transforme cette position en rayon perpendiculaire au plan de la camera

        Ray ray = MainCamera.ScreenPointToRay(new Vector3(touchPixelPosition.x, touchPixelPosition.y, 0f));
        RaycastHit hit;
        //si ce rayon rencontre un obstable on le stocke dans obj
        string name = "";
        if (Physics.Raycast(ray, out hit))
        {
            name = hit.collider.gameObject.name;
        }
        return name;
    }

    void TriggerDialog()
    {
        Quest_Dialog.OpenDialogPanel();
    }
 }
