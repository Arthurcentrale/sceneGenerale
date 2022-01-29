using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batiment : MonoBehaviour
{
    private int timeBuild;
    private List<ItemAmount> ressourcesConstru;
    private List<ItemAmount> ressourcesDeposees;
    private bool ouvrierHere;
    private bool isWorking;
    public List<Item> ressourcesProduction;
    public int quantiteNourriture;
    private string worker;
    public int chauffageNeed;
    public int chauffageActual;

    void Start()
    {
        quantiteNourriture = 0;
    }

    public int productionFood()  //on regarde tous les items de la liste ressourcesProduction pour savoir la quantité de nourriture produite par le bâtiment
    {
        int prod = 0;
        foreach(Item item in ressourcesProduction)
        {
            if (item.isFood)
            {
                prod += item.GetPoints();
            }
        }
        return prod;
    }
}
