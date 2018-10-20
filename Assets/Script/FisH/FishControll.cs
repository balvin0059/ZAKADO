using System;
using System.Threading;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class FishControll : MonoBehaviour {
    #region 單例模式
    public static FishControll instance;
    public static FishControll Instance
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
        }
    }
    #endregion
    #region 素質區  
    public float nowDamage;
    public float fishHealth;
    public int whichFish;
    public bool startFishbool;
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
        [Header("對話框面板預載")]
        public GameObject talkBox;//對話框
        public GameObject dot;//釣魚中 "..."
        public GameObject shock;//釣到魚了 "!!"
        public GameObject autoText;//自動釣魚中 "auto"
        [Header("UI介面預載")]
        public GameObject FishingButton; // 開始釣魚按鈕
        public GameObject FishEscapePanel; // 魚跑走 面板
        public GameObject FishGetPanel; // 取得魚 面板
        public Image changeBaitPanel;
        public Image changeBaitSprite;
        public GameObject noFishBaitTip;
        public Image getFishTypeSprite;
        [Header("釣魚值面板預載")]
        public GameObject UItapScreen; // 觸控範圍
        public GameObject UIFishingBar; // 釣魚值物件
        public Image UIFishingBarValue; // 釣魚值本身
        [Header("貓咪圖片預載")]
        public Image catImage; // 貓咪
        public GameObject[] effholder_os; // 釣竿特效
        public Image rodImage; // 釣竿圖片        
        [Header("自動釣魚面板預載")]
        public GameObject StopAutoBtn; // 停止自動釣魚按鈕
        public GameObject AutoAccmuPanel; // 收成飼料 面板
        public GameObject loading; // 讀取畫面
        [Header("升級魚餌面板預載")]
        public GameObject FishBaitPanel; // 魚餌面板
        public Sprite[] fishbaitQ;       
        [Header("升級釣竿面板預載")]
        public GameObject FishRodPanel; // 釣竿面板
        public Image fishRod_bg; // 釣竿背景(普通 精良 史詩)
        public Image fishRod; // 釣竿種類
        public GameObject eff_nowRod; // 釣竿特效
        public GameObject[] fishRodEffect; // 預載特效
        public Sprite[] fishRodq; // 釣竿種類貼圖
        public Sprite[] fishRodqpanel; // 釣竿背景貼圖
        public Button upgradeBtn; // 升級按鈕
        public GameObject noFoodPanel; // 飼料不夠彈出
    }
    public PreloadText preloadText;
    [Serializable]
    public struct PreloadText
    {
        public Text fishText;
        public Text timeText;
        public Text fishingName;
        public Text fishingValue;

        public Text[] fishGetText;
        [Header("升級釣竿文字預載")]
        public Text fishRodName;
        public Text RodUpgradevalue;
        public Text rod_maxText;
        public Text rod_upgradeText;
        [Header("魚餌文字預載")]
        public Text ValueAmount;
        public Text[] fishBaitAmount;
        public Text baitAmount_os;

    }
    public FishSet[] fishSet;
    #endregion
    // Use this for initialization
    void Start () {
        autoToggle.isOn = FishHolder.instance.fishValue.AutoisOn;
        preloadObject.effholder_os[(int)FishHolder.instance.fishValue.rodQuality - 1].SetActive(true);
        if (Auto)
        {
            Debug.Log(FishHolder.instance.CalculTimeSpan());
            AccmuFish();
            if (FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1] > 0)
            {
                StartCoroutine(AutoFishingThread());
                preloadObject.FishingButton.gameObject.SetActive(false);
                preloadObject.StopAutoBtn.SetActive(true);
                preloadObject.talkBox.SetActive(true);
                preloadObject.autoText.SetActive(true);
            }
        }
    }	
	// Update is called once per frame
	void Update () {

        ShowFish();
        HookFishing();
        AutoGetFish();
    }
    void AccmuFish()
    {
        int fishingtime = 0;
        fishingtime = (int)FishHolder.instance.CalculTimeSpan()/(RodQuality()/1000);
        Debug.Log(fishingtime);
        if(fishingtime < FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1])
        {
            Debug.Log(fishingtime);
        }
        else
        {
            fishingtime = FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1];
            autoToggle.isOn = false;
        }
        fishingtime = FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1];
        Debug.Log(fishingtime);
        //if (fishingtime < 0)
        //{
        //    fishingtime = FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1];
        //    autoToggle.isOn = false;
        //}
        Debug.Log(fishingtime + "次");
        for(int i = 0; i < fishingtime; i++)
        {
            AutoFishing();
        }
        fishingtime = 0;
        FishHolder.instance.fishValue.fishDate = DateTime.Now;
    }

    public void CloseAuto()
    {
        StopCoroutine(AutoFishingThread());
        autoToggle.isOn = false;
        startFishbool = false;
        preloadObject.StopAutoBtn.SetActive(false);
        preloadObject.autoText.SetActive(false);
        preloadObject.talkBox.SetActive(false);
        FishingEnd();
    }
    public void StartFishing()
    {

        if (FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1] > 0)
        {
            if (!Auto)
            {
                preloadObject.talkBox.SetActive(true);
                preloadObject.dot.SetActive(true);
                preloadObject.FishingButton.gameObject.SetActive(false);
                FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1] -= 1;
                StartCoroutine(WaitingForFishing());
            }
            else if (Auto)
            {
                startFishbool = true;
                StartCoroutine(AutoFishingThread());
                preloadObject.FishingButton.gameObject.SetActive(false);
                preloadObject.StopAutoBtn.SetActive(true);
                preloadObject.talkBox.SetActive(true);
                preloadObject.autoText.SetActive(true);
            }
        }
        else
        {
            Debug.Log(FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1]);
            Instantiate(preloadObject.noFishBaitTip, new Vector3(0, 0, 0), Quaternion.identity, preloadObject.catImage.transform);
        }
    }//點擊"開始釣魚"進入釣魚模式
    #region 手動釣魚
    IEnumerator WaitingForFishing()
    {
        startFishbool = true;
        float r = Random.Range(3, 11);
        yield return new WaitForSeconds(r);
        FishHooking();
        HookFishing();
    }//等待魚上鉤
    void FishHooking()
    {
        startFishbool = true;
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
        if (!Auto)
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
                GetFishFunc();
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
        startFishbool = false;
        nowDamage = 0;
        fishHealth = 0;
        whichFish = 0;

        preloadObject.UIFishingBar.SetActive(false);
        preloadObject.talkBox.SetActive(false);
        preloadObject.dot.SetActive(false);
        preloadObject.shock.SetActive(false);
        preloadObject.FishingButton.gameObject.SetActive(true);        
    }//釣魚結束初始化
    float RodDamage()
    {
        switch ((int)FishHolder.instance.fishValue.rodQuality)
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
        Debug.Log(FishHolder.instance.fishIndex[whichFish].Name);
        whichFish = choice;
        fishHealth = FishHolder.instance.fishIndex[fishSet[choice].ID - 1000].fishState.fishHealth;
        preloadText.fishingName.text = FishHolder.instance.fishIndex[fishSet[choice].ID - 1000].fishState.fishName;
        preloadText.fishingValue.text = FishHolder.instance.fishIndex[fishSet[choice].ID - 1000].fishState.fishValue.ToString();
        preloadObject.getFishTypeSprite.sprite = FishHolder.instance.fishIndex[fishSet[choice].ID - 1000].fishState.fishSprite;
    }//取3隻魚其中一隻
    void SetPercent()
    {
        for(int i = 0;i < fishSet.Length; i++)
        {
            fishSet[i].ProbabilityPercent = FishHolder.instance.fishIndex[i].fishState.fishPercent[(int)FishHolder.instance.fishValue.rodQuality-1].ProbabilityPercent;
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
        preloadText.baitAmount_os.text = FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality-1].ToString();
        preloadObject.changeBaitPanel.sprite = preloadObject.fishRodqpanel[(int)FishHolder.instance.fishValue.baitQuality - 1];
        preloadObject.changeBaitSprite.sprite = preloadObject.fishbaitQ[(int)FishHolder.instance.fishValue.baitQuality - 1];
    }//顯示魚飼料同步現有魚飼料
    #region 自動釣魚
    bool GetFish = false;
    IEnumerator AutoFishingThread()
    {
        Debug.Log("開始自動釣魚"+ RodQuality()/1000+"秒一次");
        while (Auto)
        {
            yield return new WaitForSeconds(RodQuality());
            GetFish = true;
        }
        StartCoroutine(AutoFishingThread());
    }
    int RodQuality()
    {
        switch ((int)FishHolder.instance.fishValue.rodQuality)
        {
            case 0:
                return 30000;
            case 1:
                return 30000;
            case 2:
                return 25000;
            case 3:
                return 20000;
        }
        return 30000;
    }
    void AutoFishing()
    {
        SetPercent();

        var choice = FishProbabilityController.ChoiceRandom(fishSet);
        whichFish = choice;
        FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1] -= 1;
        GetFishFunc();
        Debug.Log("自動捕捉到:"+ FishHolder.instance.fishIndex[whichFish].Name);
    }
    void AutoGetFish()
    {
        if (GetFish)
        {
            AutoFishing();
            GetFish = false;
        }
    }
    private void OnApplicationQuit()
    {
        FishHolder.instance.fishValue.AutoisOn = Auto;
        StopCoroutine(AutoFishingThread());
        if (Auto)
        {
            FishHolder.instance.fishValue.fishDate = DateTime.Now;
        }
        else
        {
        }
    }
    public void BackToMainScene()
    {
        FishHolder.instance.fishValue.AutoisOn = Auto;
        StopCoroutine(AutoFishingThread());
        if (Auto)
        {
            FishHolder.instance.fishValue.fishDate = DateTime.Now;
        }
        else
        {
        }
        preloadObject.loading.SetActive(true);
        preloadObject.loading.GetComponent<Loading>().GotoScene("MainScene");
    }
    #endregion

    void GetFishFunc()
    {
        FishHolder.instance.fishIndex[whichFish].amount += 1;
        FishHolder.instance.fishValue.fishAmount[whichFish] += 1;
        FishHolder.instance.amount += 1;
    }


    #region Button

    #region 升級釣竿相關
    public void OnUpgradeRod()
    {
        if (!startFishbool)
        {
            preloadObject.fishRod.sprite = preloadObject.fishRodq[(int)FishHolder.instance.fishValue.rodQuality - 1];
            preloadText.fishRodName.text = rodName();
            preloadText.fishRodName.color = rodnameColor();
            if (FishHolder.instance.fishValue.rodQuality > 0 && (int)FishHolder.instance.fishValue.rodQuality < 3)
            {
                preloadObject.fishRod.sprite = preloadObject.fishRodq[(int)FishHolder.instance.fishValue.rodQuality - 1];
                preloadText.fishRodName.text = rodName();
                preloadText.RodUpgradevalue.text = rodUpgrade().ToString();
            }
            else
            {
                preloadText.rod_upgradeText.gameObject.SetActive(false);
                preloadText.rod_maxText.gameObject.SetActive(true);
                preloadObject.upgradeBtn.gameObject.SetActive(false);
            }
            preloadObject.FishRodPanel.SetActive(true);
        }
    } // 按下釣竿按鈕
    string rodName()
    {
        switch ((int)FishHolder.instance.fishValue.rodQuality)
        {
            case 0:
                break;
            case 1:
                return "普通釣竿";
            case 2:
                return "精良釣竿";
            case 3:
                return "傳說釣竿";
        }
        return "普通釣竿";
    } // 取釣竿名稱
    int rodUpgrade()
    {
        switch ((int)FishHolder.instance.fishValue.rodQuality)
        {
            case 0:
                return 5000;
            case 1:
                return 10000;
            case 2:
                return 30000;
            case 3:
                return 30000;
        }
        return 5000;
    } // 取釣竿升級所需飼料
    Color rodnameColor()
    {
        switch ((int)FishHolder.instance.fishValue.rodQuality)
        {
            case 0:
                break;
            case 1:
                return Color.green;
            case 2:
                return Color.yellow;
            case 3:
                return Color.red;
        }
        return Color.green;
    } // 取釣竿名稱顏色
    public void OnUpgade()
    {
        if (FishHolder.instance.fishValue.rodQuality > 0 && (int)FishHolder.instance.fishValue.rodQuality < 3)
        {
            if(GlobalValue.instance.gold >= rodUpgrade())
            {
                preloadObject.effholder_os[(int)FishHolder.instance.fishValue.rodQuality - 1].SetActive(false);
                GlobalValue.instance.gold -= rodUpgrade();
                FishHolder.instance.fishValue.rodQuality++;
                preloadObject.fishRod.sprite = preloadObject.fishRodq[(int)FishHolder.instance.fishValue.rodQuality - 1];
                preloadText.fishRodName.text = rodName();
                preloadText.fishRodName.color = rodnameColor();
                preloadObject.effholder_os[(int)FishHolder.instance.fishValue.rodQuality - 1].SetActive(true);
                preloadText.RodUpgradevalue.text = rodUpgrade().ToString();
                if ((int)FishHolder.instance.fishValue.rodQuality >= 3)
                {
                    preloadText.rod_upgradeText.gameObject.SetActive(false);
                    preloadText.rod_maxText.gameObject.SetActive(true);
                    preloadObject.upgradeBtn.gameObject.SetActive(false);
                }
            }
            else
            {
                Instantiate(preloadObject.noFoodPanel, new Vector3(0, 0, 0), Quaternion.identity, preloadObject.FishRodPanel.transform);
            }
        }
    } // 升級釣竿
    #endregion

    #region 購買魚餌
    public void OnBuyFishBait()
    {
        if (!startFishbool)
        {
            preloadObject.FishBaitPanel.SetActive(true);
            for (int i = 0; i < preloadText.fishBaitAmount.Length; i++)
            {
                preloadText.fishBaitAmount[i].text = FishHolder.instance.fishValue.fishBaitAmount[i].ToString();
            }
        }
    }
    public void OnBuy(int i)
    {
        if (GlobalValue.instance.gold >= baitValue(i))
        {
            GlobalValue.instance.gold -= baitValue(i);
            FishHolder.instance.fishValue.fishBaitAmount[i]++;
            preloadText.fishBaitAmount[i].text = FishHolder.instance.fishValue.fishBaitAmount[i].ToString();
        }
        else
        {
            Instantiate(preloadObject.noFoodPanel, new Vector3(0, 0, 0), Quaternion.identity, preloadObject.FishBaitPanel.transform);
        }
    }
    int baitValue(int i)
    {
        switch (i)
        {
            case 0:
                return 100;
            case 1:
                return 300;
            case 2:
                return 500;
        }
        return 100;
    }
    public void OnChangeBait()
    {
        if (!startFishbool)
        {
            if (FishHolder.instance.fishValue.baitQuality > 0 && (int)FishHolder.instance.fishValue.baitQuality < 3)
            {
                FishHolder.instance.fishValue.baitQuality++;
                preloadText.baitAmount_os.text = FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1].ToString();
                preloadObject.changeBaitPanel.sprite = preloadObject.fishRodqpanel[(int)FishHolder.instance.fishValue.baitQuality - 1];
                preloadObject.changeBaitSprite.sprite = preloadObject.fishbaitQ[(int)FishHolder.instance.fishValue.baitQuality-1];
            }
            else
            {
                FishHolder.instance.fishValue.baitQuality = FishHolder.FishValue.BaitQuality.normal;
                preloadText.baitAmount_os.text = FishHolder.instance.fishValue.fishBaitAmount[(int)FishHolder.instance.fishValue.baitQuality - 1].ToString();
                preloadObject.changeBaitPanel.sprite = preloadObject.fishRodqpanel[(int)FishHolder.instance.fishValue.baitQuality - 1];
                preloadObject.changeBaitSprite.sprite = preloadObject.fishbaitQ[(int)FishHolder.instance.fishValue.baitQuality-1];
            }
        }
    }
    #endregion

    #region 魚簍
    public void OnFishBasket()
    {
        if (!startFishbool)
        {
            for (int i = 0; i < preloadText.fishGetText.Length; i++)
            {
                preloadText.fishGetText[i].text = FishHolder.instance.fishValue.fishAmount[i].ToString();//帶入數字
            }
            preloadText.ValueAmount.text = valueSum().ToString();
            preloadObject.AutoAccmuPanel.SetActive(true);
        }
    }
    public void sellFish()
    {
        GlobalValue.instance.gold += valueSum();
        for (int i = 0; i < preloadText.fishGetText.Length; i++)
        {
            FishHolder.instance.fishValue.fishAmount[i] = 0;
            FishHolder.instance.fishIndex[i].amount = 0;
            preloadText.fishGetText[i].text = FishHolder.instance.fishValue.fishAmount[i].ToString();
        }
        FishHolder.instance.amount = 0;
    }
    int valueSum()
    {
        int valueSum = 0;//初始化
        for (int i = 0; i < preloadText.fishGetText.Length; i++)
        {
            valueSum += FishHolder.instance.fishValue.fishAmount[i] * FishHolder.instance.fishIndex[i].fishState.fishValue;//計算價錢總合
        }
        return valueSum;
    }
    #endregion

    #endregion
}