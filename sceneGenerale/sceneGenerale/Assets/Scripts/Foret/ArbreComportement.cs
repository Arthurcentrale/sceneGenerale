using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbreComportement : MonoBehaviour
{
    public GameObject arbre;

    public string etat;
    public string essence;
    public ArrayList production;
    public int age = 0;
    public int tempsCroissance;
    private ArbreManager arbreManager;


    // Start is called before the first frame update
    void Start()
    {
        
        arbreManager = GameObject.Find("Game Manager").GetComponent<ArbreManager>();
        essence = DefineEssence(gameObject.name);
        etat = DefineEtat(gameObject.name);
        
        //StartCoroutine(TestOne());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    IEnumerator TestOne()
    {
        age += 1;
        yield return new WaitForSeconds(2);
        StartCoroutine(TestOne());
    }
    */

    string DefineEssence(string name)
    {
        if (name == "Douglas" || name == "Douglas(Clone)" || name == "Douglas Frele" || name == "Douglas Frele(Clone)") return "douglas";
        else if (name == "Chene" || name == "Chene(Clone)" || name == "Chene Frele" || name == "Chene Frele(Clone)" || name == "Chene Arbuste" || name == "Chene Arbuste Frele" || name == "Chene Arbuste(Clone)" || name == "Chene Arbuste Frele(Clone)") return "chene";
        else if (name == "Pin" || name == "Pin(Clone)" || name == "Pin Frele" || name == "Pin Frele(Clone)") return "pin";
        else if (name == "Hetre" || name == "Hetre(Clone)" || name == "Hetre Frele" || name == "Hetre Frele(Clone)") return "hetre";
        else  return "bouleau";
    }

    string DefineEtat(string name)
    {
        if (name == "Douglas" || name == "Douglas(Clone)" || name == "Chene" || name == "Chene(Clone)" || name == "Pin" || name == "Pin(Clone)" || name == "Hetre" || name == "Hetre(Clone)" || name == "Bouleau" || name == "Bouleau(Clone)")
        { return "adulteRobuste"; }
        else if (name == "Douglas Frele" || name =="Douglas Frele(Clone)" || name == "Chene Frele" || name == "Chene Frele(Clone)" || name == "Pin Frele" || name == "Pin Frele(Clone)" || name == "Hetre Frele" || name == "Hetre Frele(Clone)")
        { return "adulteFrele"; }
        else if (name =="Chene Arbuste(Clone)" || name =="Chene Arbuste Frele(Clone)" ||name == "Chene Arbuste" || name == "Chene Arbuste Frele")
        { return "arbuste"; }
        else return "non defini";
    }

}
