using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbreManager : MonoBehaviour
{

    public enum etatEnum
    {
        adulteRobuste,
        adulteFrele,
        arbuste,
        souche,

    }

    public enum essenceEnum
    {
        chene,
        hetre,
        pin,
        douglas,
        bouleau,
    }

    //les arbres robustes
    public GameObject cheneRobuste;
    public GameObject hetreRobuste;
    public GameObject douglasRobuste;
    public GameObject pinRobuste;
    public GameObject bouleauRobuste;

    //les arbres frêles
    public GameObject cheneFrele;
    public GameObject hetreFrele;
    public GameObject douglasFrele;
    public GameObject pinFrele;
    public GameObject bouleauFrele;

    //Temps de croissance
    public int tempsCroissanceChene = 6;
    public int tempsCroissanceDouglas = 4;
    public int tempsCroissancePin = 5;
    public int tempsCroissanceHetre = 4;
    public int tempsCroissanceBouleau = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
        
}
