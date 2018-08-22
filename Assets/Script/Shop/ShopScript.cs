using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class ShopScript : MonoBehaviour {
    public GameObject noMoneyPanel;
    public GameObject confirmPanel;
    public Text requierdCost;
    public Text catName;
    public Image catSprite;
    public Text catCost;
	// Use this for initialization
	void Start () {
        if (GlobalValue.instance.nowUnlockCat < GlobalValue.instance.catHolder.Length)
        {
            catName.text = GlobalValue.instance.catHolder[GlobalValue.instance.nowUnlockCat].GetComponent<CatControll>().state.name;
            catSprite.sprite = GlobalValue.instance.catSpritHolder[GlobalValue.instance.nowUnlockCat];
            catCost.text = (GlobalValue.instance.nowUnlockCat * 1000).ToString();
        }
    }
    public void OnBuy()
    {
        if (GlobalValue.instance.gold >= GlobalValue.instance.nowUnlockCat * 1000)
        {
            string s = GlobalValue.instance.gold.ToString() + "->" + (GlobalValue.instance.gold - GlobalValue.instance.nowUnlockCat * 1000).ToString();
            requierdCost.text = s;
            confirmPanel.SetActive(true);
        }
        else
        {
            noMoneyPanel.SetActive(true);
        }
    }
    public void OnConfirm()
    {
        GlobalValue.instance.gold -= GlobalValue.instance.nowUnlockCat * 1000;
        GlobalValue.instance.catBuyYet[GlobalValue.instance.nowUnlockCat] = true;
        GlobalValue.instance.gameSave.catBuyYet[GlobalValue.instance.nowUnlockCat] = GlobalValue.instance.catBuyYet[GlobalValue.instance.nowUnlockCat];
        GlobalValue.instance.nowUnlockCat += 1;
        GlobalValue.instance.gameSave.nowUnlockCat = GlobalValue.instance.nowUnlockCat;

        SaveLoadData.SaveData(GlobalValue.instance.gameSave);
        confirmPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
    public void OnCancel(GameObject g)
    {
        g.SetActive(false);
    }
}
