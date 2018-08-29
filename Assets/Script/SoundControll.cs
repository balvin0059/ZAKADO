using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundControll : MonoBehaviour {

    #region 單例
    public static SoundControll instance;
    public static SoundControll Instance
    {

        get
        {
            return instance;//get代表只能讀取  set代表可以修改
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            isMute = (PlayerPrefs.GetInt("mute", 0) == 0) ? false : true;
            DoMute();
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public AudioSource bgmAudioSource;//背景音樂
    public AudioClip buttonClip;//按鈕音效
    public AudioClip buyClip;//金幣落入金幣收集器的音效
    public AudioClip upgradeClip;//發獎金的音效
    public AudioClip eatBubbleClip;//發射子彈音效
    public AudioClip attackClip;//換槍的音效
    public AudioClip catchpooClip;//升級音效
    public AudioClip cantdoClip;
    public AudioClip attackPoo;

    public AudioClip Bg_01;
    public AudioClip Bg_02;

    public Toggle toggle;

    private bool isMute = false;//靜音開關
    public bool IsMute
    {
        get
        {
            return isMute;//只給訪問
        }

    }

    public void SwitchMuteState(bool isOn)//切換靜音開關
    {        
        isOn = toggle.isOn;
        isMute = !isOn;//取反
        DoMute();
    }

    void DoMute()
    {
        if (isMute)
        {
            bgmAudioSource.Pause();//暫停
        }
        else
        {
            bgmAudioSource.Play();//繼續播放
        }
    }

    public void PlayEffecSound(AudioClip clip)//播放音效
    {
        if (!isMute)
        {
            AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, 0));
        }
    }
    public void ChangeBgSound(AudioClip clip)//播放音效
    {
        if (!isMute)
        {
            bgmAudioSource.clip = clip;
            bgmAudioSource.Play();
        }
    }
}
