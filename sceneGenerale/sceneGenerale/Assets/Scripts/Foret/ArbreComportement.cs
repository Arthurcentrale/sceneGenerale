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
        essence = gameObject.name;
        print(essence);
        
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

    

}
