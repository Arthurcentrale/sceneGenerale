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
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = Data.NbrBouffe.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
