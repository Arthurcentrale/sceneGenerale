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
            texteBoiteDialogue.text = "Salut l'ami ! Moi, c'est l'ouvrier de la communauté. Je serais heureux de t'aider à bâtir toutes sortes de structures pour que le village s'étende.";
            ouvrier = false;
        }
        else if (agriculteur)
        {
            texteBoiteDialogue.text = "Bonjour ! Je suis agricultrice. Les plants de tomates, ça m'épate ! Ceux de bêteraves, j'en bave ! Auqnt qu blé... je vais t'en procurer ! ";
            agriculteur = false;
        }
        else if (artisan)
        {
            texteBoiteDialogue.text = "On a besoin d'un artisan ? ça tombe bien ! La responsable de l'établi, ici, c'est moi ! ";
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

    }
}
