using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // Fonction qui permet de créer des nombres aléatoires
    private static System.Random rnd = new System.Random();

    public static int GetRandom(int min, int max)
    {
        return rnd.Next(min, max);
    }

    public static void creerGo(string tag, Vector3 position, GameObject[] listeGo)
    {
        switch (tag)
        {
            case "Arbre":
                creerArbre(position, listeGo);
                break;

            default:
                Debug.LogError("Le tag entré ne correspond à aucun type de GameObject ne pouvant repousser");
                break;
        }
    }



    /******************************************************************************************************/
    // Gestion de la création d'arbres

    private static void creerArbre(Vector3 position, GameObject[] listeArbres)
    {
        string typeArbre = choixTypeArbre(separationEssencesArbres(listeArbres));

        // On recupere le prefab correspondant à l'arbre qui va pousser
        GameObject newArbre = Resources.Load(typeArbre, typeof(GameObject)) as GameObject;

        if (newArbre == null)
        {
            Debug.LogError("Pbm utils (repousse) : l'objet " + typeArbre + "n'existe pas");
            return;
        }

        newArbre.tag = "Arbre";

        Vector3 positionArbre = new Vector3(position[0], newArbre.transform.position.y, position[1]);

        // On insere le nouveau GameObject dans la scène
        Instantiate(newArbre, positionArbre, newArbre.transform.rotation, GameObject.Find("Arbres").transform);

        // Puis on change son nom
        newArbre.name = typeArbre;
    }

    private static int[] separationEssencesArbres(GameObject[] liste)
    {
        int[] res = new int[5] { 0, 0, 0, 0, 0 };

        foreach (GameObject arbre in liste)
        {
            string name = arbre.name;

            if (name.IndexOf("chene", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                res[0]++;
            }

            else if (name.IndexOf("douglas", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                res[1]++;
            }

            else if (name.IndexOf("pin", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                res[2]++;
            }

            else if (name.IndexOf("bouleau", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                res[3]++;
            }

            else
            {
                res[4]++;
            }
        }
        return res;
    }

    // Fonction qui retourne aléatoirement un type d'arbre (de manière équiprobable)
    private static string choixTypeArbre(int[] listeParEssence)
    {
        int nbChenes = listeParEssence[0];
        int nbDouglas = listeParEssence[1];
        int nbPins = listeParEssence[2];
        int nbBouleau = listeParEssence[3];
        int nbHetres = listeParEssence[4];

        int nbTotalArbres = nbChenes + nbDouglas + nbPins + nbBouleau + nbHetres;

        int x = Utils.GetRandom(0, nbTotalArbres);

        if (x < nbChenes)
        {
            return "Chene Arbuste";
        }

        else if (x < nbDouglas + nbChenes && x >= nbChenes)
        {
            return "Douglas Arbuste";
        }

        else if (x < nbDouglas + nbChenes + nbPins && x >= nbChenes + nbDouglas)
        {
            return "Pin Arbuste";
        }

        else if (x < nbDouglas + nbChenes + nbPins + nbBouleau && x >= nbDouglas + nbChenes + nbPins)
        {
            return "Bouleau Arbuste";
        }

        else
        {
            return "Hetre Arbuste";
        }
    }
}
