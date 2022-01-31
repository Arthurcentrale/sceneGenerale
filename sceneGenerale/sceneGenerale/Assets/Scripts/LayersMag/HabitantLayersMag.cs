using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HabitantLayersMag : MonoBehaviour
{
    private void Start()
    {
        updateHabitantLayers();
    }

    public static void updateHabitantLayers()
    {
        GameObject[] bats = GameObject.FindGameObjectsWithTag("habitant");
        foreach (GameObject go in bats)
        {
            int z = (int)go.transform.position[2];
            int abscisse = (int) Math.Cos(180 - go.transform.rotation[0]);
            Renderer rend = go.transform.GetChild(0).GetComponent<SpriteRenderer>();
            rend.sortingOrder = abscisse + z;
        }
        
        GameObject[] cp = GameObject.FindGameObjectsWithTag("characterPart");
        foreach (GameObject go in cp)
        {
            int z = (int)go.transform.position[2];
            int abscisse = (int)Math.Cos(180 - go.transform.rotation[0]);
            Renderer rend = go.transform.GetComponent<SpriteRenderer>();
            rend.sortingOrder = abscisse + z;
        }
    }
}
