using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using Random;

public class Chauffage : MonoBehaviour
{

    //Les scripts 
    public UI_Inventory ui_inventory;
    public ScriptATHBatis scriptATHBatis;
    public Player player;
    Maladie maladie;


    //Les éléments de l'UI: boutons/ scrollbars etc..
    public GameObject boutonValiderChauffage;
    public GameObject livreChauffage;
    private static GameObject[] listeArbres;
    private GameObject randomArbre;
    public Text textNombreBoisRobuste;
    public Text textNombreBoisFrêle;
    public Text textNombreBoisChauffage;
    public Text textBesoinJournalier;
    public Slider sliderBoisRobuste;
    public Slider sliderBoisFrêle;
    private int nombreBoisStockage=10;
    public int niveauGardeForestier=5;
    public int boisDeChauffage=0;
    // Start is called before the first frame update
    public int nombreBoisRobuste=10;
    public int nombreBoisFrêle=10;
    public Item boisR,boisF;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        //ui_inventory = player.uiInventory; //à réécrire quand on ouvre le menu chauffage
        ui_inventory = ui_inventory.GetComponent<UI_Inventory>();
        scriptATHBatis=scriptATHBatis.GetComponent<ScriptATHBatis>() ;
        //AfficherChauffage();
        
    }

    // Update is called once per frame
    void Update() // J'ai mis le script sur chauffage, qu'est actif que quand le livre est visible, donc pas de calculs en trop
    {
          textNombreBoisRobuste.text="x"+sliderBoisRobuste.value.ToString();
          textNombreBoisFrêle.text="x"+sliderBoisFrêle.value.ToString();
          textNombreBoisChauffage.text=((int)((sliderBoisRobuste.value)*3+(sliderBoisFrêle.value)*3)+boisDeChauffage).ToString() + "/" + NombreBoisStockage().ToString();

    }


    public int NombreBoisStockage(){
        if (niveauGardeForestier==1){
            return(10);
        }
        if (niveauGardeForestier==2){
            return(15);
        }
        if (niveauGardeForestier==3){
            return(25);
        }
        if (niveauGardeForestier==4){
            return(35);
        }
        if (niveauGardeForestier>=5){
            return(50);
        }
        return(0);

    }

    public void AfficherChauffage(){

        textBesoinJournalier.text=scriptATHBatis.nombreDeBâtimentsConstruits.ToString(); //On va dire que ça marche je peux pas test je peux pas construire de bâtiment :)
        livreChauffage.SetActive(true);
        textNombreBoisChauffage.text=NombreBoisStockage().ToString();
        Debug.Log(player.transform.name);
        //AjouterInventaire(boisR,1);
        nombreBoisFrêle=player.uiInventory.CountItem("Bois Frele");
        nombreBoisRobuste=player.uiInventory.CountItem("Bois");
        Debug.Log(nombreBoisFrêle);
        //Debug.Log()
         sliderBoisFrêle.maxValue=nombreBoisFrêle;
         sliderBoisRobuste.maxValue=nombreBoisRobuste;
         //sliderBoisFrêle.maxValue=10;
         //sliderBoisRobuste.maxValue=10;
    }


    public void boutonValider(){
        if (((int)((sliderBoisRobuste.value)*3+(sliderBoisFrêle.value)*3)+boisDeChauffage)<=NombreBoisStockage()){
            boisDeChauffage+=(int)((sliderBoisRobuste.value)*3+(sliderBoisFrêle.value)*3);
            RetirerInventaire(boisR,((int)(sliderBoisRobuste.value)));
            RetirerInventaire(boisF,((int)(sliderBoisFrêle.value)));
            livreChauffage.SetActive(false);
        }
        else{
            GameObject.Find("Popup").GetComponent<Popup>().popup("Vous n'avez pas assez d'espace de stockage");
        }
    }

    public void boutonAnnuler(){
        livreChauffage.SetActive(false);
    }

        
    public void FonctionMinuit(){
        if (boisDeChauffage>=scriptATHBatis.nombreDeBâtimentsConstruits){
            boisDeChauffage-=scriptATHBatis.nombreDeBâtimentsConstruits; //j'ai pas enlevé le besoin en chauffage quand les bâtiments sont détruits, il faudra faire attention
        }
        else{
            boisDeChauffage=0;
            //le garde forestier coupe 5 arbres aléatoirement, baisse de 1.5 jauge qualité de vie
            listeArbres=GameObject.FindGameObjectsWithTag("Arbre");
            int longueurListe = listeArbres.Length;
            int i=0;
            if (longueurListe>=5){
                while (i<5){
                randomArbre=listeArbres[UnityEngine.Random.Range(0, longueurListe)];
                if (!(randomArbre.name.IndexOf("malade", StringComparison.OrdinalIgnoreCase) >= 0) && !(randomArbre.name.IndexOf("souche", StringComparison.OrdinalIgnoreCase) >= 0)){
                    Destroy(randomArbre);
                    i+=1;
                }
                }
                    
            }
            else{
                //On perd encore plus de jauge
            }
        }
    }





    //fonction pour ajouter/ retirer un item de l'inventaire


    // public int CountItem(string itemname) // On compte le nombre de d'item qui s'appellent itemname dans l'inventaire
    // {
    //     /*
    //     int Amount = 0;
    //     foreach (ItemAmount ItemAmount in inventaire.Slot)
    //     {
    //         if (ItemAmount.Item.ItemName == itemname)
    //         {
    //             Amount += ItemAmount.Amount * ItemAmount.Item.Weight;
    //         }
    //         else if (ItemAmount.Item.ItemName == "Vide")
    //         {

    //         }
    //     }
    //     return Amount;
    //     */
        
    //     return player.uiInventory.CountItem(itemname);
    // }


    public void AjouterBois(){
        AjouterInventaire(boisR, 1);
    }
     void AjouterInventaire(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {

        player.inventory.AddItem(new ItemAmount(Item: item, Amount: Amount));
    }

    //fonction pour retirer un item de l'inventaire
    void RetirerInventaire(Item item, int Amount) //On ajoute Amount items dans l'inventaire
    {

        player.inventory.DelItem(new ItemAmount(Item: item, Amount: Amount));
    }
}