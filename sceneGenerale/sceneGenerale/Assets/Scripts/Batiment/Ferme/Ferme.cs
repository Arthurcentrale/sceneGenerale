using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ferme : MonoBehaviour
{
    //Ouvrir UI
    public GameObject panel;
    bool open;
    bool onPanel;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;

    //Gestion Bouffe
    public bool isOccupied;
    int BouffeTotale;
    float timer = 0f;
    public float delai = 5f;
    public int productivité = 1;
    public Player player;
    public static int compteurBlé = 0;
    public Text textBlé;

    // Start is called before the first frame update
    void Start()
    {
        onPanel = false;
        open = false;
        isOccupied = false;
        textBlé.text = 0.ToString();
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
        if (isOccupied)
        {
            timer += Time.deltaTime;
            if (timer >= delai)
            {
                timer = 0f;
                compteurBlé+= productivité;
                textBlé.text = compteurBlé.ToString();

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

    //Fonction quand on clique sur le bouton du milieu
    
    public void RendreOccupe()
    {
        isOccupied = !isOccupied;
    }
}
