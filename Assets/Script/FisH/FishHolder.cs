using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHolder : MonoBehaviour {
    #region 單例模式
    public static FishHolder instance;
    public static FishHolder Instance
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
    [Header("Fish Holder")]
    public List<FishIndex> fishIndex = new List<FishIndex>();
    public int amount;

    public FishValue fishValue;

    [Serializable]
    public struct FishValue
    {
        [Header("釣魚相關")]
        public DateTime fishDate;
        public bool AutoisOn;
        public int SpanSecond;
        public int[] fishAmount;
        public int[] fishBaitAmount;
        public RodQuality rodQuality;
        public BaitQuality baitQuality;
        public enum RodQuality
        {
            none = 0,
            normal,
            rare,
            unique
        }
        public enum BaitQuality
        {
            none = 0,
            normal,
            rare,
            unique
        }        
    }
    public float CalculTimeSpan()
    {
        Debug.Log(DateTime.Now);
        Debug.Log(fishValue.fishDate);
        if (fishValue.fishDate != null)
        {
            fishValue.SpanSecond = (int)(DateTime.Now.Subtract(fishValue.fishDate)).TotalSeconds;
            if(fishValue.SpanSecond < 0)
            {
                fishValue.SpanSecond = 0;
            }
        }else
        {
            return 0;
        }
        Debug.Log("已累積了" + fishValue.SpanSecond + "秒");
        return fishValue.SpanSecond;
    }
}

[System.Serializable]
public class FishIndex
{
    public string Name;
    public FishState fishState;
    public int id;
    public int amount;
    public int order;

    public FishIndex(FishState fishState, int id)
    {
        this.fishState = fishState;
        this.id = id;
    }
}