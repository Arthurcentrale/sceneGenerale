using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdesacAnim : MonoBehaviour
{
    public bool visibilite = true;
    // Start is called before the first frame update


    private void Start()
    {
        visibilite = false;
        gameObject.GetComponent<Animator>().enabled = false;
    }
    public void OnBecameInvisible()
    {
        visibilite = false;
        gameObject.GetComponent<Animator>().enabled = false;
        
    }

    public void OnBecameVisible()
    {
        visibilite = true;
        gameObject.GetComponent<Animator>().enabled = true;
    }
}
