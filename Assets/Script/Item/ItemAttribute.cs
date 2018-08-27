using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Item Attribute")]
public class ItemAttribute : ScriptableObject
{
    public string Desciption;

    public string itemName;
    public string itemInfo;

    public Sprite icon;

    public int itemBuffer;

    public ItemType itemType;

    public enum ItemType
    {
        None = 0,
        AttributesEffect,
        FoodBubble,
        AttackRate
    }
}
