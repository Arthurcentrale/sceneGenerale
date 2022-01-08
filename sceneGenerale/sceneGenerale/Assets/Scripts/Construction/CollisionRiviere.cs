using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRiviere : MonoBehaviour
{


    public bool autorisation = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "limitesRiverLeft" || other.name == "limitesRiverRight")
        {
            autorisation = false;
        }
    }
}
