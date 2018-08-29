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
    void Start()
    {
        Bg.sprite = bgCange[GlobalValue.instance.nowMission];
        for(int k = 0; k < missionHolder.Length; k++)
        {
            missionHolder[k].SetActive(false);
        }
        missionHolder[GlobalValue.instance.nowMission].SetActive(true);

        //GlobalValue.instance.nowMission = 0;

        clearArea[0].SetActive(GlobalValue.instance.level[0]);

        if (GlobalValue.instance.nowMission == 1)
        {
            nextButton.SetActive(false);
            backButton.SetActive(true);
        }
        else
        {
            if (GlobalValue.instance.mission[GlobalValue.instance.nowMission])
            {
                nextButton.SetActive(true);
            }
            else
            {
                nextButton.SetActive(false);
            }
            backButton.SetActive(false);
        }

        for (int i = 1; i < GlobalValue.instance.level.Length; i++)
        {
            //i = i + GlobalValue.instance.nowMission * 5;
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
            l = l + GlobalValue.instance.nowMission * 5;
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
    public void OnConfirmPanel()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        GlobalValue.instance.enegy -= 5;
        GlobalValue.instance.dateTime_next = GlobalValue.instance.dateTime_next.AddMinutes(25);
        LevelChosing = false;
        SceneManager.LoadScene("GameScene");
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
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        missionHolder[GlobalValue.instance.nowMission].SetActive(false);
        GlobalValue.instance.nowMission += 1;
        Bg.sprite = bgCange[GlobalValue.instance.nowMission];
        missionHolder[GlobalValue.instance.nowMission].SetActive(true);
        backButton.SetActive(true);
        if (GlobalValue.instance.nowMission == 1)
        {
            nextButton.SetActive(false);
        }
    }
    public void OnBackMission()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        missionHolder[GlobalValue.instance.nowMission].SetActive(false);
        GlobalValue.instance.nowMission -= 1;
        Bg.sprite = bgCange[GlobalValue.instance.nowMission];
        missionHolder[GlobalValue.instance.nowMission].SetActive(true);
        nextButton.SetActive(true);
        if (GlobalValue.instance.nowMission == 0)
        {
            backButton.SetActive(false);
        }
    }
    public void TurnOffPanel(GameObject g)
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        LevelChosing = false;
        g.SetActive(false);
    }
}
