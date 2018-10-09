using System;
using UnityEngine;

[System.Serializable]
public class FishSet : IComparable
{
    public string Name;
    // something ID
    public int ID;
    [Range(0, 100)]
    public float ProbabilityPercent;


    public float Percent
    {
        get { return ProbabilityPercent / 100; }
    }

    public int CompareTo(object obj)
    {
        return Percent.CompareTo(((FishSet)obj).Percent);
    }
}