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
        string path = "templatesArbre/" + typeArbre;

        // On recupere le prefab correspondant à l'arbre qui va pousser
        GameObject newArbre = Resources.Load(path, typeof(GameObject)) as GameObject;

        if (newArbre == null)
        {
            Debug.LogError("Pas d'arbre trouvé... path = " + path);
            return;
        }

        newArbre.tag = "Arbre";

        // On insere le nouveau GameObject dans la scène
        Instantiate(newArbre);

        // On créé le vecteur position (complet i.e. à 3 coordonnées) du nouveau GameObject
        float Ypos = newArbre.transform.position[1];
        Vector3 GoPosition = new Vector3(position[0], Ypos, position[1]);

        // On le déplace sur la scène à cette position 
        newArbre.transform.position = GoPosition;

        // Puis on change son nom
        newArbre.name = typeArbre;
    }
   

    // Fonction qui retourne aléatoirement un type d'arbre (de manière équiprobable)
    private static string choixTypeArbre()
    {
        int x = Utils.GetRandom(1, 6);

        switch (x)
        {
            case 1:
                return "Chene";

            case 2:
                return "Pin";

            /*case 3:
                return "Platane";

            /*
        case 4:
            return "connifere_OK";

        case 5:
            return "bouleau_OK";
            */

            default:
                return "Chene";
        }
    }
}
