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
}