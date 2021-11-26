using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parcelle : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {   
        //Check for a match with the specified tag on any GameObject that triggers your GameObject
        if (collider.gameObject.tag != "Player")
        {
           Destroy(this.gameObject);
        }
    }
}