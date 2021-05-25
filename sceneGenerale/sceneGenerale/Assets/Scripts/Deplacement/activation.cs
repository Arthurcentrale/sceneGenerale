using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activation : MonoBehaviour
{
    public Recolte recolte;
    public Deplacement deplacement;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        recolte = recolte.GetComponent<Recolte>();
        deplacement = deplacement.GetComponent<Deplacement>();
        animator = animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(recolte.IsCraftArbre || recolte.IsCraftFleur || recolte.IsCraftRoche)
        {
            deplacement.canmove = false ;
        }
        else
        {
            deplacement.canmove = true;
        }
    }
}
