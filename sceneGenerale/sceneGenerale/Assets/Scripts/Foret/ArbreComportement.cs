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
        tempsCroissance = DefineTempsCroissance(essence);
        age = DefineAge(etat);
        
        if (age < tempsCroissance) StartCoroutine(TestOne());
    }

    // Update is called once per frame
    
    void Update()
    {
        if (age >= tempsCroissance && etat=="arbuste") croissance();
        
    }
    
    
    IEnumerator TestOne()
    {
        while(age < tempsCroissance)
        {
            yield return new WaitForSeconds(3);
            age += 1;
            StartCoroutine(TestOne());
        }
        
    }
    

    string DefineEssence(string name)
    {
        if (name == "Douglas" || name == "Douglas(Clone)" || name == "Douglas Frele" || name == "Douglas Frele(Clone)") 
                return "douglas";
        else if (name == "Chene" || name == "Chene(Clone)" || name == "Chene Frele" || name == "Chene Frele(Clone)" 
                || name == "Chene Arbuste" || name == "Chene Arbuste Frele" || name == "Chene Arbuste(Clone)" || name == "Chene Arbuste Frele(Clone)") 
                return "chene";
        else if (name == "Pin" || name == "Pin(Clone)" || name == "Pin Frele" || name == "Pin Frele(Clone)") 
                return "pin";
        else if (name == "Hetre" || name == "Hetre(Clone)" || name == "Hetre Frele" || name == "Hetre Frele(Clone)") 
                return "hetre";
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

    int DefineTempsCroissance(string essence)
    {
        if (essence == "douglas") return arbreManager.tempsCroissanceDouglas;
        else if (essence == "chene") return arbreManager.tempsCroissanceChene;
        else if (essence == "pin") return arbreManager.tempsCroissancePin;
        else if (essence == "hetre") return arbreManager.tempsCroissanceHetre;
        else return arbreManager.tempsCroissanceBouleau;

    }

    int DefineAge(string etat)
    {
        if (etat == "arbuste") return 0;
        else return tempsCroissance;
    }

    
    void croissance()
    {
        float arbreX = transform.position.x;
        float arbreZ = transform.position.z;
        float arbreY;
        Quaternion rot;

        if (essence == "chene") { arbreY = arbreManager.cheneRobuste.transform.position.y; rot = arbreManager.cheneRobuste.transform.rotation; }
        else if (essence == "pin") { arbreY = arbreManager.pinRobuste.transform.position.y; rot = arbreManager.pinRobuste.transform.rotation; }
        else if (essence == "douglas") { arbreY = arbreManager.douglasRobuste.transform.position.y; rot = arbreManager.douglasRobuste.transform.rotation; }
        else if (essence == "hetre") { arbreY = arbreManager.hetreRobuste.transform.position.y; rot = arbreManager.hetreRobuste.transform.rotation; }
        else { arbreY = arbreManager.bouleauRobuste.transform.position.y; rot = arbreManager.bouleauRobuste.transform.rotation; }

        Vector3 pos = new Vector3(arbreX,arbreY,arbreZ);
        Instantiate(arbreManager.cheneRobuste,pos,rot);
        Destroy(gameObject);
    }
    
}
