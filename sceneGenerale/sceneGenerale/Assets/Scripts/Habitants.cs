using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class Habitants : ScriptableObject

{
    public string nom;
    public Sprite image;
    public string catégorie;
    public string métier;
    public int niveau;
    public int expérience;
    public List<int> besoins;
    public List<conditions> conditions;

}

public class conditions
{
    public batiments batiment;
    public int quantité;
}

public class batiments
{
    public string nom;
    public bool produitnourriture;
    public List<Habitants> employés;
}

