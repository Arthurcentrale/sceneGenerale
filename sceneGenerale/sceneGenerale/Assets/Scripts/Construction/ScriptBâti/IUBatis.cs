﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IUBatis : MonoBehaviour
{

    private GameObject panel;
    bool open;
    bool onPanel;
    public bool isOccupied;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("PanelBatisConstruction");
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        animator = panel.transform.GetChild(0).GetComponent<Animator>();

        onPanel = false;
        open = false;
        isOccupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
            mP = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (onPanel == false)
            {
                panel.SetActive(false);
                open = false;
            }

            if (open == false && (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) + ((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) < 1))
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("BatiChaumière"))

                {
                    //Ajouter update valeur max du slider, etc..
                    panel.transform.position = new Vector2(mP.x + panel.GetComponent<RectTransform>().rect.width, mP.y);
                    panel.gameObject.SetActive(true);
                    animator.SetTrigger("Ouverture");
                    open = true;

                }

            }

        }
    }

    public void ClickOnPanel()
    {
        onPanel = true;
        Deplacement.enMenu = true;
    }

    public void ClickOutPanel()
    {
        onPanel = false;
        Deplacement.enMenu = false;
    }
}
