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
    bool isEmpty;
    public HabitantBehaviour habitant;
    public GameObject menuinfo;
    public GameObject choixhabitant;

    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
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
                    panel.transform.position = new Vector2(mP.x + panel.GetComponent<RectTransform>().rect.width, mP.y);
                    if (habitant != null && habitant.isHoused == false)
                    {
                        panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                        panel.gameObject.SetActive(true);
                        animator.SetTrigger("ouverture1BulleCouper");
                        open = true;
                        rectTransform = panel.GetComponent<RectTransform>();
                    }
                    else if ((habitant != null && habitant.isHoused == true )|| habitant == null) 
                    {
                        panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        panel.gameObject.SetActive(true);
                        animator.SetTrigger("ouverture1BulleCouper");
                        open = true;
                        rectTransform = panel.GetComponent<RectTransform>();
                    }
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
        if (isEmpty)
        {
            FctChoix();
            panel.SetActive(false);
            choixhabitant.SetActive(true);
            open = false;
        }
        else
        {
            panel.SetActive(false);
            MenuFab.SetActive(true);
            open = false;
        }
    }

    public void FctInfo()
    {
        if (habitant == null)
        {
            menuinfo.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = null;
            menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Vacant";
            menuinfo.transform.GetChild(4).gameObject.SetActive(false);
            panel.SetActive(false);
            menuinfo.SetActive(true);
        }
        else if (habitant != null && habitant.isHoused == false)
        {
            menuinfo.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = habitant.image;
            menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = habitant.nom;
            menuinfo.transform.GetChild(4).gameObject.SetActive(true);
            panel.SetActive(false);
            menuinfo.SetActive(true);
        }
        else
        {
            menuinfo.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = habitant.image;
            menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = habitant.nom;
            menuinfo.transform.GetChild(4).gameObject.SetActive(false);
            panel.SetActive(false);
            menuinfo.SetActive(true);
        }
    }
    GameObject TrouverArtisan()
    {
        GameObject ha = new GameObject();
        GameObject habitant = GameObject.Find("habitants");
        foreach (Transform child in habitant.transform)
        {
            HabitantBehaviour Behaviour = child.GetComponent<HabitantBehaviour>();
            if (Behaviour.transform.name == "Artisan")
            {
                if (Behaviour.hasWorkplace == false)
                {
                    ha = child.gameObject;
                }
            }
        }
        return ha;
    }

    public void FctChoix()
    {

        GameObject PecheurDispo = TrouverArtisan();
        if (PecheurDispo == null)
        {

            choixhabitant.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            choixhabitant.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = PecheurDispo.GetComponent<HabitantBehaviour>().image;
            choixhabitant.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = PecheurDispo.GetComponent<HabitantBehaviour>().name;
        }
        panel.SetActive(false);
        choixhabitant.SetActive(true);
    }

    public void selectionartisan()
    {
        GameObject artisandispo = TrouverArtisan();
        habitant = artisandispo.GetComponent<HabitantBehaviour>();
        habitant.hasWorkplace = true;
        isEmpty = false;
        choixhabitant.SetActive(false);
        if(habitant.isHoused == false)
        {
            panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        panel.SetActive(true);
    }

    public void quitter()
    {
        choixhabitant.SetActive(false);
        panel.SetActive(false);
        open = false;
        animator.SetTrigger("ouverture1BulleCouper");
    }
    public void quitter2()
    {
        menuinfo.SetActive(false);
        panel.SetActive(false);
        open = false;
        animator.SetTrigger("ouverture1BulleCouper");
    }
}
