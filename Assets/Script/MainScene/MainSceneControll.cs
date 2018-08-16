using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneControll : MonoBehaviour {
    public Text goldText;
    public Text expText;
    public Text enegyText;
    public Text enegyTimeText;
    public DateTime dateTime;
    public DateTime dateTime_next;
    public TimeSpan timeSpan;
    

    void Start()
    {
              
    }

    void Update()
    {
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

   

	public void OnAdventure()
    {        
        GetLevelSchedule(GlobalValue.instance.gameSave.level);
        SceneManager.LoadScene("MapScene");
    }
    public void OnTeammate()
    {
        SceneManager.LoadScene("TeamScene");
    }
    public void OnUpgrade()
    {
        SceneManager.LoadScene("UpgradeScene");
    }
    void GetLevelSchedule(bool[] b)
    {
        for(int i = 0; i < b.Length; i++)
        {
            GlobalValue.instance.level[i] = b[i];
        }
    }

    #region Test
    public void ResetSave()
    {
        Debug.Log(GlobalValue.instance.gameSave.everSave);
        PlayerPrefs.DeleteAll();
    }
    #endregion
}