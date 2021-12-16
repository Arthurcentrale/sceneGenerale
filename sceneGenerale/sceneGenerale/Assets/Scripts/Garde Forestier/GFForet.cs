using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GFForet : MonoBehaviour
{
    //Les scripts 
    public UI_Inventory ui_inventory;
    public Player player;
    Maladie maladie;

    //Les items graines
    public Item graineCerisier;
    public Item graineChene;
    public Item graineHetre;
    public Item grainePin;
    public Item graineDouglas;
    public Item graineBouleau;

    //Les variables
    private int nombreChenes;
    private int nombreHetres;
    private int nombrePins;
    private int nombreDouglas;
    private int nombreBouleaux;
    private int[] liste;
    public bool graineDéjàDonnée=false;
    //private string essenceMaladeOui; //ça bug si je récupère directement avec maladie.essenceMalade

    //Les GameObjects Dialogue
    public GameObject dialogueBoite;
    public GameObject boite; //Panel dans lequel ya le texte
    public Text textDialogue;
    
    
    


    void Start(){//on récupère les GameObject dont on a besoin  
        dialogueBoite = GameObject.Find("DialogueBoite");
        textDialogue=dialogueBoite.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        maladie=GameObject.Find("Game Manager").GetComponent<Maladie>();
        
    }

    public void LancementDialogueGardeForestier(){
        liste=CompterLesArbres();
        nombreChenes=liste[0];
        nombreHetres=liste[1];
        nombrePins=liste[2];
        nombreDouglas=liste[3];
        nombreBouleaux=liste[4];
        //nombreChenes,nombreHetres,nombrePins,nombreDouglas,nombreBouleaux=liste[0],liste[1],liste[2],liste[3],liste[4]; il me clc quand je fais comme ça 
        textDialogue.text="Bonjour mon grand, je suis le garde forestier. J'adore les arbres! Sur ton île, il y a " +nombreChenes.ToString() + " chênes, "
        + nombreHetres.ToString()+ " hêtres, " +nombrePins.ToString() + " pins, " +nombreDouglas.ToString() + " douglas, et " +nombreBouleaux.ToString() + "bouleaux!";
        if (maladie.maladieEnCours){
            textDialogue.text+= " Au fait, j'ai apperçu quelques " +maladie.essenceMalade+ "s malades ce matin. Débarasse-t-en le plus vite possible avant qu'ils ne disparaissent tous'!";
        }
        if (!graineDéjàDonnée){
            textDialogue.text+= " Tiens! Voilà une graine que j'ai trouvée tout à l'heure, j'espère qu'elle te sera utile! ";
            DonnerGraine();
            graineDéjàDonnée=true;
        }
        
    }

    



    public int[] CompterLesArbres(){ //cette fonction compte tout ce qui a pour tag "arbre" donc les arbustes aussi il me semble (mais pas les souches!)
        // On stocke tous les Arbres du terrain dans une liste
        GameObject[] ListeGO = GameObject.FindGameObjectsWithTag("Arbre");
        //on initialise le nombre d'arbres à 0
        nombreChenes=0;
        nombreHetres=0;
        nombrePins=0;
        nombreDouglas=0;
        nombreBouleaux=0;
        foreach(GameObject item in ListeGO){
            if (item.name.IndexOf("chene", StringComparison.OrdinalIgnoreCase) >= 0){ //normalement on compte pas les souches puisqu'elles ont pas le tag arbre (à vérifier)
                nombreChenes+=1;
            }
            if (item.name.IndexOf("hetre", StringComparison.OrdinalIgnoreCase) >= 0){
                nombreHetres+=1;
            }
            if (item.name.IndexOf("pin", StringComparison.OrdinalIgnoreCase) >= 0){
                nombrePins+=1;
            }
            if (item.name.IndexOf("douglas", StringComparison.OrdinalIgnoreCase) >= 0){
                nombreDouglas+=1;
            }
            if (item.name.IndexOf("bouleau", StringComparison.OrdinalIgnoreCase) >= 0){
                nombreBouleaux+=1;
            }

        }
        int[] source = { nombreChenes, nombreHetres, nombrePins, nombreDouglas, nombreBouleaux };
        return(source);
    }




    public void DonnerGraine(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ui_inventory = player.uiInventory;
        var rand = UnityEngine.Random.Range(0,365);
        if (rand==0){
            //On donne une graine de cerisier
            AjouterInventaire(graineCerisier, 1);
        }
        if (rand>=1 && rand<=73){
            //On donne une graine de chêne
            AjouterInventaire(graineChene, 1);
        }
        if (rand>=74 && rand<=146){
            //On donne une graine de hêtre
            AjouterInventaire(graineHetre, 1);
        }
        if (rand>=147 && rand<=219){
            //On donne une graine de pin
            AjouterInventaire(grainePin, 1);
        }
        if (rand>220 && rand<=292){
            //On donne une graine de douglas
            AjouterInventaire(graineDouglas, 1);
        }
        if (rand>=293 && rand<=365){
            //On donne une graine de bouleau
            AjouterInventaire(graineBouleau, 1);
        }
    }



    // Pour savoir si ya une maladie en cours il faut récupérer le bool maladieEnCours du script maladie et le string essenceMalade









    //fonction pour ajouter un item à l'inventaire
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

