using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MairieLayerMag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int z = (int)this.transform.position[2];
        Renderer rend = this.GetComponent<SpriteRenderer>();
        rend.sortingOrder = 868 - z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
