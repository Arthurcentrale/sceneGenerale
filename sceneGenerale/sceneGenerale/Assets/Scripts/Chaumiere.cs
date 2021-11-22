using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaumiere : MonoBehaviour
{

    GameObject cible; // On cible la chaumière dont on veut afficher les paramètres
    GameObject habitantActuel;
    List<GameObject> habitantsSansMaison;
    public GameObject panel;
    bool open;
    bool onPanel;
    Vector2 mP;
    new public Camera camera;
    public bool isEmpty;
    private Animator animator;
    GameObject listehabitants;


    // Start is called before the first frame update
    void Start()
    {
        /*listehabitants = GameObject.Find("listehabitants");
        Debug.Log(listehabitants.transform.childCount);*/
        isEmpty = true;
        onPanel = false;
        open = false;
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
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
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Chaumière"))
                {
                    cible = Hit.transform.gameObject;
                    //habitantsSansMaison = TrouverHabitantSansMaison();
                    panel.transform.position = new Vector2(mP.x + panel.GetComponent<RectTransform>().rect.width, mP.y);
                    if (isEmpty)
                    {
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

    void AttribuerChaumiere()
    {
        isEmpty = false;
    }
    public void ClickOnPanel()
    {
        onPanel = true;
    }

    public void ClickOutPanel()
    {
        onPanel = false;
    }
    List<GameObject> TrouverHabitantSansMaison()
    {
        List<GameObject> ha=null;
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

    }
