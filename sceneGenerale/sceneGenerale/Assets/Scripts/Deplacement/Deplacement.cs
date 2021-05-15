using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Deplacement : MonoBehaviour
{
    public Animator animator;
    public static bool enMenu = false;
    int R; //Rayon qui délimite la zone où on peut clicker pour se déplacer ou non
    private float speed = 10.0f; //vitesse arbitraire du personnage
    private Vector3 Debut, Fin;
    private Vector3 direction;//Pour calculer la direction du déplacement
    private bool Touch, outside;
    public bool canmove;
    new public Camera camera;//Touch regarde si on touche l'écran, outside regarde si click a été fait en dehors de la zone ou non
    float c;

    public Recolte recolte;

    // Start is called before the first frame update
    void Start()
    {
        R = Screen.height / 4;
        Touch = false;
        recolte = recolte.GetComponent<Recolte>();
        canmove = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (canmove == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if ((Input.mousePosition.x - Screen.width / 2) * (Input.mousePosition.x - Screen.width / 2) + (Input.mousePosition.y - Screen.height / 2) * (Input.mousePosition.y - Screen.height / 2) > R * R)
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
                if (!enMenu){
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

        if (recolte.IsCraftArbre) //Rajouter tous les booleens de chaque scripts qui doivent désactiver le déplacement
        {
            canmove = false;
        }
        else
        {
            canmove = true;
        }
        if (canmove == false)
        {
            direction = new Vector3(0, 0, 0);
        }

        animator.SetFloat("Horizontal", c);
        //animator.SetFloat("Vertical", direction.z);
        animator.SetFloat("Speed", direction.sqrMagnitude);

    }


    private void Move(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime,Space.World);
    } //Move dans la direction du vecteur direction
}
