using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionDebloquage : MonoBehaviour
{
    public GameObject[] boutons;
    public Sprite[] sprites; //images des cases de constru de bâtiments une fois rénové


    public Sprite cadenas;

    

    private Color32 darkColor = new Color32(200,200,200,255);


    // Start is called before the first frame update
    void Start()
    {
        
        foreach (var bouton in boutons)
        {
            bouton.GetComponent<Image>().sprite = cadenas;
            bouton.GetComponent<Button>().interactable = false;
        }
    }

    public void majConstru(int i)
    {
        boutons[i].GetComponent<Image>().sprite = sprites[i];
        boutons[i].GetComponent<Button>().interactable = true;
    }
}
