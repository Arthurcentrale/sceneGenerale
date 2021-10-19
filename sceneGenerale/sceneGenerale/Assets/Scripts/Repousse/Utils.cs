﻿using System.Collections;
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

    public static void creerGo(string tag, Vector3 position)
    {
        switch (tag)
        {
            case "Arbre":
                creerArbre(position);
                break;

            default:
                Debug.LogError("Le tag entré ne correspond à aucun type de GameObject ne pouvant repousser");
                break;
        }
    }



    /******************************************************************************************************/
    // Gestion de la création d'arbres

    private static void creerArbre(Vector3 position)
    {
        string typeArbre = choixTypeArbre();

        // On recupere le prefab correspondant à l'arbre qui va pousser
        GameObject newArbre = Resources.Load(typeArbre, typeof(GameObject)) as GameObject;

        if (newArbre == null)
        {
            Debug.LogError("Pas d'arbre trouvé... path = " + typeArbre);
            return;
        }

        newArbre.tag = "Arbre";

        Vector3 positionArbre = new Vector3(position[0], newArbre.transform.position.y, position[1]);

        // On insere le nouveau GameObject dans la scène
        Instantiate(newArbre, positionArbre, newArbre.transform.rotation, GameObject.Find("Arbres").transform);

        // Puis on change son nom
        newArbre.name = typeArbre;
    }
   

    // Fonction qui retourne aléatoirement un type d'arbre (de manière équiprobable)
    private static string choixTypeArbre()
    {
        int x = Utils.GetRandom(1, 5);

        switch (x)
        {
            case 1:
                return "Chene";

            case 2:
                return "Pin";

            case 3:
                return "Platane";

            case 4:
                return "Connifere";
    /*
            case 5:
                return "bouleau_OK";
                */

            default:
                return "Chene";
        }
    }
}
