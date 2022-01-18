using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class scriptRepousse : MonoBehaviour
{
    public int debug = 0;

    // les deux prochaines variables sont en public pour l'instant (pour les tests) mais seront privées à terme
    private int nbArbresSurTerrain = 0;
    private int nbArbres; // correspond au nombre d'arbres qui pousseront à minuit
    public int nbArbreMax;

    // La variable suivante représente la distance entre un go est la premiere zone constructible 
    public int goMarge;

    // La variable suivante décrit la distance max entre deux arbres qui vont être créé
    // Ceci permet de faire des forets plus ou moins dense
    public int distanceDensite;

    // Cette derniere variable décrit le tag des objets ciblés par ce script
    public string goTag = "Arbre";


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (debug == 1)
        {
            majMinuit();
            debug = 2;
        }
    }

    public void majMinuit()
    {
        // On stocke tous les Arbres du terrain dans une liste
        GameObject[] ListeGO = GameObject.FindGameObjectsWithTag("Arbre");

        // On compte combien il y en a
        GameObject[] listeArbresSurTerrain = getArbresSurTerrain(ListeGO);
        nbArbresSurTerrain = listeArbresSurTerrain.Length;

        nbArbres = fonctionRepousse(nbArbresSurTerrain);

        fairePousserArbre(nbArbres, listeArbresSurTerrain);

        nbArbresSurTerrain = 0;

        // On met à jour les sorting order des arbres
        treeLayersMag.updateTreeLayers();
    }

    private GameObject[] getArbresSurTerrain(GameObject[] liste)
    {
        int len = liste.Length;
        GameObject[] resTmp = new GameObject[len];

        int index = 0;
        foreach (GameObject arbre in liste)
        {
            string name = arbre.name;
            if (name.IndexOf("souche", StringComparison.OrdinalIgnoreCase) >= 0 || name.IndexOf("arbuste", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                continue;
            }

            resTmp[index] = arbre;
            index++;
        }

        GameObject[] res = new GameObject[index];
        for (int i = 0; i < index; i++)
        {
            res[i] = resTmp[i];
        }
        return res;
    }


    // Fonction qui prend en argument le nombre d'arbre sur le terrain et renvoie le nombre d'arbre qui pousseront à minuit de la prochaine nuit
    private int fonctionRepousse(int nbArbresSurLeTerrain)
    {
        if (nbArbresSurLeTerrain >= nbArbreMax)
            return 0;

        else
        {
            if (nbArbresSurLeTerrain >= 1 && nbArbresSurLeTerrain <= 25)
            {
                return 1;
            }

            else if (nbArbresSurLeTerrain >= 26 && nbArbresSurLeTerrain <= 50)
            {
                return 2;
            }

            else if (nbArbresSurLeTerrain >= 51 && nbArbresSurLeTerrain <= 75)
            {
                return 3;
            }

            else
                return 0;
        }
    }

    // Fonction qui fait pousser un arbre de manière cohérente par rapport à la forêt
    private void fairePousserArbre(int nbArbre, GameObject[] ListeGO)
    {

        for (int i = 0; i < nbArbre; i++)
        {
            // On détermine la future position du GameObject 
            // On est obligé d'initialiser rand ici sinon le même chiffre sortirait à chaque fois
            var rand = new System.Random();

            Vector2 position = positionAvailable(ListeGO);
            Utils.creerGo(goTag, position, ListeGO);
        }
    }

    // Fonction qui renvoie une position aléatoire disponible parmis toutes celles disponible sur le terrain
    private Vector2 positionAvailable(GameObject[] ListeGO)
    {
        Vector2 res = new Vector2();

        // On prend aléatoirement une position sur la carte
        // Pour ce :
        // On choisit un élément de la liste au hasard (= procheObjet) et on place le nouvel objet à une distance (=dx et dy) au hasard également

        // On met des valeurs par défaut sinon la suite ne fonctionne pas mais ces valeurs peuvent être changées
        int numProcheObjet = 3;
        bool test = false;
        int dx = distanceDensite, dy = distanceDensite;

        // Ces valeurs doivent également être initialisées pour que la suite fonctionne mais les valeurs entrées sont arbitraires
        float x = 0;
        float z = 0;
        Vector2 size = new Vector2(3, 3);


        // On calcul le nombre de GO ayant le tag arbre
        int lengthListeGO = ListeGO.Length;

        // Création d'une variable pour éviter une boucle infinie
        int i = 100;

        while (test == false && i > 0)
        {
            numProcheObjet = Utils.GetRandom(0, lengthListeGO);

            // On recupere le collider de l'arbre selectionné
            Collider col = ListeGO[numProcheObjet].GetComponent<Collider>();
            size = col.bounds.size;

            // Test qui determine si la position trouvée est bien sur le plateau de jeu 
            bool verif = false;

            while (verif == false)
            {
                // On prend des nombres aléatoires qui determine la distance à l'arbre selectionné
                dx = Utils.GetRandom(-distanceDensite, distanceDensite);
                dy = Utils.GetRandom(-distanceDensite, distanceDensite);

                // La position du centre du nouvel objet est donc :
                x = ListeGO[numProcheObjet].transform.position[0] + dx;
                z = ListeGO[numProcheObjet].transform.position[2] + dy;

                // On vérifie que la position est bien sur le plateau
                // On s'autorise une marge de 5 unités
                if (!(x <= 5 || x >= 995 || z <= 5 || z >= 995))
                    verif = true;
            }

            // Au cas ou la taille ne serait pas la même suivant x, y et z 
            // On choisit la plus grande pour etre tranquille
            float sizeMax = Math.Max(size[0], size[1]);

            test = isPositionOk(numProcheObjet, x, z, ListeGO, sizeMax);
            i--;
        }
        res[0] = x;
        res[1] = z;

        return res;
    }

    // Fonction qui dit si la position aléatoire tirée est disponible 
    private bool isPositionOk(int numObjet, float x, float z, GameObject[] list, float size)
    {
        Vector2 sphereCenter = new Vector2(x, z);
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, size + goMarge);

        // Si un élément est dans la liste des collides on cherche une autre position
        if (hitColliders.Length > 0)
            return false;

        // Sinon on valide celle la
        return true;
    }
}