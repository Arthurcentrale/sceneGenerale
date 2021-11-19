using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbreManager : MonoBehaviour
{

    public enum etatEnum
    {
        adulteRobuste,
        adulteFrele,
        adulteMalade,
        arbusteMalade,
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

    //les arbres malades
    public GameObject cheneMalade;
    public GameObject hetreMalade;
    public GameObject douglasMalade;
    public GameObject pinMalade;
    public GameObject bouleauMalade;

    //Temps de croissance
    public int tempsCroissanceChene = 6;
    public int tempsCroissanceDouglas = 4;
    public int tempsCroissancePin = 5;
    public int tempsCroissanceHetre = 4;
    public int tempsCroissanceBouleau = 3;

    //absorption de Co2
    public float absorptionChene = 0.2f;
    public float absorptionHetre = 0.2f;
    public float absorptionPin = 0.1f;
    public float absorptionDouglas = 0.1f;
    public float absorptionBouleau = 0.2f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
        
}
