using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdesacAnim : MonoBehaviour
{
    public bool visibilite = true;
    // Start is called before the first frame update
    public void OnBecameInvisible()
    {
        visibilite = false;
        //gameObject.SetActive(false);
        Debug.Log("invisible");
    }

    public void OnBecameVisible()
    {
        visibilite = true;
        Debug.Log("visible");
        //gameObject.SetActive(true);
    }
}
