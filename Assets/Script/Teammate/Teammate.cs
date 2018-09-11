using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teammate : MonoBehaviour {
    public GameObject[] catButton;
    public GameObject SetPanel;
    public GameObject noSetPanel;
    public Image[] catShow;
    public bool[] catSelect;
    public List<CatList> catLists = new List<CatList>();
    public bool dontSave = false;
    public GameObject loading;
    public RectTransform context;
    void Start()
    {
        for (int i = 0; i < GlobalValue.instance.catBuyYet.Length; i++)
        {
            if (GlobalValue.instance.catBuyYet[i])
            {
                catButton[i].SetActive(GlobalValue.instance.catBuyYet[i]);
            }
            else
            {
                break;
            }
        }
        for (int i = 0; i < GlobalValue.instance.catNum.Length; i++)
        {
            if (GlobalValue.instance.catNum[i] > 0)
            {
                Debug.Log(PlayerPrefs.GetInt("catNum" + "_" + i, GlobalValue.instance.catNum[i]));
                for(int j = 0; j < GlobalValue.instance.catHolder.Length; j++)
                {
                    if(PlayerPrefs.GetInt("catNum" + "_" + i, GlobalValue.instance.catNum[i]) == GlobalValue.instance.catHolder[j].GetComponent<CatControll>().state.uid)
                    {
                        catLists.Add(new CatList(GlobalValue.instance.catHolder[j].GetComponent<CatControll>().state.name, GlobalValue.instance.catHolder[j].GetComponent<CatControll>().state.uid));
                        Debug.Log(GlobalValue.instance.catHolder[j].GetComponent<CatControll>().state.uid);
                        catShow[catLists.Count - 1].gameObject.SetActive(true);
                        catShow[catLists.Count - 1].sprite = GlobalValue.instance.catBattleSpritHolder[j];
                        catSelect[i] = true;
                    }
                }
                Debug.Log("已加入catLists列表中");
                Debug.Log(catLists.Count + "個");
                foreach (CatList temp in catLists)
                {
                    Debug.Log(temp.name);
                    Debug.Log(temp.uid);
                }
            }
            else
            {
                break;
            }
        }

        if (GlobalValue.instance.nowUnlockCat > 9)
        {
            context.sizeDelta += new Vector2(0, 150);
        }
    }

    public void AddCatList(int v)
    {        
        if (!catSelect[v])
        {
            if (catLists.Count < 3)
            {
                SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
                catLists.Add(new CatList(GlobalValue.instance.catHolder[v].GetComponent<CatControll>().state.name, GlobalValue.instance.catHolder[v].GetComponent<CatControll>().state.uid));
                GlobalValue.instance.catNum[catLists.Count - 1] = GlobalValue.instance.catHolder[v].GetComponent<CatControll>().state.uid;
                catShow[catLists.Count-1].gameObject.SetActive(true);
                catShow[catLists.Count-1].sprite = GlobalValue.instance.catBattleSpritHolder[v];
                catSelect[v] = true;
                Debug.Log("已加入catLists列表中");
                Debug.Log(catLists.Count + "個");
                foreach (CatList temp in catLists)
                {
                    Debug.Log(temp.name+","+temp.uid);
                }
            }
            else
            {
                SoundControll.Instance.PlayEffecSound(SoundControll.Instance.cantdoClip);
            }
        }
    }
    public void RemoveCatList()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        if (catLists.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                GlobalValue.instance.catNum[i] = 0;
                catShow[i].gameObject.SetActive(false);
                catShow[i].sprite = GlobalValue.instance.catBattleSpritHolder[i];                
            }
            for(int j = 0; j < catSelect.Length; j++)
            {
                catSelect[j] = false;
            }
            catLists.Clear();
            Debug.Log(catLists.Count + "個");
            foreach (CatList temp in catLists)
            {
                Debug.Log(temp.name);
                Debug.Log(temp.uid);
            }
        }
    }
    public void OnConfirm()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        SetPanel.SetActive(true);
    }
    public void OnConfirmSetTeam()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        for (int i = 2; i > 0; i--)
        {
            if (GlobalValue.instance.catNum[i] == 0)
            {
                noSetPanel.SetActive(true);
                dontSave = true;
                break;
            }
            else
            {
                dontSave = false;
            }
        }
        if (!dontSave)
        {
            for (int i = 0; i < 3; i++)
            {
                GlobalValue.instance.gameSave.catNum[i] = GlobalValue.instance.catNum[i];
            }
            GlobalValue.instance.SaveAllData();
            loading.SetActive(true);
            loading.GetComponent<Loading>().GotoScene("MainScene");
        }
        SetPanel.SetActive(false);
    }
    public void NoSetTeam()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        GlobalValue.instance.gameSave = SaveLoadData.LoadData();
        for (int j = 0; j < 3; j++)
        {
            GlobalValue.instance.catNum[j] = GlobalValue.instance.gameSave.catNum[j];
            AddCatList(j);
        }
        noSetPanel.SetActive(false);
    }
}
