using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teammate : MonoBehaviour {
    public Image[] catShow;
    public bool[] catSelect;
    public List<CatList> catLists = new List<CatList>();

    void Start()
    {
        
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
    }

    public void AddCatList(int v)
    {
        if (!catSelect[v])
        {
            if (catLists.Count < 3)
            {
                catLists.Add(new CatList(GlobalValue.instance.catHolder[v].GetComponent<CatControll>().state.name, GlobalValue.instance.catHolder[v].GetComponent<CatControll>().state.uid));
                GlobalValue.instance.catNum[catLists.Count - 1] = GlobalValue.instance.catHolder[v].GetComponent<CatControll>().state.uid;
                catShow[catLists.Count-1].gameObject.SetActive(true);
                catShow[catLists.Count-1].sprite = GlobalValue.instance.catBattleSpritHolder[v];
                catSelect[v] = true;
                Debug.Log("已加入catLists列表中");
                Debug.Log(catLists.Count + "個");
                foreach (CatList temp in catLists)
                {
                    Debug.Log(temp.name);
                    Debug.Log(temp.uid);
                }
            }
        }
    }

    public void RemoveCatList()
    {
        if (catLists.Count > 0)
        {
            for (int i = 0; i < catLists.Count; i++)
            {
                GlobalValue.instance.catNum[i] = 0;
                catShow[i].gameObject.SetActive(false);
                catShow[i].sprite = GlobalValue.instance.catBattleSpritHolder[i];
                catSelect[i] = false;
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
        for (int i = 0; i < catLists.Count; i++)
        {
            GlobalValue.instance.gameSave.catNum[i] = GlobalValue.instance.catNum[i];
            SaveLoadData.SaveData(GlobalValue.instance.gameSave);
        }
    }
}
