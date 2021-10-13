using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeLayersMag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        updateTreeLayers();
    }

    public static void updateTreeLayers()
    {
        GameObject[] treeList = GameObject.FindGameObjectsWithTag("treePart");

        if (treeList.Length == 0)
            return;

        foreach (GameObject go in treeList)
        {
            int z = (int)go.transform.position[2];
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = 868 - z;
        }

        GameObject[] treeForward = GameObject.FindGameObjectsWithTag("treeForward");

        foreach (GameObject go in treeForward)
        {
            int z = (int)go.transform.position[2];
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = 868 - z;
        }

        GameObject[] treeBack = GameObject.FindGameObjectsWithTag("treeBack");

        foreach (GameObject go in treeBack)
        {
            int z = (int)go.transform.position[2];
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = 868 - z;
        }

        GameObject[] ombres = GameObject.FindGameObjectsWithTag("ombre");

        foreach (GameObject go in ombres)
        {
            int z = (int)go.transform.position[2];
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = 868 - z-50;
        }
    }        
}
