using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironnementManager
{
    public int qualiteAir { get; set; }
    public int qualiteEau { get; set; }
    public int qualiteSol { get; set; }

    public EnvironnementManager(int air, int eau, int sol)
    {
        this.qualiteAir = air;
        this.qualiteEau = eau;
        this.qualiteSol = sol;
    }

    public void MajEnviro()
    {

    }
}
