using System;
using UnityEngine;

[System.Serializable]
public class BonusItem : IComparable
{
    // something ID
    public int ID;
    public string name;
    public int rewardType;
    public int itemNumber;
    [Range(0,100)]
    public float ProbabilityPercent;
    public float Angle; // create class viewer info


    public float Percent
    {
        get { return ProbabilityPercent / 100; }
    }

    public int CompareTo(object obj)
    {
        return Percent.CompareTo(((BonusItem) obj).Percent);
    }
}