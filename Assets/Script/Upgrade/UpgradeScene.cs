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

    public void OnChoseCat(int id)
    {

        exprequired = 500 + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.lv * 100;
        catImage.sprite = GlobalValue.instance.catSpritHolder[id];
        lvText.text = "Lv:" + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.lv.ToString();
        hpText.text = "HP:" + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.hp.ToString();
        atkText.text = "攻擊:" + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.attack.ToString();
        expText.text = exprequired.ToString();
        nameText.text = GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.name;
        topPanel.SetActive(true);
    }
}
