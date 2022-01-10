using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueOuvrier : MonoBehaviour
{

    private Animator animator;
    [HideInInspector] public int missionAvancement = 0;
    [HideInInspector] public bool missionDialogue = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    public void dialogueOuvrier(Text texte)
    {
        if (missionAvancement == 1 && missionDialogue == true)
        {
            texte.text = "Super, ce bois! Il n’y a plus qu’à rénover le bâtiment maintenant. Tu peux t'approcher du bâtiment et entâmer les travaux. Je donnerai le coup de pouce finale !";
            missionDialogue = false;
        }
        else if (missionAvancement == 2 && missionDialogue == true)
        {
            missionDialogue = false;
        }
        else texte.text = "Toujours là pour aider, ça change pas. Fais moi construire des trucs, mais pas trop hein ! Construire trop de neuf, c'est pas non plus très écolo.";
    }

    
}
