using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Recolte : MonoBehaviour
{
    //SoundDesign part
    public AudioClip treeChop;
    public AudioClip apparitionBulle;
    public AudioClip[] cassageRoche;
    private AudioSource audioSource;

    //Concrete part
    public bool IsCraftArbre; //bool pour ouvrir le menu pour couper l'arbre
    public bool IsCraftRoche; //same
    public bool IsCraftFleur;
    private bool onPanel;//same
    public GameObject FondA, FondR, FondF,menuinfo; //panel à activer pour la récolte
    public Button buttonA1, buttonA2, buttonA3; // boutons sur la panel pour l'arbre
    public Button buttonR1, buttonR2, buttonR3; // boutons pour roche
    public Button buttonF1, buttonF2, buttonF3;
    public Button buttonInfo,buttonQuitter;// boutons pour fleurs
    private Sprite boutonInfo; //pour l'anim
    RaycastHit cible; //pour cibler un gameobject
    public Inventaire inventaire;
    public UI_Inventory ui_inventory;//script de l'inventaire
    Ray R; //raycast 
    private Rect rect; //pour verifier si un clic est dans le menu ( eviter les deplacements si un menu est ouvert)
    public Item boisR,boisF, rocher, fleurs;// pour faire spawn les objets lors de la destruction de leurs parents
    Vector2 mP;
    float height, width;
    new public Camera camera;//longueur et largerur des menus de récolte

    public Player player;
    private Animator animatorA,animatorR,animatorF;

    public List<Item> itemlist;
    public List<GameObject> prefablist;

    private Transform dossierArbres;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        onPanel = false ;
        inventaire = inventaire.GetComponent<Inventaire>();
        ui_inventory = ui_inventory.GetComponent<UI_Inventory>() ;
        buttonA1 = buttonA1.GetComponent<Button>();
        buttonA1.onClick.AddListener(SpawnBuche);
        buttonA2 = buttonA2.GetComponent<Button>();
        buttonA2.onClick.AddListener(SpawnBuche);
        //buttonA3 = buttonA3.GetComponent<Button>();
        //buttonA3.onClick.AddListener(ClickOnPanel);
        // le boutons A1 servira a Couper l'arbre et récuperer les buches
        buttonF1 = buttonF1.GetComponent<Button>();//Bouton cueillir
        buttonF1.onClick.AddListener(SpawnFleurs);//Bouton cueillir
        buttonF2 = buttonF2.GetComponent<Button>();//Bordereau ramasser
        buttonF2.onClick.AddListener(SpawnFleurs);//Bordereau ramasser
        buttonR1 = buttonR1.GetComponent<Button>(); //Bouton miner
        buttonR1.onClick.AddListener(SpawnRoche);//Bouton miner
        buttonR2 = buttonR2.GetComponent<Button>(); //bordereau miner
        buttonR2.onClick.AddListener(SpawnRoche); // bordereau miner
        buttonInfo = buttonInfo.GetComponent<Button>();
        buttonInfo.onClick.AddListener(FctInfo);
        buttonQuitter = buttonQuitter.GetComponent<Button>();
        buttonQuitter.onClick.AddListener(quitter);
        


        height = FondA.GetComponent<RectTransform>().rect.height ;
        width = FondA.GetComponent<RectTransform>().rect.width;
        player = this.GetComponent<Player>();
        
        animatorA = FondA.transform.GetChild(0).GetComponent<Animator>();
        animatorR = FondR.transform.GetChild(0).GetComponent<Animator>();
        animatorF = FondF.transform.GetChild(0).GetComponent<Animator>();

        var list = Resources.LoadAll("items", typeof(Item)).Cast<Item>();
        foreach(Item item in list)
        {
            itemlist.Add(item);
        }
        var list2 = Resources.LoadAll("souches", typeof(GameObject)).Cast<GameObject>();
        foreach (GameObject go in list2)
        {
            prefablist.Add(go);
        }
        dossierArbres = GameObject.Find("Arbres").transform;

        // Pour faire fonctionner les anims
    }
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {   
                if ((IsCraftArbre | IsCraftFleur | IsCraftRoche) && onPanel == true) // si un des menus est ouvert et qu'on clique dedans, on ne crée pas de raycast
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
                        //rect = new Rect(mP.x - width /3, mP.y - height/2, width, height);
                        //rectransform.rect = RectTransformToScreenSpace(rectransform);// on crée le rectangle du menus pour vérifier avec le contains

                        if (hit.collider.CompareTag("Arbre")) // si on touche un arbre :
                        {
                        if (onPanel == true || IsCraftArbre == false && (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) + ((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) < 1))
                        {

                                audioSource.PlayOneShot(apparitionBulle); 
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
                            if (NbrPlace(boisR) > 0) // a changer lorsque l'inventaire sera fonctionnelle
                            {
                                Destroy(hit.transform.gameObject);
                                AjouterInventaire(boisR, 1); // à changer lorsque l'inventaire sera terminé
                            }
                        }

                        if ((hit.collider.CompareTag("Roche1")) || (hit.collider.CompareTag("Roche2")) || (hit.collider.CompareTag("Roche3"))) //pareil avec les roches
                        {
                        if (onPanel == true || IsCraftRoche == false && (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) + ((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) < 1))
                        {
                            audioSource.PlayOneShot(apparitionBulle);                          
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
                        if (onPanel == true || IsCraftFleur == false)
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

                    if (onPanel == false && (hit.collider.tag != "Arbre" && hit.collider.tag != "Roche1" && hit.collider.tag != "Roche2" && hit.collider.tag != "Roche3" && hit.collider.tag != "Fleurs")|| (((Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4) * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 4)) + ((Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4) * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 4)) > 1))
                        {
                            animatorA.SetTrigger("fermetture1BulleCouper");
                            animatorR.SetTrigger("fermetture1BulleCouper");
                            animatorF.SetTrigger("fermetture1BulleCouper");
                            IsCraftRoche = false;
                            IsCraftFleur = false;
                            IsCraftArbre = false;
                        }

                    }

                }
            
        }

        if (IsCraftArbre == true)
        {
            FondA.transform.position = new Vector2(mP.x - width / 3, mP.y + height);
            if (ui_inventory.NomItemEquip() == "Hache")
            {
                buttonA1.interactable = true;
                buttonA2.interactable = true;
            }
            else
            {
                buttonA1.interactable = false;
                buttonA2.interactable = false;
            }
            FondA.SetActive(true);
            
            animatorA.SetTrigger("ouverture1BulleCouper");
        }
        else
        {
            FondA.SetActive(false);
        }

        if (IsCraftRoche == true)
        {
            FondR.transform.position = new Vector2(mP.x - width/3, mP.y + height);
            if (ui_inventory.NomItemEquip() == "Pioche")
            {
                buttonR1.interactable = true;
                buttonR2.interactable = true;
            }
            else
            {
                buttonR1.interactable = false;
                buttonR2.interactable = false;
            }
            FondR.SetActive(true);
            animatorR.SetTrigger("ouverture1BulleCouper");
        }
        else
        {
            FondR.SetActive(false);
        }

        if (IsCraftFleur == true)
        {
            FondF.transform.position = new Vector2(mP.x - width/3 , mP.y+ height);
            FondF.SetActive(true);
            animatorF.SetTrigger("ouverture1BulleCouper");
        }
        else
        {
            FondF.SetActive(false);
        }
    }

    private void SpawnBuche() //fonction qui fait détruit cible et fait remplit l'inventaire ou fait spawn le bois dont on a pas la place dans l'inventaire
    {
        //player.uiInventory.ReduitDuraEquip();
        audioSource.PlayOneShot(treeChop);
        if (cible.transform.name.IndexOf("Chene", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (cible.transform.name.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (ui_inventory.NbrPlace(boisF) >= 6)  AjouterInventaire(boisF, 6);
                AjouterInventaire(itemlist[FindInlist("GraineChene")], 1);
                Instantiate(prefablist[FindPrefabinList("Souche Chene")], new Vector3(cible.transform.position.x, cible.transform.position.y - 5.08f, cible.transform.position.z - 6.75f), Quaternion.Euler(0, 0, 0),dossierArbres);

            }
            if (cible.transform.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Instantiate(prefablist[FindPrefabinList("Souche Chene")], new Vector3(cible.transform.position.x, cible.transform.position.y - 5.08f, cible.transform.position.z - 6.75f), Quaternion.Euler(0, 0, 0),dossierArbres);
            }
            if (cible.transform.name.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (ui_inventory.NbrPlace(boisF) >= 1) AjouterInventaire(boisF, 1);
            }
            else
            {
                if (ui_inventory.NbrPlace(boisR) >= 4) AjouterInventaire(boisR, 4);
                if (ui_inventory.NbrPlace(boisF) >= 3) AjouterInventaire(boisF, 3);
                AjouterInventaire(itemlist[FindInlist("GraineChene")], 1);
                Instantiate(prefablist[FindPrefabinList("Souche Chene")], new Vector3(cible.transform.position.x, cible.transform.position.y - 5.08f, cible.transform.position.z - 6.75f), Quaternion.Euler(0, 0, 0),dossierArbres);
            }

        }
        if (cible.transform.name.IndexOf("Hetre", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (cible.transform.name.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (ui_inventory.NbrPlace(boisF) >= 5)  AjouterInventaire(boisF, 5);
                AjouterInventaire(itemlist[FindInlist("GraineHetre")], 1);
                Instantiate(prefablist[FindPrefabinList("Souche Hetre")], new Vector3(cible.transform.position.x, cible.transform.position.y - 3.01f, cible.transform.position.z - 5.0f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
            if (cible.transform.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Instantiate(prefablist[FindPrefabinList("Souche Hetre")], new Vector3(cible.transform.position.x, cible.transform.position.y - 3.01f, cible.transform.position.z - 5.0f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
            if (cible.transform.name.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (ui_inventory.NbrPlace(boisF) >= 1) AjouterInventaire(boisF, 1);
            }
            else
            {
                if (ui_inventory.NbrPlace(boisR) >= 3) AjouterInventaire(boisR, 3);
                if (ui_inventory.NbrPlace(boisF) >= 3) AjouterInventaire(boisF, 3);
                AjouterInventaire(itemlist[FindInlist("GraineHetre")], 1);
                Instantiate(prefablist[FindPrefabinList("Souche Hetre")], new Vector3(cible.transform.position.x, cible.transform.position.y - 3.01f, cible.transform.position.z -5.0f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
        }
        if (cible.transform.name.IndexOf("Pin", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (cible.transform.name.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (ui_inventory.NbrPlace(boisF) >= 4) AjouterInventaire(boisF, 4);
                AjouterInventaire(itemlist[FindInlist("GrainePinM")], 1);
                Instantiate(prefablist[FindPrefabinList("Souche Pin")], new Vector3(cible.transform.position.x, cible.transform.position.y - 4.37f, cible.transform.position.z - 5.89f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
            if (cible.transform.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Instantiate(prefablist[FindPrefabinList("Souche Pin")], new Vector3(cible.transform.position.x, cible.transform.position.y - 4.37f, cible.transform.position.z - 5.89f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
            if (cible.transform.name.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (ui_inventory.NbrPlace(boisF) >= 1) AjouterInventaire(boisF, 1);
            }
            else
            {
                if (ui_inventory.NbrPlace(boisR) >= 4) AjouterInventaire(boisR, 4);
                if (ui_inventory.NbrPlace(boisF) >= 1) AjouterInventaire(boisF, 1);
                AjouterInventaire(itemlist[FindInlist("GrainePinM")], 1);
                Instantiate(prefablist[FindPrefabinList("Souche Pin")], new Vector3(cible.transform.position.x, cible.transform.position.y - 4.37f, cible.transform.position.z - 5.89f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
        }
        if (cible.transform.name.IndexOf("Douglas",StringComparison.OrdinalIgnoreCase) >=0)
        {
            if(cible.transform.name.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (ui_inventory.NbrPlace(boisF) >= 3) AjouterInventaire(boisF, 3);
                AjouterInventaire(itemlist[FindInlist("GraineDouglas")], 1);
                Instantiate(prefablist[FindPrefabinList("Souche Douglas")], new Vector3(cible.transform.position.x, cible.transform.position.y, cible.transform.position.z + 1.0f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
            if (cible.transform.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Instantiate(prefablist[FindPrefabinList("Souche Douglas")], new Vector3(cible.transform.position.x, cible.transform.position.y, cible.transform.position.z + 1.0f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
            if (cible.transform.name.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (ui_inventory.NbrPlace(boisF) >= 1) AjouterInventaire(boisF, 1);
            }
            else
            {
                if (ui_inventory.NbrPlace(boisR) >= 1) AjouterInventaire(boisR, 1);
                if (ui_inventory.NbrPlace(boisF) >= 4) AjouterInventaire(boisF, 4);
                AjouterInventaire(itemlist[FindInlist("GraineDouglas")], 1);
                Instantiate(prefablist[FindPrefabinList("Souche Douglas")], new Vector3(cible.transform.position.x, cible.transform.position.y , cible.transform.position.z+1.0f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }

        }
        if (cible.transform.name.IndexOf("Bouleau", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (cible.transform.name.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (ui_inventory.NbrPlace(boisF) >= 4) AjouterInventaire(boisF, 4);
                AjouterInventaire(itemlist[FindInlist("GraineBouleau")], 1);
                Instantiate(prefablist[FindPrefabinList("Souche Bouleau")], new Vector3(cible.transform.position.x, cible.transform.position.y - 5.08f, cible.transform.position.z - 6.75f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
            if (cible.transform.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Instantiate(prefablist[FindPrefabinList("Souche Bouleau")], new Vector3(cible.transform.position.x, cible.transform.position.y - 5.08f, cible.transform.position.z - 6.75f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
            if (cible.transform.name.IndexOf("Souche", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (ui_inventory.NbrPlace(boisF) >= 1) AjouterInventaire(boisF, 1);
            }
            else
            {
                if (ui_inventory.NbrPlace(boisF) >= 5) AjouterInventaire(boisF, 5);
                AjouterInventaire(itemlist[FindInlist("GraineBouleau")], 1);
                Instantiate(prefablist[FindPrefabinList("Souche Bouleau")], new Vector3(cible.transform.position.x, cible.transform.position.y - 5.08f, cible.transform.position.z - 6.75f), Quaternion.Euler(0, 0, 0), dossierArbres);
            }
        }
        if(cible.transform.name.IndexOf("Arbuste", StringComparison.OrdinalIgnoreCase) >= 0)
        {

        }
        {

        }
        Destroy(cible.transform.gameObject);//detruit cible
        IsCraftArbre = false;  
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
                Instantiate(fleurs.prefab, cible.transform.position - new Vector3(UnityEngine.Random.Range(-5, 5), cible.transform.position.y / 2, UnityEngine.Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
            }
        }
    }
    private void SpawnRoche() //fonction qui fait détruit cible et fait spawn spawned
    {
        player.uiInventory.ReduitDuraEquip();
        audioSource.clip = cassageRoche[UnityEngine.Random.Range(0, cassageRoche.Length)];
        audioSource.PlayOneShot(audioSource.clip);
        Destroy(cible.transform.gameObject);
        IsCraftRoche = false;//detruit cible
        float x = UnityEngine.Random.Range(0f, 1f); // variable pour le nombre de spawned a faire apparaitre
        if (0 <= x && x < 0.25) //3 spawns
        {
            if (cible.collider.CompareTag("Rocher") && cible.transform.name.IndexOf("Rocher3", StringComparison.OrdinalIgnoreCase) >= 0)
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
                        Instantiate(rocher, cible.transform.position - new Vector3(UnityEngine.Random.Range(-5, 5), cible.transform.position.y / 2, UnityEngine.Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Rocher") && cible.transform.name.IndexOf("Rocher2", StringComparison.OrdinalIgnoreCase) >= 0)
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
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(UnityEngine.Random.Range(-5, 5), cible.transform.position.y / 2, UnityEngine.Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            if (cible.collider.CompareTag("Rocher") && cible.transform.name.IndexOf("Rocher1", StringComparison.OrdinalIgnoreCase) >= 0)
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
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(UnityEngine.Random.Range(-5, 5), cible.transform.position.y / 2, UnityEngine.Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
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
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(UnityEngine.Random.Range(-5, 5), cible.transform.position.y / 2, UnityEngine.Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
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
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(UnityEngine.Random.Range(-5, 5), cible.transform.position.y / 2, UnityEngine.Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
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
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(UnityEngine.Random.Range(-5, 5), cible.transform.position.y / 2, UnityEngine.Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
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
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(UnityEngine.Random.Range(-5, 5), cible.transform.position.y / 2, UnityEngine.Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
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
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(UnityEngine.Random.Range(-5, 5), cible.transform.position.y / 2, UnityEngine.Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
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
                        Instantiate(rocher.prefab, cible.transform.position - new Vector3(UnityEngine.Random.Range(-5, 5), cible.transform.position.y / 2, UnityEngine.Random.Range(-5, 5)), Quaternion.Euler(90, 180, 0));
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

    /* private void OnGUI()
     {
         if (IsCraftArbre)
         {
             GUI.Box(rect,"cc");
         }
     }*/

    public void ClickOnPanel()
    {
        onPanel = true;
    }

    public void ClickOutPanel()
    {
        onPanel = false;
    }

    int FindInlist(string itemname) //
    {
        int i = 0;
        foreach(Item item in itemlist)
        {
            if (item.name == itemname) return i;
            i++;
        }
        return itemlist.Count + 1;
    }

    int FindPrefabinList(string prefabname)
    {
        int i = 0;
        foreach (GameObject go in prefablist)
        {
            if (go.name == prefabname) return i;
            i++;
        }
        return itemlist.Count + 1;
    }

    void quitter()
    {
        menuinfo.SetActive(false);
        FondA.gameObject.SetActive(true);
        animatorA.SetTrigger("ouverture1BulleCouper");
    }
    void FctInfo()
    {
        if (cible.transform.name.IndexOf("Chene", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (cible.transform.name.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Chene";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Frêle";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le chêne se plait particulièrement au sein d'une fôret verdoyante et il pousse lentement.";

            }
            if (cible.transform.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Chene";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Malade";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le chêne se plait particulièrement au sein d'une fôret verdoyante et il pousse lentement.";
            }
            else
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Chene";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Robuste";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le chêne se plait particulièrement au sein d'une fôret verdoyante et il pousse lentement.";
            }

        }
        if (cible.transform.name.IndexOf("Hetre", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (cible.transform.name.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Hetre";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Frêle";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "L'hêtre n'a pas d'habitat favori et pousse assez rapidement.";
            }
            if (cible.transform.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Hetre";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Malade";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "L'hêtre n'a pas d'habitat favori et pousse assez rapidement.";
            }
            else
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Hetre";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Robuste";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "L'hêtre n'a pas d'habitat favori et pousse assez rapidement.";
            }
        }
        if (cible.transform.name.IndexOf("Pin", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (cible.transform.name.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Pin Maritime";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Frêle";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le pin maritime se plait particulièrement à proximité de la mer, mais pousse assez lentement";
            }
            if (cible.transform.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Pin Maritime";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Malade";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le pin maritime se plait particulièrement à proximité de la mer, mais pousse assez lentement";
            }
            else
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Pin Maritime";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Robuste";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le pin maritime se plait particulièrement à proximité de la mer, mais pousse assez lentement";
            }
        }
        if (cible.transform.name.IndexOf("Douglas", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (cible.transform.name.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Douglas";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Frêle";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le douglas aime les zones en altitude et pousse assez rapidement.";
            }
            if (cible.transform.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Douglas";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Malade";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le douglas aime les zones en altitude et pousse assez rapidement.";
            }
            else
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Douglas";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Robuste";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le douglas aime les zones en altitude et pousse assez rapidement.";
            }

        }
        if (cible.transform.name.IndexOf("Bouleau", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (cible.transform.name.IndexOf("Frele", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Bouleau";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Frêle";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le bouleau n'a pas d'habitat favori et pousse rapidement.";
            }
            if (cible.transform.name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Bouleau";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Malade";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le bouleau n'a pas d'habitat favori et pousse rapidement.";
            }
            else
            {
                menuinfo.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Bouleau";
                menuinfo.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Robuste";
                menuinfo.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Le bouleau n'a pas d'habitat favori et pousse rapidement.";
            }
        }
        FondA.SetActive(false);
        menuinfo.SetActive(true);
    }


}