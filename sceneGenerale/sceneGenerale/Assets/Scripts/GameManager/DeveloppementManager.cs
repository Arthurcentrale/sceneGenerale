using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloppementManager
{
    public int progression;
    public List<GameObject> listeBatiment;
    public int navireConstruit { get; set; }

    public DeveloppementManager(int navire)
    {
        this.progression = 0;
        this.listeBatiment = new List<GameObject>();
        this.navireConstruit = navire;
    }
}
