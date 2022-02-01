using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            int abscisse = (int) Math.Cos(180 - go.transform.rotation[0]);
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = abscisse + z;
        }

        GameObject[] characterForward = GameObject.FindGameObjectsWithTag("characterForward");

        foreach (GameObject go in characterForward)
        {
            int z = (int)go.transform.position[2];
            int abscisse = (int)Math.Cos(180 - go.transform.rotation[0]);
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = abscisse + z;
        }

        GameObject[] characterBack = GameObject.FindGameObjectsWithTag("characterBack");

        foreach (GameObject go in characterBack)
        {
            int z = (int)go.transform.position[2];
            int abscisse = (int)Math.Cos(180 - go.transform.rotation[0]);
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = abscisse + z;
        }

        GameObject[] ombres = GameObject.FindGameObjectsWithTag("ombre");

        foreach (GameObject go in ombres)
        {
            int z = (int)go.transform.position[2];
            int abscisse = (int)Math.Cos(180 - go.transform.rotation[0]);
            Renderer rend = go.GetComponent<SpriteRenderer>();
            rend.sortingOrder = abscisse + z;
        }

    }
}
