using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Mairie : MonoBehaviour
{
    public GameObject panel;
    bool open;
    bool onPanel;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;
    public Player player;

    public GameObject PanelTableau;
    


    // Start is called before the first frame update
    void Start()
    {
        open = false;
        onPanel = false;
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
                PanelTableau.SetActive(false);
                open = false;
                
            }

            if (open == false /*&& (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) + ((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) < 1)*/)
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Mairie"))

                {
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

    void FctTableau()
    {
        Transform jauges = PanelTableau.transform.GetChild(1);
        //List<float> valeurs = new List<float> { 80,52,12,52,84,1};
        List<float> valeurs = new List<float>{ GameManager.environnementManager.qualiteAir, GameManager.environnementManager.qualiteSol , GameManager.environnementManager.qualiteEau,GameManager.socialManager.qualiteDeVie,GameManager.socialManager.ecoSensibilisation,GameManager.developpementManager.navireConstruit };
        for(int i = 0; i <= 5; i++)
        {
            Slider slider = jauges.GetChild(i).gameObject.GetComponent<Slider>();
            slider.value = valeurs[i];
            if (valeurs[i] / slider.maxValue > 0.65f)
            {
                slider.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
            }
            else if(valeurs[i] / slider.maxValue <= 0.65f && valeurs[i] / slider.maxValue > 0.30f)
            {
                slider.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                slider.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
            }
        }
    }
}
