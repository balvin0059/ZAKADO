using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour {
    public static Flowchart flowchartManager;
    public string level;
    public string missonEND;
    public string missonSTART;
    public GameObject loading;
    public Image bg;

    
    public Sprite[] bgholder;
    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        bg.sprite = bgholder[GlobalValue.instance.nowMission];
        flowchartManager = GameObject.Find("StoryManager").GetComponent<Flowchart>();
        level = "Level_" + GlobalValue.instance.nowLevel.ToString();
        missonEND = "Misson_" + GlobalValue.instance.nowMission.ToString()+"_End";
        missonSTART = "Misson_" + GlobalValue.instance.nowMission.ToString() + "_Start";
        if (GlobalValue.instance.missionEndStory)
        {
            GetStroyEnd();
        }
        else if(GlobalValue.instance.missionStartStory)
        {
            GetStroyStart();
        }
        else
        {
            GetStroy();
        }
    }
    void GetStroy()
    {
        Block levelBlock = flowchartManager.FindBlock(level);
        if (levelBlock != null)
        {
            flowchartManager.ExecuteBlock(levelBlock);
        }
        else
        {
            Skip();
        }
    }
    void GetStroyStart()
    {
        Block missonBlock = flowchartManager.FindBlock(missonSTART);
        if (missonBlock != null)
        {
            flowchartManager.ExecuteBlock(missonBlock);
        }
        else
        {
            BackMap();
        }
    }
    void GetStroyEnd()
    {
        Block missonBlock = flowchartManager.FindBlock(missonEND);
        if (missonBlock != null)
        {
            flowchartManager.ExecuteBlock(missonBlock);
        }
        else
        {
            BackMap();
        }
    }
    public void Skip()
    {
        loading.SetActive(true);
        loading.GetComponent<Loading>().GotoScene("GameScene");
    }
    public void BackMap()
    {
        GlobalValue.instance.missionEndStory = false;
        GlobalValue.instance.missionStartStory = false;
        loading.SetActive(true);
        loading.GetComponent<Loading>().GotoScene("MapScene");
    }
    public void SkipButton()
    {
        if (GlobalValue.instance.missionEndStory || GlobalValue.instance.missionStartStory)
        {
            BackMap();
        }
        else
        {
            Skip();
        }
    }
}
