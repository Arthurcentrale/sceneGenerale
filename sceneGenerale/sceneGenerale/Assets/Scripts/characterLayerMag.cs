using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterLayerMag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        updateCharacterLayer();
    }

    // Update is called once per frame
    void Update()
    {
        updateCharacterLayer();
    }

    public static void updateCharacterLayer()
    {
        GameObject[] characterParts = GameObject.FindGameObjectsWithTag("characterPart");

        foreach (GameObject go in characterParts)
        {
            int z = (int)go.transform.position[2];
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = 868 - z;
        }
    }
}
