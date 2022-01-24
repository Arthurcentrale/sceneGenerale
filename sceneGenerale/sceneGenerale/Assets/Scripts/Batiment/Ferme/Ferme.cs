using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ferme : MonoBehaviour
{
    //Ouvrir UI
    public GameObject panel;
    public bool open;
    bool onPanel;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;
    private Animator livreActivite;

    //Gestion Bouffe
    public bool isOccupied;
    int BouffeTotale;
    float timer = 0f;
    public float delai = 5f;
    public int productivité = 1;
    public Player player;
    public static int compteurBlé = 0;
    public Text textBlé;

    //Gestion habitant
    public GameObject choixhabitant;
    public HabitantBehaviour habitant;

    public int niveauAgriculteur;

    // Start is called before the first frame update
    void Start()
    {
        onPanel = false;
        open = false;
        isOccupied = false;
        textBlé.text = 0.ToString();
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
        livreActivite = GameObject.Find("LivreFerme").GetComponent<Animator>();

        niveauAgriculteur = 0;
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

            if (open == false)
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Ferme"))

                {
                    panel.transform.position = mP;//camera.ScreenToWorldPoint(mP);
                    panel.gameObject.SetActive(true);
                    animator.SetTrigger("ouverture1BulleCouper");
                    open = true;
                    
                }

            }


        }
        if (isOccupied && habitant != null && habitant.isHoused)
        {
            timer += Time.deltaTime;
            if (timer >= delai)
            {
                timer = 0f;
                compteurBlé+= productivité;
                textBlé.text = compteurBlé.ToString();

            }
            Planter.isOccupied = true;
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

    ////////////////////////////
    //Habitant//
    ////////////////////////////

    public void RendreOccupe() //Attribue le gameObject agriculteur à la ferme et le rend occupé
    {
        GameObject go = GameObject.Find("agriculteur");
        habitant = go.GetComponent<HabitantBehaviour>();
        habitant.hasWorkplace = true;
        isOccupied = !isOccupied;
        InitVariete(habitant);
    }

    void InitVariete(HabitantBehaviour habitant)
    {
        //faire pareil mais pour les nourritures de la ferme
        /*
        if (habitant.ecoLevel < 3)
        {
            SocialManager.instance.nombreAlimentsDifferents += 1;
            SocialManager.instance.Listevariete.Add(foodmanager.Findinlist("Carpe"));
            varietepecherie += 1;
        }
        if (habitant.ecoLevel < 5 && habitant.ecoLevel >= 3)
        {
            SocialManager.instance.nombreAlimentsDifferents += 2;
            SocialManager.instance.Listevariete.Add(foodmanager.Findinlist("Carpe"));
            SocialManager.instance.Listevariete.Add(foodmanager.Findinlist("Brochet"));
            varietepecherie += 2;
        }
        if (habitant.ecoLevel == 5)
        {
            SocialManager.instance.nombreAlimentsDifferents += 3;
            SocialManager.instance.Listevariete.Add(foodmanager.Findinlist("Carpe"));
            SocialManager.instance.Listevariete.Add(foodmanager.Findinlist("Brochet"));
            SocialManager.instance.Listevariete.Add(foodmanager.Findinlist("Truite"));
            varietepecherie += 3;
        }
        */

        niveauAgriculteur = habitant.ecoLevel;
    }

    int GetLevel(HabitantBehaviour habitant) //Récupérer le level de l'agriculteur
    {
        if (habitant != null) return habitant.ecoLevel;
        else return 0;
    }

    public void FctInfo()
    {
        Recap recap = this.GetComponent<Recap>();

        if (habitant == null)
        {
            recap.MajHabitant(null, "Vacant");
        }
        else
        {
            recap.MajHabitant(habitant.image, habitant.nom);
        }

        panel.SetActive(false);
        recap.OuvertureMenuRecap();
    }

    public void FctChoix()
    {
        GameObject FermierDispo = TrouverFermier();
        if (FermierDispo == null)
        {
            choixhabitant.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            choixhabitant.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = FermierDispo.GetComponent<HabitantBehaviour>().image;
            choixhabitant.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = FermierDispo.GetComponent<HabitantBehaviour>().name;
        }
        panel.SetActive(false);
        choixhabitant.SetActive(true);
    }

    public GameObject TrouverFermier()
    {
        GameObject ha = new GameObject();
        GameObject habitant = GameObject.Find("habitants");
        foreach (Transform child in habitant.transform)
        {
            HabitantBehaviour Behaviour = child.GetComponent<HabitantBehaviour>();
            if (Behaviour.transform.name == "agriculteur")
            {
                if (!Behaviour.hasWorkplace)
                {
                    ha = child.gameObject;
                }
            }
        }
        return ha;
    }

    public void SelectionFermier()
    {
        GameObject fermierDispo = TrouverFermier();
        habitant = fermierDispo.GetComponent<HabitantBehaviour>();
        habitant.hasWorkplace = true;
        isOccupied = true;
        InitVariete(habitant);

        Quitter();
    }

    public void Quitter()
    {
        choixhabitant.SetActive(false);
        panel.SetActive(false);
        open = false;
        Deplacement.enMenu = false;
    }
}