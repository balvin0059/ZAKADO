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
