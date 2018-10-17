using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Creat GameStuff/ Fish Attribute")]
public class FishState : ScriptableObject
{
    // something ID
    public int ID;
    public string fishName;
    public FishType fishType;
    [Range(0, 10000)]
    public int fishValue;
    [Range(0, 200)]
    public float fishHealth;    
    public FishPercent[] fishPercent;

    //public float Percent
    //{
    //    get { return ProbabilityPercent / 100; }
    //}

    //public int CompareTo(object obj)
    //{
    //    return Percent.CompareTo(((FishState)obj).Percent);
    //}

    public enum FishType
    {
        none = 0,
        small,
        middle,
        large
    }
}
[System.Serializable]
public class FishPercent
{
    public string Name;
    [Range(0, 100)]
    public float ProbabilityPercent;
    [HideInInspector]
    public bool bUsed;

    public float Percent
    {
        get { return ProbabilityPercent / 100; }
    }
    public int CompareTo(object obj)
    {
        return Percent.CompareTo(((FishPercent)obj).Percent);
    }
}