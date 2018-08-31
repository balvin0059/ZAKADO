using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHolder : MonoBehaviour {
    #region 單例模式
    public static QuestHolder instance;
    public static QuestHolder Instance
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

    [Header("Quest Holder")]
    public List<QuestAttr> quest = new List<QuestAttr>();
}
