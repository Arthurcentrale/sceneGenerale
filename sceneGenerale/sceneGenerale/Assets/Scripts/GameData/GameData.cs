using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public List<Vector3> listePositionsPins;
    public List<Vector3> listePositionsDouglas;
    public List<Vector3> listePositionsChenes;
    public List<Vector3> listePositionsHetres;
    public List<Vector3> listePositionsBouleaux;

    public List<Vector3> listePositionsPinsFrele;
    public List<Vector3> listePositionsDouglasFrele;
    public List<Vector3> listePositionsChenesFrele;
    public List<Vector3> listePositionsHetresFrele;
    public List<Vector3> listePositionsBouleauxFrele;

    public List<Vector3> listePositionsPinsMalade;
    public List<Vector3> listePositionsDouglasMalade;
    public List<Vector3> listePositionsChenesMalade;
    public List<Vector3> listePositionsHetresMalade;
    public List<Vector3> listePositionsBouleauxMalade;

    public List<Vector3> listePositionsPinsArbuste;
    public List<Vector3> listePositionsDouglasArbuste;
    public List<Vector3> listePositionsChenesArbuste;
    public List<Vector3> listePositionsHetresArbuste;
    public List<Vector3> listePositionsBouleauxArbuste;

    public List<Vector3> listePositionsPinsArbusteMalade;
    public List<Vector3> listePositionsDouglasArbusteMalade;
    public List<Vector3> listePositionsChenesArbusteMalade;
    public List<Vector3> listePositionsHetresArbusteMalade;
    public List<Vector3> listePositionsBouleauxArbusteMalade;

    public List<Vector3> listePositionsPinsSouche;
    public List<Vector3> listePositionsDouglasSouche;
    public List<Vector3> listePositionsChenesSouche;
    public List<Vector3> listePositionsHetresSouche;
    public List<Vector3> listePositionsBouleauxSouche;

    public List<Vector3> listePositionsPinsSoucheMalade;
    public List<Vector3> listePositionsDouglasSoucheMalade;
    public List<Vector3> listePositionsChenesSoucheMalade;
    public List<Vector3> listePositionsHetresSoucheMalade;
    public List<Vector3> listePositionsBouleauxSoucheMalade;

    public List<Vector3> listePositionsRochers1;
    public List<Vector3> listePositionsRochers2;
    public List<Vector3> listePositionsRochers3;
    public List<Vector3> listePositionsRochers4;
    public List<Vector3> listePositionsRochers5;
    public List<Vector3> listePositionsRochers6;
    public List<Vector3> listePositionsRochers7;

    public List<string> listeNomsItems;
    public List<int> listeAmountItems;
    public List<bool> listeFavoris;

    public List<string> listeBatiments;
    public List<Vector3> listePosBatiments;

    public bool isMairieRenovee;

    // Constructeur par données    
    public GameData(List<Vector3> LPPins,
                    List<Vector3> LPDouglas,
                    List<Vector3> LPChenes,
                    List<Vector3> LPHetres,
                    List<Vector3> LPBouleaux,
                    List<Vector3> LPPinsFrele,
                    List<Vector3> LPDouglasFrele,
                    List<Vector3> LPChenesFrele,
                    List<Vector3> LPHetresFrele,
                    List<Vector3> LPBouleauxFrele,
                    List<Vector3> LPPinsMalade,
                    List<Vector3> LPDouglasMalade,
                    List<Vector3> LPChenesMalade,
                    List<Vector3> LPHetresMalade,
                    List<Vector3> LPBouleauxMalade,
                    List<Vector3> LPPinsArbuste,
                    List<Vector3> LPDouglasArbuste,
                    List<Vector3> LPChenesArbuste,
                    List<Vector3> LPHetresArbuste,
                    List<Vector3> LPBouleauxArbuste,
                    List<Vector3> LPPinsArbusteMalade,
                    List<Vector3> LPDouglasArbusteMalade,
                    List<Vector3> LPChenesArbusteMalade,
                    List<Vector3> LPHetresArbusteMalade,
                    List<Vector3> LPBouleauxArbusteMalade,
                    List<Vector3> LPPinsSouche,
                    List<Vector3> LPDouglasSouche,
                    List<Vector3> LPChenesSouche,
                    List<Vector3> LPHetresSouche,
                    List<Vector3> LPBouleauxSouche,
                    List<Vector3> LPPinsSoucheMalade,
                    List<Vector3> LPDouglasSoucheMalade,
                    List<Vector3> LPChenesSoucheMalade,
                    List<Vector3> LPHetresSoucheMalade,
                    List<Vector3> LPBouleauxSoucheMalade,
                    List<Vector3> LR1,
                    List<Vector3> LR2,
                    List<Vector3> LR3,
                    List<Vector3> LR4,
                    List<Vector3> LR5,
                    List<Vector3> LR6,
                    List<Vector3> LR7,
                    List<string> listeBatiments,
                    List<Vector3> listePosBatiments,
                    List<ItemAmount> itemList,
                    List<bool> favList,
                    bool isMairieRenovee)
    {
        this.listePositionsChenes = LPChenes;
        this.listePositionsPins = LPPins;
        this.listePositionsHetres = LPHetres;
        this.listePositionsDouglas = LPDouglas;
        this.listePositionsBouleaux = LPBouleaux;

        this.listePositionsChenesFrele = LPChenesFrele;
        this.listePositionsPinsFrele = LPPinsFrele;
        this.listePositionsHetresFrele = LPHetresFrele;
        this.listePositionsDouglasFrele = LPDouglasFrele;
        this.listePositionsBouleauxFrele = LPBouleauxFrele;

        this.listePositionsChenesMalade = LPChenesMalade;
        this.listePositionsPinsMalade = LPPinsMalade;
        this.listePositionsHetresMalade = LPHetresMalade;
        this.listePositionsDouglasMalade = LPDouglasMalade;
        this.listePositionsBouleauxMalade = LPBouleauxMalade;

        this.listePositionsChenesArbuste = LPChenesArbuste;
        this.listePositionsPinsArbuste = LPPinsArbuste;
        this.listePositionsHetresArbuste = LPHetresArbuste;
        this.listePositionsDouglasArbuste = LPDouglasArbuste;
        this.listePositionsBouleauxArbuste = LPBouleauxArbuste;

        this.listePositionsChenesArbusteMalade = LPChenesArbusteMalade;
        this.listePositionsPinsArbusteMalade = LPPinsArbusteMalade;
        this.listePositionsHetresArbusteMalade = LPHetresArbusteMalade;
        this.listePositionsDouglasArbusteMalade = LPDouglasArbusteMalade;
        this.listePositionsBouleauxArbusteMalade = LPBouleauxArbusteMalade;

        this.listePositionsChenesSouche = LPChenesSouche;
        this.listePositionsPinsSouche = LPPinsSouche;
        this.listePositionsHetresSouche = LPHetresSouche;
        this.listePositionsDouglasSouche = LPDouglasSouche;
        this.listePositionsBouleauxSouche = LPBouleauxSouche;

        this.listePositionsChenesSoucheMalade = LPChenesSoucheMalade;
        this.listePositionsPinsSoucheMalade = LPPinsSoucheMalade;
        this.listePositionsHetresSoucheMalade = LPHetresSoucheMalade;
        this.listePositionsDouglasSoucheMalade = LPDouglasSoucheMalade;
        this.listePositionsBouleauxSoucheMalade = LPBouleauxSoucheMalade;

        this.listePositionsRochers1 = LR1;
        this.listePositionsRochers2 = LR2;
        this.listePositionsRochers3 = LR3;
        this.listePositionsRochers4 = LR4;
        this.listePositionsRochers5 = LR5;
        this.listePositionsRochers6 = LR6;
        this.listePositionsRochers7 = LR7;


        this.listeBatiments = listeBatiments;
        this.listePosBatiments = listePosBatiments;

        this.listeAmountItems = new List<int>();
        this.listeNomsItems = new List<string>();

        foreach (ItemAmount item in itemList)
        {
            this.listeAmountItems.Add(item.Amount);
            this.listeNomsItems.Add(item.Item.name);
        }
        this.listeFavoris = favList;

        this.isMairieRenovee = isMairieRenovee;
    }
}
