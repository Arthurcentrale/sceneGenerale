using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Defaite : MonoBehaviour
{
    // Start is called before the first frame update
     GameObject bulles;
    public Gradient grad;
    void Start()
    {
        List<float> valeurs = new List<float> { GameManager.environnementManager.qualiteEau, GameManager.environnementManager.qualiteAir, GameManager.environnementManager.qualiteSol, GameManager.socialManager.qualiteDeVie, GameManager.developpementManager.navireConstruit };
        bulles = GameObject.Find("Bulles");
        for(int i = 0; i < 5; i++)
        {
            float x = valeurs[i];
            bulles.transform.GetChild(i).GetChild(0).transform.gameObject.GetComponent<Text>().text = x.ToString() ;
            bulles.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color(x/100);
        }


    }



    Color Color(float x)
    {
        Color color = new Color();
        color = grad.Evaluate(x);
        return color;
    }
    void restart()
    {
        //jsp;
    }
}
