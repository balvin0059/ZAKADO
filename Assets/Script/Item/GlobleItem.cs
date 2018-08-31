using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GlobleItem {

    public ItemAttribute itemAttribute;
    public int id;
    public bool itemUse;
    public int amount;
    public int order;

    public GlobleItem(ItemAttribute itemAttribute, int id)
    {
        this.itemAttribute = itemAttribute;
        this.id = id;
    }
}
