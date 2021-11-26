using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironnementManager
{
    public int qualiteAir { get; set; }
    public float qualiteEau { get; set; }
    public float qualiteSol { get; set; }

    public EnvironnementManager(int air, float eau, float sol)
    {
        this.qualiteAir = air;
        this.qualiteEau = eau;
        this.qualiteSol = sol;
    }

    public void MajEnviro()
    {

    }
}
