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
    public List<ItemAmount> ressourcesProduction;
    private string worker;
    public int chauffageNeed;
    public int chauffageActual;

    public int productionFood()  //on regarde tous les items de la liste ressourcesProduction pour savoir la quantité de nourriture produite par le bâtiment
    {
        int prod = 0;
        foreach(ItemAmount item in ressourcesProduction)
        {
            if (item.Item.isFood)
            {
                prod += item.Item.GetPoints();
            }
        }
        return prod;
    }
}
