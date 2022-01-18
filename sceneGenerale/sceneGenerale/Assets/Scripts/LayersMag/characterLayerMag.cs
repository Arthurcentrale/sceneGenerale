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

        GameObject[] characterForward = GameObject.FindGameObjectsWithTag("characterForward");

        foreach (GameObject go in characterForward)
        {
            int z = (int)go.transform.position[2];
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = 868 - z;
        }

        GameObject[] characterBack = GameObject.FindGameObjectsWithTag("characterBack");

        foreach (GameObject go in characterBack)
        {
            int z = (int)go.transform.position[2];
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = 868 - z - 3;
        }

        GameObject[] ombres = GameObject.FindGameObjectsWithTag("ombre");

        foreach (GameObject go in ombres)
        {
            int z = (int)go.transform.position[2];
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = 868 - z - 50;
        }

    }
}
