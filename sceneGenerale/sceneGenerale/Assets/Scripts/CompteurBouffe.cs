using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompteurBouffe : MonoBehaviour
{
    public int NbrBouffe;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        NbrBouffe = 0;
        text.text = NbrBouffe.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
