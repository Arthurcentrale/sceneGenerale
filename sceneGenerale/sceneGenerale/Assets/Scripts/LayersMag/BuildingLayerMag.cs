using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLayerMag : MonoBehaviour
{

    private static List<string> listeNomsBat = new List<string>() { "Ferme", "Atelier", "Pecherie", "Chaumière", "Boulangerie", "MoulinVent", "MoulinEau", "BatiFerme", "BatiBoulangerie", "BatiPêcherie", "BatiChaumière", "BatiFosse", "BatiForge", "BatiMaisonPierre", "BatiGardeManger" };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void updateBatLayers()
    {
        foreach (string nom in listeNomsBat)
        {
            GameObject[] bats = GameObject.FindGameObjectsWithTag(nom);
            foreach (GameObject go in bats) {

                if (go != null)
                {
                    if (nom == "MoulinVent")
                    {
                        GameObject bat = go.transform.GetChild(1).gameObject;
                        int childCount = bat.transform.childCount;
                        int posZ = (int)bat.transform.position[2];

                        for (int i = 0; i < childCount; i++)
                        {
                            Transform child = bat.transform.GetChild(i);
                            Renderer renderer = child.GetComponent<SpriteRenderer>();

                            // Les bones n'ont pas de sprite renderer
                            if (child.name.IndexOf("bone", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                continue;
                            }

                            // On est obligé de faire ça car sinon les ailes sont derriere le moulin...
                            // On les decale donc un peu plus
                            if (child.name.IndexOf("ailes", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                renderer.sortingOrder = 894;
                            }

                            else
                            {
                                renderer.sortingOrder = 880;
                            }
                            renderer.sortingOrder -= posZ;
                        }

                    }

                    else if (nom == "Pecherie")
                    {
                        int z = (int)go.transform.position[2];
                        Renderer rend = go.transform.GetComponent<SpriteRenderer>();
                        rend.sortingOrder = 877 - z;
                    }

                    else
                    {
                        int z = (int)go.transform.position[2];
                        Renderer rend = go.transform.GetChild(0).GetComponent<SpriteRenderer>();
                        rend.sortingOrder = 877 - z;
                    }
                }
            }
        }
    }
}
