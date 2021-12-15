using System;
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
    public float absorptionCo2;

    private ArbreManager arbreManager;

    private Transform dossierArbres;

    public int contamination = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        arbreManager = GameObject.Find("Game Manager").GetComponent<ArbreManager>();
        essence = DefineEssence(gameObject.name);
        etat = DefineEtat(gameObject.name);
        tempsCroissance = DefineTempsCroissance(essence);
        age = DefineAge(etat);
        absorptionCo2 = DefineAbsorptionCo2(essence);
        StartCoroutine(testMalades());
        if (age < tempsCroissance) StartCoroutine(TestOne());

        dossierArbres = GameObject.Find("Arbres").transform;
    }

    // Update is called once per frame
    
    void Update()
    {
        if (age >= tempsCroissance && etat=="arbuste" || age >= tempsCroissance && etat == "arbusteMalade") croissance();
        tuerArbreMalade();
    }
    
    
    IEnumerator TestOne()
    {
        while(age < tempsCroissance)
        {
            yield return new WaitForSeconds(5);
            age += 1;
            StartCoroutine(TestOne());
        }
        
    }
    

    string DefineEssence(string name)
    {
        if (name == "Douglas" || name == "Douglas(Clone)" || name == "Douglas Frele" || name == "Douglas Frele(Clone)" || name=="Douglas Malade" || name=="Douglas Malade(Clone)" 
            || name == "Douglas Arbuste" || name == "Douglas Arbuste Malade" || name == "Douglas Arbuste(Clone)" || name == "Douglas Arbuste Malade(Clone)")
            return "douglas";
        else if (name == "Chene" || name == "Chene(Clone)" || name == "Chene Frele" || name == "Chene Frele(Clone)" || name == "Chene Malade" || name == "Chene Malade(Clone)"
                || name == "Chene Arbuste" || name == "Chene Arbuste Malade" || name == "Chene Arbuste(Clone)" || name == "Chene Arbuste Malade(Clone)") 
                return "chene";
        else if (name == "Pin" || name == "Pin(Clone)" || name == "Pin Frele" || name == "Pin Frele(Clone)" || name == "Pin Malade" || name == "Pin Malade(Clone)"
            || name == "Pin Arbuste" || name == "Pin Arbuste Malade" || name == "Pin Arbuste(Clone)" || name == "Pin Arbuste Malade(Clone)")
            return "pin";
        else if (name == "Hetre" || name == "Hetre(Clone)" || name == "Hetre Frele" || name == "Hetre Frele(Clone)" || name == "Hetre Malade" || name == "Hetre Malade(Clone)"
            || name == "Hetre Arbuste" || name == "Hetre Arbuste Malade" || name == "Hetre Arbuste(Clone)" || name == "Hetre Arbuste Malade(Clone)")
            return "hetre";
        else  return "bouleau";
    }

    string DefineEtat(string name)
    {
        if (name == "Douglas" || name == "Douglas(Clone)" || name == "Chene" || name == "Chene(Clone)" || name == "Pin" || name == "Pin(Clone)" || name == "Hetre" || name == "Hetre(Clone)" || name == "Bouleau" || name == "Bouleau(Clone)")
        { return "adulteRobuste"; }
        else if (name == "Douglas Frele" || name =="Douglas Frele(Clone)" || name == "Chene Frele" || name == "Chene Frele(Clone)" || name == "Pin Frele" || name == "Pin Frele(Clone)" || name == "Hetre Frele" || name == "Hetre Frele(Clone)")
        { return "adulteFrele"; }
        else if (name == "Douglas Malade" || name == "Douglas Malade(Clone)" || name == "Chene Malade" || name == "Chene Malade(Clone)" || name == "Pin Malade" || name == "Pin Malade(Clone)" || name == "Hetre Malade" || name == "Hetre Malade(Clone)" || name == "Bouleau Malade" || name == "Bouleau Malade(Clone)")
        { return "adulteMalade";}
        else if (name =="Chene Arbuste(Clone)" || name == "Chene Arbuste" 
                || name == "Douglas Arbuste" || name == "Douglas Arbuste(Clone)" 
                || name == "Pin Arbuste" || name == "Pin Arbuste(Clone)" 
                || name == "Hetre Arbuste" || name == "Hetre Arbuste(Clone)" )
        { return "arbuste"; }
        else if (name == "Chene Arbuste Malade(Clone)" || name == "Chene Arbuste Malade" || name == "Douglas Arbuste Malade" || name == "Douglas Arbuste Malade(Clone)" || name == "Pin Arbuste Malade" || name == "Pin Arbuste Malade(Clone)" || name == "Hetre Arbuste Malade" || name == "Hetre Arbuste Malade(Clone)")
        { return "arbusteMalade"; }
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
        if (etat == "arbuste" || etat=="arbusteMalade") return 0;
        else return tempsCroissance;
    }

    float DefineAbsorptionCo2(string essence)
    {
        if (etat == "adulteRobuste" || etat == "adulteFrele" || etat =="adulteMalade")
        {
            if (essence == "douglas") return arbreManager.absorptionDouglas;
            else if (essence == "chene") return arbreManager.absorptionChene;
            else if (essence == "pin") return arbreManager.absorptionPin;
            else if (essence == "hetre") return arbreManager.absorptionHetre;
            else return arbreManager.absorptionBouleau;
        }
        else return 0;
        
    }

    
    void croissance()
    {
        float arbreX = transform.position.x;
        float arbreZ = transform.position.z;
        float arbreY;
        Quaternion rot;

        if (essence == "chene") 
        { 
            arbreY = arbreManager.cheneRobuste.transform.position.y; 
            rot = arbreManager.cheneRobuste.transform.rotation;
            Vector3 pos = new Vector3(arbreX, arbreY, arbreZ);
            if (etat == "arbusteMalade")
            {
                Instantiate(arbreManager.cheneMalade, pos, rot, dossierArbres);
            }
            else Instantiate(arbreManager.cheneRobuste, pos, rot, dossierArbres);
        }
        else if (essence == "pin") 
        { 
            arbreY = arbreManager.pinRobuste.transform.position.y; 
            rot = arbreManager.pinRobuste.transform.rotation;
            Vector3 pos = new Vector3(arbreX, arbreY, arbreZ);
            if (etat == "arbusteMalade")
            {
                Instantiate(arbreManager.pinMalade, pos, rot, dossierArbres);
            }
            else Instantiate(arbreManager.pinRobuste, pos, rot, dossierArbres);
        }
        else if (essence == "douglas") 
        { 
            arbreY = arbreManager.douglasRobuste.transform.position.y; 
            rot = arbreManager.douglasRobuste.transform.rotation;
            Vector3 pos = new Vector3(arbreX, arbreY, arbreZ);
            if (etat == "arbusteMalade")
            {
                Instantiate(arbreManager.douglasMalade, pos, rot, dossierArbres);
            }
            else Instantiate(arbreManager.douglasRobuste, pos, rot, dossierArbres);
        }
        else if (essence == "hetre") 
        { 
            arbreY = arbreManager.hetreRobuste.transform.position.y; 
            rot = arbreManager.hetreRobuste.transform.rotation;
            Vector3 pos = new Vector3(arbreX, arbreY, arbreZ);
            if (etat == "arbusteMalade")
            {
                Instantiate(arbreManager.hetreMalade, pos, rot, dossierArbres);
            }
            else Instantiate(arbreManager.hetreRobuste, pos, rot, dossierArbres);
        }
        else { 
            arbreY = arbreManager.bouleauRobuste.transform.position.y; 
            rot = arbreManager.bouleauRobuste.transform.rotation;
            Vector3 pos = new Vector3(arbreX, arbreY, arbreZ);
            if (etat == "arbusteMalade")
            {
                Instantiate(arbreManager.bouleauMalade, pos, rot, dossierArbres);
            }
            else Instantiate(arbreManager.bouleauRobuste, pos, rot, dossierArbres);
        }
        
        Destroy(gameObject);
        treeLayersMag.updateTreeLayers();
    }


    public void tuerArbreMalade()
    {
        if (contamination ==12)
        Destroy(gameObject);

    }

    IEnumerator testMalades()
    {
        if (name.IndexOf("Malade", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            yield return new WaitForSeconds(1);
            contamination += 1;
            StartCoroutine(testMalades());
        }
            
    }

}
