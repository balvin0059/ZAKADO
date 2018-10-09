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