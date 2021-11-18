using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public List<Vector3> listePositionsChenes;
    public List<Vector3> listePositionsHetres;
    public List<Vector3> listePositionsPins;
    public List<Vector3> listePositionsDouglas;

    // Constructeur par données
    public GameData(List<Vector3> LPP, List<Vector3> LPC, List<Vector3> LPCH, List<Vector3> LPPL)
    {
        this.listePositionsChenes = LPCH;
        this.listePositionsPins = LPP;
        this.listePositionsHetres = LPPL;
        this.listePositionsDouglas = LPC;
    }
}
