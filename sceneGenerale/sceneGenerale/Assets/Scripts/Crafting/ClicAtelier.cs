using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClicAtelier : MonoBehaviour
{

    public AudioClip apparitionBulle;
    public GameObject MenuFab;
    public GameObject panel;
    bool open;
    bool onPanel;
    Vector2 mP;
    RectTransform rectTransform;
    public Button close;
    public Button close2;
    new public Camera camera;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        close = close.GetComponent<Button>();
        close2 = close2.GetComponent<Button>();
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
        onPanel = false;
        Vector2 mP;
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

            if ( open == false && (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) +((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) < 1))
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Atelier"))

                {
                    panel.transform.position = new Vector2(mP.x +panel.GetComponent<RectTransform>().rect.width, mP.y);
                    panel.gameObject.SetActive(true);
                    animator.SetTrigger("ouverture1BulleCouper");
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
        Deplacement.enMenu=false;
    }
    void closebigpanel()
    {
        open = false;
        Deplacement.enMenu=false;
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

    public void OpenMenu()
    {
        panel.SetActive(false);
        MenuFab.SetActive(true);
        open = false;
    }
}
