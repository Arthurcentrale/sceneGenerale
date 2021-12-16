using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueOuvrier : MonoBehaviour
{

    private Text texteBoiteDialogue;

    // Start is called before the first frame update
    void Start()
    {
        texteBoiteDialogue = GameObject.Find("Boite").transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void lancementDialogueOuvrier()
    {
        texteBoiteDialogue.text = "Salut l'ami ! Moi, c'est l'ouvrier de la communauté. Je serais heureux de t'aider à bâtir toutes sortes de structures pour que le village s'étende.";
    }
}
