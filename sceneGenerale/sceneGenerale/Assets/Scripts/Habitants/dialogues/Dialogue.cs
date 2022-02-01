using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Player player; //on veut recuperer le script inventaire sur le joueur
    public RecetteCraft hache;

    private Text texteBoiteDialogue;
    private MissionManager missionManager;

    public GFForet gfforet;

    //booléens pour savoir qui parle
    public bool ouvrier;
    public bool agriculteur;
    public bool artisan;
    public bool pecheur;
    public bool boulanger;
    public bool meunier1;
    public bool meunier2;
    public bool gardeForestier;

    //nombre de fois qu'on a parlé à ce pnj
    public int numConvOuvrier;
    public int numConvAgriculteur;
    public int numConvArtisan;
    public int numConvPecheur;
    public int numConvBoulanger;
    public int numConvMenuer1;
    public int numConvMeunier2;
    public int numConvGardeForet;

    //objets habitants avec leurs components
    private GameObject ouvrierHabitant;
    private GameObject agriculteurHabitant;
    private GameObject artisanHabitant;
    
    private GameObject meunier1Habitant;
    private GameObject meunier2Habitant;
    private GameObject boulangerHabitant;
    private GameObject gardeForestierHabitant;

    //scripts de dialogues spécifiques de chaque PNJ
    private DialogueOuvrier dialogueOuvrier;
    private DialoguePecheur dialoguePecheur;
    private DialogueArtisan dialogueArtisan;
    private DialogueAgriculteur dialogueAgriculteur;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        missionManager = GameObject.Find("menuMissionsPageGauche").GetComponent<MissionManager>();
        texteBoiteDialogue = GameObject.Find("Boite").transform.GetChild(0).GetComponent<Text>();
        dialogueOuvrier = GameObject.Find("DialogueManager").GetComponent<DialogueOuvrier>();
        dialoguePecheur = GameObject.Find("DialogueManager").GetComponent<DialoguePecheur>();
        dialogueArtisan = GameObject.Find("DialogueManager").GetComponent<DialogueArtisan>();
        dialogueAgriculteur = GameObject.Find("DialogueManager").GetComponent<DialogueAgriculteur>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void lancementDialogue()
    {
        if (ouvrier)
        {
            missionManager.Talk("Julien");
            if (numConvOuvrier == 1)
            {
                texteBoiteDialogue.text = " Tu en as mis du temps ! Cette île est vraiment merveilleuse, je n'avais pas vu autant d'arbres différents depuis... des années ! Regarde ce que j’ai trouvé au milieu de l’île. On pourrait en faire le centre du village si on la retape un peu. Prends cette hache et apporte moi un peu de bois.";
                foreach (ItemAmount ItemAmount in hache.Results)
                {
                    player.inventory.AddItem(ItemAmount);
                    ItemAmount.durability = 1000; //hache indestructible du début de jeu
                }
            }
            else if (numConvOuvrier > 1)
            {
                dialogueOuvrier.dialogueOuvrier(texteBoiteDialogue);
            }
            ouvrier = false;

        }

        else if (agriculteur) 
        {
            missionManager.Talk("Josephine");
            texteBoiteDialogue.text = "Bonjour ! Je suis agricultrice. Les plants de tomates, ça m'épate ! Ceux de bêteraves, j'en bave ! Quant au blé... je vais t'en procurer ! Sur cette île, tout pousse ! De l'autre côté de l'océan, les sols sont tellement desséchés que j'en ai la frousse ";
            agriculteur = false;
        }

        else if (artisan)
        {
            missionManager.Talk("Lisa");
            dialogueArtisan.dialogueArtisan(texteBoiteDialogue);
            artisan = false;
        }
            
        else if (pecheur)
        {
            missionManager.Talk("Gaetan");
            dialoguePecheur.animParler();
            if (numConvPecheur == 1)
            {
                texteBoiteDialogue.text = "Oh, bonjour jeune homme. Tu veux une carpe ? De la truite ? Ou un beau brochet ? J'aime passer du temps près de la rivière, son calme me relaxe. Je n'avais pas vu une eau aussi claire depuis.. des années. ";
            }
            else if (numConvPecheur > 1)
            {
                dialoguePecheur.dialoguePecheur(texteBoiteDialogue);
            }
            pecheur = false;
            
        }
        
        else if (boulanger && numConvBoulanger == 1)
        {
            missionManager.Talk("Nicolas");
            texteBoiteDialogue.text = " Salut mon grand ! Pain au chocolat ou chocolatine ? Je plaisante ! Ce qui compte, c'est que mon pain est fait avec de la farine sans pesticide chimique ! Enfin... Je crois ?";
            boulanger = false;
            
        }

        else if (gardeForestier){
            print("oui");
            gfforet.LancementDialogueGardeForestier();
            gardeForestier=false;
        }

    }

    public void choixHabitant(string nomPanel)
    {
        if (nomPanel.IndexOf("Ouvrier", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            ouvrier = true;
            numConvOuvrier += 1;
        }
        else if (nomPanel.IndexOf("Agriculteur", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            agriculteur = true;
            numConvAgriculteur += 1;
        }
        else if (nomPanel.IndexOf("Artisan", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            artisan = true;
            numConvArtisan += 1;
        }
        else if (nomPanel.IndexOf("Pecheur", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            pecheur = true;
            numConvPecheur += 1;
        }
        else if (nomPanel.IndexOf("Boulanger", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            boulanger = true;
            numConvBoulanger += 1;
        }
        else if (nomPanel.IndexOf("GardeForestier", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            gardeForestier = true;
            print("pipi");
            //numConvBoulanger += 1;
        }

    }
}
