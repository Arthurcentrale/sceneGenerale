using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClicAtelier : MonoBehaviour
{
    public GameObject panel;
    bool open;
    Vector2 mP;
    RectTransform rectTransform;
    public Button close;
    public Button close2;
    new public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        close = close.GetComponent<Button>();
        close2 = close2.GetComponent<Button>();
        
    }


        // Update is called once per frame
        void Update()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
            if ( open == false)
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Atelier"))

                {
                    panel.gameObject.SetActive(true);
                    open = true;
                    rectTransform = panel.GetComponent<RectTransform>();
                }

            }
            if (open == true)
            {
                close.onClick.AddListener(closepanel);
                close2.onClick.AddListener(closebigpanel);
            }

        }

    }
    void closepanel()
    {
        panel.gameObject.SetActive(false);
        open = false;
    }
    void closebigpanel()
    {
        open = false;
    }
}
