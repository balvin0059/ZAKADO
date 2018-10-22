using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMode : MonoBehaviour {
    #region 單例模式
    public static TestMode instance;
    public static TestMode Instance
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
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public void TestMode_(int index)
    {
        Scene scene = SceneManager.GetActiveScene();
        switch (index)
        {
            case 0:
                GlobalValue.instance.enegy = 50;
                break;
            case 1:
                GlobalValue.instance.exp += 100000;
                break;
            case 2:
                GlobalValue.instance.gold += 100000;
                break;
            case 3:
                scene = SceneManager.GetActiveScene();
                if (scene.name == "GameScene")
                {
                    PlayerControll.instance.playerNowHp = PlayerControll.instance.playerMaxHp;
                }
                break;
            case 4:
                scene = SceneManager.GetActiveScene();
                if (scene.name == "GameScene")
                {
                    PlayerControll.instance.playerNowEP = 20;
                }
                break;
            case 5:
                if (ItemHolder.instance.amount <= 0)
                {
                    for (int i = 0; i < ItemHolder.instance.globleItems.Count; i++)
                    {
                        ItemAPI.AddItem(1000+i);
                    }
                }
                break;
            case 6:
                scene = SceneManager.GetActiveScene();
                if (scene.name == "GameScene")
                {
                    TurnControll.instance.SpawnSpecialFood();
                }
                break;
            case 7:
                GlobalValue.instance.mission[0] = true;
                for (int i = 0; i < 5; i++)
                {
                    GlobalValue.instance.level[i] = true;
                }
                GlobalValue.instance.SaveAllData();
                break;
            case 8:
                Reset();
                break;

        }
    }
    void Reset()
    {
        PlayerPrefs.DeleteAll();
        GlobalValue.instance.LoadAllData();
        GlobalValue.instance.nowUnlockCat = 3;
        GlobalValue.instance.exp += 10000;
        GlobalValue.instance.gold += 10000;
        GlobalValue.instance.enegy = 50;
        GlobalValue.instance.catNum[0] = 1000;
        GlobalValue.instance.catNum[1] = 1001;
        GlobalValue.instance.catNum[2] = 1002;
        for (int i = 0; i < ItemHolder.instance.globleItems.Count; i++)
        {
            ItemAPI.DelItem(1000 + i);
        }
        Debug.Log(GlobalValue.instance.gameSave.everSave);       
        GlobalValue.instance.SaveAllData();
    }
    public Canvas canvas;
    void Update()
    {
        canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (_ispree)
        {
            TestMove();
        }
        else
        {

        }
    }
    bool _ispree;
    public void TestMove()
    {
        print("TestMove");
        if (Input.GetMouseButton(0))
        {
            Vector2 CorePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            gameObject.transform.position = CorePosition;
        }
    }
    public void OnPress()
    {
        _ispree = true;
    }
    public void OnRelese()
    {
        _ispree = false;
    }
}
