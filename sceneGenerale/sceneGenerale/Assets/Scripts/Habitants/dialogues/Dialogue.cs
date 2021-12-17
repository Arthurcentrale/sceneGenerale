using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    private Text texteBoiteDialogue;

    public bool ouvrier;
    public bool agriculteur;
    public bool artisan;
    public bool pecheur;
    public bool boulanger;
    public bool meunier1;
    public bool meunier2;
    public bool gardeForestier;

    // Start is called before the first frame update
    void Start()
    {
        texteBoiteDialogue = GameObject.Find("Boite").transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void lancementDialogue()
    {
        if (ouvrier)
        {
            texteBoiteDialogue.text = "Salut l'ami ! Moi, c'est l'ouvrier de la communauté. Je serais heureux de t'aider à bâtir toutes sortes de structures pour que le village s'étende. Cette île est une occasion merveilleuse de répartir de zéro dans ce monde pourri jusqu'à l'os.";
            ouvrier = false;
        }
        else if (agriculteur)
        {
            texteBoiteDialogue.text = "Bonjour ! Je suis agricultrice. Les plants de tomates, ça m'épate ! Ceux de bêteraves, j'en bave ! Quant au blé... je vais t'en procurer ! Sur cette île, tout pousse ! De l'autre côté de l'océan, les sols sont tellement desséchés que j'en ai la frousse ";
            agriculteur = false;
        }
        else if (artisan)
        {
            texteBoiteDialogue.text = "On a besoin d'un artisan ? ça tombe bien ! La responsable de l'établi, ici, c'est moi ! Si tu veux des outils faits maison, certifiés matériaux 100% naturels, sans colle chimique ou autre cochonnerie.. Viens me voir !";
            artisan = false;
        }
        else if (pecheur)
        {
            texteBoiteDialogue.text = "Oh, bonjour jeune homme. Tu veux une carpe ? De la truite ? Ou un beau brochet ? J'aime passer du temps près de la rivière, son calme me relaxe. Je n'avais pas vu une eau aussi claire depuis.. des années. ";
            artisan = false;
        }
        else if (boulanger)
        {
            texteBoiteDialogue.text = " Salut mon grand ! Pain au chocolat ou chocolatine ? Je plaisante ! Ce qui compte, c'est que mon pain est fait avec de la farine sans pesticides chimiques ! Enfin... Je crois ?";
            artisan = false;
        }

    }

    public void choixHabitant(string nomPanel)
    {
        if (nomPanel.IndexOf("Ouvrier", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            ouvrier = true;
        }
        else if (nomPanel.IndexOf("Agriculteur", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            agriculteur = true;
        }
        else if (nomPanel.IndexOf("Artisan", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            artisan = true;
        }
        else if (nomPanel.IndexOf("Pecheur", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            pecheur = true;
        }
        else if (nomPanel.IndexOf("Boulanger", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            boulanger = true;
        }

    }
}
