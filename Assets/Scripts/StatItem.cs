using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "StatItem", menuName = "Item/StatItem")]
public class StatItem : Item
{
    public StatItemType itemType;
    public int id;

    public override void Use()
    {
        base.Use();
        GameManager.instance.OnStatItemuUse(itemType, id);
        Inventory.instance.RemoveItem(this);
        Debug.Log("Item use stat" + name);
    }
}



public enum StatItemType
{
    HealthItem,
    ThirstItem,
    FoodItem
}
