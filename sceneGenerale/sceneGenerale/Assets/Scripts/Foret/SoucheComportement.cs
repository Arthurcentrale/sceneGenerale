using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoucheComportement : MonoBehaviour
{
    public int tempsCroissance;
    public int age = 0;
    private ArbreManager arbreManager;

    // Start is called before the first frame update
    void Start()
    {
        arbreManager = GameObject.Find("Game Manager").GetComponent<ArbreManager>();
        tempsCroissance = DefineTempsCroissance(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int DefineTempsCroissance(string name)
    {
        if (name == "Souche Douglas" || name == "Souche Douglas Malade" || name == "Souche Douglas(Copie)" || name == "Souche Douglas Malade(Copie)") return arbreManager.tempsCroissanceDouglas/2;
        else if (name == "Souche Chene" || name == "Souche Chene Malade" || name == "Souche Chene(Copie)" || name == "Souche Chene Malade(Copie)") return arbreManager.tempsCroissanceChene/2;
        else if (name == "Souche Pin" || name == "Souche Pin Malade" || name == "Souche Pin(Copie)" || name == "Souche Pin Malade(Copie)") return arbreManager.tempsCroissancePin/2;
        else if (name == "Souche Hetre" || name == "Souche Hetre Malade" || name == "Souche Hetre(Copie)" || name == "Souche Hetre Malade(Copie)") return arbreManager.tempsCroissanceHetre/2;
        else return arbreManager.tempsCroissanceBouleau/2;

    }
}
