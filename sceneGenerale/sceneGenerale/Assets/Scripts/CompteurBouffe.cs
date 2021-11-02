using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompteurBouffe : MonoBehaviour
{
    public static class Data
    {
        public static int NbrBouffe;
    }

    //public int NbrBouffe;
    public Text CBouffe;
    public Text CompteurVariete;
    public Text CompteurQualiteEau;
    // Start is called before the first frame update
    void Start()
    {
        Data.NbrBouffe = 0;
        CBouffe.text = Data.NbrBouffe.ToString();
        CompteurVariete.text = GameManager.socialManager.nombreAlimentsDifferents.ToString();
        CompteurQualiteEau.text = GameManager.environnementManager.qualiteEau.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
