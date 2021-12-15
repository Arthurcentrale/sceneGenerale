using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chaumiere : MonoBehaviour
{

    GameObject cible; // On cible la chaumière dont on veut afficher les paramètres
    List<GameObject> habitantsSansMaison;
    public GameObject panel;
    public bool open;
    public bool onPanel;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;
    GameObject listehabitants;
    public GameObject panelmenu;
    public GameObject menuinfo;
    public Button Buttoninfo,ButtonClose;
    public Button button1,button2,button3,button4;

    public CompteurBouffe compteurbouffe;


    // Start is called before the first frame update
    void Start()
    {
        /*listehabitants = GameObject.Find("listehabitants");
        Debug.Log(listehabitants.transform.childCount);*/
        onPanel = false;
        open = false;
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
        //button1 =GameObject.Find("ButtonHab1").GetComponent<Button>();
        //panelmenu = GameObject.Find("ChoixHabitant");

        //Initialisation des boutons
        button1 = button1.GetComponent<Button>();
        button1.onClick.AddListener(FctButton1);
        button2 = button2.GetComponent<Button>();
        button2.onClick.AddListener(FctButton2);
        button3 = button3.GetComponent<Button>();
        button3.onClick.AddListener(FctButton3);
        button4 = button4.GetComponent<Button>();
        button4.onClick.AddListener(FctButton4);
        Buttoninfo = Buttoninfo.GetComponent<Button>();
        Buttoninfo.onClick.AddListener(FctInfo);
        ButtonClose = ButtonClose.GetComponent<Button>();
        ButtonClose.onClick.AddListener(quitter);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (open && onPanel)
            {
            }
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            mP = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (onPanel == false)
            {
                panel.SetActive(false);
                open = false;
            }

            if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Chaumière"))
            {
                if (open == false && (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) + ((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) < 1))
                    {
                    cible = Hit.transform.gameObject;
                    //habitantsSansMaison = TrouverHabitantSansMaison();
                    panel.transform.position = new Vector2(mP.x + panel.GetComponent<RectTransform>().rect.width, mP.y);
                    if (cible.GetComponent<HabitantChaumiere>().isEmpty)
                    {
                        panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        panel.gameObject.SetActive(true);
                        animator.SetTrigger("ouverture1BulleCouper");
                        open = true;
                    }
                    else
                    {


                        panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                        panel.gameObject.SetActive(true);
                        animator.SetTrigger("ouverture1BulleCouper");
                        open = true;
                    }
                }

            }
        }
    }

    public void ClickOnPanel()
    {
        onPanel = true;
    }

    public void ClickOutPanel()
    {
        onPanel = false;    }
    List<GameObject> TrouverHabitantSansMaison()
    {
        List<GameObject> ha = new List<GameObject>();
        GameObject habitant = GameObject.Find("habitants");
        foreach (Transform child in habitant.transform)
        {
            HabitantBehaviour Behaviour = child.GetComponent<HabitantBehaviour>();
            if(Behaviour.isHoused == false)
            {
                ha.Add(child.gameObject);
            }
        }
        return ha;
    }

    void FctButton1() // A copier manuellement pour les 4 boutons
    {
        List<GameObject> liste = TrouverHabitantSansMaison();
        if (liste.Count >= 1)
        {
            liste[0].gameObject.GetComponent<HabitantBehaviour>().isHoused = true;
            cible.GetComponent<HabitantChaumiere>().habitantActuel = liste[0].gameObject;
            panelmenu.SetActive(false);
            panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
            open = false;
            animator.SetTrigger("ouverture1BulleCouper");
            cible.GetComponent<HabitantChaumiere>().isEmpty = false;
            CompteurBouffe.Data.NbrBouffe -= cible.GetComponent<HabitantChaumiere>().habitantActuel.GetComponent<HabitantBehaviour>().foodQuantity;
            compteurbouffe.CBouffe.text = CompteurBouffe.Data.NbrBouffe.ToString();
            //Debug.Log(compteurbouffe.CBouffe.text);
        }
    }
    void FctButton2() // A copier manuellement pour les 4 boutons
    {
        List<GameObject> liste = TrouverHabitantSansMaison();
        if (liste.Count >= 2)
        {
            liste[1].gameObject.GetComponent<HabitantBehaviour>().isHoused = true;
            cible.GetComponent<HabitantChaumiere>().habitantActuel = liste[1].gameObject;
            panelmenu.SetActive(false);
            panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
            open = false;
            animator.SetTrigger("ouverture1BulleCouper");
            cible.GetComponent<HabitantChaumiere>().isEmpty = false;
            CompteurBouffe.Data.NbrBouffe -= cible.GetComponent<HabitantChaumiere>().habitantActuel.GetComponent<HabitantBehaviour>().foodQuantity;
            compteurbouffe.CBouffe.text = CompteurBouffe.Data.NbrBouffe.ToString();
            //Debug.Log(compteurbouffe.CBouffe.text);
        }
    }
    void FctButton3() // A copier manuellement pour les 4 boutons
    {
        List<GameObject> liste = TrouverHabitantSansMaison();
        if (liste.Count >= 3)
        {
            liste[2].gameObject.GetComponent<HabitantBehaviour>().isHoused = true;
            cible.GetComponent<HabitantChaumiere>().habitantActuel = liste[2].gameObject;
            panelmenu.SetActive(false);
            panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
            open = false;
            animator.SetTrigger("ouverture1BulleCouper");
            cible.GetComponent<HabitantChaumiere>().isEmpty = false;
            CompteurBouffe.Data.NbrBouffe -= cible.GetComponent<HabitantChaumiere>().habitantActuel.GetComponent<HabitantBehaviour>().foodQuantity;
            compteurbouffe.CBouffe.text = CompteurBouffe.Data.NbrBouffe.ToString();
            //Debug.Log(compteurbouffe.CBouffe.text);
        }
    }
    void FctButton4() // A copier manuellement pour les 4 boutons
    {
        List<GameObject> liste = TrouverHabitantSansMaison();
        if (liste.Count >= 4)
        {
            liste[3].gameObject.GetComponent<HabitantBehaviour>().isHoused = true;
            cible.GetComponent<HabitantChaumiere>().habitantActuel = liste[3].gameObject;
            panelmenu.SetActive(false);
            panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
            open = false;
            animator.SetTrigger("ouverture1BulleCouper");
            cible.GetComponent<HabitantChaumiere>().isEmpty = false;
            CompteurBouffe.Data.NbrBouffe -= cible.GetComponent<HabitantChaumiere>().habitantActuel.GetComponent<HabitantBehaviour>().foodQuantity;
            compteurbouffe.CBouffe.text = CompteurBouffe.Data.NbrBouffe.ToString();
            //Debug.Log(compteurbouffe.CBouffe.text);
        }
    }

    public void FctPanel()
    {
        List<GameObject> liste = TrouverHabitantSansMaison();
        int i = liste.Count;
        int h= 0;
        foreach (Transform child in panelmenu.transform.GetChild(0))
        {
            h++;
            if (h > i)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.GetChild(0).gameObject.GetComponent<Image>().sprite = liste[h - 1].GetComponent<HabitantBehaviour>().image;
                child.GetChild(1).gameObject.GetComponent<Text>().text = liste[h - 1].name;
            }
        }
    }

    void FctInfo()
    {
        if (cible.GetComponent<HabitantChaumiere>().habitantActuel == null)
        {
            menuinfo.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Aucun Habitant";
            menuinfo.transform.GetChild(4).gameObject.SetActive(false);
            menuinfo.transform.GetChild(5).gameObject.SetActive(false);
            menuinfo.transform.GetChild(6).gameObject.SetActive(false);
            menuinfo.transform.GetChild(8).gameObject.SetActive(false);
            panel.SetActive(false);
            menuinfo.SetActive(true);
        }
        else
        {
            menuinfo.transform.GetChild(2).gameObject.GetComponent<Text>().text = cible.GetComponent<HabitantChaumiere>().habitantActuel.GetComponent<HabitantBehaviour>().nom;
            for(int i =0;i<3; i++)
            {
                if (i > cible.GetComponent<HabitantChaumiere>().habitantActuel.GetComponent<HabitantBehaviour>().foodVariety -1 )
                    menuinfo.transform.GetChild(4 + i).gameObject.SetActive(false);
                else menuinfo.transform.GetChild(4 + i).gameObject.SetActive(true); 
            }
            menuinfo.transform.GetChild(8).gameObject.GetComponent<Image>().sprite = cible.GetComponent<HabitantChaumiere>().habitantActuel.GetComponent<HabitantBehaviour>().image ;
            menuinfo.transform.GetChild(8).gameObject.SetActive(true);
            panel.SetActive(false);
            menuinfo.SetActive(true);

        }
    }

    void quitter()
    {
        menuinfo.SetActive(false);
        panel.gameObject.SetActive(false);
        open = false;
        animator.SetTrigger("ouverture1BulleCouper");
    }

    void quitterchoix()
    {
        panelmenu.SetActive(false);
        panel.gameObject.SetActive(true);
        animator.SetTrigger("ouverture1BulleCouper");
    }
}
