using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    List<float> valeursManagers;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        open = false;
        onPanel = false;
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //mise a jour constante des valeurs des managers sur le livre
        valeursManagers = new List<float> { EnvironnementManager.instance.qualiteAir, EnvironnementManager.instance.qualiteSol, EnvironnementManager.instance.qualiteEau, SocialManager.instance.qualiteDeVie, SocialManager.instance.ecoSensibilisation, DeveloppementManager.instance.navireConstruit };

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
        PanelTableau.SetActive(true);
    }
}
