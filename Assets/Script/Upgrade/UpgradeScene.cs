using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScene : MonoBehaviour {
    public GameObject topPanel;
    public Image catImage;
    public Text lvText;
    public Text hpText;
    public Text atkText;
    public Text expText;
    public Text nameText;
    public int exprequired;
    public int nowUid;
    public int indexCat;

    public void OnChoseCat(int id)
    {
        indexCat = id;
        Debug.Log(indexCat);
        nowUid = GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.uid;
        Debug.Log(nowUid);
        exprequired = 500 + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.lv * 100;
        catImage.sprite = GlobalValue.instance.catSpritHolder[id];
        lvText.text = "Lv:" + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.lv.ToString();
        hpText.text = "HP:" + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.hp.ToString();
        atkText.text = "攻擊:" + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.attack.ToString();
        expText.text = exprequired.ToString();
        nameText.text = GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.name;
        topPanel.SetActive(true);
    }
    public void OnUpgrade()
    {
        if(GlobalValue.instance.exp >= exprequired)
        {

            GlobalValue.instance.exp -= exprequired;
            GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.lv += 1;
            lvText.text = "Lv:" + GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.lv.ToString();
            GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.hp += 50;
            hpText.text = "HP:" + GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.hp.ToString();
            GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.attack += 10;
            atkText.text = "攻擊:" + GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.attack.ToString();
            exprequired = 500 + GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.lv * 100;
            expText.text = exprequired.ToString();


            GlobalValue.instance.gameSave.stateSave[indexCat] = GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state;
            SaveLoadData.SaveData(GlobalValue.instance.gameSave);
        }
    }
}
