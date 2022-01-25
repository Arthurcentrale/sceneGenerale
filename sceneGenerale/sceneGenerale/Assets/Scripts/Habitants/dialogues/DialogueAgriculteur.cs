using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueAgriculteur : MonoBehaviour
{

    public MissionManager missionManager;
    public GoalManager goalManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void dialogueAgriculteur(Text texte)
    {
        texte.text = "yoho";
    }

    
}
