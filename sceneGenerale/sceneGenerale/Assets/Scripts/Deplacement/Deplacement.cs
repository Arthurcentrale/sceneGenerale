using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Deplacement : MonoBehaviour
{
    public Animator animator;
    public static bool enMenu = false; //Rayon qui délimite la zone où on peut clicker pour se déplacer ou non
    public float speed; //vitesse arbitraire du personnage
    private Vector3 Debut, Fin;
    private Vector3 direction;//Pour calculer la direction du déplacement
    private bool Touch, outside;
    public bool canmove;
    new public Camera camera;//Touch regarde si on touche l'écran, outside regarde si click a été fait en dehors de la zone ou non
    float c;
    float cMemo;

    public Recolte recolte;

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        Touch = false;
        recolte = recolte.GetComponent<Recolte>();
        canmove = true;
        animator = player.GetComponent<Animator>();
        player = this.GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if (canmove == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) + ((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) > 1)
                {
                    outside = true;
                    Debut = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                }
                else
                {
                    outside = false;
                }
            }
            if (Input.GetMouseButton(0) && outside)
            {
                Touch = true;
                Fin = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            }
            else
            {
                Touch = false;
            }

            if (Touch && outside)
            {
                Vector3 a = Fin - Debut;
                Vector3 b = new Vector3(a.x, 0, a.y);
                direction = Vector3.ClampMagnitude(b, 1.0f);
                if (enMenu==false){
                    Move(direction);
                }

            }
            if (direction.x < 0) { c = -1; }
            else { c = 1; }
            

        }
        

        if (Input.GetMouseButtonUp(0))
        {
            direction = new Vector3(0, 0, 0);
        }

        /*if (recolte.IsCraftArbre || (player.uiInventory.stadeAffichage == 2)) //Rajouter tous les booleens de chaque scripts qui doivent désactiver le déplacement
        {
            canmove = false;
        }
        else
        {
            canmove = true;
        }*/
        if (enMenu == true)
        {
            direction = new Vector3(0, 0, 0);
        }


        //animator.SetFloat("Vertical", direction.z);

        animator.SetFloat("Speed", direction.sqrMagnitude);

        if (animator.GetFloat("Speed") != 0)
        {
            print("testspeed non nul");
            //animator.SetFloat("Horizontal", c);
        }
        
        if (animator.GetFloat("Speed") == 0)
        {
           
            print("test speed = 0");
            print(animator.GetCurrentAnimatorClipInfo(0));
        }

    }


    private void Move(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime,Space.World);
    } //Move dans la direction du vecteur direction
    public void MetEnMenu()
    {
        enMenu = true;
    }
    public void MetHorsMenu()
    {
        enMenu = false;
    }
}
