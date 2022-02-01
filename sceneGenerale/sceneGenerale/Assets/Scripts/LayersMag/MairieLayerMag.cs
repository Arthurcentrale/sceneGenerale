using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MairieLayerMag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int z = (int)this.transform.position[2];
        int abscisse = (int)Math.Cos(180 - this.transform.rotation[0]);
        Renderer rend = this.GetComponent<SpriteRenderer>();
        rend.sortingOrder = abscisse + z;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
