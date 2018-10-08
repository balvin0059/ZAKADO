using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour {
    #region 單例模式
    public static ItemHolder instance;
    public static ItemHolder Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("Item Holder")]
    public List<GlobleItem> globleItems = new List<GlobleItem>();
    public int amount;

}

public class ItemAPI
{    
    public static void RewardItem(int id)
    {
        ItemHolder.instance.amount += 1;
        ItemHolder.instance.globleItems[QuestHolder.instance.quest[id].questAttr.missionRewardID - 1000].order = ItemHolder.instance.amount;
        ItemHolder.instance.globleItems[QuestHolder.instance.quest[id].questAttr.missionRewardID - 1000].amount += 1;
        QuestHolder.instance.quest[id].isComplete = true;
        QuestHolder.instance.quest[id].isReward = true;
    }
    public static void AddItem(int id)
    {
        Debug.Log("GetItemID = " + id);
        if (ItemHolder.instance.globleItems[id - 1000].amount < 1)
        {
            ItemHolder.instance.amount += 1;
            ItemHolder.instance.globleItems[id - 1000].order = ItemHolder.instance.amount;
        }
        ItemHolder.instance.globleItems[id - 1000].amount += 1;
    }

}