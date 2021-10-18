using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pecherie : MonoBehaviour
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
    int PoissonEnAttente;
    float timer = 0f;
    float delai = 5f;
    public Item poisson;
    public Text textPoisson;
    public CompteurBouffe compteurbouffe;
    public Player player;
    public RecetteCraft recettecraft;

    // Start is called before the first frame update
    void Start()
    {
        onPanel = false;
        open = false;
        isOccupied = false;
        BouffeTotale = 0;
        PoissonEnAttente = 0;
        textPoisson.text = 0.ToString();
        animator = panel.transform.GetChild(0).GetComponent<Animator>();
        compteurbouffe = compteurbouffe.GetComponent<CompteurBouffe>();
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
                Deplacement.enMenu = false;
            }

            if (open == false)
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Pecherie"))

                {
                    panel.transform.position = new Vector2(mP.x + panel.GetComponent<RectTransform>().rect.width, mP.y);
                    panel.gameObject.SetActive(true);
                    animator.SetTrigger("ouverture1BulleCouper");
                    open = true;
                    Deplacement.enMenu = true;
                }

            }


        }
        if (isOccupied)
        {
            timer += Time.deltaTime;
            if (timer >= delai)
            {
                timer = 0f;
                PoissonEnAttente++;
                textPoisson.text = PoissonEnAttente.ToString();

            }
        }

    }
    public void ClickOnPanel()
    {
        onPanel = true;
    }

    public void ClickOutPanel()
    {
        onPanel = false;
    }

    //Fonction quand on clique sur le bouton du milieu
    public void RecupererPoisson()
    {
        CompteurBouffe.Data.NbrBouffe += PoissonEnAttente;
        ItemAmount itemamount = new ItemAmount(poisson, PoissonEnAttente);
        player.inventory.AddItem(itemamount);
        PoissonEnAttente = 0;
        textPoisson.text = PoissonEnAttente.ToString();
        compteurbouffe.text.text = CompteurBouffe.Data.NbrBouffe.ToString();
    }
    public void RendreOccupe()
    {
        isOccupied = !isOccupied;
    }
}
