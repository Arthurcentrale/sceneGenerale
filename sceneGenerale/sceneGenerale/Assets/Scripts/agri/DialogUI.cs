using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DialogUI : MonoBehaviour
{
    /*
    GraphicRaycaster rc;
    Touch touch;
    PointerEventData pEventData;
    EventSystem eventSystem;

    public static bool panelTouched;

    public static GameObject backgroundPanel;
    static GameObject Farm_Panel1;
    static GameObject Farm_Panel2;
    static GameObject Plant_Panel;

    void Start()
    {
        eventSystem = GetComponent<EventSystem>(); 
        rc = GetComponent<GraphicRaycaster>();

        panelTouched = false;

        backgroundPanel = this.transform.GetChild(0).gameObject;
        Farm_Panel1 = backgroundPanel.transform.GetChild(0).gameObject;
        Farm_Panel2 = backgroundPanel.transform.GetChild(1).gameObject;
        Plant_Panel = backgroundPanel.transform.GetChild(2).gameObject;
    }

    void Update()
    {
        if (Player_script.VerifyTouch())
        {
            //on récupère la position en pixel du toucher de l'utilisateur sur l'écran
            touch = Player_script.ImportTouch();
            Vector2 touchPixelPosition = touch.position;

            //Set up the new Pointer Event
            pEventData = new PointerEventData(eventSystem);
            //Set the Pointer Event Position to that of the mouse position
            pEventData.position = touchPixelPosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            rc.Raycast(pEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                //Debug.Log("Hit " + result.gameObject.name);
                panelTouched = true;
            }
        }
    }

    public static void ShowFarmPanel(int i)
    {
        backgroundPanel.SetActive(true);
        if (i == 1) Farm_Panel1.SetActive(true);
        else if (i == 2) Farm_Panel2.SetActive(true);
        else Plant_Panel.SetActive(true);
    }

    public void CloseFarmPanel(int i)
    {
        backgroundPanel.SetActive(false);
        if (i == 1) Farm_Panel1.SetActive(false);
        else if (i == 2) Farm_Panel2.SetActive(false);
        else Plant_Panel.SetActive(false);
    }

    public void CommencerChamp()
    {
        Agriculture.commencerChamp = true;
        CloseFarmPanel(1);
    }

    public void PlacerChamp()
    {
        Agriculture.placerChamp = true;
        CloseFarmPanel(2);
    }

    public void PlanterGraine()
    {
        Agriculture.planterGraine = true;
        CloseFarmPanel(3);
    }
    */
}