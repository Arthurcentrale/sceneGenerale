using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maladietest : MonoBehaviour
{
    private GameObject premierArbreMalade;
    // Start is called before the first frame update
    void Start()
    {
        premierArbreMalade=GameObject.Find("Chene Malade(Clone)");
        if (premierArbreMalade==null){
            print("caca");
        }
        
    }

    
}
