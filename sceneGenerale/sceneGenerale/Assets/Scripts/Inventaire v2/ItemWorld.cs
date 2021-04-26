using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, ItemAmount item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.Pf_ItemWorld, position,Quaternion.identity).transform;

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private ItemAmount item;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(ItemAmount item)
    {
        this.item = item;
        spriteRenderer.sprite = item.Item.Icon;
    }

    public ItemAmount GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
