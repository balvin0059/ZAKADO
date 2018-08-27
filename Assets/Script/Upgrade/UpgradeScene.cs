using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScene : MonoBehaviour {
    public GameObject[] catButton;
    public GameObject topPanel;
    public Image element;
    public Image catImage;
    public Text lvText;
    public Text hpText;
    public Text atkText;
    public Text expText;
    public Text nameText;
    public int exprequired;
    public int nowUid;
    public int indexCat;
    [Header("技能面板存取")]
    public Text[] skillText;//0 腳色名稱 1 技能名稱 2 技能說明 3 技能消耗EP
    public bool skillUseIng = false;
    public bool itemSelect = false;
    public GameObject SkillPanel;

    void Start()
    {
        for(int i = 0; i < GlobalValue.instance.catBuyYet.Length; i++)
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
    }
    #region 按鈕
    public void SkillUse()
    {
        if (!itemSelect)
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
            skillUseIng = true;
            skillText[0].text = GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.name;
            skillText[1].text = GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.actives;
            skillText[2].text = GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.actives_info;
            skillText[3].text = GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.actives_cost.ToString();
            SkillPanel.SetActive(true);
        }
    }
    public void SkillCancel()
    {
        if (!itemSelect)
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
            skillUseIng = false;
            SkillPanel.SetActive(false);
        }
    }
    public void OnChoseCat(int id)
    {
        if (!itemSelect)
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
            if (!skillUseIng)
            {
                indexCat = id;
                nowUid = GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.uid;
                Debug.Log(nowUid);
                exprequired = 500 + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.lv * 100;
                catImage.sprite = GlobalValue.instance.catSpritHolder[id];
                element.sprite = GlobalValue.instance.elementHolder[((int)GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.type) - 1];
                lvText.text = "Lv:" + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.lv.ToString("00");
                hpText.text = "HP:" + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.hp.ToString();
                atkText.text = "攻擊:" + GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.attack.ToString();
                if (GlobalValue.instance.exp > exprequired)
                {
                    expText.color = Color.black;
                    expText.text = exprequired.ToString();
                }
                else
                {
                    expText.color = Color.red;
                    expText.text = exprequired.ToString();
                }
                nameText.text = GlobalValue.instance.catHolder[id].GetComponent<CatControll>().state.name;
                topPanel.SetActive(true);
            }
        }
    }
    public void OnUpgrade()
    {
        if (!skillUseIng)
        {
            SoundControll.Instance.PlayEffecSound(SoundControll.Instance.buttonClip);
            if (!skillUseIng)
            {
                if (GlobalValue.instance.exp >= exprequired)
                {
                    SoundControll.Instance.PlayEffecSound(SoundControll.Instance.upgradeClip);
                    GlobalValue.instance.exp -= exprequired;
                    GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.lv += 1;
                    lvText.text = "Lv:" + GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.lv.ToString("00");
                    GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.hp += 50;
                    hpText.text = "HP:" + GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.hp.ToString();
                    GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.attack += 10;
                    atkText.text = "攻擊:" + GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.attack.ToString();
                    exprequired = 500 + GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state.lv * 100;
                    if (GlobalValue.instance.exp > exprequired)
                    {
                        expText.color = Color.black;
                        expText.text = exprequired.ToString();
                    }
                    else
                    {
                        expText.color = Color.red;
                        expText.text = exprequired.ToString();
                    }
                    GameObject effect = Instantiate(GlobalValue.instance.effectHolder[3]);
                    effect.transform.SetParent(catImage.gameObject.transform, false);
                    GlobalValue.instance.gameSave.stateSave[indexCat] = GlobalValue.instance.catHolder[indexCat].GetComponent<CatControll>().state;
                    GlobalValue.instance.SaveAllData();
                }
                else
                {
                    SoundControll.Instance.PlayEffecSound(SoundControll.Instance.cantdoClip);
                }
            }
        }
    }
    #endregion
}
