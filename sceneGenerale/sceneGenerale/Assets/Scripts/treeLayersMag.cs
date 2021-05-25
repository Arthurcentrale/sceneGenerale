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

        foreach (GameObject go in treeList)
        {
            int z = (int)go.transform.position[2];
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = 868 - z;
        }
    }        
}
