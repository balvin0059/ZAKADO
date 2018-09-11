using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneControll : MonoBehaviour {
    public bool OnPaneling;
    public GameObject noCatPanel;
    public bool OnQuestIng;
    public GameObject questPanel;
    public Text goldText;
    public Text expText;
    public Text enegyText;
    public Text enegyTimeText;
    public DateTime dateTime;
    public DateTime dateTime_next;
    public TimeSpan timeSpan;
    public Image indexCat;
    public GameObject enegyPanelMax;
    public GameObject enegyPanelCount;
    void Start()
    {
        indexCat.sprite = GlobalValue.instance.catSpritHolder[GlobalValue.instance.catNum[0] - 1000];
        SoundControll.instance.toggle = GameObject.Find("soundOnOff").GetComponent<Toggle>();
        if (SoundControll.instance.bgmAudioSource.clip != SoundControll.Instance.Bg_01)
        {
            SoundControll.Instance.ChangeBgSound(SoundControll.Instance.Bg_01);
        }
    }

    void Update()
    {
        //indexCat.sprite = GlobalValue.instance.catSpritHolder[GlobalValue.instance.catNum[0] - 1000];
        expText.text = GlobalValue.instance.exp.ToString();
        goldText.text = GlobalValue.instance.gold.ToString();
        if(GlobalValue.instance.enegy < GlobalValue.instance.maxEnegy)
        {
            enegyPanelMax.gameObject.SetActive(false);
            enegyText.text = GlobalValue.instance.enegy.ToString() +"/"+ GlobalValue.instance.maxEnegy.ToString();
            enegyPanelCount.gameObject.SetActive(true);
            enegyTimeText.text = (GlobalValue.instance.timeSpan.Minutes % 5).ToString() + ":" + GlobalValue.instance.timeSpan.Seconds.ToString("00");
        }
        else
        {
            enegyText.text = GlobalValue.instance.enegy.ToString();
            enegyPanelCount.gameObject.SetActive(false);
            enegyPanelMax.gameObject.SetActive(true);
        }
        
    }

    #region 按鈕
    public GameObject loadingPanel;
    public void ToggleEvent()//音樂控制關閉
    {
        SoundControll.instance.SwitchMuteState(SoundControll.instance.toggle.isOn);
    }
    public void OnAdventure()//冒險按鈕
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        if (!OnQuestIng)
        {
            if (!OnPaneling)
            {
                GetLevelSchedule(GlobalValue.instance.gameSave.level);
                loadingPanel.SetActive(true);
                loadingPanel.GetComponent<Loading>().GotoScene("MapScene");
            }
        }
    }
    public void OnTeammate()//組隊按鈕
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        if (!OnQuestIng)
        {
            if (!OnPaneling)
            {
                loadingPanel.SetActive(true);
                loadingPanel.GetComponent<Loading>().GotoScene("TeamScene");
            }
        }
    }
    public void OnUpgrade()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        if (!OnQuestIng)
        {
            if (!OnPaneling)
            {
                loadingPanel.SetActive(true);
                loadingPanel.GetComponent<Loading>().GotoScene("UpgradeScene");
            }
        }
    }//強化按鈕
    void GetLevelSchedule(bool[] b)
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        for (int i = 0; i < b.Length; i++)
        {
            GlobalValue.instance.level[i] = b[i];
        }
    }
    public void OnShop()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        if (!OnQuestIng)
        {
            if (GlobalValue.instance.nowUnlockCat < GlobalValue.instance.catHolder.Length)
            {
                loadingPanel.SetActive(true);
                loadingPanel.GetComponent<Loading>().GotoScene("ShopScene");
            }
            else
            {
                noCatPanel.SetActive(true);
                OnPaneling = true;
            }
        }
    }//商店按鈕
    public void OnClose(GameObject g)
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        g.SetActive(false);
        OnPaneling = false;
        OnQuestIng = false;
    }//關閉面板事件

    public void OnQuest()//任務按鈕
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        questPanel.SetActive(true);
        CheckquestComplete();
        OnQuestIng = true;
    }
    #endregion
    public void OnReward(int id)//帶入MissionID
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.questComplete);
        ItemHolder.instance.amount += 1;
        ItemHolder.instance.globleItems[QuestHolder.instance.quest[id].questAttr.missionRewardID - 1000].order = ItemHolder.instance.amount;
        ItemHolder.instance.globleItems[QuestHolder.instance.quest[id].questAttr.missionRewardID - 1000].amount += 1;
        QuestHolder.instance.quest[id].isComplete = true;
        QuestHolder.instance.quest[id].isReward = true;
        questRewardActive[id].SetActive(false);
        questClearActive[id].SetActive(true);

    }

    [Header("Quest Holder")]
    public GameObject[] questRewardActive;
    public GameObject[] questClearActive;
    void CheckquestComplete()
    {
        for(int i = 0; i < QuestHolder.instance.quest.Count; i++)
        {
            if(QuestHolder.instance.quest[i].isComplete)
            {
                if (QuestHolder.instance.quest[i].isReward)
                {
                    questClearActive[i].SetActive(true);
                }
                else if (!QuestHolder.instance.quest[i].isReward)
                {
                    questRewardActive[i].SetActive(true);
                }
            }
        }
    }


    #region Test
    public void ResetSave()
    {
        Debug.Log(GlobalValue.instance.gameSave.everSave);
        PlayerPrefs.DeleteAll();
        GlobalValue.instance.LoadAllData();
        Debug.Log(GlobalValue.instance.gameSave.everSave);
    }
    #endregion
}