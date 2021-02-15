using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject 
{
    public string id;
    public string ItemName;
    public Sprite Icon;
    public int Weight;
    public GameObject prefab;
    
}
