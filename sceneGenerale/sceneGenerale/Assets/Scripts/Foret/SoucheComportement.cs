using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoucheComportement : MonoBehaviour
{
    public int tempsCroissance;
    public int age = 0;
    private ArbreManager arbreManager;
    public string essence;
    public bool malade = false;
    private Transform dossierArbres;

    

    // Start is called before the first frame update
    void Start()
    {
        arbreManager = GameObject.Find("Game Manager").GetComponent<ArbreManager>();
        tempsCroissance = DefineTempsCroissance(gameObject.name);
        dossierArbres = GameObject.Find("Arbres").transform;
        StartCoroutine(TestOne());
        if (gameObject.name =="Souche Douglas Malade(Clone)" || gameObject.name == "Souche Chene Malade(Clone)" || gameObject.name == "Souche Pin Malade(Clone)" || gameObject.name == "Souche Bouleau Malade(Clone)" || gameObject.name == "Souche Hetre Malade(Clone)")
        {
            malade = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (age >= tempsCroissance && malade == false) croissanceSouche();
    }


    IEnumerator TestOne()
    {
        if (age < tempsCroissance)
        {
            yield return new WaitForSeconds(5);
            age += 1;
            StartCoroutine(TestOne());
        }

    }

    int DefineTempsCroissance(string name)
    {
        if (name == "Souche Douglas" || name == "Souche Douglas Malade" || name == "Souche Douglas(Clone)" || name == "Souche Douglas Malade(Clone)")
        {
            essence = "douglas";
            return arbreManager.tempsCroissanceDouglas / 2;
        }
        else if (name == "Souche Chene" || name == "Souche Chene Malade" || name == "Souche Chene(Clone)" || name == "Souche Chene Malade(Clone)")
        {
            essence = "chene";
            return arbreManager.tempsCroissanceChene / 2;
        }
        else if (name == "Souche Pin" || name == "Souche Pin Malade" || name == "Souche Pin(Clone)" || name == "Souche Pin Malade(Clone)")
        {
            essence = "pin";
            return arbreManager.tempsCroissancePin / 2;
        }
        else if (name == "Souche Hetre" || name == "Souche Hetre Malade" || name == "Souche Hetre(Clone)" || name == "Souche Hetre Malade(Clone)")
        {
            essence = "hetre";
            return arbreManager.tempsCroissanceHetre / 2;
        }
        else return arbreManager.tempsCroissanceBouleau / 2;

        }


    void croissanceSouche()
    {
        float arbreX = transform.position.x;
        float arbreZ = transform.position.z;
        float arbreY;
        Quaternion rot;

        if (essence == "chene")
        {
            arbreY = arbreManager.cheneRobuste.transform.position.y;
            rot = arbreManager.cheneRobuste.transform.rotation;
            Vector3 pos = new Vector3(arbreX, arbreY, arbreZ +5);
            Instantiate(arbreManager.cheneFrele, pos, rot, dossierArbres);
        }
        else if (essence == "pin")
        {
            arbreY = arbreManager.pinRobuste.transform.position.y;
            rot = arbreManager.pinRobuste.transform.rotation;
            Vector3 pos = new Vector3(arbreX, arbreY, arbreZ +5);
            Instantiate(arbreManager.pinFrele, pos, rot, dossierArbres);
        }
        else if (essence == "douglas")
        {
            arbreY = arbreManager.douglasRobuste.transform.position.y;
            rot = arbreManager.douglasRobuste.transform.rotation;
            Vector3 pos = new Vector3(arbreX, arbreY, arbreZ +5);
            Instantiate(arbreManager.douglasFrele, pos, rot, dossierArbres);
        }
        else if (essence == "hetre")
        {
            arbreY = arbreManager.hetreRobuste.transform.position.y;
            rot = arbreManager.hetreRobuste.transform.rotation;
            Vector3 pos = new Vector3(arbreX, arbreY, arbreZ +5);
            Instantiate(arbreManager.hetreFrele, pos, rot, dossierArbres);
        }
        else
        {
            arbreY = arbreManager.bouleauRobuste.transform.position.y;
            rot = arbreManager.bouleauRobuste.transform.rotation;
            Vector3 pos = new Vector3(arbreX, arbreY, arbreZ +5);
            Instantiate(arbreManager.bouleauFrele, pos, rot, dossierArbres);
        }

        Destroy(gameObject);
    }
}
