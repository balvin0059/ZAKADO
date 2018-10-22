using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Creat GameStuff/ Item Attribute")]
public class ItemAttribute : ScriptableObject
{
    public int itemID;
    public string Desciption;

    public string itemName;
    public string itemInfo;

    public Sprite icon;

    public int itemBuffer;
    public int SellValue;
    public ItemType itemType;

    public enum ItemType
    {
        None = 0,
        AttributesEffect,
        FoodBubble,
        AttackRate
    }
}
