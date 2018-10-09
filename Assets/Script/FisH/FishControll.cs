using System;
using System.Threading;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class FishControll : MonoBehaviour {

    #region 素質區  
    public float nowDamage;
    public float fishHealth;
    public bool Hooking;
    public bool Fishing;
    public bool Auto
    {
        get
        {
            return autoToggle.isOn;
        }
    }
    public Toggle autoToggle;


    public PreloadObject preloadObject;
    [Serializable]
    public struct PreloadObject
    {
        public GameObject talkBox;
        public GameObject dot;
        public GameObject shock;

        public GameObject FishingButton;
        public GameObject FishEscapePanel;
        public GameObject FishGetPanel;

        public GameObject UItapScreen;

        public GameObject UIFishingBar;
        public Image UIFishingBarValue;

        public Image catImage;

    }
    public PreloadText preloadText;
    [Serializable]
    public struct PreloadText
    {
        public Text fishText;
        public Text timeText;
        public Text fishingName;
        public Text fishingValue;
    }
    public FishSet[] fishSet;
    #endregion
    // Use this for initialization
    void Start () {
        //初始化autoThread
        autoThread = new Thread(AutoFishingThread);
        autoThread.Abort();

    }
	
	// Update is called once per frame
	void Update () {
        ShowFish();
        HookFishing();
    }





    public void StartFishing()
    {
        preloadObject.talkBox.SetActive(true);
        preloadObject.dot.SetActive(true);
        preloadObject.FishingButton.gameObject.SetActive(false);
        StartCoroutine(WaitingForFishing());
    }//點擊"開始釣魚"進入釣魚模式

    #region 手動釣魚
    IEnumerator WaitingForFishing()
    {
        float r = Random.Range(3, 11);
        yield return new WaitForSeconds(r);
        FishHooking();
        HookFishing();
    }//等待魚上鉤
    void FishHooking()
    {
        Hooking = true;
        preloadObject.dot.SetActive(false);
        preloadObject.shock.SetActive(true);
        StartCoroutine(WaitingForHookup());
    }//有魚上鉤
    IEnumerator WaitingForHookup()
    {
        yield return new WaitForSeconds(5.0f);
        if(!Fishing)
        {
            preloadObject.FishEscapePanel.SetActive(true);
        }
    }//等待確定要拉竿
    public void HookHookFish()
    {
        if (Hooking)
        {
            if (Fishing)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    nowDamage += RodDamage();
                }
            }
            else
            {
                Fishing = true;
                ChoiceRandom();
            }
        }
    }//開始釣魚模式，直接在這裡取得魚，並利用點擊螢幕增加釣魚值 
    void HookFishing()
    {
        if (Fishing)
        {
            preloadObject.UIFishingBar.SetActive(true);

            if (nowDamage < fishHealth)
            {
                nowDamage -= Time.deltaTime;
                preloadObject.UIFishingBarValue.fillAmount = nowDamage / fishHealth;
            }
            else
            {
                preloadObject.UIFishingBarValue.fillAmount = 1;
                preloadObject.FishGetPanel.SetActive(true);
                FishingEnd();
            }
        }
        else
        {
            preloadObject.UIFishingBar.SetActive(false);
        }
    }//釣魚中顯示釣魚值
    void FishingEnd()
    {
        Fishing = false;
        Hooking = false;
        nowDamage = 0;
        fishHealth = 0;

        preloadObject.UIFishingBar.SetActive(false);
        preloadObject.talkBox.SetActive(false);
        preloadObject.dot.SetActive(false);
        preloadObject.shock.SetActive(false);
        preloadObject.FishingButton.gameObject.SetActive(true);        
    }//釣魚結束初始化
    float RodDamage()
    {
        switch ((int)GlobalValue.instance.rodQuality)
        {
            case 0:
                return 0;
            case 1:
                return 5;
            case 2:
                return 10;
            case 3:
                return 15;
        }
        return 0;
    }//拿到現在釣竿的傷害值
    public void ChoiceRandom()
    {
        SetPercent();
        var choice = FishProbabilityController.ChoiceRandom(fishSet);
        Debug.Log(fishSet[choice].Name);
        fishHealth = FishHolder.instance.fishIndex[fishSet[choice].ID - 1000].fishState.fishHealth;
        preloadText.fishingName.text = FishHolder.instance.fishIndex[fishSet[choice].ID - 1000].fishState.fishName;
        preloadText.fishingValue.text = FishHolder.instance.fishIndex[fishSet[choice].ID - 1000].fishState.fishValue.ToString();
    }//取3隻魚其中一隻
    void SetPercent()
    {
        for(int i = 0;i < fishSet.Length; i++)
        {
            fishSet[i].ProbabilityPercent = FishHolder.instance.fishIndex[i].fishState.fishPercent[(int)GlobalValue.instance.rodQuality-1].ProbabilityPercent;
        }
    }//依照魚餌的等級影響釣上的魚
    #endregion

    public void ClosePanel(GameObject go)
    {
        go.SetActive(false);
        FishingEnd();
    }//關閉面板
    void ShowFish()
    {
        preloadText.fishText.text = GlobalValue.instance.gold.ToString();
    }//顯示魚飼料同步現有魚飼料

    #region 自動釣魚
    Thread autoThread;
    public void AutoFishingThread()
    {

        Thread.Sleep(1000);
    }
    #endregion
}
