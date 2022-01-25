using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueArtisan : MonoBehaviour
{

    public MissionManager missionManager;
    public GoalManager goalManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void dialogueArtisan(Text texte)
    {
        if (missionManager.nombreMissions == 2)
        {
            if (goalManager.pointsGoals == 1 || goalManager.pointsGoals == 2 )
            {
                texte.text = "Salut ! La traversée de cet océan a été infernale ! Je n’en pouvais plus ! J’ai hâte de mettre mes compétences d'artisanat au profit de la communauté !" +
                    " Il me faudrait un établi pour commencer à travailler. Et où est-ce que je loge ?";
            }
            else if (goalManager.pointsGoals == 3)
            {
                texte.text = "Les travaux avancent ? Je me les caille ici.";
            }
        }

        else texte.text = "La responsable de l'établi, ici, c'est moi ! Si tu veux des outils faits maison, certifiés matériaux 100% naturels, sans colle chimique ou autre cochonnerie, viens me voir !";
    }
}
