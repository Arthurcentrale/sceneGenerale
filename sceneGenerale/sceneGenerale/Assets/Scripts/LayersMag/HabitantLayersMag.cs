using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Renderer rend = go.transform.GetChild(0).GetComponent<SpriteRenderer>();
            rend.sortingOrder = 877 - z;
        }
    }
}
