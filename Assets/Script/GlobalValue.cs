using System;
using System.Threading;
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
    public GameSave gameSave = new GameSave();
    [Header("主介面素質區")]
    public int gold = 0;
    public int exp = 0;
    public int maxEnegy = 50;
    public int enegy = 50;
    public bool[] level;
    public int[] catNum;
    public int nowLevel;
    public DateTime nowTime;
    [Header("物件預載素質區")]
    public GameObject[] catHolder;
    public GameObject[] enemyHolder;
    public GameObject[] effectHolder;
    [Header("圖片預載素質區")]
    public Sprite[] catSpritHolder;
    public Sprite[] catBattleSpritHolder;
    public Sprite[] elementHolder; // 1 = fire, 2 = water,3 = lighting

    #region 體力系統
    public DateTime dateTime;
    public DateTime dateTime_next;
    public TimeSpan timeSpan;
    [Header("體力系統區")]
    public int deltaEnegy;
    public int recoverEg;
    public bool geSw;
    public bool everSet;
    Thread ge;
    void Start()
    {
        ge = new Thread(GetEnegy);
        gameSave = SaveLoadData.LoadData();
        if (!gameSave.everSave)
        {
            for (int i = 0; i < catHolder.Length; i++)
            {
                gameSave.stateSave[i] = catHolder[i].GetComponent<CatControll>().state;
            }
            for (int i = 0; i < catNum.Length; i++)
            {
                gameSave.catNum[i] = catNum[i];
            }
            gameSave.recordTime = DateTime.Now;
            gameSave.everSave = true;
            SaveLoadData.SaveData(gameSave);
        }
        nowTime = DateTime.Now;
        TimeSpan timeSpan_f = nowTime.Subtract(gameSave.recordTime);

        enegy = (enegy + (int)timeSpan_f.TotalSeconds / 300 > 50) ? 50 : enegy + (int)timeSpan_f.TotalSeconds / 300;
    }

    private void Update()
    {
        if (enegy < maxEnegy)
        {
            EnegyTimeSet();
            if (!geSw)
            {
                ge.Start();
            }
        }
    }

    public void GetEnegy()
    {
        while (enegy < 50)
        {
            if (geSw)
            {
                Debug.Log("已呼叫GETENEGY");
                enegy += 1;
                if(enegy >= 50)
                {
                    enegy = 50;
                    geSw = false;
                    Debug.Log("結束執行GETENEGY");
                    ge.Abort();
                }
                Thread.Sleep(300000);
            }
            else
            {
                Debug.Log("第一次執行GETENEGY");
                geSw = true;
                Thread.Sleep(300000);
            }
        }

    }

    public void EnegyTimeSet()
    {
        if (!geSw)
        {
            deltaEnegy = maxEnegy - enegy;
            dateTime_next = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            dateTime_next = dateTime_next.AddMinutes(5 * deltaEnegy);
        }
        dateTime = DateTime.Now;
        timeSpan = dateTime_next.Subtract(dateTime);
    }
    private void OnApplicationQuit()
    {
        gameSave.recordTime = DateTime.Now;
        SaveLoadData.SaveData(gameSave);
        ge.Abort();
    }
    #endregion
}

public class CatList
{
    public string name;
    public int uid;

    public CatList(string newName, int newUid)
    {
        name = newName;
        uid = newUid;
    }
}
