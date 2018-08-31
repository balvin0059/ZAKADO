using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class ShopScript : MonoBehaviour {
    public GameObject noMoneyPanel;
    public GameObject confirmPanel;
    public GameObject noCatPanel;
    public GameObject tipBuyPanel;
    public GameObject effectHolder;
    public Text requierdCost;
    public Text catName;
    public Image catSprite;
    public Text catCost;
    public GameObject buttonSetActive;
    public int catIndex;
    public GameObject lockPanel;
    public bool locking = false;
    public GameObject buyButton;
    public GameObject buyedText;
	// Use this for initialization
	void Start () {
        //if (GlobalValue.instance.nowUnlockCat < GlobalValue.instance.catHolder.Length)
        //{
        //    catName.text = GlobalValue.instance.catHolder[GlobalValue.instance.nowUnlockCat].GetComponent<CatControll>().state.name;
        //    catSprite.sprite = GlobalValue.instance.catSpritHolder[GlobalValue.instance.nowUnlockCat];
        //    catCost.text = (GlobalValue.instance.nowUnlockCat * 1000).ToString();
        //}
    }
    void Update()
    {
        catName.text = GlobalValue.instance.catHolder[gameObject.GetComponent<ScrollScript>().index+3].GetComponent<CatControll>().state.name;
        catCost.text = ((gameObject.GetComponent<ScrollScript>().index+1) * 3000).ToString();
        buyButton.SetActive(!GlobalValue.instance.catBuyYet[gameObject.GetComponent<ScrollScript>().index + 3]);
        buyedText.SetActive(GlobalValue.instance.catBuyYet[gameObject.GetComponent<ScrollScript>().index + 3]);
        if (!GlobalValue.instance.catBuyYet[gameObject.GetComponent<ScrollScript>().index + 2])
        {
            lockPanel.SetActive(true);
            locking = true;
        }
        else
        {
            lockPanel.SetActive(false);
            locking = false;
        }
    }
    public void OnBuy()
    {
        if (!locking)
        {
            if (GlobalValue.instance.gold >= GlobalValue.instance.nowUnlockCat * 1000)
            {
                SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
                string s = GlobalValue.instance.gold.ToString() + "->" + (GlobalValue.instance.gold - GlobalValue.instance.nowUnlockCat * 1000).ToString();
                requierdCost.text = s;
                confirmPanel.SetActive(true);
            }
            else
            {
                SoundControll.Instance.PlayEffecSound(SoundControll.Instance.cantdoClip);
                noMoneyPanel.SetActive(true);
            }
        }
    }
    public void OnConfirm()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buyClip);
        GlobalValue.instance.gold -= GlobalValue.instance.nowUnlockCat * 1000;
        GlobalValue.instance.catBuyYet[GlobalValue.instance.nowUnlockCat] = true;
        GlobalValue.instance.gameSave.catBuyYet[GlobalValue.instance.nowUnlockCat] = GlobalValue.instance.catBuyYet[GlobalValue.instance.nowUnlockCat];
        GlobalValue.instance.nowUnlockCat += 1;
        GlobalValue.instance.gameSave.nowUnlockCat = GlobalValue.instance.nowUnlockCat;
        if (GlobalValue.instance.nowUnlockCat >= GlobalValue.instance.catHolder.Length)
        {
            catName.text = null;
            catName.gameObject.SetActive(false);
            catCost.text = null;
            catCost.gameObject.SetActive(false);
            buttonSetActive.SetActive(false);
        }
        GlobalValue.instance.SaveAllData();
        confirmPanel.SetActive(false);
        Instantiate(GlobalValue.instance.effectHolder[4], effectHolder.transform);
        tipBuyPanel.SetActive(true);
    }
    public void OnCancel(GameObject g)
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        g.SetActive(false);
    }
    public void OnTipBuy()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        if (GlobalValue.instance.nowUnlockCat >= GlobalValue.instance.catHolder.Length)
        {
            noCatPanel.SetActive(true);
        }
            tipBuyPanel.SetActive(false);
    }
    public void OnnoCat()
    {
        SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
        noCatPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
}
