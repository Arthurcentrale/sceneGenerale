using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ItemAmount
{
    public Item Item;
    public int Amount;
}

[CreateAssetMenu]
public class RecetteCraft : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

}


