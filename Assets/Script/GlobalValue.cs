using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalValue : MonoBehaviour {
    #region 單例模式
    public static GlobalValue instance;
    public static GlobalValue Instance
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

    public int gold = 0;
    public int exp = 0;
    public int enegy = 0;

    public int[] level;

    public GameObject[] catHolder;
    public GameObject[] enemyHolder;

    public Sprite[] catSpritHolder;
}
