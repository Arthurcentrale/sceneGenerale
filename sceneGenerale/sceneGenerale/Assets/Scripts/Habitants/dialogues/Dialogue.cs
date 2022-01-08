using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    private Text texteBoiteDialogue;
    private MissionManager missionManager;

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

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.Find("menuMissionsPageGauche").GetComponent<MissionManager>();
        texteBoiteDialogue = GameObject.Find("Boite").transform.GetChild(0).GetComponent<Text>();
        dialogueOuvrier = GameObject.Find("DialogueManager").GetComponent<DialogueOuvrier>();

        dialoguePecheur = GameObject.Find("pecheur_OK").GetComponent<DialoguePecheur>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void lancementDialogue()
    {
        if (ouvrier)
        {
            if (numConvOuvrier == 1)
            {
                texteBoiteDialogue.text = "Salut l'ami ! Moi, c'est l'ouvrier de la communauté. Je serais heureux de t'aider à bâtir toutes sortes de structures pour que le village s'étende. Cette île est une occasion merveilleuse de répartir de zéro dans ce monde pourri jusqu'à l'os.";
                
            }
            else if(numConvOuvrier > 1)
            {
                dialogueOuvrier.dialogueOuvrier(texteBoiteDialogue);
            }
            ouvrier = false;
            missionManager.Talk("ouvrier");
        } 
        
        else if (agriculteur && numConvAgriculteur == 1)
        {
            texteBoiteDialogue.text = "Bonjour ! Je suis agricultrice. Les plants de tomates, ça m'épate ! Ceux de bêteraves, j'en bave ! Quant au blé... je vais t'en procurer ! Sur cette île, tout pousse ! De l'autre côté de l'océan, les sols sont tellement desséchés que j'en ai la frousse ";
            agriculteur = false;
            missionManager.Talk("agriculteur");
        }
        else if (artisan && numConvArtisan == 1)
        {
            texteBoiteDialogue.text = "On a besoin d'un artisan ? ça tombe bien ! La responsable de l'établi, ici, c'est moi ! Si tu veux des outils faits maison, certifiés matériaux 100% naturels, sans colle chimique ou autre cochonnerie.. Viens me voir !";
            artisan = false;
            missionManager.Talk("artisan");
        }
        else if (pecheur)
        {
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
            missionManager.Talk("pêcheur");
        }
        
        else if (boulanger && numConvBoulanger == 1)
        {
            texteBoiteDialogue.text = " Salut mon grand ! Pain au chocolat ou chocolatine ? Je plaisante ! Ce qui compte, c'est que mon pain est fait avec de la farine sans pesticide chimique ! Enfin... Je crois ?";
            boulanger = false;
            missionManager.Talk("boulanger");
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

    }
}
