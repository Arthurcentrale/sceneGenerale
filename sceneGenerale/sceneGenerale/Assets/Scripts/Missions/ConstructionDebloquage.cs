using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionDebloquage : MonoBehaviour
{
    public GameObject[] boutons;
    public Sprite[] sprites; //images des cases de constru de bâtiments une fois débloqué
    public bool[] autorisations;

    public Sprite cadenas;


    private Color32 darkColor = new Color32(200,200,200,255);


    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (var bouton in boutons)
        {
            bouton.GetComponent<Image>().sprite = cadenas;
            bouton.GetComponent<Button>().interactable = false;
            autorisations[i] = false;
            i++;
        }

        //partie à enlever une fois jeu prêt :
        i = 0;
        foreach (var bouton in boutons)
        {
            bouton.GetComponent<Image>().sprite = sprites[i];
            bouton.GetComponent<Button>().interactable = true;
            autorisations[i] = true;
            i++;
        }
        //jusque là
    }

    public void majConstru(int i)
    {
        boutons[i].GetComponent<Image>().sprite = sprites[i];
        boutons[i].GetComponent<Button>().interactable = true;
        autorisations[i] = true;
    }

    public void majConstruTotale()
    {
        int i = 0;
        foreach(var bouton in boutons)
        {
            if (autorisations[i]) bouton.GetComponent<Button>().interactable = true;
            i++;
        }
    }
    
}
