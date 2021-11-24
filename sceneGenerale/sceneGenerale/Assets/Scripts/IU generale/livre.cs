using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class livre : MonoBehaviour
{
    private Animator animatorLivreFerme;
    // Start is called before the first frame update
    void Start()
    {
        animatorLivreFerme = GameObject.Find("LivreFerme").GetComponent<Animator>();
    }

    // Update is called once per frame
    public void ouvertureLivre()
    {
        animatorLivreFerme.SetTrigger("Selected");
    }
}
