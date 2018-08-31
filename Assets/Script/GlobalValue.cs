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
    public bool[] mission;
    public int[] catNum;
    public int nowLevel;
    public int nowMission = 0;
    public bool everTeach;
    public bool[] catBuyYet = new bool[10];
    public int nowUnlockCat;
    public DateTime nowTime;
    [Header("物件預載素質區")]
    public GameObject[] catHolder;
    public GameObject[] enemyHolder;
    public GameObject[] effectHolder;
    [Header("圖片預載素質區")]
    public Sprite[] catSpritHolder;
    public Sprite[] catBattleSpritHolder;
    public Sprite[] elementHolder; // 1 = fire, 2 = water,3 = lighting
    public Sprite[] missionTopBg;
    public Sprite[] missionBotBg;
    public Sprite[] missionMainBg;
    [Header("遊戲內需要素質")]
    public int[] GetTypePower; // 0 = fire, 1 = water,2 = lighting
    public int playerEnegyPower;
    public int itemNBubbleBufferAmount;
    public int itemSBubbleBufferAmount;

    #region 體力系統
    public DateTime dateTime;
    public DateTime dateTime_next;
    public TimeSpan timeSpan;
    public TimeSpan timeSpan_f;
    public TimeSpan timeSpan_d;
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
            for(int i = 0; i < catBuyYet.Length; i++)
            {
                gameSave.catBuyYet[i] = catBuyYet[i];
            }
            SaveAllData();
        }
        nowTime = DateTime.Now;
        timeSpan_f = nowTime.Subtract(gameSave.recordTime);
        Debug.Log(timeSpan_f.TotalSeconds+"秒");
        int deltaenegy = (int)timeSpan_f.TotalSeconds / 300;
        LoadAllData();
        enegy = (enegy + deltaenegy > 50) ? 50 : enegy + deltaenegy;
        timeSpan_d = timeSpan_f.Add(new TimeSpan(0, 0, -deltaenegy*300));
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
        if (timeSpan > timeSpan_f)
        {
            timeSpan -= timeSpan_f;
        }else if(timeSpan > timeSpan_d)
        {
            timeSpan -= timeSpan_d;
        }
    }
    private void OnApplicationQuit()
    {
        SaveAllData();
        ge.Abort();
    }
    #endregion

    public void SaveAllData()
    {
        for (int i = 0; i < catHolder.Length; i++)
        {
            gameSave.stateSave[i] = catHolder[i].GetComponent<CatControll>().state;
        }//存貓咪資料
        for (int i = 0; i < catNum.Length; i++)
        {
            gameSave.catNum[i] = catNum[i];
        }//存貓咪隊伍編排資料
        for (int i = 0; i < catBuyYet.Length; i++)
        {
            gameSave.catBuyYet[i] = catBuyYet[i];
        }//存以購買貓咪資料
        for (int i = 0; i < level.Length; i++)
        {
            gameSave.level[i] = level[i];
        }//存現在已完成關卡資料
        for (int i = 0; i < mission.Length; i++)
        {
            gameSave.mission[i] = mission[i];
        }//存現在已完成章節資料
        for (int i = 0; i < ItemHolder.instance.globleItems.Count; i++)
        {
            gameSave.item_id[i] = ItemHolder.instance.globleItems[i].id;
            gameSave.item_Use[i] = ItemHolder.instance.globleItems[i].itemUse;
            gameSave.item_amount[i] = ItemHolder.instance.globleItems[i].amount;
            gameSave.itemOrder[i] = ItemHolder.instance.globleItems[i].order;
        }//存現有道具資料
        for(int i = 0; i < QuestHolder.instance.quest.Count; i++)
        {
            gameSave.quest_complete[i] = QuestHolder.instance.quest[i].isComplete;
            gameSave.quest_id[i] = QuestHolder.instance.quest[i].id;
        }//存現在任務資料
        gameSave.item_all = ItemHolder.instance.amount;
        gameSave.gold = gold;
        gameSave.exp = exp;
        gameSave.recordTime = DateTime.Now;
        gameSave.enegy = enegy;
        gameSave.everTeach = everTeach;
        gameSave.everSave = true;

        SaveLoadData.SaveData(gameSave);
    }//存檔
    public void LoadAllData()
    {
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
            for (int i = 0; i < catBuyYet.Length; i++)
            {
                gameSave.catBuyYet[i] = catBuyYet[i];
            }
            for (int i = 0; i < ItemHolder.instance.globleItems.Count; i++)
            {
                gameSave.item_id[i] = ItemHolder.instance.globleItems[i].id;
                gameSave.item_Use[i] = ItemHolder.instance.globleItems[i].itemUse;
                gameSave.item_amount[i] = ItemHolder.instance.globleItems[i].amount;
                gameSave.itemOrder[i] = ItemHolder.instance.globleItems[i].order;
            }
            for (int i = 0; i < QuestHolder.instance.quest.Count; i++)
            {
                gameSave.quest_complete[i] = QuestHolder.instance.quest[i].isComplete;
                gameSave.quest_id[i] = QuestHolder.instance.quest[i].id;
            }//存現在任務資料
        }
        for (int i = 0; i < catHolder.Length; i++)
        {
            catHolder[i].GetComponent<CatControll>().state = gameSave.stateSave[i];
        }
        for (int i = 0; i < catNum.Length; i++)
        {
            catNum[i] = gameSave.catNum[i];
        }
        for (int i = 0; i < level.Length; i++)
        {
            level[i] = gameSave.level[i];
        }
        for (int i = 0; i < mission.Length; i++)
        {
            mission[i] = gameSave.mission[i];
        }
        for (int i = 0; i < catBuyYet.Length; i++)
        {
            if (i < 3)
            {
                catBuyYet[i] = true;
            }
            else
            {
                catBuyYet[i] = gameSave.catBuyYet[i];
                if (!catBuyYet[i])
                {
                    nowUnlockCat = i;
                    break;
                }
            }
        }
        for (int i = 0; i < ItemHolder.instance.globleItems.Count; i++)
        {
            ItemHolder.instance.globleItems[i].id = gameSave.item_id[i];
            ItemHolder.instance.globleItems[i].itemUse = gameSave.item_Use[i];
            ItemHolder.instance.globleItems[i].amount = gameSave.item_amount[i];
            ItemHolder.instance.globleItems[i].order = gameSave.itemOrder[i];
        }
        for (int i = 0; i < QuestHolder.instance.quest.Count; i++)
        {
            QuestHolder.instance.quest[i].isComplete = gameSave.quest_complete[i];
            QuestHolder.instance.quest[i].id = gameSave.quest_id[i];
        }//讀取現在任務資料
        ItemHolder.instance.amount = gameSave.item_all;
        gold = gameSave.gold;
        exp = gameSave.exp;
        enegy = gameSave.enegy;
        everTeach = gameSave.everTeach;
    }//讀檔
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
