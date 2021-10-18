using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mairie : MonoBehaviour
{
    public GameObject panel;
    bool open;
    bool onPanel;
    Vector2 mP;
    new public Camera camera;
    private Animator animator;
    public Player player;

    public GameObject FondBouffe;



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
                open = false;
                Deplacement.enMenu = false;
            }

            if (open == false)
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Mairie"))

                {
                    panel.transform.position = new Vector2(mP.x + panel.GetComponent<RectTransform>().rect.width, mP.y);
                    panel.gameObject.SetActive(true);
                    animator.SetTrigger("ouverture1BulleCouper");
                    open = true;
                    Deplacement.enMenu = true;
                }
                if (Physics.Raycast(ray, out Hit) && Hit.collider.CompareTag("Bureau2"))
                {
                    FondBouffe.transform.position = new Vector2(mP.x, mP.y);
                    FondBouffe.gameObject.SetActive(true);
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
        onPanel = false;
    }
}
