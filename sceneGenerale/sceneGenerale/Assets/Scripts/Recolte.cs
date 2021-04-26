using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Recolte : MonoBehaviour
{
    public bool IsCraftArbre; //bool pour ouvrir le menu pour couper l'arbre
    public bool IsCraftRoche; //same
    public bool IsCraftFleur; //same
    RaycastHit cible; //pour cibler un gameobject
    public Inventaire inventaire;//script de l'inventaire
    Ray R; //raycast 
    private Rect rect; //pour verifier si un clic est dans le menu ( eviter les deplacements si un menu est ouvert)
    public Item bois, pierre, fleursdrop;// pour faire spawn les objets lors de la destruction de leurs parents
    Vector2 mP;
    new public Camera camera;//Pour stocker la position de la souris quand on clic

    // Start is called before the first frame update
    void Start()
    {
        inventaire = inventaire.GetComponent<Inventaire>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if ((IsCraftArbre | IsCraftFleur | IsCraftRoche) && (rect.Contains(new Vector2(Input.mousePosition.x, -Input.mousePosition.y + 530))) == true) // si un des menus est ouvert et qu'on clique dedans, on ne crée pas de raycast
                                                                                                                                                           //( pour éviter que le personnage ne selectionne un objet derriere le menu)
            {
            }

            else
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit)) // on verifie si le raycast a touché un gameobject
                {
                    mP = new Vector2(Input.mousePosition.x, -Input.mousePosition.y + 530); // on prend les coordonnées du clic pour créer le menu où on clic
                    rect = GUIUtility.ScreenToGUIRect(new Rect(mP.x, mP.y, 150, 250)); // on crée le rectangle du menus pour vérifier avec le contains

                    if (hit.collider.CompareTag("Arbre")) // si on touche un arbre :
                    {
                        if (IsCraftArbre == false) // si un autre menu est ouvert alors qu'on a cliqué sur l'arbre, on le ferme
                        {
                            cible = hit;
                            R = ray;
                            IsCraftFleur = false;
                            IsCraftRoche = false;
                            IsCraftArbre = true;
                        }
                        else
                        {
                            IsCraftArbre = false;
                            IsCraftArbre = true;
                        }
                    }
                    if (hit.collider.CompareTag("Untagged"))
                    {
                        IsCraftArbre = false;
                        IsCraftFleur = false;
                        IsCraftRoche = false;
                    }

                    if (hit.collider.CompareTag("Bois")) // si on clic sur une buche et qu'on a assez de place dans l'inventaire, on la récupère
                    {
                        if (NbrPlace(bois) > 0) // a changer lorsque l'inventaire sera fonctionnelle
                        {
                            Destroy(hit.transform.gameObject);
                            AjouterInventaire(bois,1); // à changer lorsque l'inventaire sera terminé
                        }
                    }

                    if ((hit.collider.CompareTag("Roche1")) || (hit.collider.CompareTag("Roche2")) || (hit.collider.CompareTag("Roche3"))) //pareil avec les roches
                    {
                        if (IsCraftRoche == false)
                        {
                            cible = hit;
                            R = ray;
                            IsCraftArbre = false;
                            IsCraftFleur = false;
                            IsCraftRoche = true;
                        }
                        else
                        {
                            IsCraftRoche = false;
                            IsCraftRoche = true;
                        }
                    }

                    if (hit.collider.CompareTag("Pierre")) // pareil que les buches
                    {
                        if (NbrPlace(pierre)> 0)
                        {
                            Destroy(hit.transform.gameObject);
                            AjouterInventaire(pierre,1) ; // a changer plus tard selon le fonctionnement de l'inventaire
                        }
                    }

                    if (hit.collider.CompareTag("Fleurs")) //same
                    {
                        if (IsCraftFleur == false)
                        {
                            cible = hit;
                            R = ray;
                            IsCraftArbre = false;
                            IsCraftRoche = false;
                            IsCraftFleur = true;
                        }
                        else
                        {
                            IsCraftFleur = false;
                            IsCraftFleur = true;
                        }
                    }

                    if (hit.collider.CompareTag("FleursDrop"))//same
                    {
                        if (NbrPlace(fleursdrop) >0)
                        {
                            Destroy(hit.transform.gameObject);
                            AjouterInventaire(fleursdrop,1); // à changer plus tard selon le fonctionnement de l'inventaire
                        }
                    }
                }
            }
        }
    }





    private void OnGUI() //affichage des menus
    {
        if (IsCraftArbre) // si le boolen est true : on affiche le menus pour les arbres
        {
            GUI.Box(new Rect(mP.x,mP.y, 150, 250), "Arbre") ;
            GUI.enabled = (CountItem("Hache") > 0); // si on a une hache, on peut cliquer sur le bouton
            if (GUI.Button(new Rect(mP.x + 35, mP.y + 40, 80, 50), "Couper")) //le bouton pour couper l'arbre
            {
                //Jouer l'animation + Coroutine pour attendre?
                if (Physics.Raycast(R, out cible))
                {
                    SpawnBuche(cible, bois); // on détruit l'arbre et on fait spawn des buches
                    IsCraftArbre = !IsCraftArbre; // une fois l'arbre détruit, on ferme le menu
                }
            }
            GUI.enabled = true;
        }

        if (IsCraftRoche) // pareil avec les roches
        {
            GUI.Box(new Rect(mP.x, mP.y, 150, 250), "Roche");
            GUI.enabled = CountItem("Pioche")> 0; // à modifier avec le slot de la pioche
            if (GUI.Button(new Rect(mP.x + 35, mP.y + 40, 80, 50), "Miner"))
            {
                //Jouer l'animation + Coroutine pour attendre?
                if (Physics.Raycast(R, out cible))
                {
                    SpawnRoche(cible, pierre);
                    IsCraftRoche = !IsCraftRoche;
                }
            }
            GUI.enabled = true;
        }

        if (IsCraftFleur)
        {
            GUI.Box(new Rect(mP.x, mP.y, 150, 250), "Fleurs");
            if (GUI.Button(new Rect(mP.x + 35, mP.y + 40, 80, 50), "Cueillir"))
            {
                //Jouer l'animation + Coroutine pour attendre?
                if (Physics.Raycast(R, out cible))
                {
                    SpawnFleurs(cible, fleursdrop);
                    IsCraftFleur = !IsCraftFleur;
                }
            }
        }
    }

    private void SpawnBuche(RaycastHit cible, Item item) //fonction qui fait détruit cible et fait spawn spawned
    {
        Destroy(cible.transform.gameObject); //detruit cible
        float x = Random.Range(0f, 1f); // variable pour le nombre de spawned a faire apparaitre
        if (0 <= x && x < 0.25) //3 spawns
        {
            if (NbrPlace(item) >= 3) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
            {
                AjouterInventaire(item, 3);
            }
            else //sinon, on remplit l'inventaire et le reste va par terre
            {
                AjouterInventaire(item, NbrPlace(item));
                for (int i = 0; i < 3 - NbrPlace(item); i++)
                {
                    Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                }
            }

        }
        else if (0.25 <= x && x < 0.75) // 4 spawns
        {
            if (NbrPlace(item) >= 4)
            {
                AjouterInventaire(item,4);
            }
            else
            {
                AjouterInventaire(item, NbrPlace(item)) ;
                for (int i = 0; i < 4 - NbrPlace(item); i++)
                {
                    Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                }
            }
        }

        else
        {
            if (NbrPlace(item)>= 5) // 5 spawns
            {
                AjouterInventaire(item,5);
            }
            else
            {
                AjouterInventaire(item,NbrPlace(item));
                for (int i = 0; i < 5 - NbrPlace(item); i++)
                {
                    Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                }
            }
        }
    }
    private void SpawnFleurs(RaycastHit cible, Item item) //Pour les fleurs, on a toujours 3 spawns
    {
        Destroy(cible.transform.gameObject); //detruit cible
        if (NbrPlace(item) >= 3) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
        {
            AjouterInventaire(item,3);
        }
        else //sinon, on remplit l'inventaire et le reste va par terre
        {
            AjouterInventaire(item,NbrPlace(item));
            for (int i = 0; i < 3 - NbrPlace(item); i++)
            {
                Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
            }
        }
    }
    private void SpawnRoche(RaycastHit cible, Item item) //fonction qui fait détruit cible et fait spawn spawned
    {
        Destroy(cible.transform.gameObject); //detruit cible
        float x = Random.Range(0f, 1f); // variable pour le nombre de spawned a faire apparaitre
        if (0 <= x && x < 0.25) //3 spawns
        {
            if (cible.collider.CompareTag("Roche1"))
            {
                if (NbrPlace(item) >= 2) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(item,2);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(item, NbrPlace(item));
                    for (int i = 0; i < 2 - NbrPlace(item); i++)
                    {
                        Instantiate(item, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche2"))
            {
                if (NbrPlace(item) >= 4) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    inventaire.Slot[1].Amount += 4;
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(item, NbrPlace(item));
                    for (int i = 0; i < 4 - NbrPlace(item); i++)
                    {
                        Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche3"))
            {
                if (NbrPlace(item) >= 6) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    inventaire.Slot[1].Amount += 6;
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(item, NbrPlace(item));
                    for (int i = 0; i < 6 - NbrPlace(item); i++)
                    {
                        Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }

        }

        else if (0.25 <= x && x < 0.75) // 4 spawns
        {
            if (cible.collider.CompareTag("Roche1"))
            {
                if (NbrPlace(item) >= 3) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    inventaire.Slot[1].Amount += 3;
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(item, NbrPlace(item));
                    for (int i = 0; i < 3 - NbrPlace(item); i++)
                    {
                        Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche2"))
            {
                if (NbrPlace(item) >= 5) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(item, 5);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(item, NbrPlace(item));
                    for (int i = 0; i < 5 - NbrPlace(item); i++)
                    {
                        Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche3"))
            {
                if (NbrPlace(item) >= 7) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(item, 7); ;
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(item, NbrPlace(item));
                    for (int i = 0; i < 7 - NbrPlace(item); i++)
                    {
                        Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }

        }

        else
        {
            if (cible.collider.CompareTag("Roche1"))
            {
                if (NbrPlace(item) >= 4) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(item, 4);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(item, NbrPlace(item));
                    for (int i = 0; i < 4 - NbrPlace(item); i++)
                    {
                        Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche2"))
            {
                if (NbrPlace(item) >= 6) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(item, 6);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(item, NbrPlace(item));
                    for (int i = 0; i < 6 - NbrPlace(item); i++)
                    {
                        Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche2"))
            {
                if (NbrPlace(item) >= 8) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(item, 8);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(item, NbrPlace(item));
                    for (int i = 0; i < 8 - NbrPlace(item); i++)
                    {
                        Instantiate(item.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }

        }
    }

    int CountItem(string itemname) // On compte le nombre de d'item qui s'appellent itemname dans l'inventaire
    {
        int Amount = 0;
        for (int i = 0; i <= inventaire.transform.GetChild(0).childCount - 2; i++)
        {
            if (inventaire.Slot[i].Item.ItemName == itemname)
            {
                Amount += inventaire.Slot[i].Amount * inventaire.Slot[i].Item.Weight;
            }
            else if (inventaire.Slot[i].Item.ItemName == "Vide")
            {

            }
        }
        return Amount;
    }


    int NbrPlace(Item item) //On compte le nombre de place pour un item
    {
        int Count = 0;
        if (item.Weight == 5) //si l'item est un outil
        {
            for (int i = 5; i <= inventaire.transform.GetChild(0).childCount - 2; i++)
            {
                if (inventaire.Slot[i].Item.ItemName == "Vide") // le nombre de place correspond aux nombre de slot vide
                {
                    Count++;
                }
            }
            return Count;
        }
        else // si l'item n'est pas un outil
        {
            for (int i = 5; i <= inventaire.transform.GetChild(0).childCount - 2; i++)
            {
                if (inventaire.Slot[i].Item.ItemName == "Vide" || inventaire.Slot[i].Item == item) // le nombre de place correspond aux nombre de slot vide et ceux ou il y a le meme item avec moins
                                                                                                   // de 64 items
                {
                    Count += 5 - inventaire.Slot[i].Amount * inventaire.Slot[i].Item.Weight;
                }
            }
            return Count;
        }
    }
    void AjouterInventaire(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {
        if (NbrPlace(item) < Amount) // Pas assez de place
        {
            Debug.Log("Il n'y a pas de place dans l'inventaire");
        }
        else
        {
            int i = 5; // pour parcourir l'inventaire
            int x = Amount; // le total d'objet à placer
            while (x != 0) // tant que l'on a pas tout placé
            {
                if (inventaire.Slot[i].Item == item) // si on a le bon item dans l'inventaire
                {
                    if (x + inventaire.Slot[i].Amount * item.Weight > 5) // si on doit placer trop d'item par rapport a la place qu'il reste dans ce slot
                    {
                        x -= 5 / item.Weight - inventaire.Slot[i].Amount;
                        inventaire.Slot[i].Amount = 5 / item.Weight; // on place ce que l'on peut et on continue de parcourir la liste pour placer le reste
                        GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = inventaire.Slot[i].Amount.ToString();
                    }
                    else // si on a assez de place , on place tout
                    {
                        inventaire.Slot[i].Amount += x;
                        GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = inventaire.Slot[i].Amount.ToString();
                        x = 0;

                    }
                }
                if (inventaire.Slot[i].Item.ItemName == "Vide") // Si l'emplacement est vide, on met les items la
                {
                    inventaire.Slot[i].Item = item;
                    inventaire.Slot[i].Amount += x;
                    x = 0;
                    // Mise a jour des sprites et textes
                    /*
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().sprite = item.Icon;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = item.ItemName;
                    GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = inventaire.Slot[i].Amount.ToString();
                    */
                }
                i++;

            }
        }
    }

}