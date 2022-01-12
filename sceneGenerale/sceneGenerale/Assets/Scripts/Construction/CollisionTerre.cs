using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTerre : MonoBehaviour
{
    public bool autorisation = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.IndexOf("Trigger", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            autorisation = false;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.IndexOf("Trigger", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            autorisation = true;
            
        }

    }
}
