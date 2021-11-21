using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public List<Vector3> listePositionsChenes;
    public List<Vector3> listePositionsHetres;
    public List<Vector3> listePositionsPins;
    public List<Vector3> listePositionsDouglas;

    public List<Vector3> listePositionsRochers1;
    public List<Vector3> listePositionsRochers2;
    public List<Vector3> listePositionsRochers3;
    public List<Vector3> listePositionsRochers4;
    public List<Vector3> listePositionsRochers5;
    public List<Vector3> listePositionsRochers6;
    public List<Vector3> listePositionsRochers7;
    public List<ItemAmount> listeItems;
    public List<bool> listeFavoris;

    public Dictionary<string, Vector3> dictionnaireBatiments;

    // Constructeur par données
    public GameData(List<Vector3> LPP, List<Vector3> LPC, List<Vector3> LPCH, List<Vector3> LPPL, List<Vector3> LR1, List<Vector3> LR2, List<Vector3> LR3, List<Vector3> LR4, List<Vector3> LR5, List<Vector3> LR6, List<Vector3> LR7, Dictionary<string, Vector3> listeBats, List<ItemAmount> itemList, List<bool> favList)
    {
        this.listePositionsChenes = LPCH;
        this.listePositionsPins = LPP;
        this.listePositionsHetres = LPPL;
        this.listePositionsDouglas = LPC;

        this.listePositionsRochers1 = LR1;
        this.listePositionsRochers1 = LR2;
        this.listePositionsRochers1 = LR3;
        this.listePositionsRochers1 = LR4;
        this.listePositionsRochers1 = LR5;
        this.listePositionsRochers1 = LR6;
        this.listePositionsRochers1 = LR7;

        this.dictionnaireBatiments = listeBats;

        this.listeItems = itemList;
        this.listeFavoris = favList;
    }
}
