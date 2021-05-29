using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recolte : MonoBehaviour
{
    public bool IsCraftArbre; //bool pour ouvrir le menu pour couper l'arbre
    public bool IsCraftRoche; //same
    public bool IsCraftFleur; //same
    public GameObject FondA, FondR, FondF; //panel à activer pour la récolte
    public Button buttonA1, buttonA2, buttonA3; // boutons sur la panel pour l'arbre
    public Button buttonR1, buttonR2, buttonR3; // boutons pour roche
    public Button buttonF1, buttonF2, buttonF3; // boutons pour fleurs
    RaycastHit cible; //pour cibler un gameobject
    public Inventaire inventaire;//script de l'inventaire
    Ray R; //raycast 
    private Rect rect; //pour verifier si un clic est dans le menu ( eviter les deplacements si un menu est ouvert)
    public Item bois, rocher, fleurs;// pour faire spawn les objets lors de la destruction de leurs parents
    Vector2 mP;
    public int a;
    float height, width;
    new public Camera camera;//longueur et largerur des menus de récolte

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        inventaire = inventaire.GetComponent<Inventaire>();
        buttonA1 = buttonA1.GetComponent<Button>();
        buttonA1.onClick.AddListener(SpawnBuche); // le boutons A1 servira a Couper l'arbre et récuperer les buches
        buttonF1 = buttonF1.GetComponent<Button>();
        buttonF1.onClick.AddListener(SpawnFleurs);
        buttonR1 = buttonR1.GetComponent<Button>();
        buttonR1.onClick.AddListener(SpawnRoche);
        height = FondA.GetComponent<RectTransform>().rect.height ;
        width = FondA.GetComponent<RectTransform>().rect.width;
        a = Screen.height / 3;
        player = this.GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {   
                if ((IsCraftArbre | IsCraftFleur | IsCraftRoche) && (rect.Contains(new Vector2(Input.mousePosition.x, Input.mousePosition.y))) == true) // si un des menus est ouvert et qu'on clique dedans, on ne crée pas de raycast
                                                                                                                                                        //( pour éviter que le personnage ne selectionne un objet derriere le menu)
                {
                }

                else
                {
                    RaycastHit hit;
                    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit)) // on verifie si le raycast a touché un gameobject
                    {
                        mP = new Vector2(Input.mousePosition.x, Input.mousePosition.y); // on prend les coordonnées du clic pour créer le menu où on clic
                        rect = new Rect(mP.x, mP.y - height, width, height); // on crée le rectangle du menus pour vérifier avec le contains

                        if (hit.collider.CompareTag("Arbre")) // si on touche un arbre :
                        {
                            if(((Input.mousePosition.x - Screen.width / 2) * (Input.mousePosition.x - Screen.width / 2) + (Input.mousePosition.y - Screen.height / 2) * (Input.mousePosition.y - Screen.height / 2) < a * a))
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
                                    cible = hit;
                                    R = ray;
                                    IsCraftArbre = false;
                                    IsCraftArbre = true;
                                }
                            }

                        }

                        if (hit.collider.CompareTag("Bois")) // si on clic sur une buche et qu'on a assez de place dans l'inventaire, on la récupère
                        {
                            if (NbrPlace(bois) > 0) // a changer lorsque l'inventaire sera fonctionnelle
                            {
                                Destroy(hit.transform.gameObject);
                                AjouterInventaire(bois, 1); // à changer lorsque l'inventaire sera terminé
                            }
                        }

                        if ((hit.collider.CompareTag("Roche1")) || (hit.collider.CompareTag("Roche2")) || (hit.collider.CompareTag("Roche3"))) //pareil avec les roches
                        {
                            if (((Input.mousePosition.x - Screen.width / 2) * (Input.mousePosition.x - Screen.width / 2) + (Input.mousePosition.y - Screen.height / 2) * (Input.mousePosition.y - Screen.height / 2) < a * a))
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
                                    cible = hit;
                                    R = ray;
                                    IsCraftRoche = false;
                                    IsCraftRoche = true;
                                }
                            }
                        }

                        /* if (hit.collider.CompareTag("Rocher")) // pareil que les buches
                         {
                             if (NbrPlace(rocher) > 0)
                             {
                                 Destroy(hit.transform.gameObject);
                                 AjouterInventaire(rocher, 1); // a changer plus tard selon le fonctionnement de l'inventaire
                             }
                         }*/

                        if (hit.collider.CompareTag("Fleurs")) //same
                        {
                            if(((Input.mousePosition.x - Screen.width / 2) * (Input.mousePosition.x - Screen.width / 2) + (Input.mousePosition.y - Screen.height / 2) * (Input.mousePosition.y - Screen.height / 2) < a * a))
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
                                    cible = hit;
                                    R = ray;
                                    IsCraftFleur = false;
                                    IsCraftFleur = true;
                                }
                            }
                        
                        }

                        if (hit.collider.CompareTag("FleursDrop"))//same
                        {
                            if (NbrPlace(fleurs) > 0)
                            {
                                Destroy(hit.transform.gameObject);
                                AjouterInventaire(fleurs, 1); // à changer plus tard selon le fonctionnement de l'inventaire
                            }
                        }

                        if ((hit.collider.tag != "Arbre" && hit.collider.tag != "Roche1" && hit.collider.tag != "Roche2" && hit.collider.tag != "Roche3" && hit.collider.tag != "Fleurs")|| ((Input.mousePosition.x - Screen.width / 2) * (Input.mousePosition.x - Screen.width / 2) + (Input.mousePosition.y - Screen.height / 2) * (Input.mousePosition.y - Screen.height / 2) > a * a))
                    {
                            IsCraftRoche = false;
                            IsCraftFleur = false;
                            IsCraftArbre = false;
                        }

                    }

                }
            
        }

        if (IsCraftArbre == true)
        {
            FondA.transform.position = new Vector2(mP.x, mP.y );
            if (CountItem("Hache") > 0)
            {
                buttonA1.interactable = true;
            }
            else
            {
                buttonA1.interactable = false;
            }
            FondA.SetActive(true);
        }
        else
        {
            FondA.SetActive(false);
        }

        if (IsCraftRoche == true)
        {
            FondR.transform.position = new Vector2(mP.x, mP.y );
            if (CountItem("Pioche") > 0)
            {
                buttonR1.interactable = true;
            }
            else
            {
                buttonR1.interactable = false;
            }
            FondR.SetActive(true);
        }
        else
        {
            FondR.SetActive(false);
        }

        if (IsCraftFleur == true)
        {
            FondF.transform.position = new Vector2(mP.x, mP.y);
            FondF.SetActive(true);
        }
        else
        {
            FondF.SetActive(false);
        }
    }

    private void SpawnBuche() //fonction qui fait détruit cible et fait remplit l'inventaire ou fait spawn le bois dont on a pas la place dans l'inventaire
    {
        Destroy(cible.transform.gameObject);//detruit cible
        IsCraftArbre = false;
        float x = Random.Range(0f, 1f); // variable pour le nombre de spawned a faire apparaitre
        if (0 <= x && x < 0.25) //3 spawns
        {
            if (NbrPlace(bois) >= 3) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
            {
                AjouterInventaire(bois, 3);
            }
            else //sinon, on remplit l'inventaire et le reste va par terre
            {
                AjouterInventaire(bois, NbrPlace(bois));
                for (int i = 0; i < 3 - NbrPlace(bois); i++)
                {
                    Instantiate(bois.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(0, 0, 0));
                }
            }

        }
        else if (0.25 <= x && x < 0.75) // 4 spawns
        {
            if (NbrPlace(bois) >= 4)
            {
                AjouterInventaire(bois, 4);
            }
            else
            {
                AjouterInventaire(bois, NbrPlace(bois));
                for (int i = 0; i < 4 - NbrPlace(bois); i++)
                {
                    Instantiate(bois.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(0,0, 0));
                }
            }
        }

        else
        {
            if (NbrPlace(bois) >= 5) // 5 spawns
            {
                AjouterInventaire(bois, 5);
            }
            else
            {
                AjouterInventaire(bois, NbrPlace(bois));
                for (int i = 0; i < 5 - NbrPlace(bois); i++)
                {
                    Instantiate(bois.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(0, 0, 0));
                }
            }
        }
    }
    private void SpawnFleurs() //Pour les fleurs, on a toujours 3 spawns
    {
        Destroy(cible.transform.gameObject);
        IsCraftFleur = false;//detruit cible
        if (NbrPlace(fleurs) >= 3) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
        {
            AjouterInventaire(fleurs, 3);
        }
        else //sinon, on remplit l'inventaire et le reste va par terre
        {
            AjouterInventaire(fleurs, NbrPlace(fleurs));
            for (int i = 0; i < 3 - NbrPlace(fleurs); i++)
            {
                Instantiate(fleurs.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
            }
        }
    }
    private void SpawnRoche() //fonction qui fait détruit cible et fait spawn spawned
    {
        Destroy(cible.transform.gameObject);
        IsCraftRoche = false;//detruit cible
        float x = Random.Range(0f, 1f); // variable pour le nombre de spawned a faire apparaitre
        if (0 <= x && x < 0.25) //3 spawns
        {
            if (cible.collider.CompareTag("Roche1"))
            {
                if (NbrPlace(rocher) >= 2) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(rocher, 2);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(rocher, NbrPlace(rocher));
                    for (int i = 0; i < 2 - NbrPlace(rocher); i++)
                    {
                        Instantiate(rocher, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche2"))
            {
                if (NbrPlace(rocher) >= 4) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(rocher, 4);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(rocher, NbrPlace(rocher));
                    for (int i = 0; i < 4 - NbrPlace(rocher); i++)
                    {
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche3"))
            {
                if (NbrPlace(rocher) >= 6) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(rocher, 6);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(rocher, NbrPlace(rocher));
                    for (int i = 0; i < 6 - NbrPlace(rocher); i++)
                    {
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }

        }

        else if (0.25 <= x && x < 0.75) // 4 spawns
        {
            if (cible.collider.CompareTag("Roche1"))
            {
                if (NbrPlace(rocher) >= 3) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(rocher, 3);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(rocher, NbrPlace(rocher));
                    for (int i = 0; i < 3 - NbrPlace(rocher); i++)
                    {
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche2"))
            {
                if (NbrPlace(rocher) >= 5) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(rocher, 5);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(rocher, NbrPlace(rocher));
                    for (int i = 0; i < 5 - NbrPlace(rocher); i++)
                    {
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche3"))
            {
                if (NbrPlace(rocher) >= 7) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(rocher, 7); ;
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(rocher, NbrPlace(rocher));
                    for (int i = 0; i < 7 - NbrPlace(rocher); i++)
                    {
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }

        }

        else
        {
            if (cible.collider.CompareTag("Roche1"))
            {
                if (NbrPlace(rocher) >= 4) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(rocher, 4);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(rocher, NbrPlace(rocher));
                    for (int i = 0; i < 4 - NbrPlace(rocher); i++)
                    {
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche2"))
            {
                if (NbrPlace(rocher) >= 6) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(rocher, 6);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(rocher, NbrPlace(rocher));
                    for (int i = 0; i < 6 - NbrPlace(rocher); i++)
                    {
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Roche2"))
            {
                if (NbrPlace(rocher) >= 8) // A remplacer quand l'inventaire sera fonctionnel, mais en gros si on a plus de trois places dans le bon slot de l'inventaire, tout va directement dedans
                {
                    AjouterInventaire(rocher, 8);
                }
                else //sinon, on remplit l'inventaire et le reste va par terre
                {
                    AjouterInventaire(rocher, NbrPlace(rocher));
                    for (int i = 0; i < 8 - NbrPlace(rocher); i++)
                    {
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(Random.Range(-5, 5), cible.transform.position.y / 2, Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }

        }
    }

    public int CountItem(string itemname) // On compte le nombre de d'item qui s'appellent itemname dans l'inventaire
    {
        /*
        int Amount = 0;
        foreach (ItemAmount ItemAmount in inventaire.Slot)
        {
            if (ItemAmount.Item.ItemName == itemname)
            {
                Amount += ItemAmount.Amount * ItemAmount.Item.Weight;
            }
            else if (ItemAmount.Item.ItemName == "Vide")
            {

            }
        }
        return Amount;
        */
        return player.uiInventory.CountItem(itemname);
    }

    int NbrPlace(Item item) //On compte le nombre de place pour un item
    {
        /*
        int Count = 0;
        if (item.Weight == 64) //si l'item est un outil
        {
            foreach (ItemAmount ItemAmount in inventaire.Slot)
            {
                if (ItemAmount.Item.ItemName == "Vide") // le nombre de place correspond aux nombre de slot vide
                {
                    Count++;
                }
            }
            return Count;
        }
        else // si l'item n'est pas un outil
        {
            foreach (ItemAmount ItemAmount in inventaire.Slot)
            {
                if (ItemAmount.Item.ItemName == "Vide" || ItemAmount.Item == item) // le nombre de place correspond aux nombre de slot vide et ceux ou il y a le meme item avec moins
                                                                                   // de 64 items
                {
                    Count += 64 - ItemAmount.Amount * ItemAmount.Item.Weight;
                }
            }
            return Count;
        }
        */
        return player.uiInventory.NbrPlace(item);
    }

    void AjouterInventaire(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {
        /*
        if (NbrPlace(item) < Amount) // Pas assez de place
        {
            Debug.Log("Il n'y a pas de place dans l'inventaire");
        }
        else
        {
            int i = 0; // pour parcourir l'inventaire
            int x = Amount; // le total d'objet à placer
            while (x != 0) // tant que l'on a pas tout placé
            {
                if (inventaire.Slot[i].Item == item) // si on a le bon item dans l'inventaire
                {
                    if (x + inventaire.Slot[i].Amount * item.Weight > 64) // si on doit placer trop d'item par rapport a la place qu'il reste dans ce slot
                    {
                        x -= 64 / item.Weight - inventaire.Slot[i].Amount;
                        inventaire.Slot[i].Amount = 64 / item.Weight; // on place ce que l'on peut et on continue de parcourir la liste pour placer le reste
                    }
                    else // si on a assez de place , on place tout
                    {
                        inventaire.Slot[i].Amount += x;
                        x = 0;

                    }
                }
                if (inventaire.Slot[i].Item.ItemName == "Vide")// Si l'emplacement est vide, on met les items la
                {
                    inventaire.Slot[i].Item = item;
                    inventaire.Slot[i].Amount += x;
                    x = 0;
                    // Mise a jour des sprites et textes
                    
                    //GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().sprite = item.Icon;
                    //GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>().text = item.ItemName;
                    //GameObject.Find("Inventory").transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = inventaire.Slot[i].Amount.ToString();
                }
                i++;

            }
        }
        */
        player.inventory.AddItem(new ItemAmount(Item: item, Amount: Amount));
    }
}