using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLayerMag : MonoBehaviour
{

    private static List<string> listeNomsBat = new List<string>() { "Pecherie", "Chaumière", "Boulangerie", "MoulinVent", "MoulinEau", "BatiFerme", "BatiPêcherie", "BatiChaumière", "BatiFosse", "BatiForge", "BatiMaisonPierre", "BatiGardeManger" };
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
            Debug.Log(nom);
            GameObject bat = GameObject.FindGameObjectWithTag(nom);
            if (bat != null)
            {
                int z = (int)bat.transform.position[2];
                Renderer rend = bat.transform.GetChild(0).GetComponent<SpriteRenderer>();
                rend.sortingOrder = 868 - z;
            }
        }
    }
}
