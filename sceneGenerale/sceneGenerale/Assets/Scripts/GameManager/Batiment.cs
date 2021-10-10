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
    private List<ItemAmount> ressourcesProduction;
    private string worker;
    private int chauffageNeed;
    private int chauffageActual;
}
