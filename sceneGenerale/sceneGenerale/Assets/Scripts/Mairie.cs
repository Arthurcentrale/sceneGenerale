using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Mairie : MonoBehaviour
{
    public GameObject panel;
    public bool open;
    public bool onPanel;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;
    Player player;
    public GameObject PanelTableau;
    public GameObject PanelBureau;
    List<float> valeursManagers;

    public TimeManager timemanager;
    GFForet gf;
    public Button prog;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        open = false;
        onPanel = false;
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
        timemanager = GameObject.Find("Game Manager").GetComponent<TimeManager>();
        /*prog = prog.GetComponent<Button>();
        prog.onClick.AddListener(FctBureau*/

    }

    // Update is called once per frame
    void Update()
    {
        //mise a jour constante des valeurs des managers sur le livre
        //valeursManagers = new List<float> { EnvironnementManager.instance.qualiteAir, EnvironnementManager.instance.qualiteSol, EnvironnementManager.instance.qualiteEau, SocialManager.instance.qualiteDeVie, SocialManager.instance.ecoSensibilisation, DeveloppementManager.instance.navireConstruit };
        //--------//
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
        
            mP = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (onPanel == false)
            {
                panel.SetActive(false);
                PanelTableau.SetActive(false);
                open = false;
                
            }

            if (open == false /*&& (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) + ((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) < 1)*/)
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Mairie"))

                {
                    Debug.Log(Hit.transform.tag);
                    panel.transform.position = new Vector2(mP.x + panel.GetComponent<RectTransform>().rect.width, mP.y);
                    panel.gameObject.SetActive(true);
                    animator.SetTrigger("ouverture1BulleCouper");
                    open = true;
                    
                }
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Tableau"))
                {
                    FctTableau();
                    PanelTableau.gameObject.SetActive(true);
                    open = true;
                    Deplacement.enMenu = true;
                }
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Bureau2"))
                {
                    FctBureau();
                    PanelBureau.gameObject.SetActive(true);
                    open = true;
                    Deplacement.enMenu = true;
                }

            }
        }
    }
    public void ClickOnPanel()
    {
        Deplacement.enMenu = true;
        onPanel = true;
    }

    public void ClickOutPanel()
    {
        onPanel = false;
        Deplacement.enMenu = false;
    }

    public void FctTableau()
    {
        Transform jauges = PanelTableau.transform.GetChild(1);
        //List<float> valeurs = new List<float> { 80,52,12,52,84,1};
        valeursManagers = new List<float>{ EnvironnementManager.instance.qualiteAir, EnvironnementManager.instance.qualiteSol , EnvironnementManager.instance.qualiteEau, SocialManager.instance.qualiteDeVie, SocialManager.instance.ecoSensibilisation, DeveloppementManager.instance.navireConstruit };

        for(int i = 0; i <= 5; i++)
        {
            Slider slider = jauges.GetChild(i).gameObject.GetComponent<Slider>();
            slider.value = valeursManagers[i];

            Text text = jauges.GetChild(i).GetChild(3).gameObject.GetComponent<Text>();
            text.text = valeursManagers[i].ToString();

            if (valeursManagers[i] / slider.maxValue > 0.65f)
            {
                slider.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
            }
            else if(valeursManagers[i] / slider.maxValue <= 0.65f && valeursManagers[i] / slider.maxValue > 0.30f)
            {
                slider.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                slider.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
            }
        }

        Text text2 = jauges.GetChild(7).gameObject.GetComponent<Text>();
        timemanager = GameObject.Find("Game Manager").GetComponent<TimeManager>();
        text2.text = timemanager.nombreDeJoursPassés.ToString();
        PanelTableau.SetActive(true);
        Deplacement.enMenu = true;
        open = true;
    }
    public void FctBureau()
    {
        // On recupère le nom des aliments produits sur l'ile
        // On affiche les noms des aliments
        GameObject aliments = PanelBureau.transform.Find("NourritureBureau").gameObject;
        int h = 0;
        while (h <aliments.transform.childCount)
        {
            if (h < SocialManager.instance.Listevariete.Count)
            {
                aliments.transform.GetChild(h).gameObject.SetActive(true);
                aliments.transform.GetChild(h).gameObject.GetComponent<Image>().sprite = SocialManager.instance.Listevariete[h].Icon;
                
            }
            else
            {
                aliments.transform.GetChild(h).gameObject.SetActive(false);
            }
            h++;
        }

        //On recupère les batiments construits
        List<GameObject> listesbatiments = DeveloppementManager.instance.listeBatiment;
        GameObject batiments = PanelBureau.transform.Find("BatimentsBureau").gameObject;
        //On affiche les batiments construits
        int j = 0;
        while (j < batiments.transform.childCount)
        {
            if (j < listesbatiments.Count)
            {
                if (listesbatiments[j].name.IndexOf("Déplaçable", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    batiments.transform.GetChild(j).GetComponent<Text>().text = SimplifierNom(listesbatiments[j].name.ToString());
                    batiments.transform.GetChild(j).gameObject.SetActive(true);
                    j++;
                }
            }
            else
            {
                batiments.transform.GetChild(j).gameObject.SetActive(false);
                j++;
            }
        }

        // On construit les tableaux des infos à rentrer dans le tableau ui
        gf = GameObject.Find("PlaneDialogueGardeForestier").GetComponent<GFForet>();
        int[] arbresrobustes = gf.CompterLesArbresRobustes();
        int[] arbresmalades = gf.CompterLesArbresMalades();
        int[] arbresfreles = gf.CompterLesArbresFrele();
        // On fait l'affichage
        GameObject Tableau = PanelBureau.transform.Find("TableauBureau").gameObject;
        for(int i =3;i<8;i++)
        {
            Tableau.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = arbresrobustes[i-3].ToString();
            Tableau.transform.GetChild(i).GetChild(1).gameObject.GetComponent<Text>().text = arbresfreles[i-3].ToString();
            Tableau.transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = arbresmalades[i-3].ToString();
        }
        PanelBureau.SetActive(true);
        open = true;
        Deplacement.enMenu = true;
    }

    string SimplifierNom(string nom)
    {
        string nouveau = "";
        foreach(char c in nom)
        {
            if(c==' ')
            {
                return nouveau;
            }
            else
            {
                nouveau += c;
            }
        }

        return nouveau;

    }
}
