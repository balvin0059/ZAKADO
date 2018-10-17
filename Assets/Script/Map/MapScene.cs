using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class MapScene : MonoBehaviour {
    public Image Bg;
    public Sprite[] bgCange;
    public GameObject noenegyPanel;
    public GameObject confirmenegyPanel;
    public GameObject nextButton;
    public GameObject backButton;
    public Text enegyText;
    public GameObject[] missionHolder;
    public GameObject[] levelHolder;
    public GameObject[] clearArea;
    public bool LevelChosing = false;
    public Text missionText;
    void Start()
    {
        MissionBgMusic();
        Time.timeScale = 1;

        missionText.text = GetMission();
        Bg.sprite = bgCange[GlobalValue.instance.nowMission];

        for(int k = 0; k < missionHolder.Length; k++)
        {
            missionHolder[k].SetActive(false);
        }
        missionHolder[GlobalValue.instance.nowMission].SetActive(true);
        clearArea[0].SetActive(GlobalValue.instance.level[0]);
        if (GlobalValue.instance.nowMission == GlobalValue.instance.mission.Length - 1)
        {
            nextButton.SetActive(false);
            backButton.SetActive(true);
        }
        else
        {
            if (GlobalValue.instance.mission[GlobalValue.instance.nowMission])
            {
                if (GlobalValue.instance.nowMission == 0)
                {
                    nextButton.SetActive(true);
                    backButton.SetActive(false);
                }
                else
                {
                    nextButton.SetActive(true);
                    backButton.SetActive(true);
                }
            }
            else
            {
                if (GlobalValue.instance.nowMission == 0)
                {
                    nextButton.SetActive(false);
                    backButton.SetActive(false);
                }
                else
                {
                    nextButton.SetActive(false);
                    backButton.SetActive(true);
                }
            }   
        }
        for (int i = 1; i < GlobalValue.instance.level.Length; i++)
        {
            if (GlobalValue.instance.level[i - 1])
            {
                levelHolder[i].SetActive(GlobalValue.instance.level[i - 1]);
                clearArea[i].SetActive(GlobalValue.instance.level[i]);
            }
            else
            {
                break;
            }
        }
    }
	public void OnLevel(int l)
    {
        if (!LevelChosing)
        {
            LevelChosing = true;
            l = l + GlobalValue.instance.nowMission * 6;
            if (GlobalValue.instance.enegy - 5 < 0)
            {
                SoundControll.Instance.PlayEffecSound(SoundControll.Instance.cantdoClip);
                noenegyPanel.SetActive(true);
            }
            else
            {
                SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
                GlobalValue.instance.nowLevel = l;
                string str = GlobalValue.instance.enegy.ToString() + " -> " + (GlobalValue.instance.enegy - 5).ToString();
                enegyText.text = str;
                confirmenegyPanel.SetActive(true);
            }
        }
    }
    public GameObject loading;
    public void OnConfirmPanel()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        GlobalValue.instance.enegy -= 5;
        GlobalValue.instance.dateTime_next = GlobalValue.instance.dateTime_next.AddMinutes(25);
        LevelChosing = false;
        if (!GlobalValue.instance.level[GlobalValue.instance.nowLevel])
        {
            loading.SetActive(true);
            loading.GetComponent<Loading>().GotoScene("StoryScene");
        }
        else
        {
            loading.SetActive(true);
            loading.GetComponent<Loading>().GotoScene("GameScene");
        }
}
    public void OnNoEnegyPanel()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        noenegyPanel.SetActive(false);
        LevelChosing = false;
        //or something ads
    }
    public void OnADButton()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        noenegyPanel.SetActive(false);
        LevelChosing = false;
        AdsScript.instance.ShowAd("rewardedVideo");
    }
    public void OnNextMission()
    {
        if (!LevelChosing)
        {            
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
            missionHolder[GlobalValue.instance.nowMission].SetActive(false);
            GlobalValue.instance.nowMission += 1;
            Bg.sprite = bgCange[GlobalValue.instance.nowMission];
            missionHolder[GlobalValue.instance.nowMission].SetActive(true);
            missionText.text = GetMission();
            backButton.SetActive(true);
            MissionBgMusic();
            if (GlobalValue.instance.mission[GlobalValue.instance.nowMission])
            {
                nextButton.SetActive(true);
            }
            else
            {
                nextButton.SetActive(false);
            }
            if (GlobalValue.instance.nowMission == GlobalValue.instance.mission.Length-1)
            {
                nextButton.SetActive(false);
            }
            if(GlobalValue.instance.nowStory < GlobalValue.instance.nowMission)
            {
                GlobalValue.instance.missionStartStory = true;
                loading.SetActive(true);
                loading.GetComponent<Loading>().GotoScene("StoryScene");
                GlobalValue.instance.nowStory++;
            }
        }
    }
    public void OnBackMission()
    {
        if (!LevelChosing)
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
            missionHolder[GlobalValue.instance.nowMission].SetActive(false);
            GlobalValue.instance.nowMission -= 1;
            Bg.sprite = bgCange[GlobalValue.instance.nowMission];
            missionHolder[GlobalValue.instance.nowMission].SetActive(true);
            missionText.text = GetMission();
            nextButton.SetActive(true);
            MissionBgMusic();
            if (GlobalValue.instance.nowMission == 0)
            {
                backButton.SetActive(false);
            }
        }
    }
    public void TurnOffPanel(GameObject g)
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        LevelChosing = false;
        g.SetActive(false);
    }
    string GetMission()
    {

        switch (GlobalValue.instance.nowMission)
        {
            case 0:
                return "第一章";
            case 1:
                return "第二章";
            case 2:
                return "第三章";
        }
        return "";
    }
    void MissionBgMusic()
    {
        switch (GlobalValue.instance.nowMission)
        {
            case 0:
                if (SoundControll.instance.bgmAudioSource.clip != SoundControll.Instance.Bg_02)
                {
                    SoundControll.Instance.ChangeBgSound(SoundControll.Instance.Bg_02);
                }
                break;
            case 1:
                if (SoundControll.instance.bgmAudioSource.clip != SoundControll.Instance.Bg_03)
                {
                    SoundControll.Instance.ChangeBgSound(SoundControll.Instance.Bg_03);
                }
                break;
            case 2:
                if (SoundControll.instance.bgmAudioSource.clip != SoundControll.Instance.Bg_04)
                {
                    SoundControll.Instance.ChangeBgSound(SoundControll.Instance.Bg_04);
                }
                break;
        }
    }
}
