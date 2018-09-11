using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsScript : MonoBehaviour {
    [SerializeField]
    string gameId = "2763605";

    #region 單例
    public static AdsScript instance;
    public static AdsScript Instance
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
            Advertisement.Initialize(gameId);
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public void ShowAd()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Debug.Log("廣告");
        Advertisement.Show("rewardedVideo", options);
    }
    public void ShowAd(string zone = "")
    {
        if(string.Equals(zone, ""))
        {
            zone = null;
        }

        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Debug.Log("廣告");
        Advertisement.Show(zone, options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            GlobalValue.instance.enegy += 5;
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
}