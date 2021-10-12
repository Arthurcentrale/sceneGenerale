using UnityEngine;
using System;

[CreateAssetMenu]
[Serializable]
public class Item : ScriptableObject
{
    public string id;
    public string ItemName;
    public Sprite Icon;
    public int Weight;
    public GameObject prefab;

    public bool isFood;

    public void Init(string ItemName)
    {
        this.ItemName = ItemName;
    }

    public static Item Create_Instance(string ItemName)
    {
        var data = ScriptableObject.CreateInstance<Item>();
        data.Init(ItemName);
        return data;
    }

    /*
    public Sprite GetSprite()
    {
        switch (ItemName)
        {
            default:
            case "Wood": return ItemAssets.Instance.WoodIcon;
            case "Berry": return ItemAssets.Instance.BerryIcon;
        }
    }
    */

    
    public virtual int GetPoints()  //fonction qui retourne 0 de base mais qui retournera les variables foodPoints ou burnPoints si l'item
    {                               //est dans une sous-classe Aliment ou Combustible
        return 0;
    }
}

[CreateAssetMenu]
[Serializable]
public class Aliment : Item
{
    public int foodPoints;

    public override int GetPoints()
    {
        return foodPoints;
    }
}

[CreateAssetMenu]
[Serializable]
public class Combustible : Item
{
    public bool isBurnt;
    public int burnPoints;

    public override int GetPoints()
    {
        return burnPoints;
    }
}
