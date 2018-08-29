using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneControll : MonoBehaviour {
    public bool OnPaneling;
    public GameObject noCatPanel;
    public Text goldText;
    public Text expText;
    public Text enegyText;
    public Text enegyTimeText;
    public DateTime dateTime;
    public DateTime dateTime_next;
    public TimeSpan timeSpan;
    public Image indexCat;
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
            enegyText.text = GlobalValue.instance.enegy.ToString() +"/"+ GlobalValue.instance.maxEnegy.ToString();
            enegyTimeText.gameObject.SetActive(true);
            enegyTimeText.text = (GlobalValue.instance.timeSpan.Minutes % 5).ToString() + ":" + GlobalValue.instance.timeSpan.Seconds.ToString("00");
        }
        else
        {
            enegyText.text = GlobalValue.instance.enegy.ToString();
            enegyTimeText.gameObject.SetActive(false);
        }
        
    }

    #region 按鈕
    public void ToggleEvent()
    {
        SoundControll.instance.SwitchMuteState(SoundControll.instance.toggle.isOn);
    }
    public void OnAdventure()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        if (!OnPaneling)
        {
            GetLevelSchedule(GlobalValue.instance.gameSave.level);
            SceneManager.LoadScene("MapScene");
        }
    }
    public void OnTeammate()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        if (!OnPaneling)
        {
            SceneManager.LoadScene("TeamScene");
        }
    }
    public void OnUpgrade()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        if (!OnPaneling)
        {
            SceneManager.LoadScene("UpgradeScene");
        }
    }
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
        if (GlobalValue.instance.nowUnlockCat < GlobalValue.instance.catHolder.Length)
        {

            SceneManager.LoadScene("ShopScene");
        }
        else
        {
            noCatPanel.SetActive(true);
            OnPaneling = true;
        }
    }
    public void OnClose()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        noCatPanel.SetActive(false);
        OnPaneling = false;
    }
    #endregion
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