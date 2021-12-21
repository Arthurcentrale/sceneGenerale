using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueOuvrier : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    public void dialogueOuvrier(Text texte)
    {
        texte.text = "Toujours là pour aider, ça change pas. Fais moi construire des trucs, mais pas trop hein ! Construire trop de neuf, c'est pas non plus très écolo.";
    }

    
}
