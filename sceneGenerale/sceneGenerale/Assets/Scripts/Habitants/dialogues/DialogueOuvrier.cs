using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueOuvrier : MonoBehaviour
{
    [HideInInspector] public Player player;
    private Animator animator;
    public MissionManager missionManager;
    public GoalManager goalManager;

    //items donnés par l'ouvrier
    public Item paille;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();
    }


    public void dialogueOuvrier(Text texte)
    {
        if (missionManager.nombreMissions == 1)
        {
            if (goalManager.pointsGoals == 1)
            {
                float i = Random.Range(0, 1);
                if (i<0.5f) texte.text = "Du bois, mon ami ! Cette mairie ne va pas se rénover toute seule non plus ! 10 morceaux bien solides devraient suffir. Ne me ramène pas de la brindille de bois frêle, ça n'a aucune valeur en construction.";
                else texte.text = "Je peux rien construire sans le bois. Utilise ta hache pour couper quelques arbres.";
            }
            else if (goalManager.pointsGoals == 2)
            {
                texte.text = "Parfait ! Il n’y a plus qu’à rénover le bâtiment maintenant. Va jeter un coup d’oeil toi-même pour orchestrer les travaux et déposer les matériaux près de cette ruine.";
            }
        }
        else if (missionManager.nombreMissions == 2)
        {
            if (goalManager.pointsGoals == 1)
            {
                texte.text = "Super on pourra réunir toutes les informations dont on a besoin dans la mairie. Maintenant il nous reste plus qu’à accueillir " +
                    "les nouveaux arrivants du mieux qu’on peut ! Il devrait déjà en être arrivé au ponton. Va les accueillir!";
            }
            if (goalManager.pointsGoals == 3)
            {
                player.inventory.AddItem(new ItemAmount(Item: paille, Amount: 1));
                texte.text = "Pour construire une chaumière tu auras besoin de paille, ça tombe bien il m’en reste encore un peu, prend ça ! Pour le reste des matériaux tu vas devoir te débrouiller en revanche, bonne chance !";
            }
        }

        else texte.text = "Toujours là pour aider, ça change pas. Fais moi construire des trucs, mais pas trop hein ! Construire trop de neuf, c'est pas non plus très écolo.";
    }
        
        
}

    

